using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrackingBle.Models.Dto.MstAccessCctvDto;

namespace TrackingBle.Services
{
    public interface IMstAccessCctvService
    {
        Task<MstAccessCctvDto> GetByIdAsync(Guid id);
        Task<IEnumerable<MstAccessCctvDto>> GetAllAsync();
        Task<MstAccessCctvDto> CreateAsync(MstAccessCctvCreateDto createDto);
        Task UpdateAsync(Guid id, MstAccessCctvUpdateDto updateDto);
        Task DeleteAsync(Guid id);
    }
}