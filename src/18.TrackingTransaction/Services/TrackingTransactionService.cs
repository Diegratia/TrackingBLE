using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using TrackingBle.src._18TrackingTransaction.Data;
using TrackingBle.src._18TrackingTransaction.Models.Domain;
using TrackingBle.src._18TrackingTransaction.Models.Dto.TrackingTransactionDtos;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace TrackingBle.src._18TrackingTransaction.Services
{
    public class TrackingTransactionService : ITrackingTransactionService
    {
        private readonly TrackingTransactionDbContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;

        public TrackingTransactionService(
            TrackingTransactionDbContext context,
            IMapper mapper,
            IHttpClientFactory httpClientFactory,
            IHttpContextAccessor httpContextAccessor,
            IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
        }

        public async Task<IEnumerable<TrackingTransactionDto>> GetAllTrackingTransactionsAsync()
        {
            var transactions = await _context.TrackingTransactions.ToListAsync();
            var dtos = _mapper.Map<List<TrackingTransactionDto>>(transactions);

            foreach (var dto in dtos)
            {
                dto.Reader = await GetReaderAsync(dto.ReaderId);
                dto.FloorplanMaskedArea = await GetFloorplanMaskedAreaAsync(dto.FloorplanMaskedAreaId);
            }

            return dtos;
        }

        public async Task<TrackingTransactionDto> GetTrackingTransactionByIdAsync(Guid id)
        {
            var transaction = await _context.TrackingTransactions.FirstOrDefaultAsync(t => t.Id == id);
            if (transaction == null) return null;

            var dto = _mapper.Map<TrackingTransactionDto>(transaction);
            dto.Reader = await GetReaderAsync(dto.ReaderId);
            dto.FloorplanMaskedArea = await GetFloorplanMaskedAreaAsync(dto.FloorplanMaskedAreaId);

            return dto;
        }

        public async Task<TrackingTransactionDto> CreateTrackingTransactionAsync(TrackingTransactionCreateDto createDto)
        {
            if (createDto == null) throw new ArgumentNullException(nameof(createDto));

            await ValidateReaderAsync(createDto.ReaderId);
            await ValidateFloorplanMaskedAreaAsync(createDto.FloorplanMaskedAreaId);

            var transaction = _mapper.Map<TrackingTransaction>(createDto);
            transaction.Id = Guid.NewGuid();

            _context.TrackingTransactions.Add(transaction);
            await _context.SaveChangesAsync();

            var dto = _mapper.Map<TrackingTransactionDto>(transaction);
            dto.Reader = await GetReaderAsync(dto.ReaderId);
            dto.FloorplanMaskedArea = await GetFloorplanMaskedAreaAsync(dto.FloorplanMaskedAreaId);

            return dto;
        }

        public async Task UpdateTrackingTransactionAsync(Guid id, TrackingTransactionUpdateDto updateDto)
        {
            if (updateDto == null) throw new ArgumentNullException(nameof(updateDto));

            var transaction = await _context.TrackingTransactions.FindAsync(id);
            if (transaction == null)
            {
                throw new KeyNotFoundException($"TrackingTransaction with ID {id} not found.");
            }

            if (transaction.ReaderId != updateDto.ReaderId)
                await ValidateReaderAsync(updateDto.ReaderId);
            if (transaction.FloorplanMaskedAreaId != updateDto.FloorplanMaskedAreaId)
                await ValidateFloorplanMaskedAreaAsync(updateDto.FloorplanMaskedAreaId);

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

        private async Task<MstBleReaderDto> GetReaderAsync(Guid readerId)
        {
            var client = _httpClientFactory.CreateClient("MstBleReaderService");
            var response = await client.GetAsync($"/{readerId}");
            return response.IsSuccessStatusCode ? await response.Content.ReadFromJsonAsync<MstBleReaderDto>() : null;
        }

        private async Task<FloorplanMaskedAreaDto> GetFloorplanMaskedAreaAsync(Guid floorplanMaskedAreaId)
        {
            var client = _httpClientFactory.CreateClient("FloorplanMaskedAreaService");
            var response = await client.GetAsync($"/{floorplanMaskedAreaId}");
            return response.IsSuccessStatusCode ? await response.Content.ReadFromJsonAsync<FloorplanMaskedAreaDto>() : null;
        }

        private async Task ValidateReaderAsync(Guid readerId)
        {
            var client = _httpClientFactory.CreateClient("MstBleReaderService");
            var response = await client.GetAsync($"/{readerId}");
            if (!response.IsSuccessStatusCode)
            {
                throw new ArgumentException($"BLE Reader with ID {readerId} not found.");
            }
        }

        private async Task ValidateFloorplanMaskedAreaAsync(Guid floorplanMaskedAreaId)
        {
            var client = _httpClientFactory.CreateClient("FloorplanMaskedAreaService");
            var response = await client.GetAsync($"/{floorplanMaskedAreaId}");
            if (!response.IsSuccessStatusCode)
            {
                throw new ArgumentException($"FloorplanMaskedArea with ID {floorplanMaskedAreaId} not found.");
            }
        }
    }

    public class HttpClientAuthorizationDelegatingHandler : DelegatingHandler
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HttpClientAuthorizationDelegatingHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = _httpContextAccessor.HttpContext?.Request.Headers["Authorization"].ToString();
            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token.Replace("Bearer ", ""));
                Console.WriteLine($"Forwarding token to request: {token}");
            }
            else
            {
                Console.WriteLine("No Authorization token found in HttpContext.");
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}