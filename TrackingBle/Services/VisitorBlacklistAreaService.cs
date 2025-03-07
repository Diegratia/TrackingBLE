using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrackingBle.Data;
using TrackingBle.Models.Domain;
using TrackingBle.Models.Dto.VisitorBlacklistAreaDto;

namespace TrackingBle.Services
{
    public class VisitorBlacklistAreaService : IVisitorBlacklistAreaService
    {
        private readonly TrackingBleDbContext _context;
        private readonly IMapper _mapper;

        public VisitorBlacklistAreaService(TrackingBleDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<VisitorBlacklistAreaDto> CreateVisitorBlacklistAreaAsync(VisitorBlacklistAreaCreateDto dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            if (!await _context.FloorplanMaskedAreas.AnyAsync(f => f.Id == dto.FloorplanId))
                throw new ArgumentException($"Floorplan with ID {dto.FloorplanId} not found.");
            if (!await _context.Visitors.AnyAsync(v => v.Id == dto.VisitorId))
                throw new ArgumentException($"Visitor with ID {dto.VisitorId} not found.");

            var blacklistArea = _mapper.Map<VisitorBlacklistArea>(dto);
            blacklistArea.Id = Guid.NewGuid();

            _context.VisitorBlacklistAreas.Add(blacklistArea);
            await _context.SaveChangesAsync();

            return _mapper.Map<VisitorBlacklistAreaDto>(blacklistArea);
        }

        public async Task<VisitorBlacklistAreaDto> GetVisitorBlacklistAreaByIdAsync(Guid id)
        {
            var blacklistArea = await _context.VisitorBlacklistAreas
                .Include(v => v.Floorplan)
                .Include(v => v.Visitor)
                .FirstOrDefaultAsync(v => v.Id == id);
            return _mapper.Map<VisitorBlacklistAreaDto>(blacklistArea);
        }

        public async Task<IEnumerable<VisitorBlacklistAreaDto>> GetAllVisitorBlacklistAreasAsync()
        {
            var blacklistAreas = await _context.VisitorBlacklistAreas
                .Include(v => v.Floorplan)
                .Include(v => v.Visitor)
                .ToListAsync();
            return _mapper.Map<IEnumerable<VisitorBlacklistAreaDto>>(blacklistAreas);
        }

        public async Task UpdateVisitorBlacklistAreaAsync(Guid id, VisitorBlacklistAreaUpdateDto dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            var blacklistArea = await _context.VisitorBlacklistAreas.FindAsync(id);
            if (blacklistArea == null)
            {
                throw new KeyNotFoundException($"VisitorBlacklistArea with ID {id} not found.");
            }

            if (!await _context.FloorplanMaskedAreas.AnyAsync(f => f.Id == dto.FloorplanId))
                throw new ArgumentException($"Floorplan with ID {dto.FloorplanId} not found.");
            if (!await _context.Visitors.AnyAsync(v => v.Id == dto.VisitorId))
                throw new ArgumentException($"Visitor with ID {dto.VisitorId} not found.");

            _mapper.Map(dto, blacklistArea);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteVisitorBlacklistAreaAsync(Guid id)
        {
            var blacklistArea = await _context.VisitorBlacklistAreas.FindAsync(id);
            if (blacklistArea == null)
            {
                throw new KeyNotFoundException($"VisitorBlacklistArea with ID {id} not found.");
            }

            _context.VisitorBlacklistAreas.Remove(blacklistArea);
            await _context.SaveChangesAsync();
        }
    }
}