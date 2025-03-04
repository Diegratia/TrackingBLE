using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrackingBle.Models.Dto;

namespace TrackingBle.Services
{
    public interface IMstIntegrationService
    {
        Task<IEnumerable<MstIntegrationDto>> GetAllAsync();
        Task<MstIntegrationDto> GetByIdAsync(Guid id);
        Task<MstIntegrationDto> CreateAsync(MstIntegrationDto dto);
        Task UpdateAsync(Guid id, MstIntegrationDto dto);
        Task DeleteAsync(Guid id);
    }
}