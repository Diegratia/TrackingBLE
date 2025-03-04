using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TrackingBle.Data;
using TrackingBle.Models.Domain;
using TrackingBle.Models.Dto.MstApplicationDto;

namespace TrackingBle.Services
{
    public class MstApplicationService : IMstApplicationService
    {
        private readonly TrackingBleDbContext _context;

        public MstApplicationService(TrackingBleDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MstApplicationDto>> GetAllApplicationsAsync()
        {
            return await _context.MstApplications
                .Select(a => new MstApplicationDto
                {
                    Id = a.Id,
                    Generate = a.Generate,
                    ApplicationName = a.ApplicationName,
                    OrganizationType = a.OrganizationType,
                    OrganizationAddress = a.OrganizationAddress,
                    ApplicationType = a.ApplicationType,
                    ApplicationRegistered = a.ApplicationRegistered,
                    ApplicationExpired = a.ApplicationExpired,
                    HostName = a.HostName,
                    HostPhone = a.HostPhone,
                    HostEmail = a.HostEmail,
                    HostAddress = a.HostAddress,
                    ApplicationCustomName = a.ApplicationCustomName,
                    ApplicationCustomDomain = a.ApplicationCustomDomain,
                    ApplicationCustomPort = a.ApplicationCustomPort,
                    LicenseCode = a.LicenseCode,
                    LicenseType = a.LicenseType,
                    ApplicationStatus = a.ApplicationStatus
                })
                .ToListAsync();
        }

        public async Task<MstApplicationDto> GetApplicationByIdAsync(Guid id)
        {
            return await _context.MstApplications
                .Where(a => a.Id == id)
                .Select(a => new MstApplicationDto
                {
                    Id = a.Id,
                    Generate = a.Generate,
                    ApplicationName = a.ApplicationName,
                    OrganizationType = a.OrganizationType,
                    OrganizationAddress = a.OrganizationAddress,
                    ApplicationType = a.ApplicationType,
                    ApplicationRegistered = a.ApplicationRegistered,
                    ApplicationExpired = a.ApplicationExpired,
                    HostName = a.HostName,
                    HostPhone = a.HostPhone,
                    HostEmail = a.HostEmail,
                    HostAddress = a.HostAddress,
                    ApplicationCustomName = a.ApplicationCustomName,
                    ApplicationCustomDomain = a.ApplicationCustomDomain,
                    ApplicationCustomPort = a.ApplicationCustomPort,
                    LicenseCode = a.LicenseCode,
                    LicenseType = a.LicenseType,
                    ApplicationStatus = a.ApplicationStatus
                })
                .FirstOrDefaultAsync();
        }

        public async Task<MstApplicationDto> CreateApplicationAsync(MstApplicationCreateDto dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            var application = new MstApplication
            {
                Id = Guid.NewGuid(),
                ApplicationName = dto.ApplicationName,
                OrganizationType = dto.OrganizationType,
                OrganizationAddress = dto.OrganizationAddress,
                ApplicationType = dto.ApplicationType,
                ApplicationRegistered = dto.ApplicationRegistered,
                ApplicationExpired = dto.ApplicationExpired,
                HostName = dto.HostName,
                HostPhone = dto.HostPhone,
                HostEmail = dto.HostEmail,
                HostAddress = dto.HostAddress,
                ApplicationCustomName = dto.ApplicationCustomName,
                ApplicationCustomDomain = dto.ApplicationCustomDomain,
                ApplicationCustomPort = dto.ApplicationCustomPort,
                LicenseCode = dto.LicenseCode,
                LicenseType = dto.LicenseType,
                ApplicationStatus = dto.ApplicationStatus
            };

            _context.MstApplications.Add(application);
            await _context.SaveChangesAsync();

            return new MstApplicationDto
            {
                Id = application.Id,
                Generate = application.Generate,
                ApplicationName = application.ApplicationName,
                OrganizationType = application.OrganizationType,
                OrganizationAddress = application.OrganizationAddress,
                ApplicationType = application.ApplicationType,
                ApplicationRegistered = application.ApplicationRegistered,
                ApplicationExpired = application.ApplicationExpired,
                HostName = application.HostName,
                HostPhone = application.HostPhone,
                HostEmail = application.HostEmail,
                HostAddress = application.HostAddress,
                ApplicationCustomName = application.ApplicationCustomName,
                ApplicationCustomDomain = application.ApplicationCustomDomain,
                ApplicationCustomPort = application.ApplicationCustomPort,
                LicenseCode = application.LicenseCode,
                LicenseType = application.LicenseType,
                ApplicationStatus = application.ApplicationStatus
            };
        }

        public async Task UpdateApplicationAsync(Guid id, MstApplicationUpdateDto dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            var application = await _context.MstApplications.FindAsync(id);
            if (application == null)
            {
                throw new KeyNotFoundException($"Application with ID {id} not found.");
            }

            // Update hanya properti yang dikirim oleh klien
            application.ApplicationName = dto.ApplicationName;
            application.OrganizationType = dto.OrganizationType;
            application.OrganizationAddress = dto.OrganizationAddress;
            application.ApplicationType = dto.ApplicationType;
            application.ApplicationRegistered = dto.ApplicationRegistered;
            application.ApplicationExpired = dto.ApplicationExpired;
            application.HostName = dto.HostName;
            application.HostPhone = dto.HostPhone;
            application.HostEmail = dto.HostEmail;
            application.HostAddress = dto.HostAddress;
            application.ApplicationCustomName = dto.ApplicationCustomName;
            application.ApplicationCustomDomain = dto.ApplicationCustomDomain;
            application.ApplicationCustomPort = dto.ApplicationCustomPort;
            application.LicenseCode = dto.LicenseCode;
            application.LicenseType = dto.LicenseType;
            application.ApplicationStatus = dto.ApplicationStatus;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteApplicationAsync(Guid id)
        {
            var application = await _context.MstApplications.FindAsync(id);
            if (application == null)
            {
                throw new KeyNotFoundException($"Application with ID {id} not found.");
            }

            _context.MstApplications.Remove(application);
            await _context.SaveChangesAsync();
        }
    }
}