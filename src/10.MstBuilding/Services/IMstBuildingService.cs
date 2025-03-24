using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrackingBle.src._10MstBuilding.Models.Dto.MstBuildingDtos;

namespace TrackingBle.src._10MstBuilding.Services
{
    public interface IMstBuildingService
    {
        Task<MstBuildingDto> GetByIdAsync(Guid id);
        Task<IEnumerable<MstBuildingDto>> GetAllAsync();
        Task<MstBuildingDto> CreateAsync(MstBuildingCreateDto dto);
        Task UpdateAsync(Guid id, MstBuildingUpdateDto dto);
        Task DeleteAsync(Guid id);
    }
}