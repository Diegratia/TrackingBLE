using System;

namespace TrackingBle.src._8MstBleReader.Models.Dto.MstBleReaderDtos
{
    public class MstBleReaderCreateDto
    {
        public Guid BrandId { get; set; }
        public string Name { get; set; }
        public string Mac { get; set; }
        public string Ip { get; set; }
        public decimal LocationX { get; set; }
        public decimal LocationY { get; set; }
        public long LocationPxX { get; set; }
        public long LocationPxY { get; set; }
        public string EngineReaderId { get; set; }
    }
}