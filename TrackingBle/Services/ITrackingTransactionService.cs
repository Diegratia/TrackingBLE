using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrackingBle.Models.Dto.TrackingTransactionDto;

namespace TrackingBle.Services
{
    public interface ITrackingTransactionService
    {
        Task<IEnumerable<TrackingTransactionDto>> GetAllTrackingTransactionsAsync();
        Task<TrackingTransactionDto> CreateTrackingTransactionAsync(TrackingTransactionCreateDto dto);
        Task<TrackingTransactionDto> GetTrackingTransactionByIdAsync(Guid id);
        Task UpdateTrackingTransactionAsync(Guid id, TrackingTransactionUpdateDto dto);
        Task DeleteTrackingTransactionAsync(Guid id);
    }
}