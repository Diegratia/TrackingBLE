using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrackingBle.Models.Domain
{
    public class MstAccessControl
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Generate { get; set; }

        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [StringLength(255)]
        public string ControllerBrandId { get; set; } // Ubah ke Guid jika ada relasi

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
        public string DoorId { get; set; } // Ubah ke Guid jika ada relasi

        [Required]
        public string Raw { get; set; }

        [Required]
        [ForeignKey("Integration")]
        public Guid IntegrationId { get; set; }

        [Required]
        [ForeignKey("Application")]
        public Guid ApplicationId { get; set; }

        [Required]
        [StringLength(255)]
        public string CreatedBy { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public DateTime UpdatedBy { get; set; } // Sesuai database, tapi nama membingungkan

        [Required]
        public long UpdatedAt { get; set; }

        [Required]
        public int Status { get; set; }

        public virtual MstApplication Application { get; set; }

        public virtual MstIntegration Integration { get; set; }
    }
}