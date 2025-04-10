using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrackingBle.Models.Dto.MstOrganizationDtos;

namespace TrackingBle.src._17MstOrganization.Service
{
    public interface IMstOrganizationService
    {
        Task<IEnumerable<MstOrganizationDto>> GetAllOrganizationsAsync();
        Task<MstOrganizationDto> GetOrganizationByIdAsync(Guid id);
        Task<MstOrganizationDto> CreateOrganizationAsync(MstOrganizationCreateDto createDto);
        Task UpdateOrganizationAsync(Guid id, MstOrganizationUpdateDto updateDto);
        Task DeleteOrganizationAsync(Guid id);
    }
}