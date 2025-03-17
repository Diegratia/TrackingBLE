using System;

namespace TrackingBle.Models.Dto.MstFloorplanDtos
{
    public class MstFloorplanCreateDto
    {
        public string Name { get; set; }
        public Guid FloorId { get; set; }
        public Guid ApplicationId { get; set; }
    }
}