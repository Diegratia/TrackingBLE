using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using TrackingBle.src._17MstOrganization.Data;
using TrackingBle.src._17MstOrganization.Models.Domain;
using TrackingBle.src._17MstOrganization.Models.Dto.MstOrganizationDtos;

namespace TrackingBle.src._17MstOrganization.Services
{
    public class MstOrganizationService : IMstOrganizationService
    {
        private readonly MstOrganizationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public MstOrganizationService(
            MstOrganizationDbContext context,
            IMapper mapper,
            IHttpClientFactory httpClientFactory,
            IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task<IEnumerable<MstOrganizationDto>> GetAllOrganizationsAsync()
        {
            var organizations = await _context.MstOrganizations
                .Where(o => o.Status != 0) // Hanya ambil yang aktif
                .ToListAsync();
            return _mapper.Map<IEnumerable<MstOrganizationDto>>(organizations);
        }

        public async Task<MstOrganizationDto> GetOrganizationByIdAsync(Guid id)
        {
            var organization = await _context.MstOrganizations
                .FirstOrDefaultAsync(o => o.Id == id && o.Status != 0);
            return organization == null ? null : _mapper.Map<MstOrganizationDto>(organization);
        }

        public async Task<MstOrganizationDto> CreateOrganizationAsync(MstOrganizationCreateDto dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            // Validasi ApplicationId
            await ValidateApplicationAsync(dto.ApplicationId);

            var organization = _mapper.Map<MstOrganization>(dto);
            organization.Id = Guid.NewGuid();
            organization.Status = 1;
            organization.CreatedBy = "system"; // Ganti dengan user dari context jika ada
            organization.CreatedAt = DateTime.UtcNow;
            organization.UpdatedBy = "system";
            organization.UpdatedAt = DateTime.UtcNow;

            _context.MstOrganizations.Add(organization);
            await _context.SaveChangesAsync();

            return _mapper.Map<MstOrganizationDto>(organization);
        }

        public async Task UpdateOrganizationAsync(Guid id, MstOrganizationUpdateDto dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            var organization = await _context.MstOrganizations
                .FirstOrDefaultAsync(o => o.Id == id && o.Status != 0);
            if (organization == null)
            {
                throw new KeyNotFoundException($"Organization with ID {id} not found or has been deleted.");
            }

            // Validasi ApplicationId jika berubah
            if (organization.ApplicationId != dto.ApplicationId)
            {
                await ValidateApplicationAsync(dto.ApplicationId);
            }

            _mapper.Map(dto, organization);
            organization.UpdatedBy = "system";
            organization.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteOrganizationAsync(Guid id)
        {
            var organization = await _context.MstOrganizations
                .FirstOrDefaultAsync(o => o.Id == id && o.Status != 0);
            if (organization == null)
            {
                throw new KeyNotFoundException($"Organization with ID {id} not found or already deleted.");
            }

            organization.Status = 0;
            organization.UpdatedBy = "system";
            organization.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
        }

        private async Task ValidateApplicationAsync(Guid applicationId)
        {
            var client = _httpClientFactory.CreateClient("MstApplicationService");
            var response = await client.GetAsync($"/api/mstapplication/{applicationId}");
            if (!response.IsSuccessStatusCode)
            {
                throw new ArgumentException($"Application with ID {applicationId} not found.");
            }
        }
    }
}