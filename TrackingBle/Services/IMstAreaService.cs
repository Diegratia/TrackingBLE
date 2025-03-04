using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrackingBle.Models.Dto;

namespace TrackingBle.Services
{
    public interface IMstAreaService
    {
        Task<IEnumerable<MstAreaDto>> GetAllAsync();
        Task<MstAreaDto> GetByIdAsync(Guid id);
        Task<MstAreaDto> CreateAsync(MstAreaDto dto);
        Task UpdateAsync(Guid id, MstAreaDto dto);
        Task DeleteAsync(Guid id);
    }
}