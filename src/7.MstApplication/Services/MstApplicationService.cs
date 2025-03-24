using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrackingBle.src._7MstApplication.Data;
using TrackingBle.src._7MstApplication.Models.Domain;
using TrackingBle.src._7MstApplication.Models.Dto.MstApplicationDtos;

namespace TrackingBle.src._7MstApplication.Services
{
    public class MstApplicationService : IMstApplicationService
    {
        private readonly MstApplicationDbContext _context;
        private readonly IMapper _mapper;

        public MstApplicationService(MstApplicationDbContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<MstApplicationDto>> GetAllApplicationsAsync()
        {
            var applications = await _context.MstApplications.ToListAsync();
            if (!applications.Any())
            {
                Console.WriteLine("No MstApplications found in database.");
                return new List<MstApplicationDto>();
            }

            Console.WriteLine($"Found {applications.Count} MstApplications in database.");
            return _mapper.Map<IEnumerable<MstApplicationDto>>(applications);
        }

        public async Task<MstApplicationDto> GetApplicationByIdAsync(Guid id)
        {
            var application = await _context.MstApplications.FirstOrDefaultAsync(a => a.Id == id);
            if (application == null)
            {
                Console.WriteLine($"MstApplication with ID {id} not found in database.");
                return null;
            }

            return _mapper.Map<MstApplicationDto>(application);
        }

        public async Task<MstApplicationDto> CreateApplicationAsync(MstApplicationCreateDto dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            var application = _mapper.Map<MstApplication>(dto);
            application.Id = Guid.NewGuid();
            application.ApplicationStatus = 1;

            _context.MstApplications.Add(application);
            await _context.SaveChangesAsync();

            var resultDto = _mapper.Map<MstApplicationDto>(application);
            Console.WriteLine($"Created MstApplication with ID {application.Id}");
            return resultDto;
        }

        public async Task UpdateApplicationAsync(Guid id, MstApplicationUpdateDto dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            var application = await _context.MstApplications.FindAsync(id);
            if (application == null)
                throw new KeyNotFoundException($"MstApplication with ID {id} not found.");

            _mapper.Map(dto, application);
            await _context.SaveChangesAsync();

            Console.WriteLine($"Updated MstApplication with ID {id}");
        }

        public async Task DeleteApplicationAsync(Guid id)
        {
            var application = await _context.MstApplications.FindAsync(id);
            if (application == null)
                throw new KeyNotFoundException($"MstApplication with ID {id} not found.");

            application.ApplicationStatus = 0;
            await _context.SaveChangesAsync();

            Console.WriteLine($"Soft-deleted MstApplication with ID {id}");
        }
    }
}