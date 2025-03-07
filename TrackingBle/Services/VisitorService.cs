using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrackingBle.Data;
using TrackingBle.Models.Domain;
using TrackingBle.Models.Dto.VisitorDto;

namespace TrackingBle.Services
{
    public class VisitorService : IVisitorService
    {
        private readonly TrackingBleDbContext _context;
        private readonly IMapper _mapper;

        public VisitorService(TrackingBleDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<VisitorDto> CreateVisitorAsync(VisitorCreateDto dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            if (!await _context.Visitors.AnyAsync(f => f.Id == dto.ApplicationId))
                throw new ArgumentException($"Application with ID {dto.ApplicationId} not found.");

            var visitor = _mapper.Map<Visitor>(dto);
            visitor.Id = Guid.NewGuid();

            _context.Visitors.Add(visitor);
            await _context.SaveChangesAsync();

            return _mapper.Map<VisitorDto>(visitor);
        }

        public async Task<VisitorDto> GetVisitorByIdAsync(Guid id)
        {
            var visitor = await _context.Visitors
                .Include(v => v.Application)
                .FirstOrDefaultAsync(v => v.Id == id);
            return _mapper.Map<VisitorDto>(visitor);
        }

        public async Task<IEnumerable<VisitorDto>> GetAllVisitorsAsync()
        {
            var visitor = await _context.Visitors
                .Include(v => v.Application)
                .ToListAsync();
            return _mapper.Map<IEnumerable<VisitorDto>>(visitor);
        }

        public async Task UpdateVisitorAsync(Guid id, VisitorUpdateDto dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            var visitor = await _context.Visitors.FindAsync(id);
            if (visitor == null)
            {
                throw new KeyNotFoundException($"Visitor with ID {id} not found.");
            }

            if (!await _context.FloorplanMaskedAreas.AnyAsync(f => f.Id == dto.ApplicationId))
                throw new ArgumentException($"Application with ID {dto.ApplicationId} not found.");

            _mapper.Map(dto, visitor);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteVisitorAsync(Guid id)
        {
            var visitor = await _context.Visitors.FindAsync(id);
            if (visitor == null)
            {
                throw new KeyNotFoundException($"Visitor with ID {id} not found.");
            }

            _context.Visitors.Remove(visitor);
            await _context.SaveChangesAsync();
        }
    }
}