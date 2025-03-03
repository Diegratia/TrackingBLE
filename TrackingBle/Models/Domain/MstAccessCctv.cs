using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrackingBle.Models.Domain
{
    public class MstAccessCctv
    {
        [Key]
        [StringLength(32)]
        public string Id { get; set; }
        
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Generate { get; set; }
        
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        
        [Required]
        public string Rtsp { get; set; }
        
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
        [StringLength(32)]
        public string IntegrationId { get; set; }
        
        [Required]
        [StringLength(32)]
        public string ApplicationId { get; set; }
        
        [Required]
        public int Status { get; set; }

        [ForeignKey("IntegrationId")]
        public virtual MstIntegration Integration { get; set; }
        
        [ForeignKey("ApplicationId")]
        public virtual MstApplication Application { get; set; }
    }
}
