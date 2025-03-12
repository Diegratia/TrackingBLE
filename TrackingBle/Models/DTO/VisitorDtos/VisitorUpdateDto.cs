using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackingBle.Models.Dto.VisitorDtos
{
    public class VisitorUpdateDto
    {
        public string PersonId { get; set; }
        public string IdentityId { get; set; }
        public string CardNumber { get; set; }
        public string BleCardNumber { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }        
        public IFormFile FaceImage { get; set; }
        public Guid ApplicationId { get; set; }
        public long PortalKey { get; set; }
        public string CheckinBy { get; set; } = "";
        public string CheckoutBy { get; set; } = "";
        public string DenyBy { get; set; } = "";
        public string BlockBy { get; set; } = "";
        public string UnblockBy { get; set; } = "";
        public string ReasonDeny { get; set; } = "";
        public string ReasonBlock { get; set; } = "";
        public string ReasonUnblock { get; set; } = "";
        public string Status { get; set; }

    }
}