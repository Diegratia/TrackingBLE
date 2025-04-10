using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrackingBle.Models.Dto.MstFloorplanDtos;

namespace TrackingBle.Services.Interfaces
{
    public interface IMstFloorplanService
    {
        Task<MstFloorplanDto> CreateAsync(MstFloorplanCreateDto dto);
        Task<MstFloorplanDto> GetByIdAsync(Guid id);
        Task<IEnumerable<MstFloorplanDto>> GetAllAsync();
        Task UpdateAsync(Guid Id,MstFloorplanUpdateDto dto);
        Task DeleteAsync(Guid id);
    }
}