using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using TrackingBle.src._8MstBleReader.Data;
using TrackingBle.src._8MstBleReader.Models.Domain;
using TrackingBle.src._8MstBleReader.Models.Dto.MstBleReaderDtos;
using Microsoft.Extensions.Configuration;
using TrackingBle.src.Common.Models;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace TrackingBle.src._8MstBleReader.Services
{
    public class MstBleReaderService : IMstBleReaderService
    {
        private readonly MstBleReaderDbContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MstBleReaderService(
            MstBleReaderDbContext context,
            IMapper mapper,
            IHttpClientFactory httpClientFactory,
            IHttpContextAccessor httpContextAccessor,
            IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
        }

        public async Task<MstBleReaderDto> GetByIdAsync(Guid id)
        {
            var bleReader = await _context.MstBleReaders.FirstOrDefaultAsync(b => b.Id == id);
            if (bleReader == null) return null;

            var dto = _mapper.Map<MstBleReaderDto>(bleReader);
            dto.Brand = await GetBrandAsync(bleReader.BrandId); 
            return dto;
        }

        public async Task<IEnumerable<MstBleReaderDto>> GetAllAsync()
        {
            var bleReaders = await _context.MstBleReaders.ToListAsync();
            var dtos = _mapper.Map<List<MstBleReaderDto>>(bleReaders);
            foreach (var dto in dtos)
            {
                dto.Brand = await GetBrandAsync(dto.BrandId); 
            }
            return dtos;
        }

        public async Task<MstBleReaderDto> CreateAsync(MstBleReaderCreateDto createDto)
        {
            var brandClient = _httpClientFactory.CreateClient("MstBrandService");
            var brandResponse = await brandClient.GetAsync($"/{createDto.BrandId}");
            if (!brandResponse.IsSuccessStatusCode)
                throw new ArgumentException($"Brand with ID {createDto.BrandId} not found.");

            var bleReader = _mapper.Map<MstBleReader>(createDto);
            var username = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Name)?.Value ?? "";

            bleReader.Status = 1;
            bleReader.CreatedBy = username;
            bleReader.CreatedAt = DateTime.UtcNow;
            bleReader.UpdatedBy = username;
            bleReader.UpdatedAt = DateTime.UtcNow;

            _context.MstBleReaders.Add(bleReader);
            await _context.SaveChangesAsync();

            var dto = _mapper.Map<MstBleReaderDto>(bleReader);
            dto.Brand = await GetBrandAsync(bleReader.BrandId);
            return dto;
        }

        public async Task UpdateAsync(Guid id, MstBleReaderUpdateDto updateDto)
        {
            var bleReader = await _context.MstBleReaders.FindAsync(id);
            if (bleReader == null)
                throw new KeyNotFoundException("BleReader not found");

            var brandClient = _httpClientFactory.CreateClient("MstBrandService");
            var brandResponse = await brandClient.GetAsync($"/{updateDto.BrandId}");
            if (!brandResponse.IsSuccessStatusCode)
                throw new ArgumentException($"Brand with ID {updateDto.BrandId} not found.");

            _mapper.Map(updateDto, bleReader);
            bleReader.UpdatedBy = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Name)?.Value ?? "";
            bleReader.UpdatedAt = DateTime.UtcNow;

            _context.MstBleReaders.Update(bleReader);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var bleReader = await _context.MstBleReaders.FindAsync(id);
            if (bleReader == null)
                throw new KeyNotFoundException("BleReader not found");

            bleReader.Status = 0;
            bleReader.UpdatedBy = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Name)?.Value ?? "";

            bleReader.UpdatedAt = DateTime.UtcNow;
            // _context.MstBleReaders.Update(bleReader);
            await _context.SaveChangesAsync();
        }

        private async Task<MstBrandDto> GetBrandAsync(Guid brandId)
        {
            var client = _httpClientFactory.CreateClient("MstBrandService");
            var response = await client.GetAsync($"/{brandId}");
            if (!response.IsSuccessStatusCode) return null;

            var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<MstBrandDto>>();
            return apiResponse?.Collection?.Data;
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