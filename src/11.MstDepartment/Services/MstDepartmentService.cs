using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TrackingBle.src._11MstDepartment.Data;
using TrackingBle.src._11MstDepartment.Models.Domain;
using TrackingBle.src._11MstDepartment.Models.Dto.MstDepartmentDtos;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Net.Http;
using Microsoft.Extensions.Configuration;

namespace TrackingBle.src._11MstDepartment.Services
{
    public class MstDepartmentService : IMstDepartmentService
    {
        private readonly MstDepartmentDbContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;

        public MstDepartmentService(
            MstDepartmentDbContext context,
            IMapper mapper,
            IHttpClientFactory httpClientFactory,
            IHttpContextAccessor httpContextAccessor,
            IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
        }

        public async Task<MstDepartmentDto> GetByIdAsync(Guid id)
        {
            var department = await _context.MstDepartments
                .FirstOrDefaultAsync(d => d.Id == id && d.Status != 0);
            if (department == null) return null;

            var dto = _mapper.Map<MstDepartmentDto>(department);
            // dto.Application = await GetApplicationAsync(department.ApplicationId);
            return dto;
        }

        public async Task<IEnumerable<MstDepartmentDto>> GetAllAsync()
        {
            var departments = await _context.MstDepartments
                .Where(d => d.Status != 0)
                .ToListAsync();
            var dtos = _mapper.Map<List<MstDepartmentDto>>(departments);
            // foreach (var dto in dtos)
            // {
            //     dto.Application = await GetApplicationAsync(dto.ApplicationId);
            // }
            return dtos;
        }

        public async Task<MstDepartmentDto> CreateAsync(MstDepartmentCreateDto createDto)
        {
            // Validasi ApplicationId via HttpClient
            var client = _httpClientFactory.CreateClient("MstApplicationService");
            var response = await client.GetAsync($"/{createDto.ApplicationId}");
            if (!response.IsSuccessStatusCode)
                throw new ArgumentException($"Application with ID {createDto.ApplicationId} not found.");

            var department = _mapper.Map<MstDepartment>(createDto);
            var username = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Name)?.Value ?? "";
            department.Id = Guid.NewGuid();
            department.Status = 1;
            department.CreatedAt = DateTime.UtcNow;
            department.UpdatedAt = DateTime.UtcNow;
            department.CreatedBy = username;
            department.UpdatedBy = username;

            _context.MstDepartments.Add(department);
            await _context.SaveChangesAsync();

            var dto = _mapper.Map<MstDepartmentDto>(department);
            // dto.Application = await GetApplicationAsync(department.ApplicationId);
            return dto;
        }

        public async Task UpdateAsync(Guid id, MstDepartmentUpdateDto updateDto)
        {
            var department = await _context.MstDepartments.FindAsync(id);
            if (department == null || department.Status == 0)
                throw new KeyNotFoundException("Department not found");

            
            if (department.ApplicationId != updateDto.ApplicationId)
            {
                var client = _httpClientFactory.CreateClient("MstApplicationService");
                var response = await client.GetAsync($"/{updateDto.ApplicationId}");
                if (!response.IsSuccessStatusCode)
                    throw new ArgumentException($"Application with ID {updateDto.ApplicationId} not found.");
                department.ApplicationId = updateDto.ApplicationId;
            }

            
            department.Code = updateDto.Code;
            department.Name = updateDto.Name;
            department.DepartmentHost = updateDto.DepartmentHost;
            department.UpdatedBy = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Name)?.Value ?? "";
            department.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var department = await _context.MstDepartments.FindAsync(id);
            if (department == null || department.Status == 0)
                throw new KeyNotFoundException("Department not found");

            department.Status = 0;
            department.UpdatedBy = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Name)?.Value ?? "";
            department.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
        }

        private async Task<MstApplicationDto> GetApplicationAsync(Guid applicationId)
        {
            var client = _httpClientFactory.CreateClient("MstApplicationService");
            var response = await client.GetAsync($"/{applicationId}");
            if (!response.IsSuccessStatusCode) return null;
            return await response.Content.ReadFromJsonAsync<MstApplicationDto>();
        }
    }

    // public class HttpClientAuthorizationDelegatingHandler : DelegatingHandler
    // {
    //     private readonly IHttpContextAccessor _httpContextAccessor;

    //     public HttpClientAuthorizationDelegatingHandler(IHttpContextAccessor httpContextAccessor)
    //     {
    //         _httpContextAccessor = httpContextAccessor;
    //     }

    //     protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    //     {
    //         var token = _httpContextAccessor.HttpContext?.Request.Headers["Authorization"].ToString();
    //         if (!string.IsNullOrEmpty(token))
    //         {
    //             request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token.Replace("Bearer ", ""));
    //             Console.WriteLine($"Forwarding token to request: {token}");
    //         }
    //         else
    //         {
    //             Console.WriteLine("No Authorization token found in HttpContext.");
    //         }

    //         return await base.SendAsync(request, cancellationToken);
    //     }
    // }
}