using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrackingBle.src._15MstIntegration.Models.Dto.MstIntegrationDtos;

namespace TrackingBle.src._15MstIntegration.Services
{
    public interface IMstIntegrationService
    {
        Task<MstIntegrationDto> GetByIdAsync(Guid id);
        Task<IEnumerable<MstIntegrationDto>> GetAllAsync();
        Task<MstIntegrationDto> CreateAsync(MstIntegrationCreateDto createDto);
        Task UpdateAsync(Guid id, MstIntegrationUpdateDto updateDto);
        Task DeleteAsync(Guid id);
    }
}