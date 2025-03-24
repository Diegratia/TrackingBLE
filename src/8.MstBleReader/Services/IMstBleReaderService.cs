using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrackingBle.src._8MstBleReader.Models.Dto.MstBleReaderDtos;

namespace TrackingBle.src._8MstBleReader.Services
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