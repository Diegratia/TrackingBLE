using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrackingBle.src._7MstApplication.Models.Dto.MstApplicationDtos;

namespace TrackingBle.src._7MstApplication.Services
{
    public interface IMstApplicationService
    {
        Task<IEnumerable<MstApplicationDto>> GetAllApplicationsAsync();
        Task<MstApplicationDto> GetApplicationByIdAsync(Guid id);
        Task<MstApplicationDto> CreateApplicationAsync(MstApplicationCreateDto dto);
        Task UpdateApplicationAsync(Guid id, MstApplicationUpdateDto dto);
        Task DeleteApplicationAsync(Guid id);
    }
}