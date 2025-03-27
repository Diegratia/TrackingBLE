using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using TrackingBle.src._3FloorplanDevice.Data;
using TrackingBle.src._3FloorplanDevice.Models.Domain;
using TrackingBle.src._3FloorplanDevice.Models.Dto.FloorplanDeviceDtos;
using TrackingBle.src.Common.Models;
using Microsoft.Extensions.Configuration;

namespace TrackingBle.src._3FloorplanDevice.Services
{
    public class FloorplanDeviceService : IFloorplanDeviceService
    {
        private readonly FloorplanDeviceDbContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly JsonSerializerOptions _jsonOptions;

        public FloorplanDeviceService(
            FloorplanDeviceDbContext context,
            IMapper mapper,
            IHttpClientFactory httpClientFactory,
            IConfiguration configuration)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true 
            };
        }

        public async Task<FloorplanDeviceDto> GetByIdAsync(Guid id)
        {
            var device = await _context.FloorplanDevices
                .FirstOrDefaultAsync(fd => fd.Id == id && fd.Status != 0);
            if (device == null)
            {
                Console.WriteLine($"FloorplanDevice with ID {id} not found.");
                return null;
            }

            var dto = _mapper.Map<FloorplanDeviceDto>(device);
            await PopulateRelationsAsync(dto);
            return dto;
        }

        public async Task<IEnumerable<FloorplanDeviceDto>> GetAllAsync()
        {
            var devices = await _context.FloorplanDevices
                .Where(fd => fd.Status != 0)
                .ToListAsync();

            if (!devices.Any())
            {
                Console.WriteLine("No FloorplanDevices found in database.");
                return new List<FloorplanDeviceDto>();
            }

            Console.WriteLine($"Found {devices.Count} FloorplanDevices in database.");
            var dtos = _mapper.Map<List<FloorplanDeviceDto>>(devices);

            foreach (var dto in dtos)
            {
                await PopulateRelationsAsync(dto);
            }

            return dtos;
        }
        
        public async Task<FloorplanDeviceDto> CreateAsync(FloorplanDeviceCreateDto createDto)
        {
            if (createDto == null) throw new ArgumentNullException(nameof(createDto));

            // Validasi foreign key dengan HttpClient
            await ValidateForeignKeys(createDto.FloorplanId, createDto.AccessCctvId, createDto.ReaderId, createDto.AccessControlId, createDto.FloorplanMaskedAreaId, createDto.ApplicationId);

            var device = _mapper.Map<FloorplanDevice>(createDto);
            device.Id = Guid.NewGuid();
            device.Status = 1;
            device.CreatedBy = "system";
            device.CreatedAt = DateTime.UtcNow;
            device.UpdatedBy = "system";
            device.UpdatedAt = DateTime.UtcNow;

            _context.FloorplanDevices.Add(device);
            await _context.SaveChangesAsync();

            var resultDto = _mapper.Map<FloorplanDeviceDto>(device);
            await PopulateRelationsAsync(resultDto);
            return resultDto;
        }

        public async Task UpdateAsync(Guid id, FloorplanDeviceUpdateDto updateDto)
        {
            if (updateDto == null) throw new ArgumentNullException(nameof(updateDto));

            var device = await _context.FloorplanDevices.FindAsync(id);
            if (device == null || device.Status == 0)
                throw new KeyNotFoundException($"FloorplanDevice with ID {id} not found.");

            // Validasi foreign key jika berubah
            await ValidateForeignKeysIfChanged(device, updateDto);

            _mapper.Map(updateDto, device);
            device.UpdatedBy = "system";
            device.UpdatedAt = DateTime.UtcNow;

            _context.FloorplanDevices.Update(device);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var device = await _context.FloorplanDevices.FindAsync(id);
            if (device == null || device.Status == 0)
                throw new KeyNotFoundException($"FloorplanDevice with ID {id} not found.");

            device.Status = 0;
            await _context.SaveChangesAsync();
        }
