using System;

namespace TrackingBle.Models.Dto.MstApplicationDto
{
    public class MstApplicationUpdateDto
    {
        public string ApplicationName { get; set; }
        public OrganizationType OrganizationType { get; set; }
        public string OrganizationAddress { get; set; }
        public ApplicationType ApplicationType { get; set; }
        // public DateTime ApplicationRegistered { get; set; }
        // public DateTime ApplicationExpired { get; set; }
        public string HostName { get; set; }
        public string HostPhone { get; set; }
        public string HostEmail { get; set; }
        public string HostAddress { get; set; }
        public string ApplicationCustomName { get; set; }
        public string ApplicationCustomDomain { get; set; }
        public string ApplicationCustomPort { get; set; }
        public string LicenseCode { get; set; }
        public LicenseType LicenseType { get; set; }
    }
}