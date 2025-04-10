using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrackingBle.Models.Dto.MstAccessCctvDtos;

namespace TrackingBle.src._5MstAccessCctv.Services
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