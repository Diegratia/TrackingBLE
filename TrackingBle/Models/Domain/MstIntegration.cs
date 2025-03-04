using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackingBle.Models.Domain
{

    public class MstIntegration
    {
         [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Generate { get; set; } 

        [Key]
        public Guid Id { get; set; } = Guid.NewGuid(); 

        [Required]
        [StringLength(255)]
        public string BrandId { get; set; }

        [Required]
        public string IntegrationType { get; set; }

        [Required]
        public string ApiTypeAuth { get; set; }

        [Required]
        public string ApiUrl { get; set; }

        [Required]
        [StringLength(255)]
        public string ApiAuthUsername { get; set; }

        [Required]
        [StringLength(255)]
        public string ApiAuthPasswd { get; set; }

        [Required]
        [StringLength(255)]
        public string ApiKeyField { get; set; }

        [Required]
        [StringLength(255)]
        public string ApiKeyValue { get; set; }

        [Required]
        [ForeignKey("ApplicationId")]  
        public Guid ApplicationId { get; set; }

        [Required]
        [StringLength(255)]
        public string CreatedBy { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public long UpdatedBy { get; set; }

        [Required]
        public long UpdatedAt { get; set; }

        [Required]
        public int Status { get; set; }

        public virtual MstApplication Application { get; set; }
    }

}
