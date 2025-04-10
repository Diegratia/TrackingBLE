using System;
using TrackingBle.Models.Dto.MstApplicationDtos;

namespace TrackingBle.Models.Dto.MstDepartmentDtos
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
        public MstApplicationDto Application { get; set; }
    }
}