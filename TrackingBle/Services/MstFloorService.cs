using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrackingBle.Data;
using TrackingBle.Models.Domain;
using TrackingBle.Models.Dto.MstFloorDto;

namespace TrackingBle.Services
{
    public class MstFloorService : IMstFloorService
    {
        private readonly TrackingBleDbContext _context;
        private readonly IMapper _mapper;

        public MstFloorService(TrackingBleDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<MstFloorDto> GetByIdAsync(Guid id)
        {
            var floor = await _context.MstFloors
                .FirstOrDefaultAsync(f => f.Id == id);
            return floor == null ? null : _mapper.Map<MstFloorDto>(floor);
        }

        public async Task<IEnumerable<MstFloorDto>> GetAllAsync()
        {
            var floors = await _context.MstFloors.ToListAsync();
            return _mapper.Map<IEnumerable<MstFloorDto>>(floors);
        }

        public async Task<MstFloorDto> CreateAsync(MstFloorCreateDto createDto)
        {
            var floor = _mapper.Map<MstFloor>(createDto);
            floor.CreatedBy = "";
            floor.UpdatedBy = "";
            floor.Status = 1;

            _context.MstFloors.Add(floor);
            await _context.SaveChangesAsync();
            return _mapper.Map<MstFloorDto>(floor);
        }

        public async Task UpdateAsync(Guid id, MstFloorUpdateDto updateDto)
        {
            var floor = await _context.MstFloors.FindAsync(id);
            if (floor == null)
                throw new KeyNotFoundException("Floor not found");

             floor.UpdatedBy = "";
            _mapper.Map(updateDto, floor);
           

            _context.MstFloors.Update(floor);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var floor = await _context.MstFloors.FindAsync(id);
            if (floor == null)
                throw new KeyNotFoundException("Floor not found");

            floor.Status = 1;

            // _context.MstFloors.Remove(floor);
            await _context.SaveChangesAsync();
        }
    }
}