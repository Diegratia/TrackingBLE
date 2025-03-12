using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper; // Tambahkan namespace AutoMapper
using Microsoft.EntityFrameworkCore;
using TrackingBle.Data;
using TrackingBle.Models.Domain;
using TrackingBle.Models.Dto.MstApplicationDtos;

namespace TrackingBle.Services
{
    public class MstApplicationService : IMstApplicationService
    {
        private readonly TrackingBleDbContext _context;
        private readonly IMapper _mapper; // Tambahkan IMapper

        public MstApplicationService(TrackingBleDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MstApplicationDto>> GetAllApplicationsAsync()
        {
            var applications = await _context.MstApplications.ToListAsync();
            return _mapper.Map<IEnumerable<MstApplicationDto>>(applications);
        }

        public async Task<MstApplicationDto> GetApplicationByIdAsync(Guid id)
        {
            var application = await _context.MstApplications.FirstOrDefaultAsync(a => a.Id == id);
            return _mapper.Map<MstApplicationDto>(application);
        }

        public async Task<MstApplicationDto> CreateApplicationAsync(MstApplicationCreateDto dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            // Map dari DTO ke model
            var application = _mapper.Map<MstApplication>(dto);
            application.Id = Guid.NewGuid(); // Tetap hasilkan Id di server
            application.ApplicationStatus = 1;

            _context.MstApplications.Add(application);
            await _context.SaveChangesAsync();

            // Map kembali ke DTO untuk respons
            return _mapper.Map<MstApplicationDto>(application);
        }

        public async Task UpdateApplicationAsync(Guid id, MstApplicationUpdateDto dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            var application = await _context.MstApplications.FindAsync(id);
            if (application == null)
            {
                throw new KeyNotFoundException($"Application with ID {id} not found.");
            }

            // Map dari DTO ke model yang sudah ada
            _mapper.Map(dto, application);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteApplicationAsync(Guid id)
        {
            var application = await _context.MstApplications.FindAsync(id);
            if (application == null)
            {
                throw new KeyNotFoundException($"Application with ID {id} not found.");
            }

            application.ApplicationStatus = 0;
            // _context.MstApplications.Remove(application);
            await _context.SaveChangesAsync();
        }
    }
}