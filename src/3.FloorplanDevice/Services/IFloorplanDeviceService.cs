using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrackingBle.src._3FloorplanDevice.Models.Dto.FloorplanDeviceDtos;

namespace TrackingBle.src._3FloorplanDevice.Services
{
    public interface IFloorplanDeviceService
    {
        Task<FloorplanDeviceDto> CreateAsync(FloorplanDeviceCreateDto dto);
        Task<FloorplanDeviceDto> GetByIdAsync(Guid id);
        Task<IEnumerable<FloorplanDeviceDto>> GetAllAsync();
        Task UpdateAsync( Guid Id, FloorplanDeviceUpdateDto dto);
        Task DeleteAsync(Guid id);
    }
}