using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrackingBle.src._13MstFloor.Models.Dto.MstFloorDtos;

namespace TrackingBle.src._13MstFloor.Services
{
    public interface IMstFloorService
    {
        Task<MstFloorDto> GetByIdAsync(Guid id);
        Task<IEnumerable<MstFloorDto>> GetAllAsync();
        Task<MstFloorDto> CreateAsync(MstFloorCreateDto createDto);
        Task<MstFloorDto> UpdateAsync(Guid id, MstFloorUpdateDto updateDto);
        Task DeleteAsync(Guid id);
    }
}