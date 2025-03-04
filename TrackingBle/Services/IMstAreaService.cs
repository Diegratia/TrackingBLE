using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrackingBle.Models.Dto.MstAreaDto;

namespace TrackingBle.Services
{
    public interface IMstAreaService
    {
        Task<MstAreaDto> GetByIdAsync(Guid id);
        Task<IEnumerable<MstAreaDto>> GetAllAsync();
        Task<MstAreaDto> CreateAsync(MstAreaCreateDto createDto);
        Task UpdateAsync(Guid id, MstAreaUpdateDto updateDto);
        Task DeleteAsync(Guid id);
    }
}