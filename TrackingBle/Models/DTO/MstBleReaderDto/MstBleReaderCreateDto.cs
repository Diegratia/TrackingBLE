using System;

namespace TrackingBle.Models.Dto.MstBleReaderDto
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
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int Status { get; set; }
    }
}