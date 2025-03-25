using System;
using TrackingBle.src._3FloorplanDevice.Models.Domain;


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
        public MstAccessCctvDto AccessCctv { get; set; }
        public MstBleReaderDto Reader { get; set; }
        public MstAccessControlDto AccessControl { get; set; }
        public FloorplanMaskedAreaDto FloorplanMaskedArea { get; set; }
        
    }

        public class MstFloorplanDto
    {
        public int Generate { get; set; }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid FloorId { get; set; }
        public Guid ApplicationId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int Status { get; set; }
        // public MstFloorDto Floor { get; set; }
    }

        public class MstAccessCctvDto
     {
        public long Generate { get; set; }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Rtsp { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Guid IntegrationId { get; set; }
        public Guid ApplicationId { get; set; }
        public int? Status { get; set; }

        // public MstIntegrationDto Integration { get; set; }
    }

     public class MstBleReaderDto
    {
        public int Generate { get; set; }
        public Guid Id { get; set; }
        public Guid BrandId { get; set; }
        public string Name { get; set; }
        public string Mac { get; set; }
        public string Ip { get; set; }
        public decimal LocationX { get; set; }
        public decimal LocationY { get; set; }
        public long LocationPxX { get; set; }
        public long LocationPxY { get; set; }
        public string EngineReaderId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int? Status { get; set; }
        // public MstBrandDto Brand { get; set;}
    }

      public class MstBrandDto
    {
        public int Generate { get; set; }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Tag { get; set; }
        public int? Status { get; set; }
    }

     public class FloorplanMaskedAreaDto
        {
            public int Generate { get; set; }
            public Guid Id { get; set; }
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
            public string CreatedBy { get; set; }
            public DateTime CreatedAt { get; set; }
            public string UpdatedBy { get; set; }
            public DateTime UpdatedAt { get; set; }
            public int? Status { get; set; }
            // public MstFloorDto Floor { get; set; }
            // public MstFloorplanDto Floorplan { get; set; }
        }

         public class MstAccessControlDto
    {
        public long Generate { get; set; }
        public Guid Id { get; set; }
        public Guid ControllerBrandId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public string Channel { get; set; }
        public string DoorId { get; set; }
        public string Raw { get; set; }
        public Guid IntegrationId { get; set; }
        public Guid ApplicationId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string UpdatedBy { get; set; } 
        public DateTime UpdatedAt { get; set; }
        public int? Status { get; set; }

        // public MstBrandDto Brand { get; set; }
        // public MstIntegrationDto Integration { get; set; }
    }


}