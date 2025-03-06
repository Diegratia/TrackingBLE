using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TrackingBle.Data;
using TrackingBle.Models.Domain;
using TrackingBle.Models.Dto.MstOrganizationDto;

namespace TrackingBle.Services
{
    public class MstOrganizationService : IMstOrganizationService
    {
        private readonly TrackingBleDbContext _context;
        private readonly IMapper _mapper;

        public MstOrganizationService(TrackingBleDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MstOrganizationDto>> GetAllOrganizationsAsync()
        {
            var organizations = await _context.MstOrganizations.ToListAsync();
            return _mapper.Map<IEnumerable<MstOrganizationDto>>(organizations);
        }

        public async Task<MstOrganizationDto> GetOrganizationByIdAsync(Guid id)
        {
            var organization = await _context.MstOrganizations.FirstOrDefaultAsync(o => o.Id == id);
            return _mapper.Map<MstOrganizationDto>(organization);
        }

        public async Task<MstOrganizationDto> CreateOrganizationAsync(MstOrganizationCreateDto dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            var organization = _mapper.Map<MstOrganization>(dto);
            organization.Id = Guid.NewGuid();
            organization.Status = 1; 
            organization.CreatedBy = ""; 
            organization.CreatedAt = DateTime.Now;
            organization.UpdatedBy = ""; 
            organization.UpdatedAt = DateTime.Now;

            _context.MstOrganizations.Add(organization);
            await _context.SaveChangesAsync();

            return _mapper.Map<MstOrganizationDto>(organization);
        }

        public async Task UpdateOrganizationAsync(Guid id, MstOrganizationUpdateDto dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            var organization = await _context.MstOrganizations.FindAsync(id);
            if (organization == null || organization.Status == 0) 
            {
                throw new KeyNotFoundException($"Organization with ID {id} not found or has been deleted.");
            }

            _mapper.Map(dto, organization);
            organization.UpdatedBy = ""; 
            organization.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteOrganizationAsync(Guid id)
        {
            var organization = await _context.MstOrganizations.FindAsync(id);
            if (organization == null || organization.Status == 0) 
            {
                throw new KeyNotFoundException($"Organization with ID {id} not found or already deleted.");
            }

            organization.Status = 0;
            organization.UpdatedBy = ""; 
            organization.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();
        }
    }
}