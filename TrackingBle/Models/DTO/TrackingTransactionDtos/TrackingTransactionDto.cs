using System;

namespace TrackingBle.Models.Dto.TrackingTransactionDtos
{

    public class TrackingTransactionDto
    {
        public Guid Id { get; set; }
        public DateTime TransTime { get; set; }
        public Guid ReaderId { get; set; }
        public long CardId { get; set; }
        public Guid FloorplanId { get; set; }
        public decimal CoordinateX { get; set; }
        public decimal CoordinateY { get; set; }
        public long CoordinatePxX { get; set; }
        public long CoordinatePxY { get; set; }
        public string AlarmStatus { get; set; } // Enum sebagai string
        public long Battery { get; set; }
    }
}