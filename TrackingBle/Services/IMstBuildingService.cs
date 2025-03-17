using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrackingBle.Models.Dto.MstBuildingDtos;
using TrackingBle.Services;

namespace TrackingBle.Services.Interfaces
{
    public interface IMstBuildingService
    {
        Task<MstBuildingDto> CreateAsync(MstBuildingCreateDto dto);
        Task<MstBuildingDto> GetByIdAsync(Guid id);
        Task<IEnumerable<MstBuildingDto>> GetAllAsync();
        Task UpdateAsync(Guid id, MstBuildingUpdateDto dto);
        Task DeleteAsync(Guid id);
    }
}