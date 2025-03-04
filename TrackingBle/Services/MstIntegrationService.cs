using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TrackingBle.Data;
using TrackingBle.Models.Domain;
using TrackingBle.Models.Dto;

namespace TrackingBle.Services
{
    public class MstIntegrationService : IMstIntegrationService
    {
        private readonly TrackingBleDbContext _context;

        public MstIntegrationService(TrackingBleDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MstIntegrationDto>> GetAllAsync()
        {
            return await _context.MstIntegrations
                .Select(i => new MstIntegrationDto
                {
                    Generate = i.Generate,
                    Id = i.Id,
                    BrandId = i.BrandId,
                    IntegrationType = i.IntegrationType,
                    ApiTypeAuth = i.ApiTypeAuth,
                    ApiUrl = i.ApiUrl,
                    ApiAuthUsername = i.ApiAuthUsername,
                    ApiAuthPasswd = i.ApiAuthPasswd,
                    ApiKeyField = i.ApiKeyField,
                    ApiKeyValue = i.ApiKeyValue,
                    ApplicationId = i.ApplicationId,
                    CreatedBy = i.CreatedBy,
                    CreatedAt = i.CreatedAt,
                    UpdatedBy = i.UpdatedBy,
                    UpdatedAt = i.UpdatedAt,
                    Status = i.Status
                })
                .ToListAsync();
        }

        public async Task<MstIntegrationDto> GetByIdAsync(Guid id)
        {
            return await _context.MstIntegrations
                .Where(i => i.Id == id)
                .Select(i => new MstIntegrationDto
                {
                    Generate = i.Generate,
                    Id = i.Id,
                    BrandId = i.BrandId,
                    IntegrationType = i.IntegrationType,
                    ApiTypeAuth = i.ApiTypeAuth,
                    ApiUrl = i.ApiUrl,
                    ApiAuthUsername = i.ApiAuthUsername,
                    ApiAuthPasswd = i.ApiAuthPasswd,
                    ApiKeyField = i.ApiKeyField,
                    ApiKeyValue = i.ApiKeyValue,
                    ApplicationId = i.ApplicationId,
                    CreatedBy = i.CreatedBy,
                    CreatedAt = i.CreatedAt,
                    UpdatedBy = i.UpdatedBy,
                    UpdatedAt = i.UpdatedAt,
                    Status = i.Status
                })
                .FirstOrDefaultAsync();
        }

        public async Task<MstIntegrationDto> CreateAsync(MstIntegrationDto dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            var mstIntegration = new MstIntegration
            {
                Id = dto.Id == Guid.Empty ? Guid.NewGuid() : dto.Id,
                Generate = dto.Generate, // Akan diatur oleh database
                BrandId = dto.BrandId,
                IntegrationType = dto.IntegrationType,
                ApiTypeAuth = dto.ApiTypeAuth,
                ApiUrl = dto.ApiUrl,
                ApiAuthUsername = dto.ApiAuthUsername,
                ApiAuthPasswd = dto.ApiAuthPasswd,
                ApiKeyField = dto.ApiKeyField,
                ApiKeyValue = dto.ApiKeyValue,
                ApplicationId = dto.ApplicationId,
                CreatedBy = dto.CreatedBy,
                CreatedAt = dto.CreatedAt,
                UpdatedBy = dto.UpdatedBy,
                UpdatedAt = dto.UpdatedAt,
                Status = dto.Status
            };

            _context.MstIntegrations.Add(mstIntegration);
            await _context.SaveChangesAsync();

            // Update DTO dengan nilai yang dihasilkan
            dto.Id = mstIntegration.Id;
            dto.Generate = mstIntegration.Generate;
            return dto;
        }

        public async Task UpdateAsync(Guid id, MstIntegrationDto dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));
            if (id != dto.Id) throw new ArgumentException("Id mismatch");

            var existingIntegration = await _context.MstIntegrations.FindAsync(id);
            if (existingIntegration == null) throw new KeyNotFoundException("Integration not found");

            // Update properties
            existingIntegration.Generate = dto.Generate;
            existingIntegration.BrandId = dto.BrandId;
            existingIntegration.IntegrationType = dto.IntegrationType;
            existingIntegration.ApiTypeAuth = dto.ApiTypeAuth;
            existingIntegration.ApiUrl = dto.ApiUrl;
            existingIntegration.ApiAuthUsername = dto.ApiAuthUsername;
            existingIntegration.ApiAuthPasswd = dto.ApiAuthPasswd;
            existingIntegration.ApiKeyField = dto.ApiKeyField;
            existingIntegration.ApiKeyValue = dto.ApiKeyValue;
            existingIntegration.ApplicationId = dto.ApplicationId;
            existingIntegration.CreatedBy = dto.CreatedBy;
            existingIntegration.CreatedAt = dto.CreatedAt;
            existingIntegration.UpdatedBy = dto.UpdatedBy;
            existingIntegration.UpdatedAt = dto.UpdatedAt;
            existingIntegration.Status = dto.Status;

            _context.MstIntegrations.Update(existingIntegration);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var integration = await _context.MstIntegrations.FindAsync(id);
            if (integration == null) throw new KeyNotFoundException("Integration not found");

            _context.MstIntegrations.Remove(integration);
            await _context.SaveChangesAsync();
        }
    }
}