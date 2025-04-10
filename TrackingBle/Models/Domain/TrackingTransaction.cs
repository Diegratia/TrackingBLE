using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrackingBle.Models.Domain
{
    public class TrackingTransaction
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public DateTime TransTime { get; set; }

        [Required]
        [ForeignKey("Reader")]
        public Guid ReaderId { get; set; } // rear_reader, reader terdekat

        [Required]
        public long CardId { get; set; }

        [Required]
        [ForeignKey("FloorplanMaskedArea")]
        public Guid FloorplanMaskedAreaId { get; set; }

        [Required]
        public decimal CoordinateX { get; set; }

        [Required]
        public decimal CoordinateY { get; set; }

        [Required]
        public long CoordinatePxX { get; set; }

        [Required]
        public long CoordinatePxY { get; set; }

        [Required]
        public AlarmStatus AlarmStatus { get; set; }

        [Required]
        public long Battery { get; set; }

        public virtual MstBleReader Reader { get; set; }

        public virtual FloorplanMaskedArea FloorplanMaskedArea { get; set; }
    }
}