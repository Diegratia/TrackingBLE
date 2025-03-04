using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrackingBle.Models.Dto.MstAccessControlDto;

namespace TrackingBle.Services
{
    public interface IMstAccessControlService
    {
        Task<MstAccessControlDto> GetByIdAsync(Guid id);
        Task<IEnumerable<MstAccessControlDto>> GetAllAsync();
        Task<MstAccessControlDto> CreateAsync(MstAccessControlCreateDto createDto);
        Task UpdateAsync(Guid id, MstAccessControlUpdateDto updateDto);
        Task DeleteAsync(Guid id);
    }
}