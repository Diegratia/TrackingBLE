using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrackingBle.Models.Dto.VisitorDtos;

namespace TrackingBle.src._19Visitor.Service
{
    public interface IVisitorService
    {
        Task<VisitorDto> CreateVisitorAsync(VisitorCreateDto createDto);
        Task<VisitorDto> GetVisitorByIdAsync(Guid id);
        Task<IEnumerable<VisitorDto>> GetAllVisitorsAsync();
        Task<VisitorDto> UpdateVisitorAsync(Guid id, VisitorUpdateDto updateDto);
        Task DeleteVisitorAsync(Guid id);
    }
}