using System;

namespace TrackingBle.Models.Dto.MstMemberDtos
{
    public class MstMemberUpdateDto
    {
        public string PersonId { get; set; }
        public Guid OrganizationId { get; set; }
        public Guid DepartmentId { get; set; }
        public Guid DistrictId { get; set; }
        public string IdentityId { get; set; }
        public string CardNumber { get; set; }
        public string BleCardNumber { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; } // Enum sebagai string
        public string Address { get; set; }
        public IFormFile FaceImage { get; set; }
        public DateOnly BirthDate { get; set; }
        public string HeadMember1 { get; set; }
        public string HeadMember2 { get; set; }
        public Guid ApplicationId { get; set; }
        public string StatusEmployee { get; set; } // Enum sebagai string
    }
}