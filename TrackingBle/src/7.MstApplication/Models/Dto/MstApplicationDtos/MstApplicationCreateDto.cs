using System;

namespace TrackingBle.src._7MstApplication.Models.Dto.MstApplicationDtos
{
    public class MstApplicationCreateDto
    {
        public string ApplicationName { get; set; }
        public string OrganizationType { get; set; }
        public string OrganizationAddress { get; set; }
        public string ApplicationType { get; set; }
        // public DateTime ApplicationRegistered { get; set; }
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
    }
}