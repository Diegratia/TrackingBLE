using System;

namespace TrackingBle.src._18TrackingTransaction.Models.Dto.TrackingTransactionDtos
{

    public class TrackingTransactionDto
    {
        public Guid Id { get; set; }
        public DateTime TransTime { get; set; }
        public Guid ReaderId { get; set; }
        public long CardId { get; set; }
        public Guid FloorplanMaskedAreaId { get; set; }
        public decimal CoordinateX { get; set; }
        public decimal CoordinateY { get; set; }
        public long CoordinatePxX { get; set; }
        public long CoordinatePxY { get; set; }
        public string AlarmStatus { get; set; } // Enum sebagai string
        public long Battery { get; set; }

        public MstBleReaderDto Reader { get; set; }
        public FloorplanMaskedAreaDto FloorplanMaskedArea { get; set; }
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

        
}