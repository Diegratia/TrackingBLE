using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackingBle.src._17MstOrganization.Models.Dto.MstOrganizationDtos
{
    public class MstOrganizationUpdateDto
    {

        public string Code { get; set; }
        public string Name { get; set; }
        public string OrganizationHost { get; set; }
        public Guid ApplicationId { get; set; }
    
    }
}

