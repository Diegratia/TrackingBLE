using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrackingBle.Data;
using TrackingBle.Models.Dto.MstBrandDto;
using TrackingBle.Models.Domain;

namespace TrackingBle.Services
{
    public class MstBrandService : IMstBrandService
    {
        private readonly TrackingBleDbContext _context;
        private readonly IMapper _mapper;

        public MstBrandService(TrackingBleDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<MstBrandDto> GetByIdAsync(Guid id)
        {
            var brand = await _context.MstBrands
                .FirstOrDefaultAsync(b => b.Id == id);
            return brand == null ? null : _mapper.Map<MstBrandDto>(brand);
        }

        public async Task<IEnumerable<MstBrandDto>> GetAllAsync()
        {
            var brands = await _context.MstBrands.ToListAsync();
            return _mapper.Map<IEnumerable<MstBrandDto>>(brands);
        }

        public async Task<MstBrandDto> CreateAsync(MstBrandCreateDto createDto)
        {
            var brand = _mapper.Map<MstBrand>(createDto);
            _context.MstBrands.Add(brand);
            await _context.SaveChangesAsync();
            return _mapper.Map<MstBrandDto>(brand);
        }

        public async Task UpdateAsync(Guid id, MstBrandUpdateDto updateDto)
        {
            var brand = await _context.MstBrands.FindAsync(id);
            if (brand == null)
                throw new KeyNotFoundException("Brand not found");

            _mapper.Map(updateDto, brand);
            _context.MstBrands.Update(brand);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var brand = await _context.MstBrands.FindAsync(id);
            if (brand == null)
                throw new KeyNotFoundException("Brand not found");

            _context.MstBrands.Remove(brand);
            await _context.SaveChangesAsync();
        }
    }
}