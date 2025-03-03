using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrackingBle.Models.Domain
{
    public class MstBleReader
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Generate { get; set; }

        [Key]
        [StringLength(255)]
        public string Id { get; set; }

        [Required]
        [StringLength(255)]
        public string BrandId { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        [StringLength(255)]
        public string Mac { get; set; }

        [Required]
        [StringLength(255)]
        public string Ip { get; set; }

        [Required]
        public decimal LocationX { get; set; }

        [Required]
        public decimal LocationY { get; set; }

        [Required]
        public long LocationPxX { get; set; }

        [Required]
        public long LocationPxY { get; set; }

        [Required]
        [StringLength(255)]
        public string EngineReaderId { get; set; }

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

        [ForeignKey("BrandId")]
        public virtual MstBrand Brand { get; set; }

        public virtual ICollection<TrackingTransaction> TrackingTransactions { get; set; } = new List<TrackingTransaction>();
    }
}