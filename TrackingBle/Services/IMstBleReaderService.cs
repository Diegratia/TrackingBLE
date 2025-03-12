using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrackingBle.Models.Dto.MstBleReaderDtos;

namespace TrackingBle.Services
{
    public interface IMstBleReaderService
    {
        Task<MstBleReaderDto> GetByIdAsync(Guid id);
        Task<IEnumerable<MstBleReaderDto>> GetAllAsync();
        Task<MstBleReaderDto> CreateAsync(MstBleReaderCreateDto createDto);
        Task UpdateAsync(Guid id, MstBleReaderUpdateDto updateDto);
        Task DeleteAsync(Guid id);
    }
}