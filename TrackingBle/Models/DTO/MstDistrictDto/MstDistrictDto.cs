using System;

namespace TrackingBle.Models.Dto.MstDistrictDto
{
    public class MstDistrictDto
    {
        public int Generate { get; set; }
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string DistrictHost { get; set; }
        public Guid ApplicationId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int Status { get; set; }
    }
}