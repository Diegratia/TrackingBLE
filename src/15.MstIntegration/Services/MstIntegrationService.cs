using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using TrackingBle.src._15MstIntegration.Data;
using TrackingBle.src._15MstIntegration.Models.Domain;
using TrackingBle.src._15MstIntegration.Models.Dto.MstIntegrationDtos;
using TrackingBle.src.Common.Models;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace TrackingBle.src._15MstIntegration.Services
{
    public class MstIntegrationService : IMstIntegrationService
    {
        private readonly MstIntegrationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly JsonSerializerOptions _jsonOptions;

        public MstIntegrationService(
            MstIntegrationDbContext context,
            IMapper mapper,
            IHttpClientFactory httpClientFactory,
            IHttpContextAccessor httpContextAccessor)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
            _httpContextAccessor = httpContextAccessor;
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true // abaikan case sensitif
            };
        }

        public async Task<MstIntegrationDto> GetByIdAsync(Guid id)
        {
            var integration = await _context.MstIntegrations.FirstOrDefaultAsync(i => i.Id == id);
            if (integration == null)
            {
                Console.WriteLine($"MstIntegration with ID {id} not found in database.");
                return null;
            }

            var dto = _mapper.Map<MstIntegrationDto>(integration);
            dto.Brand = await GetBrandAsync(integration.BrandId);
            return dto;
        }

        public async Task<IEnumerable<MstIntegrationDto>> GetAllAsync()
        {
            var integrations = await _context.MstIntegrations.ToListAsync();
            if (!integrations.Any())
            {
                Console.WriteLine("No MstIntegrations found in database.");
                return new List<MstIntegrationDto>();
            }

            Console.WriteLine($"Found {integrations.Count} MstIntegrations in database.");
            var dtos = _mapper.Map<List<MstIntegrationDto>>(integrations);

            foreach (var dto in dtos)
            {
                dto.Brand = await GetBrandAsync(dto.BrandId);
            }

            return dtos;
        }

        public async Task<MstIntegrationDto> CreateAsync(MstIntegrationCreateDto createDto)
        {
            if (createDto == null) throw new ArgumentNullException(nameof(createDto));

            var brandClient = _httpClientFactory.CreateClient("MstBrandService");
            Console.WriteLine($"Validating Brand with ID {createDto.BrandId} at {brandClient.BaseAddress}api/mstbrand/{createDto.BrandId}");
            var brandResponse = await brandClient.GetAsync($"/{createDto.BrandId}");
            if (!brandResponse.IsSuccessStatusCode)
                throw new ArgumentException($"Brand with ID {createDto.BrandId} not found. Status: {brandResponse.StatusCode}");

            var appClient = _httpClientFactory.CreateClient("MstApplicationService");
            Console.WriteLine($"Validating Application with ID {createDto.ApplicationId} at {appClient.BaseAddress}api/mstapplication/{createDto.ApplicationId}");
            var appResponse = await appClient.GetAsync($"api/mstapplication/{createDto.ApplicationId}");
            if (!appResponse.IsSuccessStatusCode)
                throw new ArgumentException($"Application with ID {createDto.ApplicationId} not found. Status: {appResponse.StatusCode}");

            var integration = _mapper.Map<MstIntegration>(createDto);
            var username = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Name)?.Value ?? "";

            integration.Id = Guid.NewGuid();
            integration.Status = 1;
            integration.CreatedBy = username;
            integration.CreatedAt = DateTime.UtcNow;
            integration.UpdatedBy = username;
            integration.UpdatedAt = DateTime.UtcNow;

            _context.MstIntegrations.Add(integration);
            await _context.SaveChangesAsync();

            var dto = _mapper.Map<MstIntegrationDto>(integration);
            dto.Brand = await GetBrandAsync(integration.BrandId);
            return dto;
        }

        public async Task UpdateAsync(Guid id, MstIntegrationUpdateDto updateDto)
        {
            if (updateDto == null) throw new ArgumentNullException(nameof(updateDto));

            var integration = await _context.MstIntegrations.FindAsync(id);
            if (integration == null)
                throw new KeyNotFoundException($"MstIntegration with ID {id} not found.");

            if (integration.BrandId != updateDto.BrandId)
            {
                var brandClient = _httpClientFactory.CreateClient("MstBrandService");
                Console.WriteLine($"Validating Brand with ID {updateDto.BrandId} at {brandClient.BaseAddress}api/mstbrand/{updateDto.BrandId}");
                var brandResponse = await brandClient.GetAsync($"/{updateDto.BrandId}");
                if (!brandResponse.IsSuccessStatusCode)
                    throw new ArgumentException($"Brand with ID {updateDto.BrandId} not found. Status: {brandResponse.StatusCode}");
            }

            if (integration.ApplicationId != updateDto.ApplicationId)
            {
                var appClient = _httpClientFactory.CreateClient("MstApplicationService");
                Console.WriteLine($"Validating Application with ID {updateDto.ApplicationId} at {appClient.BaseAddress}api/mstapplication/{updateDto.ApplicationId}");
                var appResponse = await appClient.GetAsync($"api/mstapplication/{updateDto.ApplicationId}");
                if (!appResponse.IsSuccessStatusCode)
                    throw new ArgumentException($"Application with ID {updateDto.ApplicationId} not found. Status: {appResponse.StatusCode}");
            }

            _mapper.Map(updateDto, integration);
            integration.UpdatedBy = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Name)?.Value ?? "";
            integration.UpdatedAt = DateTime.UtcNow;

            _context.MstIntegrations.Update(integration);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var integration = await _context.MstIntegrations.FindAsync(id);
            if (integration == null)
                throw new KeyNotFoundException($"MstIntegration with ID {id} not found.");

            integration.UpdatedBy = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Name)?.Value ?? "";
            integration.UpdatedAt = DateTime.UtcNow;
            integration.Status = 0;
            await _context.SaveChangesAsync();
        }

        private async Task<MstBrandDto> GetBrandAsync(Guid brandId)
        {
            var client = _httpClientFactory.CreateClient("MstBrandService");
            Console.WriteLine($"Calling MstBrandService at {client.BaseAddress}api/mstbrand/{brandId}");
            var response = await client.GetAsync($"/{brandId}");
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Failed to get Brand with ID {brandId}. Status: {response.StatusCode}");
                return null;
            }

            var json = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Brand response JSON: {json}");
            try
            {
                var apiResponse = JsonSerializer.Deserialize<ApiResponse<MstBrandDto>>(json, _jsonOptions);
                if (apiResponse?.Success == true && apiResponse.Collection?.Data != null)
                {
                    Console.WriteLine($"Successfully deserialized Brand with ID {brandId}");
                    return apiResponse.Collection.Data;
                }

                Console.WriteLine($"No valid data found in Brand response for ID {brandId}");
                return null;
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Error deserializing Brand JSON: {ex.Message}. JSON: {json}");
                return null;
            }
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