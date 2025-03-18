using System;

namespace TrackingBle.src._3FloorplanDevice.Models.Dto.FloorplanDeviceDtos
{
    public class FloorplanDeviceUpdateDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public Guid FloorplanId { get; set; }
        public Guid AccessCctvId { get; set; }
        public Guid ReaderId { get; set; }
        public Guid AccessControlId { get; set; }
        public decimal PosX { get; set; }
        public decimal PosY { get; set; }
        public long PosPxX { get; set; }
        public long PosPxY { get; set; }
        public Guid FloorplanMaskedAreaId { get; set; }
        public Guid ApplicationId { get; set; }
        public string UpdatedBy { get; set; }
        public string DeviceStatus { get; set; }
    }
}