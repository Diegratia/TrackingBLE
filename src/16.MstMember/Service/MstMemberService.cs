using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TrackingBle.src._16MstMember.Data;
using TrackingBle.src._16MstMember.Models.Domain;
using TrackingBle.src._16MstMember.Models.Dto.MstMemberDtos;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace TrackingBle.src._16MstMember.Services
{
    public class MstMemberService : IMstMemberService
    {
        private readonly MstMemberDbContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;
        private readonly JsonSerializerOptions _jsonOptions;
        private readonly string[] _allowedImageTypes = new[] { "image/jpeg", "image/jpg", "image/png" };
        private const long MaxFileSize = 5 * 1024 * 1024; // 5 MB

        public MstMemberService(
            MstMemberDbContext context,
            IMapper mapper,
            IHttpClientFactory httpClientFactory,
            IHttpContextAccessor httpContextAccessor,
            IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true 
            };
        }

        public async Task<IEnumerable<MstMemberDto>> GetAllMembersAsync()
        {
            var members = await _context.MstMembers
                .Where(m => m.Status != 0)
                .ToListAsync();
            var dtos = _mapper.Map<List<MstMemberDto>>(members);
            foreach (var dto in dtos)
            {
                dto.Organization = await GetOrganizationAsync(dto.OrganizationId);
                dto.Department = await GetDepartmentAsync(dto.DepartmentId);
                dto.District = await GetDistrictAsync(dto.DistrictId);
            }
            return dtos;
        }

        public async Task<MstMemberDto> GetMemberByIdAsync(Guid id)
        {
            var member = await _context.MstMembers
                .FirstOrDefaultAsync(m => m.Id == id && m.Status != 0);
            if (member == null) return null;

            var dto = _mapper.Map<MstMemberDto>(member);
            dto.Organization = await GetOrganizationAsync(member.OrganizationId);
            dto.Department = await GetDepartmentAsync(member.DepartmentId);
            dto.District = await GetDistrictAsync(member.DistrictId);
            return dto;
        }

        public async Task<MstMemberDto> CreateMemberAsync(MstMemberCreateDto createDto)
        {
            if (createDto == null) throw new ArgumentNullException(nameof(createDto));

            // validasi Id lewat http 
            await ValidateApplicationAsync(createDto.ApplicationId);
            await ValidateOrganizationAsync(createDto.OrganizationId);
            await ValidateDepartmentAsync(createDto.DepartmentId);
            await ValidateDistrictAsync(createDto.DistrictId);

            var member = _mapper.Map<MstMember>(createDto);
            var username = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Name)?.Value ?? "";

            member.Id = Guid.NewGuid();
            member.Status = 1;
            member.CreatedAt = DateTime.UtcNow;
            member.UpdatedAt = DateTime.UtcNow;
            member.CreatedBy = username;
            member.UpdatedBy = username;
            member.JoinDate = DateOnly.FromDateTime(DateTime.UtcNow);
            member.ExitDate = DateOnly.MaxValue;

            // Handle file upload
            if (createDto.FaceImage != null && createDto.FaceImage.Length > 0)
            {
                try
                {
                    if (!_allowedImageTypes.Contains(createDto.FaceImage.ContentType))
                        throw new ArgumentException("Only image files (jpg, png, jpeg) are allowed.");
                    if (createDto.FaceImage.Length > MaxFileSize)
                        throw new ArgumentException("File size exceeds 5 MB limit.");

                    var uploadDir = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", "MemberFaceImages");
                    Directory.CreateDirectory(uploadDir);
                    var fileName = $"{Guid.NewGuid()}_{createDto.FaceImage.FileName}";
                    var filePath = Path.Combine(uploadDir, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await createDto.FaceImage.CopyToAsync(stream);
                    }

                    member.FaceImage = $"/Uploads/MemberFaceImages/{fileName}";
                    member.UploadFr = 1;
                    member.UploadFrError = "Upload successful";
                }
                catch (Exception ex)
                {
                    member.UploadFr = 2;
                    member.UploadFrError = ex.Message;
                    member.FaceImage = null;
                }
            }
            else
            {
                member.UploadFr = 0;
                member.UploadFrError = "No file uploaded";
                member.FaceImage = null;
            }

            _context.MstMembers.Add(member);
            await _context.SaveChangesAsync();

            var dto = _mapper.Map<MstMemberDto>(member);
            dto.Organization = await GetOrganizationAsync(member.OrganizationId);
            dto.Department = await GetDepartmentAsync(member.DepartmentId);
            dto.District = await GetDistrictAsync(member.DistrictId);
            return dto;
        }

        public async Task<MstMemberDto> UpdateMemberAsync(Guid id, MstMemberUpdateDto updateDto)
        {
            if (updateDto == null) throw new ArgumentNullException(nameof(updateDto));

            var member = await _context.MstMembers.FindAsync(id);
            if (member == null || member.Status == 0)
                throw new KeyNotFoundException($"Member with ID {id} not found or has been deleted.");

            // Validasi IDs jika berubah
            if (member.ApplicationId != updateDto.ApplicationId)
                await ValidateApplicationAsync(updateDto.ApplicationId);
            if (member.OrganizationId != updateDto.OrganizationId)
                await ValidateOrganizationAsync(updateDto.OrganizationId);
            if (member.DepartmentId != updateDto.DepartmentId)
                await ValidateDepartmentAsync(updateDto.DepartmentId);
            if (member.DistrictId != updateDto.DistrictId)
                await ValidateDistrictAsync(updateDto.DistrictId);

            // Handle file upload
            if (updateDto.FaceImage != null && updateDto.FaceImage.Length > 0)
            {
                try
                {
                    if (!_allowedImageTypes.Contains(updateDto.FaceImage.ContentType))
                        throw new ArgumentException("Only image files (jpg, png, jpeg) are allowed.");
                    if (updateDto.FaceImage.Length > MaxFileSize)
                        throw new ArgumentException("File size exceeds 5 MB limit.");

                    var uploadDir = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", "MemberFaceImages");
                    Directory.CreateDirectory(uploadDir);
                    var fileName = $"{Guid.NewGuid()}_{updateDto.FaceImage.FileName}";
                    var filePath = Path.Combine(uploadDir, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await updateDto.FaceImage.CopyToAsync(stream);
                    }

                    member.FaceImage = $"/Uploads/MemberFaceImages/{fileName}";
                    member.UploadFr = 1;
                    member.UploadFrError = "Upload successful";
                }
                catch (Exception ex)
                {
                    member.UploadFr = 2;
                    member.UploadFrError = ex.Message;
                    member.FaceImage = null;
                }
            }

            _mapper.Map(updateDto, member);
            member.UpdatedBy = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Name)?.Value ?? "";
            member.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            var dto = _mapper.Map<MstMemberDto>(member);
            dto.Organization = await GetOrganizationAsync(member.OrganizationId);
            dto.Department = await GetDepartmentAsync(member.DepartmentId);
            dto.District = await GetDistrictAsync(member.DistrictId);
            return dto;
        }

        public async Task DeleteMemberAsync(Guid id)
        {
            var member = await _context.MstMembers.FindAsync(id);
            if (member == null || member.Status == 0)
                throw new KeyNotFoundException($"Member with ID {id} not found or already deleted.");

            
            member.UpdatedBy = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Name)?.Value ?? "";
            member.UpdatedAt = DateTime.UtcNow;
            member.ExitDate = DateOnly.FromDateTime(DateTime.UtcNow);
            member.Status = 0;

            await _context.SaveChangesAsync();
        }

        private async Task ValidateApplicationAsync(Guid applicationId)
        {
            var client = _httpClientFactory.CreateClient("MstApplicationService");
            Console.WriteLine($"Validating Application with ID {applicationId} at {client.BaseAddress}/{applicationId}");
            var response = await client.GetAsync($"/{applicationId}");
            if (!response.IsSuccessStatusCode)
                throw new ArgumentException($"Application with ID {applicationId} not found. Status: {response.StatusCode}");
        }

        private async Task ValidateOrganizationAsync(Guid organizationId)
        {
            var client = _httpClientFactory.CreateClient("MstOrganizationService");
            Console.WriteLine($"Validating Organization with ID {organizationId} at {client.BaseAddress}/{organizationId}");
            var response = await client.GetAsync($"/{organizationId}");
            if (!response.IsSuccessStatusCode)
                throw new ArgumentException($"Organization with ID {organizationId} not found. Status: {response.StatusCode}");
        }

        private async Task ValidateDepartmentAsync(Guid departmentId)
        {
            var client = _httpClientFactory.CreateClient("MstDepartmentService");
            Console.WriteLine($"Validating Department with ID {departmentId} at {client.BaseAddress}/{departmentId}");
            var response = await client.GetAsync($"/{departmentId}");
            if (!response.IsSuccessStatusCode)
                throw new ArgumentException($"Department with ID {departmentId} not found. Status: {response.StatusCode}");
        }

        private async Task ValidateDistrictAsync(Guid districtId)
        {
            var client = _httpClientFactory.CreateClient("MstDistrictService");
            Console.WriteLine($"Validating District with ID {districtId} at {client.BaseAddress}/{districtId}");
            var response = await client.GetAsync($"/{districtId}");
            if (!response.IsSuccessStatusCode)
                throw new ArgumentException($"District with ID {districtId} not found. Status: {response.StatusCode}");
        }

        private async Task<MstOrganizationDto> GetOrganizationAsync(Guid organizationId)
        {
            var client = _httpClientFactory.CreateClient("MstOrganizationService");
            Console.WriteLine($"Fetching Organization with ID {organizationId} from {client.BaseAddress}/{organizationId}");
            var response = await client.GetAsync($"/{organizationId}");
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Failed to get Organization with ID {organizationId}. Status: {response.StatusCode}");
                return null;
            }

            var json = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Organization response JSON: {json}");
            try
            {
                var apiResponse = JsonSerializer.Deserialize<ApiResponse<MstOrganizationDto>>(json, _jsonOptions);
                if (apiResponse?.Success == true && apiResponse.Collection?.Data != null)
                {
                    Console.WriteLine($"Successfully deserialized Organization with ID {organizationId}");
                    return apiResponse.Collection.Data;
                }

                Console.WriteLine($"No valid data found in Organization response for ID {organizationId}");
                return null;
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Error deserializing Organization JSON: {ex.Message}. JSON: {json}");
                throw; 
            }
        }

        private async Task<MstDepartmentDto> GetDepartmentAsync(Guid departmentId)
        {
            var client = _httpClientFactory.CreateClient("MstDepartmentService");
            Console.WriteLine($"Fetching Department with ID {departmentId} from {client.BaseAddress}/{departmentId}");
            var response = await client.GetAsync($"/{departmentId}");
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Failed to get Department with ID {departmentId}. Status: {response.StatusCode}");
                return null;
            }

            var json = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Department response JSON: {json}");
            try
            {
                var apiResponse = JsonSerializer.Deserialize<ApiResponse<MstDepartmentDto>>(json, _jsonOptions);
                if (apiResponse?.Success == true && apiResponse.Collection?.Data != null)
                {
                    Console.WriteLine($"Successfully deserialized Department with ID {departmentId}");
                    return apiResponse.Collection.Data;
                }

                Console.WriteLine($"No valid data found in Department response for ID {departmentId}");
                return null;
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Error deserializing Department JSON: {ex.Message}. JSON: {json}");
                throw; 
            }
        }

        private async Task<MstDistrictDto> GetDistrictAsync(Guid districtId)
        {
            var client = _httpClientFactory.CreateClient("MstDistrictService");
            Console.WriteLine($"Fetching District with ID {districtId} from {client.BaseAddress}/{districtId}");
            var response = await client.GetAsync($"/{districtId}");
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Failed to get District with ID {districtId}. Status: {response.StatusCode}");
                return null;
            }

            var json = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"District response JSON: {json}");
            try
            {
                var apiResponse = JsonSerializer.Deserialize<ApiResponse<MstDistrictDto>>(json, _jsonOptions);
                if (apiResponse?.Success == true && apiResponse.Collection?.Data != null)
                {
                    Console.WriteLine($"Successfully deserialized District with ID {districtId}");
                    return apiResponse.Collection.Data;
                }

                Console.WriteLine($"No valid data found in District response for ID {districtId}");
                return null;
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Error deserializing District JSON: {ex.Message}. JSON: {json}");
                throw; 
            }
        }
    }

    // wrapper untuk respons API
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Msg { get; set; }
        public CollectionData<T> Collection { get; set; }
        public int Code { get; set; } 
    }

    public class CollectionData<T>
    {
        public T Data { get; set; }
    }

    public class HttpClientAuthorizationDelegatingHandler : DelegatingHandler
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HttpClientAuthorizationDelegatingHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = _httpContextAccessor.HttpContext?.Request.Headers["Authorization"].ToString();
            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token.Replace("Bearer ", ""));
                Console.WriteLine($"Forwarding token to request: {token}");
            }
            else
            {
                Console.WriteLine("No Authorization token found in HttpContext.");
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}