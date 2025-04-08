using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using TrackingBle.src._4FloorplanMaskedArea.Data;
using TrackingBle.src._4FloorplanMaskedArea.Models.Domain;
using TrackingBle.src._4FloorplanMaskedArea.Models.Dto.FloorplanMaskedAreaDtos;
using TrackingBle.src.Common.Models;
using Microsoft.Extensions.Configuration;

namespace TrackingBle.src._4FloorplanMaskedArea.Services
{
    public class FloorplanMaskedAreaService : IFloorplanMaskedAreaService
    {
        private readonly FloorplanMaskedAreaDbContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpClientFactory _httpClientFactory; 
        private readonly IConfiguration _configuration;

        public FloorplanMaskedAreaService(
            FloorplanMaskedAreaDbContext context,
            IMapper mapper,
            IHttpClientFactory httpClientFactory, 
            IConfiguration configuration)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public async Task<FloorplanMaskedAreaDto> GetByIdAsync(Guid id)
        {
            var area = await _context.FloorplanMaskedAreas.FirstOrDefaultAsync(a => a.Id == id);
            if (area == null)
            {
                Console.WriteLine($"FloorplanMaskedArea with ID {id} not found.");
                return null;
            }

            var dto = _mapper.Map<FloorplanMaskedAreaDto>(area);
            await PopulateRelationsAsync(dto);
            return dto;
        }

        public async Task<IEnumerable<FloorplanMaskedAreaDto>> GetAllAsync()
        {
            var areas = await _context.FloorplanMaskedAreas.ToListAsync();
            if (!areas.Any())
            {
                Console.WriteLine("No FloorplanMaskedAreas found in database.");
                return new List<FloorplanMaskedAreaDto>();
            }

            Console.WriteLine($"Found {areas.Count} FloorplanMaskedAreas in database.");
            var dtos = _mapper.Map<List<FloorplanMaskedAreaDto>>(areas);
            foreach (var dto in dtos)
            {
                await PopulateRelationsAsync(dto);
            }
            return dtos;
        }

        public async Task<FloorplanMaskedAreaDto> CreateAsync(FloorplanMaskedAreaCreateDto createDto)
        {
            if (createDto == null) throw new ArgumentNullException(nameof(createDto));

            // Validasi foreign key
            await ValidateForeignKeys(createDto.FloorId, createDto.FloorplanId);

            var area = _mapper.Map<FloorplanMaskedArea>(createDto);
            area.Id = Guid.NewGuid(); // Tambahkan jika Id tidak diatur di DTO
            area.Status = 1;
            area.CreatedBy = "system";
            area.CreatedAt = DateTime.UtcNow;
            area.UpdatedBy = "system";
            area.UpdatedAt = DateTime.UtcNow;

            _context.FloorplanMaskedAreas.Add(area);
            await _context.SaveChangesAsync();

            var dto = _mapper.Map<FloorplanMaskedAreaDto>(area);
            await PopulateRelationsAsync(dto);
            return dto;
        }

        public async Task UpdateAsync(Guid id, FloorplanMaskedAreaUpdateDto updateDto)
        {
            if (updateDto == null) throw new ArgumentNullException(nameof(updateDto));

            var area = await _context.FloorplanMaskedAreas.FindAsync(id);
            if (area == null)
            {
                Console.WriteLine($"FloorplanMaskedArea with ID {id} not found.");
                throw new KeyNotFoundException("Area not found");
            }

            // Validasi foreign key jika berubah
            await ValidateForeignKeysIfChanged(area, updateDto);

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
            {
                Console.WriteLine($"FloorplanMaskedArea with ID {id} not found.");
                throw new KeyNotFoundException("Area not found");
            }

            area.Status = 0;
            await _context.SaveChangesAsync();
        }

