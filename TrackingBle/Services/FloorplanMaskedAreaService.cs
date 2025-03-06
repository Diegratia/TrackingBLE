using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrackingBle.Data;
using TrackingBle.Models.Dto.FloorplanMaskedAreaDto;
using TrackingBle.Models.Domain;

namespace TrackingBle.Services
{
    public class FloorplanMaskedAreaService : IFloorplanMaskedAreaService
    {
        private readonly TrackingBleDbContext _context;
        private readonly IMapper _mapper;

        public FloorplanMaskedAreaService(TrackingBleDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<FloorplanMaskedAreaDto> GetByIdAsync(Guid id)
        {
            var area = await _context.FloorplanMaskedAreas
                .FirstOrDefaultAsync(a => a.Id == id);
            return area == null ? null : _mapper.Map<FloorplanMaskedAreaDto>(area);
        }

        public async Task<IEnumerable<FloorplanMaskedAreaDto>> GetAllAsync()
        {
            var areas = await _context.FloorplanMaskedAreas.ToListAsync();
            return _mapper.Map<IEnumerable<FloorplanMaskedAreaDto>>(areas);
        }

        public async Task<FloorplanMaskedAreaDto> CreateAsync(FloorplanMaskedAreaCreateDto createDto)
        {
            var area = _mapper.Map<FloorplanMaskedArea>(createDto);

             area.Status = 1;
             area.CreatedBy = "";
             area.UpdatedBy = "";

            _context.FloorplanMaskedAreas.Add(area);
            await _context.SaveChangesAsync();
            return _mapper.Map<FloorplanMaskedAreaDto>(area);
        }

        public async Task UpdateAsync(Guid id, FloorplanMaskedAreaUpdateDto updateDto)
        {
            var area = await _context.FloorplanMaskedAreas.FindAsync(id);
            if (area == null)
                throw new KeyNotFoundException("Area not found");

            area.UpdatedBy = "";

            _mapper.Map(updateDto, area);
            _context.FloorplanMaskedAreas.Update(area);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var area = await _context.FloorplanMaskedAreas.FindAsync(id);
            if (area == null)
                throw new KeyNotFoundException("Area not found");

            area.Status = 0;

            // _context.FloorplanMaskedAreas.Remove(area);
            await _context.SaveChangesAsync();
        }
    }
}