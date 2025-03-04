using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrackingBle.Models.Dto.MstFloorDto;

namespace TrackingBle.Services
{
    public interface IMstFloorService
    {
        Task<MstFloorDto> GetByIdAsync(Guid id);
        Task<IEnumerable<MstFloorDto>> GetAllAsync();
        Task<MstFloorDto> CreateAsync(MstFloorCreateDto createDto);
        Task UpdateAsync(Guid id, MstFloorUpdateDto updateDto);
        Task DeleteAsync(Guid id);
    }
}