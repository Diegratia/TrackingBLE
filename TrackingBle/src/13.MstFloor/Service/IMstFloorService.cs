using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrackingBle.src._13_MstFloor.Models.Dto.MstFloorDtos;

namespace TrackingBle.src._13MstFloor.Service
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