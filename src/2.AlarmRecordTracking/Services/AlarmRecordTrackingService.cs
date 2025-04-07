using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using TrackingBle.src._2AlarmRecordTracking.Data;
using TrackingBle.src._2AlarmRecordTracking.Models.Domain;
using TrackingBle.src._2AlarmRecordTracking.Models.Dto.AlarmRecordTrackingDtos;
using TrackingBle.src.Common.Models;
using Microsoft.Extensions.Configuration;

namespace TrackingBle.src._2AlarmRecordTracking.Services
{
    public class AlarmRecordTrackingService : IAlarmRecordTrackingService
    {
        private readonly AlarmRecordTrackingDbContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public AlarmRecordTrackingService(
            AlarmRecordTrackingDbContext context,
            IMapper mapper,
            IHttpClientFactory httpClientFactory,
            IConfiguration configuration)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public async Task<AlarmRecordTrackingDto> GetByIdAsync(Guid id)
        {
            var alarm = await _context.AlarmRecordTrackings
                .FirstOrDefaultAsync(a => a.Id == id);
            if (alarm == null)
            {
                Console.WriteLine($"AlarmRecordTracking with ID {id} not found.");
                return null;
            }

            var dto = _mapper.Map<AlarmRecordTrackingDto>(alarm);
            await PopulateRelationsAsync(dto);
            return dto;
        }

        public async Task<IEnumerable<AlarmRecordTrackingDto>> GetAllAsync()
        {
            var alarms = await _context.AlarmRecordTrackings
                .ToListAsync();

            if (!alarms.Any())
            {
                Console.WriteLine("No AlarmRecordTrackings found in database.");
                return new List<AlarmRecordTrackingDto>();
            }

            Console.WriteLine($"Found {alarms.Count} AlarmRecordTrackings in database.");
            var dtos = _mapper.Map<List<AlarmRecordTrackingDto>>(alarms);

            foreach (var dto in dtos)
            {
                await PopulateRelationsAsync(dto);
            }

            return dtos;
        }

        public async Task<AlarmRecordTrackingDto> CreateAsync(AlarmRecordTrackingCreateDto createDto)
        {
            if (createDto == null) throw new ArgumentNullException(nameof(createDto));

            // Validasi relasi dengan HttpClient
            await ValidateForeignKeys(createDto.VisitorId, createDto.ReaderId, createDto.FloorplanMaskedAreaId, createDto.ApplicationId);

            var alarm = _mapper.Map<AlarmRecordTracking>(createDto);
            alarm.Id = Guid.NewGuid();
            alarm.Timestamp = DateTime.UtcNow;
            alarm.IdleTimestamp = DateTime.UtcNow;
            alarm.DoneTimestamp = DateTime.MaxValue;
            alarm.CancelTimestamp = DateTime.MaxValue;
            alarm.WaitingTimestamp = DateTime.MaxValue;
            alarm.InvestigatedTimestamp = DateTime.MaxValue;
            alarm.IdleBy = "System";
            alarm.DoneBy = "System";
            alarm.CancelBy = "System";
            alarm.WaitingBy = "System";
            alarm.InvestigatedBy = "System";
            alarm.InvestigatedDoneAt = DateTime.MaxValue;

            _context.AlarmRecordTrackings.Add(alarm);
            await _context.SaveChangesAsync();

            var resultDto = _mapper.Map<AlarmRecordTrackingDto>(alarm);
            await PopulateRelationsAsync(resultDto);
            return resultDto;
        }

        public async Task UpdateAsync(Guid id, AlarmRecordTrackingUpdateDto updateDto)
        {
            if (updateDto == null) throw new ArgumentNullException(nameof(updateDto));

            var alarm = await _context.AlarmRecordTrackings.FindAsync(id);
            if (alarm == null)
                throw new KeyNotFoundException($"AlarmRecordTracking with ID {id} not found.");

            await ValidateForeignKeysIfChanged(alarm, updateDto);

            _mapper.Map(updateDto, alarm);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var alarm = await _context.AlarmRecordTrackings.FindAsync(id);
            if (alarm == null)
                throw new KeyNotFoundException($"AlarmRecordTracking with ID {id} not found.");

            _context.AlarmRecordTrackings.Remove(alarm);
            await _context.SaveChangesAsync();
        }

        private async Task PopulateRelationsAsync(AlarmRecordTrackingDto dto)
        {
            dto.Visitor = await GetVisitorAsync(dto.VisitorId);
            dto.Reader = await GetReaderAsync(dto.ReaderId);
            dto.FloorplanMaskedArea = await GetFloorplanMaskedAreaAsync(dto.FloorplanMaskedAreaId);
        }

