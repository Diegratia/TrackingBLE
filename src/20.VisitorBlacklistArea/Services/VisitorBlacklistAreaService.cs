using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using TrackingBle.src._20VisitorBlacklistArea.Models.Domain;
using TrackingBle.src._20VisitorBlacklistArea.Models.Dto.VisitorBlacklistAreaDtos;
using TrackingBle.src._20VisitorBlacklistArea.Data;
using TrackingBle.src.Common.Models; 

namespace TrackingBle.src._20VisitorBlacklistArea.Services
{
    public class VisitorBlacklistAreaService : IVisitorBlacklistAreaService
    {
        private readonly VisitorBlacklistAreaDbContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpClientFactory _httpClientFactory;

        public VisitorBlacklistAreaService(
            VisitorBlacklistAreaDbContext context,
            IMapper mapper,
            IHttpClientFactory httpClientFactory)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        }

        public async Task<VisitorBlacklistAreaDto> GetVisitorBlacklistAreaByIdAsync(Guid id)
        {
            var blacklistArea = await _context.VisitorBlacklistAreas.FirstOrDefaultAsync(v => v.Id == id);
            if (blacklistArea == null)
            {
                Console.WriteLine($"VisitorBlacklistArea with ID {id} not found in database.");
                return null;
            }

            var dto = _mapper.Map<VisitorBlacklistAreaDto>(blacklistArea);
            dto.FloorplanMaskedArea = await GetFloorplanMaskedAreaAsync(blacklistArea.FloorplanMaskedAreaId);
            dto.Visitor = await GetVisitorAsync(blacklistArea.VisitorId);
            return dto;
        }

        public async Task<IEnumerable<VisitorBlacklistAreaDto>> GetAllVisitorBlacklistAreasAsync()
        {
            var blacklistAreas = await _context.VisitorBlacklistAreas.ToListAsync();
            if (!blacklistAreas.Any())
            {
                Console.WriteLine("No VisitorBlacklistAreas found in database.");
                return new List<VisitorBlacklistAreaDto>();
            }

            Console.WriteLine($"Found {blacklistAreas.Count} VisitorBlacklistAreas in database.");
            foreach (var area in blacklistAreas)
            {
                Console.WriteLine($"ID: {area.Id}, FloorplanMaskedAreaId: {area.FloorplanMaskedAreaId}, VisitorId: {area.VisitorId}");
            }

            var dtos = _mapper.Map<List<VisitorBlacklistAreaDto>>(blacklistAreas);

            foreach (var dto in dtos)
            {
                dto.FloorplanMaskedArea = await GetFloorplanMaskedAreaAsync(dto.FloorplanMaskedAreaId);
                dto.Visitor = await GetVisitorAsync(dto.VisitorId);
            }

            return dtos;
        }

        public async Task<VisitorBlacklistAreaDto> CreateVisitorBlacklistAreaAsync(VisitorBlacklistAreaCreateDto createDto)
        {
            if (createDto == null) throw new ArgumentNullException(nameof(createDto));

            var floorplanMaskedAreaClient = _httpClientFactory.CreateClient("FloorplanMaskedAreaService");
            var floorplanResponse = await floorplanMaskedAreaClient.GetAsync($"api/floorplanmaskedareas/{createDto.FloorplanMaskedAreaId}");
            if (!floorplanResponse.IsSuccessStatusCode)
                throw new ArgumentException($"FloorplanMaskedArea with ID {createDto.FloorplanMaskedAreaId} not found. Status: {floorplanResponse.StatusCode}");

            var visitorClient = _httpClientFactory.CreateClient("VisitorService");
            var visitorResponse = await visitorClient.GetAsync($"api/visitors/{createDto.VisitorId}");
            if (!visitorResponse.IsSuccessStatusCode)
                throw new ArgumentException($"Visitor with ID {createDto.VisitorId} not found. Status: {visitorResponse.StatusCode}");

            var blacklistArea = _mapper.Map<VisitorBlacklistArea>(createDto);
            blacklistArea.Id = Guid.NewGuid();

            _context.VisitorBlacklistAreas.Add(blacklistArea);
            await _context.SaveChangesAsync();

            var dto = _mapper.Map<VisitorBlacklistAreaDto>(blacklistArea);
            dto.FloorplanMaskedArea = await GetFloorplanMaskedAreaAsync(blacklistArea.FloorplanMaskedAreaId);
            dto.Visitor = await GetVisitorAsync(blacklistArea.VisitorId);
            return dto;
        }

