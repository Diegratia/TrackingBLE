using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrackingBle.Models.Dto.FloorplanMaskedAreaDtos;

namespace TrackingBle.Services
{
    public interface IFloorplanMaskedAreaService
    {
        Task<FloorplanMaskedAreaDto> GetByIdAsync(Guid id);
        Task<IEnumerable<FloorplanMaskedAreaDto>> GetAllAsync();
        Task<FloorplanMaskedAreaDto> CreateAsync(FloorplanMaskedAreaCreateDto createDto);
        Task UpdateAsync(Guid id, FloorplanMaskedAreaUpdateDto updateDto);
        Task DeleteAsync(Guid id);
    }
}