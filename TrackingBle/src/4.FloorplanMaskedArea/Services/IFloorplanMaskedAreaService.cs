    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using TrackingBle.src._4FloorplanMaskedArea.Models.Dto.FloorplanMaskedAreaDtos;

    namespace TrackingBle.src._4FloorplanMaskedArea.Services
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