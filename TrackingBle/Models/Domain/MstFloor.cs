using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrackingBle.Models.Domain
{
    public class MstFloor
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Generate { get; set; } 

        [Key]
        public Guid Id { get; set; } = Guid.NewGuid(); 

      
        [StringLength(255)]
        public string BuildingId { get; set; }

      
        public string Name { get; set; } 


        public string FloorImage { get; set; }

   
        public long PixelX { get; set; }

    
        public long PixelY { get; set; }


        public long FloorX { get; set; }


        public long FloorY { get; set; }


        public decimal MeterPerPx { get; set; }

    
        public long EngineFloorId { get; set; }

   
        [StringLength(255)]
        public string CreatedBy { get; set; }

    
        public DateTime CreatedAt { get; set; }

     
        [StringLength(255)]
        public string UpdatedBy { get; set; }
     
        public DateTime UpdatedAt { get; set; }
    
        public int? Status { get; set; } = 1;

        public virtual ICollection<FloorplanMaskedArea> FloorplanMaskedArea { get; set; } = new List<FloorplanMaskedArea>();
    }
}