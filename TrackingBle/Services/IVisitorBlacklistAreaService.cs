using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrackingBle.Models.Dto.VisitorBlacklistAreaDtos;

namespace TrackingBle.Services
{
    public interface IVisitorBlacklistAreaService
    {
        Task<VisitorBlacklistAreaDto> CreateVisitorBlacklistAreaAsync(VisitorBlacklistAreaCreateDto createDto);
        Task<VisitorBlacklistAreaDto> GetVisitorBlacklistAreaByIdAsync(Guid id);
        Task<IEnumerable<VisitorBlacklistAreaDto>> GetAllVisitorBlacklistAreasAsync();
        Task UpdateVisitorBlacklistAreaAsync(Guid id, VisitorBlacklistAreaUpdateDto updatedto);
        Task DeleteVisitorBlacklistAreaAsync(Guid id);
    }
}