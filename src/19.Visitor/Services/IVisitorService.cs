using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrackingBle.src._19Visitor.Models.Dto.VisitorDtos;

namespace TrackingBle.src._19Visitor.Services
{
    public interface IVisitorService
    {
        Task<IEnumerable<VisitorDto>> GetAllVisitorsAsync();
        Task<VisitorDto> GetVisitorByIdAsync(Guid id);
        Task<VisitorDto> CreateVisitorAsync(VisitorCreateDto dto);
        Task<VisitorDto> UpdateVisitorAsync(Guid id, VisitorUpdateDto dto);
        Task DeleteVisitorAsync(Guid id);
    }
}