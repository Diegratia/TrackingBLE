using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace TrackingBle.Models.Domain
{
    public class MstMember
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Generate { get; set; }

        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [StringLength(255)]
        public string PersonId { get; set; }

        [Required]
        [ForeignKey("Organization")]
        public Guid OrganizationId { get; set; }

        [Required]
        [ForeignKey("Department")]
        public Guid DepartmentId { get; set; }

        [Required]
        [ForeignKey("District")]
        public Guid DistrictId { get; set; }

        [Required]
        [StringLength(255)]
        public string IdentityId { get; set; }

        [Required]
        [StringLength(255)]
        public string CardNumber { get; set; }

        [Required]
        [StringLength(255)]
        public string BleCardNumber { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        [StringLength(255)]
        public string Phone { get; set; }

        [Required]
        [StringLength(255)]
        public string Email { get; set; }

        [Required]
        public Gender Gender { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string FaceImage { get; set; }

        [Required]
        public int UploadFr { get; set; } = 0; 

        [Required]
        public string UploadFrError { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        [Required]
        public DateTime JoinDate { get; set; }

        [Required]
        public DateTime ExitDate { get; set; }

        [Required]
        [StringLength(255)]
        public string HeadMember1 { get; set; }

        [Required]
        [StringLength(255)]
        public string HeadMember2 { get; set; }

        [Required]
        [ForeignKey("Application")]
        public Guid ApplicationId { get; set; }

        [Required]
        public StatusEmployee StatusEmployee { get; set; }

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
        public int? Status { get; set; } = 1;

        public virtual MstApplication Application { get; set; }

        public virtual MstOrganization Organization { get; set; }

        public virtual MstDepartment Department { get; set; }

        public virtual MstDistrict District { get; set; }
    }
}