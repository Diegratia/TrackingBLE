using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrackingBle.Models.Dto.MstBrandDtos;

namespace TrackingBle.Services
{
    public interface IMstBrandService
    {
        Task<MstBrandDto> GetByIdAsync(Guid id);
        Task<IEnumerable<MstBrandDto>> GetAllAsync();
        Task<MstBrandDto> CreateAsync(MstBrandCreateDto createDto);
        Task UpdateAsync(Guid id, MstBrandUpdateDto updateDto);
        Task DeleteAsync(Guid id);
    }
}