using System;

namespace TrackingBle.src._4FloorplanMaskedArea.Models.Dto.FloorplanMaskedAreaDtos
{
    public class FloorplanMaskedAreaUpdateDto
    {
        public Guid FloorplanId { get; set; }
        public Guid FloorId { get; set; }
        public string Name { get; set; }
        public string AreaShape { get; set; }
        public string ColorArea { get; set; }
        public string RestrictedStatus { get; set; }
        public string EngineAreaId { get; set; }
        public long WideArea { get; set; }
        public long PositionPxX { get; set; }
        public long PositionPxY { get; set; }
    }
}