using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrackingBle.Models.Domain
{
    public class TrackingTransaction
    {
        [Key]
        [StringLength(255)]
        public string Id { get; set; }

        [Required]
        public DateTime TransTime { get; set; }

        [Required]
        [StringLength(255)]
        public string ReaderId { get; set; }

        [Required]
        public long CardId { get; set; }

        [Required]
        [StringLength(255)]
        public string AreaId { get; set; }

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

        [ForeignKey("ReaderId")]
        public virtual MstBleReader Reader { get; set; }

        [ForeignKey("AreaId")]
        public virtual MstArea Area { get; set; }
    }
}