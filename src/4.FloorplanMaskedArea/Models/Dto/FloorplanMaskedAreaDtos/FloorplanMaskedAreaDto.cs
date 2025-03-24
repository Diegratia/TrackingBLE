    using System;

    namespace TrackingBle.src._4FloorplanMaskedArea.Models.Dto.FloorplanMaskedAreaDtos
    {
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
            public MstFloorDto Floor { get; set; }
            public MstFloorplanDto Floorplan { get; set; }
        }

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

    }