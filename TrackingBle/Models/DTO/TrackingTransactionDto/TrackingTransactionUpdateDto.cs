using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackingBle.Models.Dto.TrackingTransactionDto
{
        public class TrackingTransactionUpdateDto
    {
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