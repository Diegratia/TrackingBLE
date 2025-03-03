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
        [Key]
        [StringLength(32)]
        public string Id { get; set; }

        [Required]
        public int Generate { get; set; }

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
        [StringLength(32)]
        public string ApplicationId { get; set; }

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

        [ForeignKey("ApplicationId")]
        public virtual MstApplication Application { get; set; }
    }

}
