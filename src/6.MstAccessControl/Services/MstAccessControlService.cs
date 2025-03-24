using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using TrackingBle.src._6MstAccessControl.Data;
using TrackingBle.src._6MstAccessControl.Models.Domain;
using TrackingBle.src._6MstAccessControl.Models.Dto.MstAccessControlDtos;
using TrackingBle.src.Common.Models;

namespace TrackingBle.src._6MstAccessControl.Services
{
    public class MstAccessControlService : IMstAccessControlService
    {
        private readonly MstAccessControlDbContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpClientFactory _httpClientFactory;

        public MstAccessControlService(
            MstAccessControlDbContext context,
            IMapper mapper,
            IHttpClientFactory httpClientFactory)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        }

        public async Task<MstAccessControlDto> GetByIdAsync(Guid id)
        {
            var accessControl = await _context.MstAccessControls.FirstOrDefaultAsync(a => a.Id == id);
            if (accessControl == null)
            {
                Console.WriteLine($"MstAccessControl with ID {id} not found in database.");
                return null;
            }

            var dto = _mapper.Map<MstAccessControlDto>(accessControl);
            dto.Brand = await GetBrandAsync(accessControl.ControllerBrandId);
            dto.Integration = await GetIntegrationAsync(accessControl.IntegrationId);
            return dto;
        }

        public async Task<IEnumerable<MstAccessControlDto>> GetAllAsync()
        {
            var accessControls = await _context.MstAccessControls.ToListAsync();
            if (!accessControls.Any())
            {
                Console.WriteLine("No MstAccessControls found in database.");
                return new List<MstAccessControlDto>();
            }

            Console.WriteLine($"Found {accessControls.Count} MstAccessControls in database.");
            var dtos = _mapper.Map<List<MstAccessControlDto>>(accessControls);

            foreach (var dto in dtos)
            {
                dto.Brand = await GetBrandAsync(dto.ControllerBrandId);
                dto.Integration = await GetIntegrationAsync(dto.IntegrationId);
            }

            return dtos;
        }

        public async Task<MstAccessControlDto> CreateAsync(MstAccessControlCreateDto createDto)
        {
            if (createDto == null) throw new ArgumentNullException(nameof(createDto));

            var brandClient = _httpClientFactory.CreateClient("MstBrandService");
            var brandResponse = await brandClient.GetAsync($"api/mstbrand/{createDto.ControllerBrandId}");
            if (!brandResponse.IsSuccessStatusCode)
                throw new ArgumentException($"Brand with ID {createDto.ControllerBrandId} not found. Status: {brandResponse.StatusCode}");

            var integrationClient = _httpClientFactory.CreateClient("MstIntegrationService");
            var integrationResponse = await integrationClient.GetAsync($"api/mstintegrations/{createDto.IntegrationId}");
            if (!integrationResponse.IsSuccessStatusCode)
                throw new ArgumentException($"Integration with ID {createDto.IntegrationId} not found. Status: {integrationResponse.StatusCode}");

            var appClient = _httpClientFactory.CreateClient("MstApplicationService");
            var appResponse = await appClient.GetAsync($"api/mstapplication/{createDto.ApplicationId}");
            if (!appResponse.IsSuccessStatusCode)
                throw new ArgumentException($"Application with ID {createDto.ApplicationId} not found. Status: {appResponse.StatusCode}");

            var accessControl = _mapper.Map<MstAccessControl>(createDto);
            accessControl.Id = Guid.NewGuid();
            accessControl.Status = 1;
            accessControl.CreatedBy = "system";
            accessControl.CreatedAt = DateTime.UtcNow;
            accessControl.UpdatedBy = "system";
            accessControl.UpdatedAt = DateTime.UtcNow;

            _context.MstAccessControls.Add(accessControl);
            await _context.SaveChangesAsync();

            var dto = _mapper.Map<MstAccessControlDto>(accessControl);
            dto.Brand = await GetBrandAsync(accessControl.ControllerBrandId);
            dto.Integration = await GetIntegrationAsync(accessControl.IntegrationId);
            return dto;
        }

