namespace TrackingBle.Models.Dto.MstDepartmentDto
{
    public class MstDepartmentUpdateDto
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string DepartmentHost { get; set; }
        public Guid ApplicationId { get; set; }
        public string UpdatedBy { get; set; } // Opsional dari client, default "System" di service
        public int Status { get; set; }
    }
}