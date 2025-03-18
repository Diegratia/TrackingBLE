using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrackingBle.Models.Dto.AlarmRecordTrackingDtos;

namespace TrackingBle.src._2AlarmRecordTracking.Services
{
    public interface IAlarmRecordTrackingService
    {
        Task<AlarmRecordTrackingDto> GetByIdAsync(Guid id);
        Task<IEnumerable<AlarmRecordTrackingDto>> GetAllAsync();
        Task<AlarmRecordTrackingDto> CreateAsync(AlarmRecordTrackingCreateDto createDto);
        Task UpdateAsync(Guid id, AlarmRecordTrackingUpdateDto updateDto);
        Task DeleteAsync(Guid id);
    }
}