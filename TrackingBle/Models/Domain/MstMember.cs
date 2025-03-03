using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrackingBle.Models.Domain
{
    public class MstMember
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Generate { get; set; }

        [Key]
        [StringLength(255)]
        public string Id { get; set; }

        [Required]
        [StringLength(255)]
        public string PersonId { get; set; }

        [Required]
        [StringLength(255)]
        public string OrganizationId { get; set; }

        [Required]
        [StringLength(255)]
        public string DepartmentId { get; set; }

        [Required]
        [StringLength(255)]
        public string DistrictId { get; set; }

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
        public GenderEnum Gender { get; set; }

        public enum GenderEnum
        {
            Male,
            Female
        }
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
        [StringLength(255)]
        public string ApplicationId { get; set; }

        [Required]
        public StatusEmployeeType StatusEmployee { get; set; }

        public enum StatusEmployeeType {
            Contract,
            FullTime,
            Intern
        }

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

        [ForeignKey("ApplicationId")]
        public virtual MstApplication Application { get; set; }

        [ForeignKey("OrganizationId")]
        public virtual MstOrganization Organization { get; set; }

        [ForeignKey("DepartmentId")]
        public virtual MstDepartment Department { get; set; }

        [ForeignKey("DistrictId")]
        public virtual MstDistrict District { get; set; }
    }
}