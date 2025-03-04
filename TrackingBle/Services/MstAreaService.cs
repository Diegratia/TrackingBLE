using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TrackingBle.Data;
using TrackingBle.Models.Domain;
using TrackingBle.Models.Dto;

namespace TrackingBle.Services
{
    public class MstAreaService : IMstAreaService
    {
        private readonly TrackingBleDbContext _context;

        public MstAreaService(TrackingBleDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MstAreaDto>> GetAllAsync()
        {
            return await _context.MstAreas
                .Select(a => new MstAreaDto
                {
                    Generate = a.Generate,
                    Id = a.Id,
                    FloorId = a.FloorId,
                    Name = a.Name,
                    AreaShape = a.AreaShape,
                    ColorArea = a.ColorArea,
                    RestrictedStatus = a.RestrictedStatus.ToString(), // Convert enum to string for DTO
                    EngineAreaId = a.EngineAreaId,
                    WideArea = a.WideArea,
                    PositionPxX = a.PositionPxX,
                    PositionPxY = a.PositionPxY,
                    CreatedBy = a.CreatedBy,
                    CreatedAt = a.CreatedAt,
                    UpdatedBy = a.UpdatedBy,
                    UpdatedAt = a.UpdatedAt,
                    Status = a.Status
                })
                .ToListAsync();
        }

        public async Task<MstAreaDto> GetByIdAsync(Guid id)
        {
            return await _context.MstAreas
                .Where(a => a.Id == id)
                .Select(a => new MstAreaDto
                {
                    Generate = a.Generate,
                    Id = a.Id,
                    FloorId = a.FloorId,
                    Name = a.Name,
                    AreaShape = a.AreaShape,
                    ColorArea = a.ColorArea,
                    RestrictedStatus = a.RestrictedStatus.ToString(), // Convert enum to string for DTO
                    EngineAreaId = a.EngineAreaId,
                    WideArea = a.WideArea,
                    PositionPxX = a.PositionPxX,
                    PositionPxY = a.PositionPxY,
                    CreatedBy = a.CreatedBy,
                    CreatedAt = a.CreatedAt,
                    UpdatedBy = a.UpdatedBy,
                    UpdatedAt = a.UpdatedAt,
                    Status = a.Status
                })
                .FirstOrDefaultAsync();
        }

        public async Task<MstAreaDto> CreateAsync(MstAreaDto dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            var mstArea = new MstArea
            {
                Id = dto.Id == Guid.Empty ? Guid.NewGuid() : dto.Id,
                Generate = dto.Generate, // Akan diatur oleh database
                
                FloorId = dto.FloorId,
                Name = dto.Name,
                AreaShape = dto.AreaShape,
                ColorArea = dto.ColorArea,
                RestrictedStatus = Enum.Parse<RestrictedStatus>(dto.RestrictedStatus, true), // Convert string to enum
                EngineAreaId = dto.EngineAreaId,
                WideArea = dto.WideArea,
                PositionPxX = dto.PositionPxX,
                PositionPxY = dto.PositionPxY,
                CreatedBy = dto.CreatedBy,
                CreatedAt = dto.CreatedAt,
                UpdatedBy = dto.UpdatedBy,
                UpdatedAt = dto.UpdatedAt,
                Status = dto.Status
            };

            _context.MstAreas.Add(mstArea);
            await _context.SaveChangesAsync();

            dto.Id = mstArea.Id;
            dto.Generate = mstArea.Generate;
            return dto;
        }

        public async Task UpdateAsync(Guid id, MstAreaDto dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));
            if (id != dto.Id) throw new ArgumentException("Id mismatch");

            var existingArea = await _context.MstAreas.FindAsync(id);
            if (existingArea == null) throw new KeyNotFoundException("Area not found");

            existingArea.FloorId = dto.FloorId;
            existingArea.Name = dto.Name;
            existingArea.AreaShape = dto.AreaShape;
            existingArea.ColorArea = dto.ColorArea;
            existingArea.RestrictedStatus = Enum.Parse<RestrictedStatus>(dto.RestrictedStatus, true); // Convert string to enum
            existingArea.EngineAreaId = dto.EngineAreaId;
            existingArea.WideArea = dto.WideArea;
            existingArea.PositionPxX = dto.PositionPxX;
            existingArea.PositionPxY = dto.PositionPxY;
            existingArea.CreatedBy = dto.CreatedBy;
            existingArea.CreatedAt = dto.CreatedAt;
            existingArea.UpdatedBy = dto.UpdatedBy;
            existingArea.UpdatedAt = dto.UpdatedAt;
            existingArea.Status = dto.Status;

            _context.MstAreas.Update(existingArea);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var area = await _context.MstAreas.FindAsync(id);
            if (area == null) throw new KeyNotFoundException("Area not found");

            _context.MstAreas.Remove(area);
            await _context.SaveChangesAsync();
        }
    }
}