using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrackingBle.src._12MstDistrict.Models.Dto.MstDistrictDtos;

namespace TrackingBle.src._12MstDistrict.Services
{
    public interface IMstDistrictService
    {
        Task<MstDistrictDto> GetByIdAsync(Guid id);
        Task<IEnumerable<MstDistrictDto>> GetAllAsync();
        Task<MstDistrictDto> CreateAsync(MstDistrictCreateDto createDto);
        Task UpdateAsync(Guid id, MstDistrictUpdateDto updateDto);
        Task DeleteAsync(Guid id);
    }
}