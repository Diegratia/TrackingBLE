namespace TrackingBle.src._13MstFloor.Models.Dto.MstFloorDtos
{
    public class MstFloorUpdateDto
    {
        public string BuildingId { get; set; }
        public string Name { get; set; }
        public IFormFile FloorImage { get; set; }
        public long PixelX { get; set; }
        public long PixelY { get; set; }
        public long FloorX { get; set; }
        public long FloorY { get; set; }
        public decimal MeterPerPx { get; set; }
        public long EngineFloorId { get; set; }
    }
}