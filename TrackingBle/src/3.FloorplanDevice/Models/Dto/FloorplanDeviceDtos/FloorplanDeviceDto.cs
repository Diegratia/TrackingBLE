using System;
using TrackingBle.Models.Domain;
using TrackingBle.Models.Dto.FloorplanMaskedAreaDtos;
using TrackingBle.Models.Dto.MstAccessControlDtos;
using TrackingBle.Models.Dto.MstBleReaderDtos;
using TrackingBle.Models.Dto.MstFloorplanDtos;

namespace TrackingBle.src._3FloorplanDevice.Models.Dto.FloorplanDeviceDtos
{
    public class FloorplanDeviceDto
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
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string DeviceStatus { get; set; }
        public int? Status { get; set; }

        public MstFloorplanDto Floorplan { get; set; }
        public MstAccessCctv AccessCctv { get; set; }
        public MstBleReaderDto Reader { get; set; }
        public MstAccessControlDto AccessControl { get; set; }
        public FloorplanMaskedAreaDto FloorplanMaskedArea { get; set; }
        
    }
}