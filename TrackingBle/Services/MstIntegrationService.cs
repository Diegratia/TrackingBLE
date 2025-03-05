using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TrackingBle.Data;
using TrackingBle.Models.Dto.MstIntegrationDto;
using TrackingBle.Models.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TrackingBle.Services
{
    public class MstIntegrationService : IMstIntegrationService
    {
        private readonly TrackingBleDbContext _context;
        private readonly IMapper _mapper;

        public MstIntegrationService(TrackingBleDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<MstIntegrationDto> GetByIdAsync(Guid id)
        {
            var integration = await _context.MstIntegrations
                .FirstOrDefaultAsync(i => i.Id == id);
            return integration == null ? null : _mapper.Map<MstIntegrationDto>(integration);
        }

        public async Task<IEnumerable<MstIntegrationDto>> GetAllAsync()
        {
            var integrations = await _context.MstIntegrations.ToListAsync();
            return _mapper.Map<IEnumerable<MstIntegrationDto>>(integrations);
        }

        public async Task<MstIntegrationDto> CreateAsync(MstIntegrationCreateDto createDto)
        {
        

            var integration = _mapper.Map<MstIntegration>(createDto);
                // Set default "System" 
            integration.Status = 1;
            integration.CreatedBy ??= "System";
            integration.UpdatedBy ??= "System";
            _context.MstIntegrations.Add(integration);
            await _context.SaveChangesAsync();
            return _mapper.Map<MstIntegrationDto>(integration);
        }

        public async Task UpdateAsync(Guid id, MstIntegrationUpdateDto updateDto)
        {
            var integration = await _context.MstIntegrations.FindAsync(id);
            if (integration == null)
                throw new KeyNotFoundException("Integration not found");

            integration.UpdatedBy ??= "System";

            _mapper.Map(updateDto, integration);
            _context.MstIntegrations.Update(integration);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var integration = await _context.MstIntegrations.FindAsync(id);
            if (integration == null)
                throw new KeyNotFoundException("Integration not found");

            integration.Status = 0;

            // _context.MstIntegrations.Remove(integration);
            await _context.SaveChangesAsync();
        }
    }
}