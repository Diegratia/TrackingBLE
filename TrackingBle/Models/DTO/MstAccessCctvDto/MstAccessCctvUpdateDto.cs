namespace TrackingBle.Models.Dto.MstAccessCctvDto
{
    public class MstAccessCctvUpdateDto
    {
        public string Name { get; set; }
        public string Rtsp { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Guid IntegrationId { get; set; }
        public Guid ApplicationId { get; set; }
        public int Status { get; set; }
    }
}