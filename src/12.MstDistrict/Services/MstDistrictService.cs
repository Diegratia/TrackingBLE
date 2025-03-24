using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TrackingBle.src._12MstDistrict.Data;
using TrackingBle.src._12MstDistrict.Models.Domain;
using TrackingBle.src._12MstDistrict.Models.Dto.MstDistrictDtos;
using System.Net.Http;
using Microsoft.Extensions.Configuration;

namespace TrackingBle.src._12MstDistrict.Services
{
    public class MstDistrictService : IMstDistrictService
    {
        private readonly MstDistrictDbContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public MstDistrictService(
            MstDistrictDbContext context,
            IMapper mapper,
            IHttpClientFactory httpClientFactory,
            IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task<MstDistrictDto> GetByIdAsync(Guid id)
        {
            var district = await _context.MstDistricts
                .FirstOrDefaultAsync(d => d.Id == id && d.Status != 0);
            if (district == null) return null;

            var dto = _mapper.Map<MstDistrictDto>(district);
            // Uncomment jika ingin menyertakan Application via HttpClient
            // dto.Application = await GetApplicationAsync(district.ApplicationId);
            return dto;
        }

        public async Task<IEnumerable<MstDistrictDto>> GetAllAsync()
        {
            var districts = await _context.MstDistricts
                .Where(d => d.Status != 0)
                .ToListAsync();
            var dtos = _mapper.Map<List<MstDistrictDto>>(districts);
            // Uncomment jika ingin menyertakan Application via HttpClient
            // foreach (var dto in dtos)
            // {
            //     dto.Application = await GetApplicationAsync(dto.ApplicationId);
            // }
            return dtos;
        }

        public async Task<MstDistrictDto> CreateAsync(MstDistrictCreateDto createDto)
        {
            // Validasi ApplicationId via HttpClient
            var client = _httpClientFactory.CreateClient("MstApplicationService");
            var response = await client.GetAsync($"/api/mstapplication/{createDto.ApplicationId}");
            if (!response.IsSuccessStatusCode)
                throw new ArgumentException($"Application with ID {createDto.ApplicationId} not found.");

            var district = _mapper.Map<MstDistrict>(createDto);
            district.Id = Guid.NewGuid();
            district.Status = 1;
            district.CreatedAt = DateTime.UtcNow;
            district.UpdatedAt = DateTime.UtcNow;
            district.CreatedBy = "system"; // Ganti dengan data login user nanti
            district.UpdatedBy = "system";

            _context.MstDistricts.Add(district);
            await _context.SaveChangesAsync();

            var dto = _mapper.Map<MstDistrictDto>(district);
            // Uncomment jika ingin menyertakan Application via HttpClient
            // dto.Application = await GetApplicationAsync(district.ApplicationId);
            return dto;
        }

        public async Task UpdateAsync(Guid id, MstDistrictUpdateDto updateDto)
        {
            var district = await _context.MstDistricts.FindAsync(id);
            if (district == null || district.Status == 0)
                throw new KeyNotFoundException("District not found");

            // Validasi ApplicationId jika berubah
            if (district.ApplicationId != updateDto.ApplicationId)
            {
                var client = _httpClientFactory.CreateClient("MstApplicationService");
                var response = await client.GetAsync($"/api/mstapplication/{updateDto.ApplicationId}");
                if (!response.IsSuccessStatusCode)
                    throw new ArgumentException($"Application with ID {updateDto.ApplicationId} not found.");
                district.ApplicationId = updateDto.ApplicationId;
            }

            // Update properti secara manual untuk hindari Generate
            district.Code = updateDto.Code;
            district.Name = updateDto.Name;
            district.DistrictHost = updateDto.DistrictHost;
            district.UpdatedBy = "system"; // Ganti dengan data login user nanti
            district.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var district = await _context.MstDistricts.FindAsync(id);
            if (district == null || district.Status == 0)
                throw new KeyNotFoundException("District not found");

            district.Status = 0; // Soft delete
            district.UpdatedBy = "system"; // Ganti dengan data login user nanti
            district.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
        }

        private async Task<MstApplicationDto> GetApplicationAsync(Guid applicationId)
        {
            var client = _httpClientFactory.CreateClient("MstApplicationService");
            var response = await client.GetAsync($"/api/mstapplication/{applicationId}");
            if (!response.IsSuccessStatusCode) return null;
            return await response.Content.ReadFromJsonAsync<MstApplicationDto>();
        }
    }
}