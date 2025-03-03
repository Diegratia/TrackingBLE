using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackingBle.Models.Domain
{
    public class MstApplication
    {
        [Key]
        [StringLength(32)] // Explicitly set length
        public string Id { get; set; }
        
        [Required]
        public long Generate { get; set; }
        
        [Required]
        [StringLength(255)]
        public string ApplicationName { get; set; }
        
        [Required]
        public string OrganizationType { get; set; }
        
        [Required]
        public string OrganizationAddress { get; set; }
        
        [Required]
        public string ApplicationType { get; set; }
        
        [Required]
        public DateTime ApplicationRegistered { get; set; }
        
        [Required]
        public DateTime ApplicationExpired { get; set; }
        
        [Required]
        [StringLength(255)]
        public string HostName { get; set; }
        
        [Required]
        [StringLength(255)]
        public string HostPhone { get; set; }
        
        [Required]
        [StringLength(255)]
        public string HostEmail { get; set; }
        
        [Required]
        [StringLength(255)]
        public string HostAddress { get; set; }
        
        [Required]
        [StringLength(255)]
        public string ApplicationCustomName { get; set; }
        
        [Required]
        [StringLength(255)]
        public string ApplicationCustomDomain { get; set; }
        
        [Required]
        [StringLength(255)]
        public string ApplicationCustomPort { get; set; }
        
        [Required]
        [StringLength(255)]
        public string LicenseCode { get; set; }
        
        [Required]
        public string LicenseType { get; set; }
        
        [Required]
        public int ApplicationStatus { get; set; }

        public virtual ICollection<MstIntegration> Integrations { get; set; } = new List<MstIntegration>();

    
    }
}
