using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrackingBle.Data;
using TrackingBle.Models.Domain;
using TrackingBle.Models.Dto.MstBleReaderDtos;

namespace TrackingBle.src._8MstBleReader.Services
{
    public class MstBleReaderService : IMstBleReaderService
    {
        private readonly TrackingBleDbContext _context;
        private readonly IMapper _mapper;

        public MstBleReaderService(TrackingBleDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<MstBleReaderDto> GetByIdAsync(Guid id)
        {
            var bleReader = await _context.MstBleReaders
                .FirstOrDefaultAsync(b => b.Id == id);
            return bleReader == null ? null : _mapper.Map<MstBleReaderDto>(bleReader);
        }

        public async Task<IEnumerable<MstBleReaderDto>> GetAllAsync()
        {
            var bleReaders = await _context.MstBleReaders.ToListAsync();
            return _mapper.Map<IEnumerable<MstBleReaderDto>>(bleReaders);
        }

        public async Task<MstBleReaderDto> CreateAsync(MstBleReaderCreateDto createDto)
        {
             // Validasi BrandId
            var brand = await _context.MstBrands.FirstOrDefaultAsync(b => b.Id == createDto.BrandId);
            if (brand == null)
                throw new ArgumentException($"Brand with ID {createDto.BrandId} not found.");

            var bleReader = _mapper.Map<MstBleReader>(createDto);

            bleReader.CreatedBy = "";
            bleReader.UpdatedBy = "";
            bleReader.Status = 1;

            _context.MstBleReaders.Add(bleReader);
            await _context.SaveChangesAsync();
            return _mapper.Map<MstBleReaderDto>(bleReader);
        }

        public async Task UpdateAsync(Guid id, MstBleReaderUpdateDto updateDto)
        {
            var bleReader = await _context.MstBleReaders.FindAsync(id);
            if (bleReader == null)
                throw new KeyNotFoundException("BLE Reader not found");

             if (bleReader.BrandId != updateDto.BrandId)
            {
                var brand = await _context.MstBrands.FirstOrDefaultAsync(b => b.Id == updateDto.BrandId);
                if (brand == null)
                    throw new ArgumentException($"Brand with ID {updateDto.BrandId} not found.");
                // Tidak perlu set BrandData, cukup update BrandId
                bleReader.BrandId = updateDto.BrandId;
            }

            bleReader.UpdatedBy = "";

            _mapper.Map(updateDto, bleReader);
            // _context.MstBleReaders.Update(bleReader);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var bleReader = await _context.MstBleReaders.FindAsync(id);
            if (bleReader == null)
                throw new KeyNotFoundException("BLE Reader not found");
            
            bleReader.Status = 0;

            // _context.MstBleReaders.Remove(bleReader);
            await _context.SaveChangesAsync();
        }
    }
}