using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrackingBle.src._11MstDepartment.Models.Dto.MstDepartmentDtos;

namespace TrackingBle.src._11MstDepartment.Services
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