        // Metode baru untuk mengentralisasi pengisian relasi
        private async Task PopulateRelationsAsync(FloorplanMaskedAreaDto dto)
        {
            Console.WriteLine($"Populating relations for FloorplanMaskedArea ID: {dto.Id}");
            dto.Floor = await GetFloorAsync(dto.FloorId);
            Console.WriteLine($"Floor: {(dto.Floor != null ? "Loaded" : "Null")}");
            dto.Floorplan = await GetFloorplanAsync(dto.FloorplanId);
            Console.WriteLine($"Floorplan: {(dto.Floorplan != null ? "Loaded" : "Null")}");
        }

        private async Task<MstFloorDto> GetFloorAsync(Guid floorId)
        {
            var client = _httpClientFactory.CreateClient("MstFloorService");
            Console.WriteLine($"Fetching Floor with ID {floorId} from {client.BaseAddress}");
            var response = await client.GetAsync($"/{floorId}");
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Failed to get Floor with ID {floorId}. Status: {response.StatusCode}");
                return null;
            }

            var json = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Floor Response: {json}");
            try
            {
                // Jika respons menggunakan ApiResponse
                var apiResponse = JsonSerializer.Deserialize<ApiResponse<MstFloorDto>>(json);
                return apiResponse?.Collection?.Data;
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Error deserializing Floor JSON: {ex.Message}. JSON: {json}");
                // Alternatif: deserialisasi langsung jika tidak ada ApiResponse
                return JsonSerializer.Deserialize<MstFloorDto>(json);
            }
        }

        private async Task<MstFloorplanDto> GetFloorplanAsync(Guid floorplanId)
        {
            var client = _httpClientFactory.CreateClient("MstFloorplanService");
            Console.WriteLine($"Fetching Floorplan with ID {floorplanId} from {client.BaseAddress}");
            var response = await client.GetAsync($"/{floorplanId}");
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Failed to get Floorplan with ID {floorplanId}. Status: {response.StatusCode}");
                return null;
            }

            var json = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Floorplan Response: {json}");
            try
            {
                // Jika respons menggunakan ApiResponse
                var apiResponse = JsonSerializer.Deserialize<ApiResponse<MstFloorplanDto>>(json);
                return apiResponse?.Collection?.Data;
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Error deserializing Floorplan JSON: {ex.Message}. JSON: {json}");
                // Alternatif: deserialisasi langsung jika tidak ada ApiResponse
                return JsonSerializer.Deserialize<MstFloorplanDto>(json);
            }
        }

        private async Task ValidateForeignKeys(Guid floorId, Guid floorplanId)
        {
            var floorClient = _httpClientFactory.CreateClient("MstFloorService");
            var floorResponse = await floorClient.GetAsync($"/{floorId}");
            if (!floorResponse.IsSuccessStatusCode)
                throw new ArgumentException($"Floor with ID {floorId} not found. Status: {floorResponse.StatusCode}");

            var floorplanClient = _httpClientFactory.CreateClient("MstFloorplanService");
            var floorplanResponse = await floorplanClient.GetAsync($"/{floorplanId}");
            if (!floorplanResponse.IsSuccessStatusCode)
                throw new ArgumentException($"Floorplan with ID {floorplanId} not found. Status: {floorplanResponse.StatusCode}");
        }

        private async Task ValidateForeignKeysIfChanged(FloorplanMaskedArea area, FloorplanMaskedAreaUpdateDto updateDto)
        {
            if (area.FloorId != updateDto.FloorId)
            {
                var client = _httpClientFactory.CreateClient("MstFloorService");
                var response = await client.GetAsync($"/{updateDto.FloorId}");
                if (!response.IsSuccessStatusCode)
                    throw new ArgumentException($"Floor with ID {updateDto.FloorId} not found. Status: {response.StatusCode}");
            }

            if (area.FloorplanId != updateDto.FloorplanId)
            {
                var client = _httpClientFactory.CreateClient("MstFloorplanService");
                var response = await client.GetAsync($"/{updateDto.FloorplanId}");
                if (!response.IsSuccessStatusCode)
                    throw new ArgumentException($"Floorplan with ID {updateDto.FloorplanId} not found. Status: {response.StatusCode}");
            }
        }
    }
}