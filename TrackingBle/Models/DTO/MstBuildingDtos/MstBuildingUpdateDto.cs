using System;

namespace TrackingBle.Models.Dto.MstBuildingDtos
{
    public class MstBuildingUpdateDto
    { 
        public string Name { get; set; }
        public string Image { get; set; }
        public Guid ApplicationId { get; set; }
    }
}