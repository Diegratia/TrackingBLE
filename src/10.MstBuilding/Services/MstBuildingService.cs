using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TrackingBle.src._10MstBuilding.Data;
using TrackingBle.src._10MstBuilding.Models.Domain;
using TrackingBle.src._10MstBuilding.Models.Dto.MstBuildingDtos;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace TrackingBle.src._10MstBuilding.Services
{
    public class MstBuildingService : IMstBuildingService
    {
        private readonly MstBuildingDbContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;

        public MstBuildingService(
            MstBuildingDbContext context,
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

        public async Task<MstBuildingDto> GetByIdAsync(Guid id)
        {
            var building = await _context.MstBuildings
                .FirstOrDefaultAsync(b => b.Id == id && b.Status != 0);
            return building == null ? null : _mapper.Map<MstBuildingDto>(building);
        }

        public async Task<IEnumerable<MstBuildingDto>> GetAllAsync()
        {
            var buildings = await _context.MstBuildings
                .Where(b => b.Status != 0)
                .ToListAsync();
            return _mapper.Map<IEnumerable<MstBuildingDto>>(buildings);
        }

        public async Task<MstBuildingDto> CreateAsync(MstBuildingCreateDto dto)
        {
            // Validasi ApplicationId via MstApplicationService
            var client = _httpClientFactory.CreateClient("MstApplicationService");
            var response = await client.GetAsync($"/{dto.ApplicationId}");
            if (!response.IsSuccessStatusCode)
                throw new ArgumentException($"Application with ID {dto.ApplicationId} not found.");

            var building = _mapper.Map<MstBuilding>(dto);
            var username = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Name)?.Value ?? "";
            building.Id = Guid.NewGuid();
            building.Status = 1;
            building.CreatedAt = DateTime.UtcNow;
            building.UpdatedAt = DateTime.UtcNow;
            building.CreatedBy = username;
            building.UpdatedBy = username;

            _context.MstBuildings.Add(building);
            await _context.SaveChangesAsync();

            return _mapper.Map<MstBuildingDto>(building);
        }

        public async Task UpdateAsync(Guid id, MstBuildingUpdateDto dto)
        {
            var building = await _context.MstBuildings.FindAsync(id);
            if (building == null || building.Status == 0)
                throw new KeyNotFoundException("Building not found");

            // Validasi ApplicationId jika berubah
            if (building.ApplicationId != dto.ApplicationId)
            {
                var client = _httpClientFactory.CreateClient("MstApplicationService");
                var response = await client.GetAsync($"/{dto.ApplicationId}");
                if (!response.IsSuccessStatusCode)
                    throw new ArgumentException($"Application with ID {dto.ApplicationId} not found.");
                building.ApplicationId = dto.ApplicationId;
            }

            // Map DTO ke entitas 
            _mapper.Map(dto, building);
            building.UpdatedBy = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Name)?.Value ?? "";
            building.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var building = await _context.MstBuildings.FindAsync(id);
            if (building == null || building.Status == 0)
                throw new KeyNotFoundException("Building not found");

            building.Status = 0; // Soft delete
            building.UpdatedBy = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Name)?.Value ?? "";
            building.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
        }
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