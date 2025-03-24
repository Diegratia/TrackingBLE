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

namespace TrackingBle.src._10MstBuilding.Services
{
    public class MstBuildingService : IMstBuildingService
    {
        private readonly MstBuildingDbContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public MstBuildingService(
            MstBuildingDbContext context,
            IMapper mapper,
            IHttpClientFactory httpClientFactory,
            IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _httpClientFactory = httpClientFactory;
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
            var response = await client.GetAsync($"/api/mstapplication/{dto.ApplicationId}");
            if (!response.IsSuccessStatusCode)
                throw new ArgumentException($"Application with ID {dto.ApplicationId} not found.");

            var building = _mapper.Map<MstBuilding>(dto);
            building.Id = Guid.NewGuid();
            building.Status = 1;
            building.CreatedAt = DateTime.UtcNow;
            building.UpdatedAt = DateTime.UtcNow;
            building.CreatedBy = "system";
            building.UpdatedBy = "system";

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
                var response = await client.GetAsync($"/api/mstapplication/{dto.ApplicationId}");
                if (!response.IsSuccessStatusCode)
                    throw new ArgumentException($"Application with ID {dto.ApplicationId} not found.");
                building.ApplicationId = dto.ApplicationId;
            }

            // Map DTO ke entitas sebelum set audit fields
            _mapper.Map(dto, building);
            building.UpdatedBy = "system";
            building.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var building = await _context.MstBuildings.FindAsync(id);
            if (building == null || building.Status == 0)
                throw new KeyNotFoundException("Building not found");

            building.Status = 0; // Soft delete
            building.UpdatedBy = "system";
            building.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
        }
    }
}