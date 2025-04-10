namespace TrackingBle.Models.Dto.MstDistrictDtos
{
    public class MstDistrictCreateDto
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string DistrictHost { get; set; }
        public Guid ApplicationId { get; set; }
    }
}