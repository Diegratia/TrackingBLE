using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrackingBle.Models.Dto.TrackingTransactionDtos;

namespace TrackingBle.src._18TrackingTransaction.Service
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