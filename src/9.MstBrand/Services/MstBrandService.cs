using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrackingBle.src._9MstBrand.Data;
using TrackingBle.src._9MstBrand.Models.Dto.MstBrandDtos;
using TrackingBle.src._9MstBrand.Models.Domain;

namespace TrackingBle.src._9MstBrand.Services
{
    public class MstBrandService : IMstBrandService
    {
        private readonly MstBrandDbContext _context;
        private readonly IMapper _mapper;

        public MstBrandService(MstBrandDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<MstBrandDto> GetByIdAsync(Guid id)
        {
            var brand = await _context.MstBrands
                .FirstOrDefaultAsync(b => b.Id == id && b.Status != 0); // Filter status aktif
            if (brand == null)
            {
                Console.WriteLine($"Brand with ID {id} not found or inactive.");
                return null;
            }
            Console.WriteLine($"Retrieved brand Generate: {brand.Generate}");
            return _mapper.Map<MstBrandDto>(brand);
        }

        public async Task<IEnumerable<MstBrandDto>> GetAllAsync()
        {
            var brands = await _context.MstBrands
                .Where(b => b.Status != 0) // Hanya ambil yang aktif
                .ToListAsync();
            Console.WriteLine($"Retrieved {brands.Count} brands.");
            return _mapper.Map<IEnumerable<MstBrandDto>>(brands);
        }

        public async Task<MstBrandDto> CreateAsync(MstBrandCreateDto createDto)
        {
            var brand = _mapper.Map<MstBrand>(createDto);
            brand.Status = 1; // Default status aktif
            brand.Id = Guid.NewGuid(); // Pastikan ID baru

            _context.MstBrands.Add(brand);
            await _context.SaveChangesAsync();
            Console.WriteLine($"Created brand with Generate: {brand.Generate}");
            return _mapper.Map<MstBrandDto>(brand);
        }

        public async Task UpdateAsync(Guid id, MstBrandUpdateDto updateDto)
        {
            var brand = await _context.MstBrands
                .FirstOrDefaultAsync(b => b.Id == id && b.Status != 0);
            if (brand == null)
            {
                Console.WriteLine($"Brand with ID {id} not found or inactive.");
                throw new KeyNotFoundException("Brand not found");
            }

            Console.WriteLine($"Generate before update: {brand.Generate}");
            _mapper.Map(updateDto, brand);
            Console.WriteLine($"Generate after update: {brand.Generate}");
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var brand = await _context.MstBrands
                .FirstOrDefaultAsync(b => b.Id == id && b.Status != 0);
            if (brand == null)
            {
                Console.WriteLine($"Brand with ID {id} not found or already inactive.");
                throw new KeyNotFoundException("Brand not found");
            }

            brand.Status = 0; // Soft delete
            await _context.SaveChangesAsync();
            Console.WriteLine($"Brand with ID {id} marked as inactive.");
        }
    }
}