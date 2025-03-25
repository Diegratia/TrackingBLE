using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using TrackingBle.src._5MstAccessCctv.Data;
using TrackingBle.src._5MstAccessCctv.Models.Domain;
using TrackingBle.src._5MstAccessCctv.Models.Dto.MstAccessCctvDtos;
using TrackingBle.src.Common.Models;

namespace TrackingBle.src._5MstAccessCctv.Services
{
    public class MstAccessCctvService : IMstAccessCctvService
    {
        private readonly MstAccessCctvDbContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly JsonSerializerOptions _jsonOptions;

        public MstAccessCctvService(
            MstAccessCctvDbContext context,
            IMapper mapper,
            IHttpClientFactory httpClientFactory)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true // Mengabaikan perbedaan huruf besar/kecil
            };
        }

        public async Task<MstAccessCctvDto> GetByIdAsync(Guid id)
        {
            var accessCctv = await _context.MstAccessCctvs.FirstOrDefaultAsync(a => a.Id == id);
            if (accessCctv == null)
            {
                Console.WriteLine($"MstAccessCctv with ID {id} not found in database.");
                return null;
            }

            var dto = _mapper.Map<MstAccessCctvDto>(accessCctv);
            dto.Integration = await GetIntegrationAsync(accessCctv.IntegrationId);
            return dto;
        }

        public async Task<IEnumerable<MstAccessCctvDto>> GetAllAsync()
        {
            var accessCctvs = await _context.MstAccessCctvs.ToListAsync();
            if (!accessCctvs.Any())
            {
                Console.WriteLine("No MstAccessCctvs found in database.");
                return new List<MstAccessCctvDto>();
            }

            Console.WriteLine($"Found {accessCctvs.Count} MstAccessCctvs in database.");
            var dtos = _mapper.Map<List<MstAccessCctvDto>>(accessCctvs);

            foreach (var dto in dtos)
            {
                dto.Integration = await GetIntegrationAsync(dto.IntegrationId);
            }

            return dtos;
        }

        public async Task<MstAccessCctvDto> CreateAsync(MstAccessCctvCreateDto createDto)
        {
            if (createDto == null) throw new ArgumentNullException(nameof(createDto));

            var integrationClient = _httpClientFactory.CreateClient("MstIntegrationService");
            var integrationResponse = await integrationClient.GetAsync($"api/mstintegration/{createDto.IntegrationId}");
            if (!integrationResponse.IsSuccessStatusCode)
                throw new ArgumentException($"Integration with ID {createDto.IntegrationId} not found. Status: {integrationResponse.StatusCode}");

            var appClient = _httpClientFactory.CreateClient("MstApplicationService");
            var appResponse = await appClient.GetAsync($"api/mstapplication/{createDto.ApplicationId}");
            if (!appResponse.IsSuccessStatusCode)
                throw new ArgumentException($"Application with ID {createDto.ApplicationId} not found. Status: {appResponse.StatusCode}");

            var accessCctv = _mapper.Map<MstAccessCctv>(createDto);
            accessCctv.Id = Guid.NewGuid();
            accessCctv.Status = 1;
            accessCctv.CreatedBy = "system";
            accessCctv.CreatedAt = DateTime.UtcNow;
            accessCctv.UpdatedBy = "system";
            accessCctv.UpdatedAt = DateTime.UtcNow;

            _context.MstAccessCctvs.Add(accessCctv);
            await _context.SaveChangesAsync();

            var dto = _mapper.Map<MstAccessCctvDto>(accessCctv);
            dto.Integration = await GetIntegrationAsync(accessCctv.IntegrationId);
            return dto;
        }

        public async Task UpdateAsync(Guid id, MstAccessCctvUpdateDto updateDto)
        {
            if (updateDto == null) throw new ArgumentNullException(nameof(updateDto));

            var accessCctv = await _context.MstAccessCctvs.FindAsync(id);
            if (accessCctv == null)
                throw new KeyNotFoundException($"MstAccessCctv with ID {id} not found.");

            if (accessCctv.IntegrationId != updateDto.IntegrationId)
            {
                var integrationClient = _httpClientFactory.CreateClient("MstIntegrationService");
                var integrationResponse = await integrationClient.GetAsync($"api/mstintegration/{updateDto.IntegrationId}");
                if (!integrationResponse.IsSuccessStatusCode)
                    throw new ArgumentException($"Integration with ID {updateDto.IntegrationId} not found. Status: {integrationResponse.StatusCode}");
            }

            if (accessCctv.ApplicationId != updateDto.ApplicationId)
            {
                var appClient = _httpClientFactory.CreateClient("MstApplicationService");
                var appResponse = await appClient.GetAsync($"api/mstapplication/{updateDto.ApplicationId}");
                if (!appResponse.IsSuccessStatusCode)
                    throw new ArgumentException($"Application with ID {updateDto.ApplicationId} not found. Status: {appResponse.StatusCode}");
            }

            _mapper.Map(updateDto, accessCctv);
            accessCctv.UpdatedBy = "system";
            accessCctv.UpdatedAt = DateTime.UtcNow;

            _context.MstAccessCctvs.Update(accessCctv);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var accessCctv = await _context.MstAccessCctvs.FindAsync(id);
            if (accessCctv == null)
                throw new KeyNotFoundException($"MstAccessCctv with ID {id} not found.");

            accessCctv.Status = 0;
            await _context.SaveChangesAsync();
        }

        private async Task<MstIntegrationDto> GetIntegrationAsync(Guid integrationId)
        {
            var client = _httpClientFactory.CreateClient("MstIntegrationService");
            Console.WriteLine($"Calling MstIntegrationService at {client.BaseAddress}api/mstintegration/{integrationId}");
            var response = await client.GetAsync($"api/mstintegration/{integrationId}");
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Failed to get Integration with ID {integrationId}. Status: {response.StatusCode}");
                return null;
            }

            var json = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Integration response JSON: {json}");
            try
            {
                var apiResponse = JsonSerializer.Deserialize<ApiResponse<MstIntegrationDto>>(json, _jsonOptions);
                if (apiResponse?.Success == true && apiResponse.Collection?.Data != null)
                {
                    Console.WriteLine($"Successfully deserialized Integration with ID {integrationId}");
                    return apiResponse.Collection.Data;
                }

                Console.WriteLine($"No valid data found in Integration response for ID {integrationId}");
                return null;
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Error deserializing Integration JSON: {ex.Message}. JSON: {json}");
                return null;
            }
        }
    }
}