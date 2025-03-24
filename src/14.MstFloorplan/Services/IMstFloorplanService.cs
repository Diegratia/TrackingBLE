using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrackingBle.src._14MstFloorplan.Models.Dto.MstFloorplanDtos;

namespace TrackingBle.src._14MstFloorplan.Services
{
  public interface IMstFloorplanService
    {
        Task<MstFloorplanDto> GetByIdAsync(Guid id);
        Task<IEnumerable<MstFloorplanDto>> GetAllAsync();
        Task<MstFloorplanDto> CreateAsync(MstFloorplanCreateDto createDto);
        Task UpdateAsync(Guid id, MstFloorplanUpdateDto updateDto);
        Task DeleteAsync(Guid id);
    }
}