namespace TrackingBle.Models.Dto.MstFloorDto
{
    public class MstFloorCreateDto
    {
        public string BuildingId { get; set; }
        public long Name { get; set; }
        public string FloorImage { get; set; }
        public long PixelX { get; set; }
        public long PixelY { get; set; }
        public long FloorX { get; set; }
        public long FloorY { get; set; }
        public decimal MeterPerPx { get; set; }
        public long EngineFloorId { get; set; }
        public int Status { get; set; }
    }
}