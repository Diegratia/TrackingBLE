

namespace TrackingBle.Models.Dto.MstAreaDto
{
    public class MstAreaCreateDto
    {
        public Guid FloorId { get; set; }
        public string Name { get; set; }
        public string AreaShape { get; set; }
        public string ColorArea { get; set; }
        public RestrictedStatus RestrictedStatus { get; set; }
        public string EngineAreaId { get; set; }
        public long WideArea { get; set; }
        public long PositionPxX { get; set; }
        public long PositionPxY { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int Status { get; set; }
    }


}