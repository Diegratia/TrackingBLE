namespace TrackingBle.Models.Dto.MstAccessCctvDto
{
    public class MstAccessCctvDto
    {
        public long Generate { get; set; }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Rtsp { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Guid IntegrationId { get; set; }
        public Guid ApplicationId { get; set; }
        public int Status { get; set; }
    }
}