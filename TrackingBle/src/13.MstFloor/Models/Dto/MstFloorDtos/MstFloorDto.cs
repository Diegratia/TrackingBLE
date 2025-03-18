using System;

namespace TrackingBle.src._13MstFloor.Models.Dto.MstFloorDtos
{
    public class MstFloorDto
    {
        public int Generate { get; set; }
        public Guid Id { get; set; }
        public string BuildingId { get; set; }
        public string Name { get; set; }
        public string FloorImage { get; set; }
        public long PixelX { get; set; }
        public long PixelY { get; set; }
        public long FloorX { get; set; }
        public long FloorY { get; set; }
        public decimal MeterPerPx { get; set; }
        public long EngineFloorId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int? Status { get; set; }
    }
}