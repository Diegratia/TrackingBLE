using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using TrackingBle.src._13MstFloor.Data;
using TrackingBle.src._13MstFloor.Models.Domain;
using TrackingBle.src._13MstFloor.Models.Dto.MstFloorDtos;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace TrackingBle.src._13MstFloor.Services
{
    public class MstFloorService : IMstFloorService
    {
        private readonly MstFloorDbContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly JsonSerializerOptions _jsonOptions;
        private readonly string[] _allowedImageTypes = new[] { "image/jpeg", "image/png", "image/jpg" };
        private const long MaxFileSize = 1 * 1024 * 1024; // Maksimal 1MB

        public MstFloorService(
            MstFloorDbContext context,
            IMapper mapper,
            IHttpClientFactory httpClientFactory,
            IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _mapper = mapper;
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true // abaikan case sensitif
            };
        }

        public async Task<MstFloorDto> GetByIdAsync(Guid id)
        {
            var floor = await _context.MstFloors.FirstOrDefaultAsync(f => f.Id == id);
            if (floor == null)
            {
                Console.WriteLine($"MstFloor with ID {id} not found in database.");
                return null;
            }

            var dto = _mapper.Map<MstFloorDto>(floor);
            dto.Building = await GetBuildingAsync(floor.BuildingId);
            Console.WriteLine($"Building for Floor ID {id}: {(dto.Building != null ? "Loaded" : "Null")}");
            return dto;
        }

        public async Task<IEnumerable<MstFloorDto>> GetAllAsync()
        {
            var floors = await _context.MstFloors.ToListAsync();
            if (!floors.Any())
            {
                Console.WriteLine("No MstFloors found in database.");
                return new List<MstFloorDto>();
            }

            Console.WriteLine($"Found {floors.Count} MstFloors in database.");
            var dtos = _mapper.Map<List<MstFloorDto>>(floors);

            foreach (var dto in dtos)
            {
                dto.Building = await GetBuildingAsync(dto.BuildingId);
                Console.WriteLine($"Building for Floor ID {dto.Id}: {(dto.Building != null ? "Loaded" : "Null")}");
            }

            return dtos;
        }

        public async Task<MstFloorDto> CreateAsync(MstFloorCreateDto createDto)
        {
            // Validasi BuildingId via HTTP
            var buildingClient = _httpClientFactory.CreateClient("BuildingService");
            Console.WriteLine($"Validating Building with ID {createDto.BuildingId} at {buildingClient.BaseAddress}/api/mstbuilding/{createDto.BuildingId}");
            var buildingResponse = await buildingClient.GetAsync($"/{createDto.BuildingId}");
            if (!buildingResponse.IsSuccessStatusCode)
            {
                Console.WriteLine($"Failed to validate Building with ID {createDto.BuildingId}. Status: {buildingResponse.StatusCode}");
                throw new ArgumentException($"Building with ID {createDto.BuildingId} not found.");
            }

            var floor = _mapper.Map<MstFloor>(createDto);

            // Upload gambar
            if (createDto.FloorImage != null && createDto.FloorImage.Length > 0)
            {
                if (!_allowedImageTypes.Contains(createDto.FloorImage.ContentType))
                    throw new ArgumentException("Only image files (jpg, png, jpeg) are allowed.");

                if (createDto.FloorImage.Length > MaxFileSize)
                    throw new ArgumentException("File size exceeds 1 MB limit.");

                var uploadDir = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", "FloorImages");
                Directory.CreateDirectory(uploadDir);

                var fileName = $"{Guid.NewGuid()}_{createDto.FloorImage.FileName}";
                var filePath = Path.Combine(uploadDir, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await createDto.FloorImage.CopyToAsync(stream);
                }

                floor.FloorImage = $"/Uploads/FloorImages/{fileName}";
            }

            var username = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Name)?.Value ?? "";
            floor.CreatedBy = username;
            floor.CreatedAt = DateTime.UtcNow;
            floor.UpdatedBy = username;
            floor.UpdatedAt = DateTime.UtcNow;
            floor.Status = 1;

            _context.MstFloors.Add(floor);
            await _context.SaveChangesAsync();

            var dto = _mapper.Map<MstFloorDto>(floor);
            dto.Building = await GetBuildingAsync(floor.BuildingId);
            return dto;
        }

        public async Task<MstFloorDto> UpdateAsync(Guid id, MstFloorUpdateDto updateDto)
        {
            var floor = await _context.MstFloors.FindAsync(id);
            if (floor == null)
                throw new KeyNotFoundException("Floor not found");

            var buildingClient = _httpClientFactory.CreateClient("BuildingService");
            Console.WriteLine($"Validating Building with ID {updateDto.BuildingId} at {buildingClient.BaseAddress}/api/mstbuilding/{updateDto.BuildingId}");
            var buildingResponse = await buildingClient.GetAsync($"/{updateDto.BuildingId}");
            if (!buildingResponse.IsSuccessStatusCode)
            {
                Console.WriteLine($"Failed to validate Building with ID {updateDto.BuildingId}. Status: {buildingResponse.StatusCode}");
                throw new ArgumentException($"Building with ID {updateDto.BuildingId} not found.");
            }

            if (updateDto.FloorImage != null && updateDto.FloorImage.Length > 0)
            {
                if (!_allowedImageTypes.Contains(updateDto.FloorImage.ContentType))
                    throw new ArgumentException("Only image files (jpg, png, jpeg) are allowed.");

                if (updateDto.FloorImage.Length > MaxFileSize)
                    throw new ArgumentException("File size exceeds 1 MB limit.");

                var uploadDir = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", "FloorImages");
                Directory.CreateDirectory(uploadDir);

                if (!string.IsNullOrEmpty(floor.FloorImage))
                {
                    var oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), floor.FloorImage.TrimStart('/'));
                    if (File.Exists(oldFilePath))
                        File.Delete(oldFilePath);
                }

                var fileName = $"{Guid.NewGuid()}_{updateDto.FloorImage.FileName}";
                var filePath = Path.Combine(uploadDir, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await updateDto.FloorImage.CopyToAsync(stream);
                }

                floor.FloorImage = $"/Uploads/FloorImages/{fileName}";
            }

            _mapper.Map(updateDto, floor);
            
            floor.UpdatedBy = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Name)?.Value ?? "";
            floor.UpdatedAt = DateTime.UtcNow;

            _context.MstFloors.Update(floor);
            await _context.SaveChangesAsync();

            var dto = _mapper.Map<MstFloorDto>(floor);
            dto.Building = await GetBuildingAsync(floor.BuildingId);
            return dto;
        }

        public async Task DeleteAsync(Guid id)
        {
            var floor = await _context.MstFloors.FindAsync(id);
            if (floor == null)
                throw new KeyNotFoundException("Floor not found");

            var maskedAreaClient = _httpClientFactory.CreateClient("FloorplanMaskedAreaService");
            Console.WriteLine($"Checking FloorplanMaskedAreas for Floor ID {id} at {maskedAreaClient.BaseAddress}/api/floorplanmaskedarea/byfloor/{id}");
            // var maskedAreaResponse = await maskedAreaClient.GetAsync($"/api/floorplanmaskedarea/byfloor/{id}");
            var maskedAreaResponse = await maskedAreaClient.GetAsync($"/{id}");
            if (maskedAreaResponse.IsSuccessStatusCode)
            {
                var maskedAreas = await maskedAreaResponse.Content.ReadFromJsonAsync<List<dynamic>>();
                if (maskedAreas.Any(a => (int)a.status == 1))
                    throw new InvalidOperationException("Cannot delete Floor because it is used by active FloorplanMaskedAreas.");
            }

            floor.UpdatedBy = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Name)?.Value ?? "";
            floor.UpdatedAt = DateTime.UtcNow;
            floor.Status = 0;
            // _context.MstFloors.Update(floor);
            await _context.SaveChangesAsync();
        }

        private async Task<MstBuildingDto> GetBuildingAsync(Guid buildingId)
        {
            var client = _httpClientFactory.CreateClient("BuildingService");
            Console.WriteLine($"Fetching Building with ID {buildingId} from {client.BaseAddress}/api/mstbuilding/{buildingId}");
            var response = await client.GetAsync($"/{buildingId}");
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Failed to get Building with ID {buildingId}. Status: {response.StatusCode}");
                return null;
            }

            var json = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Building response JSON: {json}");
            try
            {
                var apiResponse = JsonSerializer.Deserialize<ApiResponse<MstBuildingDto>>(json, _jsonOptions);
                if (apiResponse?.Success == true && apiResponse.Collection?.Data != null)
                {
                    Console.WriteLine($"Successfully deserialized Building with ID {buildingId}");
                    return apiResponse.Collection.Data;
                }

                Console.WriteLine($"No valid data found in Building response for ID {buildingId}");
                return null;
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Error deserializing Building JSON: {ex.Message}. JSON: {json}");
                return null;
            }
        }
    }

    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Msg { get; set; }
        public CollectionData<T> Collection { get; set; }
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