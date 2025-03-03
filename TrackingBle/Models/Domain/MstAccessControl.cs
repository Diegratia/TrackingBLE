using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;

namespace TrackingBle.Models.Domain
{


    public class MstAccessControl
    {
        [Key]
        [StringLength(32)]
        public string Id { get; set; }
        
        [Required]
        public long Generate { get; set; }
        
        [Required]
        [StringLength(255)]
        public string ControllerBrandId { get; set; }
        
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        
        [Required]
        [StringLength(255)]
        public string Type { get; set; }
        
        [Required]
        public string Description { get; set; }
        
        [Required]
        [StringLength(255)]
        public string Channel { get; set; }
        
        [Required]
        [StringLength(255)]
        public string DoorId { get; set; }
        
        [Required]
        public string Raw { get; set; }
        
        [Required]
        [StringLength(32)]
        public string IntegrationId { get; set; }
        
        [Required]
        [StringLength(32)]
        public string ApplicationId { get; set; }
        
        [Required]
        [StringLength(255)]
        public string CreatedBy { get; set; }
        
        [Required]
        public DateTime CreatedAt { get; set; }
        
        [Required]
        public DateTime UpdatedBy { get; set; }
        
        [Required]
        public long UpdatedAt { get; set; }
        
        [Required]
        public int Status { get; set; }

        [ForeignKey("ApplicationId")]
        public virtual MstApplication Application { get; set; }
        
        [ForeignKey("IntegrationId")]
        public virtual MstIntegration Integration { get; set; }
    }
}
