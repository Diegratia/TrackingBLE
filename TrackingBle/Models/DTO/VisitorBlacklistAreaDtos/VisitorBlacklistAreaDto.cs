using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrackingBle.Models.Domain;
using TrackingBle.Models.Dto.FloorplanMaskedAreaDtos;
using TrackingBle.Models.Dto.VisitorDtos;

namespace TrackingBle.Models.Dto.VisitorBlacklistAreaDtos
{
    public class VisitorBlacklistAreaDto
    {
        public long Generate { get; set; } 

        public Guid Id { get; set; }

        public Guid FloorplanMaskedAreaId { get; set; }

        public Guid VisitorId { get; set; }

        public FloorplanMaskedAreaDto FloorplanMaskedArea { get; set; }
        public VisitorDto Visitor { get; set; }

    }
}
