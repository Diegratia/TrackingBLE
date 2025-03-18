using System;

namespace TrackingBle.src._17MstOrganization.Models.Dto.MstOrganizationDtos
{
    public class MstOrganizationCreateDto
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string OrganizationHost { get; set; }
        public Guid ApplicationId { get; set; }
    }
  
}