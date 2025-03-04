namespace TrackingBle.Models.Dto.MstAccessControlDto
{
    public class MstAccessControlUpdateDto
    {
        public string ControllerBrandId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public string Channel { get; set; }
        public string DoorId { get; set; }
        public string Raw { get; set; }
        public Guid IntegrationId { get; set; }
        public Guid ApplicationId { get; set; }
        public DateTime UpdatedBy { get; set; } // Sesuai model
        public long UpdatedAt { get; set; }
        public int Status { get; set; }
    }
}