        public async Task UpdateAsync(Guid id, MstAccessControlUpdateDto updateDto)
        {
            if (updateDto == null) throw new ArgumentNullException(nameof(updateDto));

            var accessControl = await _context.MstAccessControls.FindAsync(id);
            if (accessControl == null)
                throw new KeyNotFoundException($"MstAccessControl with ID {id} not found.");

            if (accessControl.ControllerBrandId != updateDto.ControllerBrandId)
            {
                var brandClient = _httpClientFactory.CreateClient("MstBrandService");
                var brandResponse = await brandClient.GetAsync($"api/mstbrand/{updateDto.ControllerBrandId}");
                if (!brandResponse.IsSuccessStatusCode)
                    throw new ArgumentException($"Brand with ID {updateDto.ControllerBrandId} not found. Status: {brandResponse.StatusCode}");
            }

            if (accessControl.IntegrationId != updateDto.IntegrationId)
            {
                var integrationClient = _httpClientFactory.CreateClient("MstIntegrationService");
                var integrationResponse = await integrationClient.GetAsync($"api/mstintegrations/{updateDto.IntegrationId}");
                if (!integrationResponse.IsSuccessStatusCode)
                    throw new ArgumentException($"Integration with ID {updateDto.IntegrationId} not found. Status: {integrationResponse.StatusCode}");
            }

            if (accessControl.ApplicationId != updateDto.ApplicationId)
            {
                var appClient = _httpClientFactory.CreateClient("MstApplicationService");
                var appResponse = await appClient.GetAsync($"api/mstapplication/{updateDto.ApplicationId}");
                if (!appResponse.IsSuccessStatusCode)
                    throw new ArgumentException($"Application with ID {updateDto.ApplicationId} not found. Status: {appResponse.StatusCode}");
            }

            _mapper.Map(updateDto, accessControl);
            accessControl.UpdatedBy = "system";
            accessControl.UpdatedAt = DateTime.UtcNow;

            _context.MstAccessControls.Update(accessControl);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var accessControl = await _context.MstAccessControls.FindAsync(id);
            if (accessControl == null)
                throw new KeyNotFoundException($"MstAccessControl with ID {id} not found.");

            accessControl.Status = 0;
            await _context.SaveChangesAsync();
        }

        private async Task<MstBrandDto> GetBrandAsync(Guid brandId)
        {
            var client = _httpClientFactory.CreateClient("MstBrandService");
            Console.WriteLine($"Calling MstBrandService at {client.BaseAddress}api/mstbrand/{brandId}");
            var response = await client.GetAsync($"api/mstbrand/{brandId}");
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Failed to get Brand with ID {brandId}. Status: {response.StatusCode}");
                return null;
            }

            var json = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Brand response JSON: {json}");
            try
            {
                var apiResponse = JsonSerializer.Deserialize<ApiResponse<MstBrandDto>>(json);
                return apiResponse?.Collection?.Data;
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Error deserializing Brand JSON: {ex.Message}. JSON: {json}");
                return null;
            }
        }

        private async Task<MstIntegrationDto> GetIntegrationAsync(Guid integrationId)
        {
            var client = _httpClientFactory.CreateClient("MstIntegrationService");
            Console.WriteLine($"Calling MstIntegrationService at {client.BaseAddress}api/mstintegrations/{integrationId}");
            var response = await client.GetAsync($"api/mstintegrations/{integrationId}");
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Failed to get Integration with ID {integrationId}. Status: {response.StatusCode}");
                return null;
            }

            var json = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Integration response JSON: {json}");
            try
            {
                var apiResponse = JsonSerializer.Deserialize<ApiResponse<MstIntegrationDto>>(json);
                return apiResponse?.Collection?.Data;
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Error deserializing Integration JSON: {ex.Message}. JSON: {json}");
                return null;
            }
        }
    }
}