        private async Task<VisitorDto> GetVisitorAsync(Guid visitorId)
        {
            var client = _httpClientFactory.CreateClient("VisitorService");
            var response = await client.GetAsync($"/{visitorId}");
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Failed to get Visitor with ID {visitorId}. Status: {response.StatusCode}");
                return null;
            }

            var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<VisitorDto>>();
            return apiResponse?.Collection?.Data;
        }

        private async Task<MstBleReaderDto> GetReaderAsync(Guid readerId)
        {
            var client = _httpClientFactory.CreateClient("MstBleReaderService");
            var response = await client.GetAsync($"/{readerId}");
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Failed to get Reader with ID {readerId}. Status: {response.StatusCode}");
                return null;
            }

            var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<MstBleReaderDto>>();
            return apiResponse?.Collection?.Data;
        }

        private async Task<FloorplanMaskedAreaDto> GetFloorplanMaskedAreaAsync(Guid floorplanMaskedAreaId)
        {
            var client = _httpClientFactory.CreateClient("FloorplanMaskedAreaService");
            var response = await client.GetAsync($"/{floorplanMaskedAreaId}");
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Failed to get FloorplanMaskedArea with ID {floorplanMaskedAreaId}. Status: {response.StatusCode}");
                return null;
            }

            var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<FloorplanMaskedAreaDto>>();
            return apiResponse?.Collection?.Data;
        }

        private async Task ValidateForeignKeys(Guid visitorId, Guid readerId, Guid floorplanMaskedAreaId, Guid applicationId)
        {
            var visitorClient = _httpClientFactory.CreateClient("VisitorService");
            var visitorResponse = await visitorClient.GetAsync($"/{visitorId}");
            if (!visitorResponse.IsSuccessStatusCode)
                throw new ArgumentException($"Visitor with ID {visitorId} not found. Status: {visitorResponse.StatusCode}");

            var readerClient = _httpClientFactory.CreateClient("MstBleReaderService");
            var readerResponse = await readerClient.GetAsync($"/{readerId}");
            if (!readerResponse.IsSuccessStatusCode)
                throw new ArgumentException($"Reader with ID {readerId} not found. Status: {readerResponse.StatusCode}");

            var maskedAreaClient = _httpClientFactory.CreateClient("FloorplanMaskedAreaService");
            var maskedAreaResponse = await maskedAreaClient.GetAsync($"/{floorplanMaskedAreaId}");
            if (!maskedAreaResponse.IsSuccessStatusCode)
                throw new ArgumentException($"FloorplanMaskedArea with ID {floorplanMaskedAreaId} not found. Status: {maskedAreaResponse.StatusCode}");

            var appClient = _httpClientFactory.CreateClient("MstApplicationService");
            var appResponse = await appClient.GetAsync($"/{applicationId}");
            if (!appResponse.IsSuccessStatusCode)
                throw new ArgumentException($"Application with ID {applicationId} not found. Status: {appResponse.StatusCode}");
        }

        private async Task ValidateForeignKeysIfChanged(AlarmRecordTracking alarm, AlarmRecordTrackingUpdateDto updateDto)
        {
            if (alarm.VisitorId != updateDto.VisitorId)
            {
                var client = _httpClientFactory.CreateClient("VisitorService");
                var response = await client.GetAsync($"/{updateDto.VisitorId}");
                if (!response.IsSuccessStatusCode)
                    throw new ArgumentException($"Visitor with ID {updateDto.VisitorId} not found. Status: {response.StatusCode}");
            }

            if (alarm.ReaderId != updateDto.ReaderId)
            {
                var client = _httpClientFactory.CreateClient("MstBleReaderService");
                var response = await client.GetAsync($"/{updateDto.ReaderId}");
                if (!response.IsSuccessStatusCode)
                    throw new ArgumentException($"Reader with ID {updateDto.ReaderId} not found. Status: {response.StatusCode}");
            }

            if (alarm.FloorplanMaskedAreaId != updateDto.FloorplanMaskedAreaId)
            {
                var client = _httpClientFactory.CreateClient("FloorplanMaskedAreaService");
                var response = await client.GetAsync($"/{updateDto.FloorplanMaskedAreaId}");
                if (!response.IsSuccessStatusCode)
                    throw new ArgumentException($"FloorplanMaskedArea with ID {updateDto.FloorplanMaskedAreaId} not found. Status: {response.StatusCode}");
            }

            if (alarm.ApplicationId != updateDto.ApplicationId)
            {
                var client = _httpClientFactory.CreateClient("MstApplicationService");
                var response = await client.GetAsync($"/{updateDto.ApplicationId}");
                if (!response.IsSuccessStatusCode)
                    throw new ArgumentException($"Application with ID {updateDto.ApplicationId} not found. Status: {response.StatusCode}");
            }
        }
    }
}