        public async Task UpdateVisitorBlacklistAreaAsync(Guid id, VisitorBlacklistAreaUpdateDto updateDto)
        {
            if (updateDto == null) throw new ArgumentNullException(nameof(updateDto));

            var blacklistArea = await _context.VisitorBlacklistAreas.FindAsync(id);
            if (blacklistArea == null)
                throw new KeyNotFoundException($"VisitorBlacklistArea with ID {id} not found.");

            if (blacklistArea.FloorplanMaskedAreaId != updateDto.FloorplanMaskedAreaId)
            {
                var floorplanMaskedAreaClient = _httpClientFactory.CreateClient("FloorplanMaskedAreaService");
                var floorplanResponse = await floorplanMaskedAreaClient.GetAsync($"api/floorplanmaskedareas/{updateDto.FloorplanMaskedAreaId}");
                if (!floorplanResponse.IsSuccessStatusCode)
                    throw new ArgumentException($"FloorplanMaskedArea with ID {updateDto.FloorplanMaskedAreaId} not found. Status: {floorplanResponse.StatusCode}");
            }

            if (blacklistArea.VisitorId != updateDto.VisitorId)
            {
                var visitorClient = _httpClientFactory.CreateClient("VisitorService");
                var visitorResponse = await visitorClient.GetAsync($"api/visitors/{updateDto.VisitorId}");
                if (!visitorResponse.IsSuccessStatusCode)
                    throw new ArgumentException($"Visitor with ID {updateDto.VisitorId} not found. Status: {visitorResponse.StatusCode}");
            }

            _mapper.Map(updateDto, blacklistArea);
            _context.VisitorBlacklistAreas.Update(blacklistArea);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteVisitorBlacklistAreaAsync(Guid id)
        {
            var blacklistArea = await _context.VisitorBlacklistAreas.FindAsync(id);
            if (blacklistArea == null)
                throw new KeyNotFoundException($"VisitorBlacklistArea with ID {id} not found.");

            _context.VisitorBlacklistAreas.Remove(blacklistArea);
            await _context.SaveChangesAsync();
        }

        private async Task<FloorplanMaskedAreaDto> GetFloorplanMaskedAreaAsync(Guid floorplanMaskedAreaId)
        {
            var client = _httpClientFactory.CreateClient("FloorplanMaskedAreaService");
            Console.WriteLine($"Calling FloorplanMaskedAreaService at {client.BaseAddress}api/floorplanmaskedareas/{floorplanMaskedAreaId}");
            var response = await client.GetAsync($"api/floorplanmaskedareas/{floorplanMaskedAreaId}");
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Failed to get FloorplanMaskedArea with ID {floorplanMaskedAreaId}. Status: {response.StatusCode}");
                return null;
            }

            var json = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"FloorplanMaskedArea response JSON: {json}");
            try
            {
                var apiResponse = JsonSerializer.Deserialize<ApiResponse<FloorplanMaskedAreaDto>>(json);
                return apiResponse?.Collection?.Data;
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Error deserializing FloorplanMaskedArea JSON: {ex.Message}. JSON: {json}");
                return null;
            }
        }

        private async Task<VisitorDto> GetVisitorAsync(Guid visitorId)
        {
            var client = _httpClientFactory.CreateClient("VisitorService");
            Console.WriteLine($"Calling VisitorService at {client.BaseAddress}api/visitors/{visitorId}");
            var response = await client.GetAsync($"api/visitors/{visitorId}");
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Failed to get Visitor with ID {visitorId}. Status: {response.StatusCode}");
                return null;
            }

            var json = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Visitor response JSON: {json}");
            try
            {
                var apiResponse = JsonSerializer.Deserialize<ApiResponse<VisitorDto>>(json);
                return apiResponse?.Collection?.Data;
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Error deserializing Visitor JSON: {ex.Message}. JSON: {json}");
                return null;
            }
        }
    }
}