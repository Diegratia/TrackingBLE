using System;
// using TrackingBle.src._11MstDepartment.Models.Dto.MstApplicationDtos;

namespace TrackingBle.src._11MstDepartment.Models.Dto.MstDepartmentDtos
{
    public class MstDepartmentDto
    {
        public int Generate { get; set; }
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string DepartmentHost { get; set; }
        public Guid ApplicationId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int? Status { get; set; }
        // public MstApplicationDto Application { get; set; }
    }


    public class MstApplicationDto
    {
        public long Generate { get; set; }
        public Guid Id { get; set; }
        public string ApplicationName { get; set; }
        public string OrganizationType { get; set; }
        public string OrganizationAddress { get; set; }
        public string ApplicationType { get; set; }
        public DateTime ApplicationRegistered { get; set; }
        public DateTime ApplicationExpired { get; set; }
        public string HostName { get; set; }
        public string HostPhone { get; set; }
        public string HostEmail { get; set; }
        public string HostAddress { get; set; }
        public string ApplicationCustomName { get; set; }
        public string ApplicationCustomDomain { get; set; }
        public string ApplicationCustomPort { get; set; }
        public string LicenseCode { get; set; }
        public string LicenseType { get; set; }
        public int? ApplicationStatus { get; set; }
    }

    
}