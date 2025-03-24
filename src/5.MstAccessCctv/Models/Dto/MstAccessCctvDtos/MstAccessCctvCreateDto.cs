namespace TrackingBle.src._5MstAccessCctv.Models.Dto.MstAccessCctvDtos
{
    public class MstAccessCctvCreateDto
    {
        public string Name { get; set; }
        public string Rtsp { get; set; }
        public Guid IntegrationId { get; set; }
        public Guid ApplicationId { get; set; }
    }
}