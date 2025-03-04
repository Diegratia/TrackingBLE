using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrackingBle.Data;
using TrackingBle.Models.Dto.MstAreaDto;
using TrackingBle.Models.Domain;

namespace TrackingBle.Services
{
    public class MstAreaService : IMstAreaService
    {
        private readonly TrackingBleDbContext _context;
        private readonly IMapper _mapper;

        public MstAreaService(TrackingBleDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<MstAreaDto> GetByIdAsync(Guid id)
        {
            var area = await _context.MstAreas
                .FirstOrDefaultAsync(a => a.Id == id);
            return area == null ? null : _mapper.Map<MstAreaDto>(area);
        }

        public async Task<IEnumerable<MstAreaDto>> GetAllAsync()
        {
            var areas = await _context.MstAreas.ToListAsync();
            return _mapper.Map<IEnumerable<MstAreaDto>>(areas);
        }

        public async Task<MstAreaDto> CreateAsync(MstAreaCreateDto createDto)
        {
            var area = _mapper.Map<MstArea>(createDto);
            _context.MstAreas.Add(area);
            await _context.SaveChangesAsync();
            return _mapper.Map<MstAreaDto>(area);
        }

        public async Task UpdateAsync(Guid id, MstAreaUpdateDto updateDto)
        {
            var area = await _context.MstAreas.FindAsync(id);
            if (area == null)
                throw new KeyNotFoundException("Area not found");

            _mapper.Map(updateDto, area);
            _context.MstAreas.Update(area);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var area = await _context.MstAreas.FindAsync(id);
            if (area == null)
                throw new KeyNotFoundException("Area not found");

            _context.MstAreas.Remove(area);
            await _context.SaveChangesAsync();
        }
    }
}