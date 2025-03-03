using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrackingBle.Models.Domain
{
    public class MstFloor
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Generate { get; set; }

        [Key]
        [StringLength(255)]
        public string Id { get; set; }

        [Required]
        [StringLength(255)]
        public string BuildingId { get; set; }

        [Required]
        public long Name { get; set; } // BIGINT in SQL, might want string if it's not numeric

        [Required]
        public string FloorImage { get; set; }

        [Required]
        public long PixelX { get; set; }

        [Required]
        public long PixelY { get; set; }

        [Required]
        public long FloorX { get; set; }

        [Required]
        public long FloorY { get; set; }

        [Required]
        public decimal MeterPerPx { get; set; }

        [Required]
        public long EngineFloorId { get; set; }

        [Required]
        [StringLength(255)]
        public string CreatedBy { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        [StringLength(255)]
        public string UpdatedBy { get; set; }

        [Required]
        public DateTime UpdatedAt { get; set; }

        [Required]
        public int Status { get; set; }

        public virtual ICollection<MstArea> Areas { get; set; } = new List<MstArea>();
    }
}