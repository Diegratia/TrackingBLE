using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrackingBle.src._17MstOrganization.Models.Dto.MstOrganizationDtos;

namespace TrackingBle.src._17MstOrganization.Services
{
    public interface IMstOrganizationService
    {
        Task<IEnumerable<MstOrganizationDto>> GetAllOrganizationsAsync();
        Task<MstOrganizationDto> GetOrganizationByIdAsync(Guid id);
        Task<MstOrganizationDto> CreateOrganizationAsync(MstOrganizationCreateDto dto);
        Task UpdateOrganizationAsync(Guid id, MstOrganizationUpdateDto dto);
        Task DeleteOrganizationAsync(Guid id);
    }
}