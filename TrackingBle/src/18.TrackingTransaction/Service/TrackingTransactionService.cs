using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrackingBle.Data;
using TrackingBle.Models.Domain;
using TrackingBle.Models.Dto.TrackingTransactionDtos;

namespace TrackingBle.src._18TrackingTransaction.Service
{
    public class TrackingTransactionService : ITrackingTransactionService
    {
        private readonly TrackingBleDbContext _context;
        private readonly IMapper _mapper;

        public TrackingTransactionService(TrackingBleDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TrackingTransactionDto> CreateTrackingTransactionAsync(TrackingTransactionCreateDto createDto)
        {
            if (createDto == null) throw new ArgumentNullException(nameof(createDto));

            var transaction = _mapper.Map<TrackingTransaction>(createDto);
            transaction.Id = Guid.NewGuid();

            _context.TrackingTransactions.Add(transaction);
            await _context.SaveChangesAsync();

            return _mapper.Map<TrackingTransactionDto>(transaction);
        }

        public async Task<TrackingTransactionDto> GetTrackingTransactionByIdAsync(Guid id)
        {
            var transaction = await _context.TrackingTransactions
                .Include(t => t.Reader)
                .Include(t => t.FloorplanMaskedArea)
                .FirstOrDefaultAsync(t => t.Id == id);
            return _mapper.Map<TrackingTransactionDto>(transaction);
        }

        public async Task<IEnumerable<TrackingTransactionDto>> GetAllTrackingTransactionsAsync()
        {
            var transactions = await _context.TrackingTransactions
                .Include(t => t.Reader)
                .Include(t => t.FloorplanMaskedArea)
                .ToListAsync();
            return _mapper.Map<IEnumerable<TrackingTransactionDto>>(transactions);
        }

        public async Task UpdateTrackingTransactionAsync(Guid id, TrackingTransactionUpdateDto updateDto)
        {
            if (updateDto == null) throw new ArgumentNullException(nameof(updateDto));

            var transaction = await _context.TrackingTransactions.FindAsync(id);
            if (transaction == null)
            {
                throw new KeyNotFoundException($"TrackingTransaction with ID {id} not found.");
            }

            _mapper.Map(updateDto, transaction);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTrackingTransactionAsync(Guid id)
        {
            var transaction = await _context.TrackingTransactions.FindAsync(id);
            if (transaction == null)
            {
                throw new KeyNotFoundException($"TrackingTransaction with ID {id} not found.");
            }

            _context.TrackingTransactions.Remove(transaction);
            await _context.SaveChangesAsync();
        }
    }
}