using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrackingBle.Models.Dto.MstDepartmentDto;

namespace TrackingBle.Services
{
    public interface IMstDepartmentService
    {
        Task<MstDepartmentDto> GetByIdAsync(Guid id);
        Task<IEnumerable<MstDepartmentDto>> GetAllAsync();
        Task<MstDepartmentDto> CreateAsync(MstDepartmentCreateDto createDto);
        Task UpdateAsync(Guid id, MstDepartmentUpdateDto updateDto);
        Task DeleteAsync(Guid id);
    }
}