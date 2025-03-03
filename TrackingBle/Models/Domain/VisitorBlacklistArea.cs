using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrackingBle.Models.Domain
{
    public class VisitorBlacklistArea
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Generate { get; set; }

        [Key]
        [StringLength(255)]
        public string Id { get; set; }

        [Required]
        [StringLength(255)]
        public string AreaId { get; set; }

        [Required]
        [StringLength(255)]
        public string VisitorId { get; set; }

        [ForeignKey("AreaId")]
        public virtual MstArea Area { get; set; }

        [ForeignKey("VisitorId")]
        public virtual Visitor Visitor { get; set; }
    }
}