private async Task PopulateRelationsAsync(FloorplanDeviceDto dto)
        {
            Console.WriteLine($"Populating relations for FloorplanDevice ID: {dto.Id}");
            dto.Floorplan = await GetFloorplanAsync(dto.FloorplanId);
            Console.WriteLine($"Floorplan: {(dto.Floorplan != null ? "Loaded" : "Null")}");
            dto.AccessCctv = await GetAccessCctvAsync(dto.AccessCctvId);
            Console.WriteLine($"AccessCctv: {(dto.AccessCctv != null ? "Loaded" : "Null")}");
            dto.Reader = await GetReaderAsync(dto.ReaderId);
            Console.WriteLine($"Reader: {(dto.Reader != null ? "Loaded" : "Null")}");
            dto.AccessControl = await GetAccessControlAsync(dto.AccessControlId);
            Console.WriteLine($"AccessControl: {(dto.AccessControl != null ? "Loaded" : "Null")}");
            dto.FloorplanMaskedArea = await GetFloorplanMaskedAreaAsync(dto.FloorplanMaskedAreaId);
            Console.WriteLine($"FloorplanMaskedArea: {(dto.FloorplanMaskedArea != null ? "Loaded" : "Null")}");
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
            return DeserializeSingle<MstFloorplanDto>(json, floorplanId);
        }

        private async Task<MstAccessCctvDto> GetAccessCctvAsync(Guid accessCctvId)
        {
            var client = _httpClientFactory.CreateClient("MstAccessCctvService");
            Console.WriteLine($"Fetching AccessCctv with ID {accessCctvId} from {client.BaseAddress}");
            var response = await client.GetAsync($"/{accessCctvId}");
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Failed to get AccessCctv with ID {accessCctvId}. Status: {response.StatusCode}");
                return null;
            }

            var json = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"AccessCctv Response: {json}");
            return DeserializeSingle<MstAccessCctvDto>(json, accessCctvId);
        }

        private async Task<MstBleReaderDto> GetReaderAsync(Guid readerId)
        {
            var client = _httpClientFactory.CreateClient("MstBleReaderService");
            Console.WriteLine($"Fetching Reader with ID {readerId} from {client.BaseAddress}");
            var response = await client.GetAsync($"/{readerId}");
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Failed to get Reader with ID {readerId}. Status: {response.StatusCode}");
                return null;
            }

            var json = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Reader Response: {json}");
            return DeserializeSingle<MstBleReaderDto>(json, readerId);
        }

        private async Task<MstAccessControlDto> GetAccessControlAsync(Guid accessControlId)
        {
            var client = _httpClientFactory.CreateClient("MstAccessControlService");
            Console.WriteLine($"Fetching AccessControl with ID {accessControlId} from {client.BaseAddress}");
            var response = await client.GetAsync($"/{accessControlId}");
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Failed to get AccessControl with ID {accessControlId}. Status: {response.StatusCode}");
                return null;
            }

            var json = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"AccessControl Response: {json}");
            return DeserializeSingle<MstAccessControlDto>(json, accessControlId);
        }

        private async Task<FloorplanMaskedAreaDto> GetFloorplanMaskedAreaAsync(Guid floorplanMaskedAreaId)
        {
            var client = _httpClientFactory.CreateClient("FloorplanMaskedAreaService");
            Console.WriteLine($"Fetching FloorplanMaskedArea with ID {floorplanMaskedAreaId} from {client.BaseAddress}");
            var response = await client.GetAsync($"/{floorplanMaskedAreaId}");
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Failed to get FloorplanMaskedArea with ID {floorplanMaskedAreaId}. Status: {response.StatusCode}");
                return null;
            }

            var json = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"FloorplanMaskedArea Response: {json}");
            return DeserializeSingle<FloorplanMaskedAreaDto>(json, floorplanMaskedAreaId);
        }

        private T DeserializeSingle<T>(string json, Guid id) where T : class
        {
            if (string.IsNullOrEmpty(json))
            {
                Console.WriteLine("Received empty JSON response.");
                return null;
            }

            try
            {
                // Deserialisasi sebagai ApiResponse<T> (data tunggal)
                var apiResponse = JsonSerializer.Deserialize<ApiResponse<T>>(json, _jsonOptions);
                if (apiResponse?.Success == true && apiResponse.Collection?.Data != null)
                {
                    var dataId = apiResponse.Collection.Data.GetType().GetProperty("Id")?.GetValue(apiResponse.Collection.Data)?.ToString();
                    if (dataId == id.ToString())
                    {
                        return apiResponse.Collection.Data;
                    }
                    Console.WriteLine($"ID mismatch: expected {id}, got {dataId}");
                    return null;
                }

                // Jika gagal, coba deserialisasi langsung sebagai T
                var directResult = JsonSerializer.Deserialize<T>(json, _jsonOptions);
                if (directResult != null)
                {
                    var directId = directResult.GetType().GetProperty("Id")?.GetValue(directResult)?.ToString();
                    if (directId == id.ToString())
                    {
                        return directResult;
                    }
                    Console.WriteLine($"ID mismatch in direct deserialization: expected {id}, got {directId}");
                    return null;
                }

                Console.WriteLine("No valid data found in response.");
                return null;
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Error deserializing JSON: {ex.Message}. JSON: {json}");
                return null;
            }
        }

    

        private async Task ValidateForeignKeys(Guid floorplanId, Guid accessCctvId, Guid readerId, Guid accessControlId, Guid floorplanMaskedAreaId, Guid applicationId)
        {
            var floorplanClient = _httpClientFactory.CreateClient("MstFloorplanService");
            var floorplanResponse = await floorplanClient.GetAsync($"/{floorplanId}");
            if (!floorplanResponse.IsSuccessStatusCode)
                throw new ArgumentException($"Floorplan with ID {floorplanId} not found. Status: {floorplanResponse.StatusCode}");

            var cctvClient = _httpClientFactory.CreateClient("MstAccessCctvService");
            var cctvResponse = await cctvClient.GetAsync($"/{accessCctvId}");
            if (!cctvResponse.IsSuccessStatusCode)
                throw new ArgumentException($"AccessCctv with ID {accessCctvId} not found. Status: {cctvResponse.StatusCode}");

            var readerClient = _httpClientFactory.CreateClient("MstBleReaderService");
            var readerResponse = await readerClient.GetAsync($"/{readerId}");
            if (!readerResponse.IsSuccessStatusCode)
                throw new ArgumentException($"Reader with ID {readerId} not found. Status: {readerResponse.StatusCode}");

            var controlClient = _httpClientFactory.CreateClient("MstAccessControlService");
            var controlResponse = await controlClient.GetAsync($"/{accessControlId}");
            if (!controlResponse.IsSuccessStatusCode)
                throw new ArgumentException($"AccessControl with ID {accessControlId} not found. Status: {controlResponse.StatusCode}");

            var maskedAreaClient = _httpClientFactory.CreateClient("FloorplanMaskedAreaService");
            var maskedAreaResponse = await maskedAreaClient.GetAsync($"/{floorplanMaskedAreaId}");
            if (!maskedAreaResponse.IsSuccessStatusCode)
                throw new ArgumentException($"FloorplanMaskedArea with ID {floorplanMaskedAreaId} not found. Status: {maskedAreaResponse.StatusCode}");

            var appClient = _httpClientFactory.CreateClient("MstApplicationService");
            var appResponse = await appClient.GetAsync($"/{applicationId}");
            if (!appResponse.IsSuccessStatusCode)
                throw new ArgumentException($"Application with ID {applicationId} not found. Status: {appResponse.StatusCode}");
        }

        private async Task ValidateForeignKeysIfChanged(FloorplanDevice device, FloorplanDeviceUpdateDto updateDto)
        {
            if (device.FloorplanId != updateDto.FloorplanId)
            {
                var client = _httpClientFactory.CreateClient("MstFloorplanService");
                var response = await client.GetAsync($"/{updateDto.FloorplanId}");
                if (!response.IsSuccessStatusCode)
                    throw new ArgumentException($"Floorplan with ID {updateDto.FloorplanId} not found. Status: {response.StatusCode}");
            }

            if (device.AccessCctvId != updateDto.AccessCctvId)
            {
                var client = _httpClientFactory.CreateClient("MstAccessCctvService");
                var response = await client.GetAsync($"/{updateDto.AccessCctvId}");
                if (!response.IsSuccessStatusCode)
                    throw new ArgumentException($"AccessCctv with ID {updateDto.AccessCctvId} not found. Status: {response.StatusCode}");
            }

            if (device.ReaderId != updateDto.ReaderId)
            {
                var client = _httpClientFactory.CreateClient("MstBleReaderService");
                var response = await client.GetAsync($"/{updateDto.ReaderId}");
                if (!response.IsSuccessStatusCode)
                    throw new ArgumentException($"Reader with ID {updateDto.ReaderId} not found. Status: {response.StatusCode}");
            }

            if (device.AccessControlId != updateDto.AccessControlId)
            {
                var client = _httpClientFactory.CreateClient("MstAccessControlService");
                var response = await client.GetAsync($"/{updateDto.AccessControlId}");
                if (!response.IsSuccessStatusCode)
                    throw new ArgumentException($"AccessControl with ID {updateDto.AccessControlId} not found. Status: {response.StatusCode}");
            }

            if (device.FloorplanMaskedAreaId != updateDto.FloorplanMaskedAreaId)
            {
                var client = _httpClientFactory.CreateClient("FloorplanMaskedAreaService");
                var response = await client.GetAsync($"/{updateDto.FloorplanMaskedAreaId}");
                if (!response.IsSuccessStatusCode)
                    throw new ArgumentException($"FloorplanMaskedArea with ID {updateDto.FloorplanMaskedAreaId} not found. Status: {response.StatusCode}");
            }

            if (device.ApplicationId != updateDto.ApplicationId)
            {
                var client = _httpClientFactory.CreateClient("MstApplicationService");
                var response = await client.GetAsync($"/{updateDto.ApplicationId}");
                if (!response.IsSuccessStatusCode)
                    throw new ArgumentException($"Application with ID {updateDto.ApplicationId} not found. Status: {response.StatusCode}");
            }
        }
    }
}