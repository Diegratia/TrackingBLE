using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrackingBle.Data;
using TrackingBle.Models.Domain;
using TrackingBle.Models.Dto.MstDistrictDto;

namespace TrackingBle.Services
{
    public class MstDistrictService : IMstDistrictService
    {
        private readonly TrackingBleDbContext _context;
        private readonly IMapper _mapper;

        public MstDistrictService(TrackingBleDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<MstDistrictDto> GetByIdAsync(Guid id)
        {
            var district = await _context.MstDistricts
                .FirstOrDefaultAsync(d => d.Id == id);
            return district == null ? null : _mapper.Map<MstDistrictDto>(district);
        }

        public async Task<IEnumerable<MstDistrictDto>> GetAllAsync()
        {
            var districts = await _context.MstDistricts.ToListAsync();
            return _mapper.Map<IEnumerable<MstDistrictDto>>(districts);
        }

        public async Task<MstDistrictDto> CreateAsync(MstDistrictCreateDto createDto)
        {
            var district = _mapper.Map<MstDistrict>(createDto);
            district.CreatedBy = "System"; // Default di server
            district.UpdatedBy = "System"; // Default di server

            _context.MstDistricts.Add(district);
            await _context.SaveChangesAsync();
            return _mapper.Map<MstDistrictDto>(district);
        }

        public async Task UpdateAsync(Guid id, MstDistrictUpdateDto updateDto)
        {
            var district = await _context.MstDistricts.FindAsync(id);
            if (district == null)
                throw new KeyNotFoundException("District not found");

            _mapper.Map(updateDto, district);
            district.UpdatedBy = "System"; // Default di server

            _context.MstDistricts.Update(district);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var district = await _context.MstDistricts.FindAsync(id);
            if (district == null)
                throw new KeyNotFoundException("District not found");

            _context.MstDistricts.Remove(district);
            await _context.SaveChangesAsync();
        }
    }
}