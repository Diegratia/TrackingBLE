using System;

namespace TrackingBle.Models.Dto.MstOrganizationDto
{
    public class MstOrganizationCreateDto
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string OrganizationHost { get; set; }
        public Guid ApplicationId { get; set; }
    }
  
}