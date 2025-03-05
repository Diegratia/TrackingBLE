using System;

namespace TrackingBle.Models.Dto.MstBrandDto
{
    public class MstBrandDto
    {
        public int Generate { get; set; }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Tag { get; set; }
        public int? Status { get; set; }
    }
}