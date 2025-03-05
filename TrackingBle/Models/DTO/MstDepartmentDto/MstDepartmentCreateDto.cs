namespace TrackingBle.Models.Dto.MstDepartmentDto
{
    public class MstDepartmentCreateDto
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string DepartmentHost { get; set; }
        public Guid ApplicationId { get; set; }
    }
}