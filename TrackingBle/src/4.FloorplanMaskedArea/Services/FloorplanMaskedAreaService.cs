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
        private readonly IHttpClientFactory _httpClientFactory; // Updated to factory
        private readonly IConfiguration _configuration;

        public FloorplanMaskedAreaService(
            FloorplanMaskedAreaDbContext context,
            IMapper mapper,
            IHttpClientFactory httpClientFactory, // Updated constructor
            IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _httpClientFactory = httpClientFactory;
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
            var floorClient = _httpClientFactory.CreateClient("MstFloorService"); // Use factory
            var floorResponse = await floorClient.GetAsync($"/api/mstfloor/{createDto.FloorId}");
            if (!floorResponse.IsSuccessStatusCode)
                throw new ArgumentException($"Floor with ID {createDto.FloorId} not found.");

            var floorplanClient = _httpClientFactory.CreateClient("MstFloorplanService"); // Use factory
            var floorplanResponse = await floorplanClient.GetAsync($"/api/mstfloorplan/{createDto.FloorplanId}");
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

            var floorClient = _httpClientFactory.CreateClient("MstFloorService");
            var floorResponse = await floorClient.GetAsync($"/api/mstfloor/{updateDto.FloorId}");
            if (!floorResponse.IsSuccessStatusCode)
                throw new ArgumentException($"Floor with ID {updateDto.FloorId} not found.");

            var floorplanClient = _httpClientFactory.CreateClient("MstFloorplanService");
            var floorplanResponse = await floorplanClient.GetAsync($"/api/mstfloorplan/{updateDto.FloorplanId}");
            if (!floorplanResponse.IsSuccessStatusCode)
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

        private async Task<MstFloorDto> GetFloorAsync(Guid floorId)
        {
            var client = _httpClientFactory.CreateClient("MstFloorService"); // Use factory
            var response = await client.GetAsync($"/api/mstfloor/{floorId}");
            if (!response.IsSuccessStatusCode) return null;
            return await response.Content.ReadFromJsonAsync<MstFloorDto>();
        }

        private async Task<MstFloorplanDto> GetFloorplanAsync(Guid floorplanId)
        {
            var client = _httpClientFactory.CreateClient("MstFloorplanService"); // Use factory
            var response = await client.GetAsync($"/api/mstfloorplan/{floorplanId}");
            if (!response.IsSuccessStatusCode) return null;
            return await response.Content.ReadFromJsonAsync<MstFloorplanDto>();
        }
    }
}