using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace TrackingBle.Models.Domain
{
    public class MstBrand
    {
        [Key]
        public string Id { get; set; }
        
        [Required]
        public int Generate { get; set; }
        
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        
        [Required]
        [StringLength(255)]
        public string Tag { get; set; }
        
        [Required]
        public int Status { get; set; }
    }
}
