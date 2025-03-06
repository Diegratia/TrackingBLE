using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrackingBle.Models.Domain
{
    public class VisitorBlacklistArea
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Generate { get; set; } 

        [Key]
        public Guid Id { get; set; } = Guid.NewGuid(); 

        [Required]
        [ForeignKey("Floorplan")]
        public Guid FloorplanId { get; set; }

        [Required]
        [ForeignKey("Visitor")]
        public Guid VisitorId { get; set; }

        public virtual FloorplanMaskedArea Floorplan { get; set; }
 
        public virtual Visitor Visitor { get; set; }
    }
}