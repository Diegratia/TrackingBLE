namespace TrackingBle.Models.Dto.MstDistrictDto
{
    public class MstDistrictUpdateDto
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string DistrictHost { get; set; }
        public Guid ApplicationId { get; set; }
    }
}