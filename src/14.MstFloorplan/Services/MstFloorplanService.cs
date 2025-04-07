using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using TrackingBle.src._14MstFloorplan.Data;
using TrackingBle.src._14MstFloorplan.Models.Domain;
using TrackingBle.src._14MstFloorplan.Models.Dto.MstFloorplanDtos;
using Microsoft.Extensions.Configuration;
using TrackingBle.src.Common.Models;

namespace TrackingBle.src._14MstFloorplan.Services
{
    public class MstFloorplanService : IMstFloorplanService
    {
        private readonly MstFloorplanDbContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public MstFloorplanService(
            MstFloorplanDbContext context,
            IMapper mapper,
            IHttpClientFactory httpClientFactory,
            IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task<MstFloorplanDto> GetByIdAsync(Guid id)
        {
            var floorplan = await _context.MstFloorplans.FirstOrDefaultAsync(f => f.Id == id);
            if (floorplan == null) return null;

            var dto = _mapper.Map<MstFloorplanDto>(floorplan);
            dto.Floor = await GetFloorAsync(floorplan.FloorId);
            return dto;
        }

        public async Task<IEnumerable<MstFloorplanDto>> GetAllAsync()
        {
            var floorplans = await _context.MstFloorplans.ToListAsync();
            var dtos = _mapper.Map<List<MstFloorplanDto>>(floorplans);
            foreach (var dto in dtos)
            {
                dto.Floor = await GetFloorAsync(dto.FloorId);
            }
            return dtos;
        }

        public async Task<MstFloorplanDto> CreateAsync(MstFloorplanCreateDto createDto)
        {
            var floorClient = _httpClientFactory.CreateClient("MstFloorService");
            var floorResponse = await floorClient.GetAsync($"/{createDto.FloorId}");
            if (!floorResponse.IsSuccessStatusCode)
                throw new ArgumentException($"Floor with ID {createDto.FloorId} not found.");

            var floorplan = _mapper.Map<MstFloorplan>(createDto);
            floorplan.Status = 1;
            floorplan.CreatedBy = "system";
            floorplan.CreatedAt = DateTime.UtcNow;
            floorplan.UpdatedBy = "system";
            floorplan.UpdatedAt = DateTime.UtcNow;

            _context.MstFloorplans.Add(floorplan);
            await _context.SaveChangesAsync();

            var dto = _mapper.Map<MstFloorplanDto>(floorplan);
            dto.Floor = await GetFloorAsync(floorplan.FloorId);
            return dto;
        }

        public async Task UpdateAsync(Guid id, MstFloorplanUpdateDto updateDto)
        {
            var floorplan = await _context.MstFloorplans.FindAsync(id);
            if (floorplan == null)
                throw new KeyNotFoundException("Floorplan not found");

            var floorClient = _httpClientFactory.CreateClient("MstFloorService");
            var floorResponse = await floorClient.GetAsync($"/{updateDto.FloorId}");
            if (!floorResponse.IsSuccessStatusCode)
                throw new ArgumentException($"Floor with ID {updateDto.FloorId} not found.");

            _mapper.Map(updateDto, floorplan);
            floorplan.UpdatedBy = "system";
            floorplan.UpdatedAt = DateTime.UtcNow;

            _context.MstFloorplans.Update(floorplan);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var floorplan = await _context.MstFloorplans.FindAsync(id);
            if (floorplan == null)
                throw new KeyNotFoundException("Floorplan not found");

            floorplan.Status = 0;
            _context.MstFloorplans.Update(floorplan);
            await _context.SaveChangesAsync();
        }

        private async Task<MstFloorDto> GetFloorAsync(Guid floorId)
        {
            var client = _httpClientFactory.CreateClient("MstFloorService");
            var response = await client.GetAsync($"/{floorId}");
            if (!response.IsSuccessStatusCode) return null;
            var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<MstFloorDto>>();
            return apiResponse?.Collection?.Data;
        }
    }
}