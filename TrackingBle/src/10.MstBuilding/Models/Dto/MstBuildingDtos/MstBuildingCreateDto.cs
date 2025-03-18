using System;

namespace TrackingBle.src._10MstBuilding.Models.Dto.MstBuildingDtos
{
    public class MstBuildingCreateDto
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public Guid ApplicationId { get; set; }
    }
}