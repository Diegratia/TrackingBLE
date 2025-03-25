using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrackingBle.src._20VisitorBlacklistArea.Models.Domain;

namespace TrackingBle.src._20VisitorBlacklistArea.Models.Dto.VisitorBlacklistAreaDtos
{
    public class VisitorBlacklistAreaDto
    {
        public long Generate { get; set; } 

        public Guid Id { get; set; }

        public Guid FloorplanMaskedAreaId { get; set; }

        public Guid VisitorId { get; set; }

        public FloorplanMaskedAreaDto FloorplanMaskedArea { get; set; }
        public VisitorDto Visitor { get; set; }
    }

     public class VisitorDto
    {
        public long Generate { get; set; }
        public Guid Id { get; set; }
        public string PersonId { get; set; }
        public string IdentityId { get; set; }
        public string CardNumber { get; set; }
        public string BleCardNumber { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }       
         public string FaceImage { get; set; }
        public int UploadFr { get; set; } = 0;
        public string UploadFrError { get; set; }
        public Guid ApplicationId { get; set; }
        public DateTime RegisteredDate { get; set; }
        public DateTime VisitorArrival { get; set; }
        public DateTime VisitorEnd { get; set; }
        public long PortalKey { get; set; }
        public DateTime TimestampPreRegistration { get; set; }
        public DateTime TimestampCheckedIn { get; set; }
        public DateTime TimestampCheckedOut { get; set; }
        public DateTime TimestampDeny { get; set; }
        public DateTime TimestampBlocked { get; set; }
        public DateTime TimestampUnblocked { get; set; }
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

     public class FloorplanMaskedAreaDto
        {
            public int Generate { get; set; }
            public Guid Id { get; set; }
            public Guid FloorplanId { get; set; }
            public Guid FloorId { get; set; }
            public string Name { get; set; }
            public string AreaShape { get; set; }
            public string ColorArea { get; set; }
            public string RestrictedStatus { get; set; }
            public string EngineAreaId { get; set; }
            public long WideArea { get; set; }
            public long PositionPxX { get; set; }
            public long PositionPxY { get; set; }
            public string CreatedBy { get; set; }
            public DateTime CreatedAt { get; set; }
            public string UpdatedBy { get; set; }
            public DateTime UpdatedAt { get; set; }
            public int? Status { get; set; }
          
        }
}
