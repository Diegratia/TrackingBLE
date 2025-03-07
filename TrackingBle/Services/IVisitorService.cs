using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrackingBle.Models.Dto.VisitorDto;

namespace TrackingBle.Services
{
    public interface IVisitorService
    {
        Task<VisitorDto> CreateVisitorAsync(VisitorCreateDto dto);
        Task<VisitorDto> GetVisitorByIdAsync(Guid id);
        Task<IEnumerable<VisitorDto>> GetAllVisitorsAsync();
        Task UpdateVisitorAsync(Guid id, VisitorUpdateDto dto);
        Task DeleteVisitorAsync(Guid id);
    }
}