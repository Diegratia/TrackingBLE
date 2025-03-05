namespace TrackingBle.Models.Dto.MstAccessCctvDto
{
    public class MstAccessCctvCreateDto
    {
        public string Name { get; set; }
        public string Rtsp { get; set; }
        public Guid IntegrationId { get; set; }
        public Guid ApplicationId { get; set; }
    }
}