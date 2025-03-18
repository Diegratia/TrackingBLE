using System;

namespace TrackingBle.src._14MstFloorplan.Models.Dto.MstFloorplanDtos
{
    public class MstFloorplanCreateDto
    {
        public string Name { get; set; }
        public Guid FloorId { get; set; }
        public Guid ApplicationId { get; set; }
    }
}