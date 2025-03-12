using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackingBle.Models.Dto.VisitorBlacklistAreaDtos
{
    public class VisitorBlacklistAreaDto
    {
        public long Generate { get; set; } 

        public Guid Id { get; set; }

        public Guid FloorplanId { get; set; }

        public Guid VisitorId { get; set; }


    }
}
