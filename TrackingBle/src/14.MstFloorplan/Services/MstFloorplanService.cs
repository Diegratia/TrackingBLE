using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TrackingBle.src._14MstFloorplan.Data;
using TrackingBle.src._14MstFloorplan.Models.Domain;
using TrackingBle.src._14MstFloorplan.Models.Dto.MstFloorplanDtos;
using TrackingBle.src._13MstFloor.Models.Domain;
using TrackingBle.src._14MstFloorplan.Services;

namespace TrackingBle.src._14MstFloorplan.Services
{

    public class MstFloorplanService : IMstFloorplanService
    {
        private readonly MstFloorplanDbContext _context;
        private readonly IMapper _mapper;

        public MstFloorplanService(MstFloorplanDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<MstFloorplanDto> GetByIdAsync(Guid id)
        {
            var floorplan = await _context.MstFloorplans
                .Include(f => f.Floor)
                .FirstOrDefaultAsync(f => f.Id == id && f.Status != 0);

            return floorplan == null ? null : _mapper.Map<MstFloorplanDto>(floorplan);
        }

        public async Task<IEnumerable<MstFloorplanDto>> GetAllAsync()
        {
            var floorplans = await _context.MstFloorplans
                .Include(f => f.Floor)
                .Where(f => f.Status != 0)
                .ToListAsync();

            return _mapper.Map<IEnumerable<MstFloorplanDto>>(floorplans);
        }

        public async Task<MstFloorplanDto> CreateAsync(MstFloorplanCreateDto dto)
        {
            var floor = await _context.MstFloors.FirstOrDefaultAsync(f => f.Id == dto.FloorId);
            if (floor == null)
                throw new ArgumentException($"Floor with ID {dto.FloorId} not found.");

            var floorplan = _mapper.Map<MstFloorplan>(dto);
            floorplan.Id = Guid.NewGuid();
            floorplan.Status = 1;
            floorplan.CreatedAt = DateTime.UtcNow;
            floorplan.UpdatedAt = DateTime.UtcNow;
            floorplan.CreatedBy ??= "system"; // Ganti dengan autentikasi jika ada
            floorplan.UpdatedBy ??= "system";

            _context.MstFloorplans.Add(floorplan);
            await _context.SaveChangesAsync();

            var savedFloorplan = await _context.MstFloorplans
                .Include(f => f.Floor)
                .FirstOrDefaultAsync(f => f.Id == floorplan.Id);
            return _mapper.Map<MstFloorplanDto>(savedFloorplan);
        }

        public async Task UpdateAsync(Guid id, MstFloorplanUpdateDto dto)
        {
            var floorplan = await _context.MstFloorplans.FindAsync(id);
            if (floorplan == null || floorplan.Status == 0)
                throw new KeyNotFoundException("Floorplan not found");

            if (floorplan.FloorId != dto.FloorId)
            {
                var floor = await _context.MstFloors.FirstOrDefaultAsync(f => f.Id == dto.FloorId);
                if (floor == null)
                    throw new ArgumentException($"Floor with ID {dto.FloorId} not found.");
                floorplan.FloorId = dto.FloorId;
            }

            _mapper.Map(dto, floorplan);
            floorplan.UpdatedBy ??= "system"; // Ganti dengan autentikasi jika ada
            floorplan.UpdatedAt = DateTime.UtcNow;

            _context.MstFloorplans.Update(floorplan);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var floorplan = await _context.MstFloorplans.FindAsync(id);
            if (floorplan == null || floorplan.Status == 0)
                throw new KeyNotFoundException("Floorplan not found");

            floorplan.Status = 0; // Soft delete
            await _context.SaveChangesAsync();
        }
    }
}