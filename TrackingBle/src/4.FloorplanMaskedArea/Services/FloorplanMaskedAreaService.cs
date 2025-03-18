using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using TrackingBle.src._4FloorplanMaskedArea.Data;
using TrackingBle.src._4FloorplanMaskedArea.Models.Domain;
using TrackingBle.src._4FloorplanMaskedArea.Models.Dto.FloorplanMaskedAreaDtos;
using Microsoft.Extensions.Configuration;

namespace TrackingBle.src._4FloorplanMaskedArea.Services
{
    public class FloorplanMaskedAreaService : IFloorplanMaskedAreaService
    {
        private readonly FloorplanMaskedAreaDbContext _context;
        private readonly IMapper _mapper;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public FloorplanMaskedAreaService(
            FloorplanMaskedAreaDbContext context,
            IMapper mapper,
            HttpClient httpClient,
            IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<FloorplanMaskedAreaDto> GetByIdAsync(Guid id)
        {
            var area = await _context.FloorplanMaskedAreas.FirstOrDefaultAsync(a => a.Id == id);
            if (area == null) return null;

            var dto = _mapper.Map<FloorplanMaskedAreaDto>(area);
            dto.Floor = await GetFloorAsync(area.FloorId);
            dto.Floorplan = await GetFloorplanAsync(area.FloorplanId);
            return dto;
        }

        public async Task<IEnumerable<FloorplanMaskedAreaDto>> GetAllAsync()
        {
            var areas = await _context.FloorplanMaskedAreas.ToListAsync();
            var dtos = _mapper.Map<List<FloorplanMaskedAreaDto>>(areas);
            foreach (var dto in dtos)
            {
                dto.Floor = await GetFloorAsync(dto.FloorId);
                dto.Floorplan = await GetFloorplanAsync(dto.FloorplanId);
            }
            return dtos;
        }

        public async Task<FloorplanMaskedAreaDto> CreateAsync(FloorplanMaskedAreaCreateDto createDto)
        {
            var floorResponse = await _httpClient.GetAsync($"{_configuration["ServiceUrls:MstFloorService"]}/api/mstfloor/{createDto.FloorId}");
            if (!floorResponse.IsSuccessStatusCode)
                throw new ArgumentException($"Floor with ID {createDto.FloorId} not found.");

            var floorplanResponse = await _httpClient.GetAsync($"{_configuration["ServiceUrls:MstFloorplanService"]}/api/mstfloorplan/{createDto.FloorplanId}");
            if (!floorplanResponse.IsSuccessStatusCode)
                throw new ArgumentException($"Floorplan with ID {createDto.FloorplanId} not found.");

            var area = _mapper.Map<FloorplanMaskedArea>(createDto);
            area.Status = 1;
            area.CreatedBy = "system";
            area.CreatedAt = DateTime.UtcNow;
            area.UpdatedBy = "system";
            area.UpdatedAt = DateTime.UtcNow;

            _context.FloorplanMaskedAreas.Add(area);
            await _context.SaveChangesAsync();

            var dto = _mapper.Map<FloorplanMaskedAreaDto>(area);
            dto.Floor = await GetFloorAsync(area.FloorId);
            dto.Floorplan = await GetFloorplanAsync(area.FloorplanId);
            return dto;
        }

        public async Task UpdateAsync(Guid id, FloorplanMaskedAreaUpdateDto updateDto)
        {
            var area = await _context.FloorplanMaskedAreas.FindAsync(id);
            if (area == null)
                throw new KeyNotFoundException("Area not found");

            // Mock validasi sementara
            if (updateDto.FloorId == Guid.Empty)
                throw new ArgumentException($"Floor with ID {updateDto.FloorId} not found.");
            if (updateDto.FloorplanId == Guid.Empty)
                throw new ArgumentException($"Floorplan with ID {updateDto.FloorplanId} not found.");

            _mapper.Map(updateDto, area);
            area.UpdatedBy = "system";
            area.UpdatedAt = DateTime.UtcNow;

            _context.FloorplanMaskedAreas.Update(area);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var area = await _context.FloorplanMaskedAreas.FindAsync(id);
            if (area == null)
                throw new KeyNotFoundException("Area not found");

            area.Status = 0;
            _context.FloorplanMaskedAreas.Update(area);
            await _context.SaveChangesAsync();
        }

        private async Task<FloorDto> GetFloorAsync(Guid floorId)
        {
            var response = await _httpClient.GetAsync($"{_configuration["ServiceUrls:MstFloorService"]}/api/mstfloor/{floorId}");
            if (!response.IsSuccessStatusCode) return null;
            return await response.Content.ReadFromJsonAsync<FloorDto>();
        }

        private async Task<FloorplanDto> GetFloorplanAsync(Guid floorplanId)
        {
            var response = await _httpClient.GetAsync($"{_configuration["ServiceUrls:MstFloorplanService"]}/api/mstfloorplan/{floorplanId}");
            if (!response.IsSuccessStatusCode) return null;
            return await response.Content.ReadFromJsonAsync<FloorplanDto>();
        }
    }
}