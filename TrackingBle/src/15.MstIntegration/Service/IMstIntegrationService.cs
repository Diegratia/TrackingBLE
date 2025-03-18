using TrackingBle.Models.Dto.MstIntegrationDtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TrackingBle.src._15MstIntegration.Service
{
    public interface IMstIntegrationService
    {
        Task<MstIntegrationDto> GetByIdAsync(Guid id);
        Task<IEnumerable<MstIntegrationDto>> GetAllAsync();
        Task<MstIntegrationDto> CreateAsync(MstIntegrationCreateDto createDto);
        Task UpdateAsync(Guid id, MstIntegrationUpdateDto updateDto); // Returns Task
        Task DeleteAsync(Guid id); // Returns Task
    }
}