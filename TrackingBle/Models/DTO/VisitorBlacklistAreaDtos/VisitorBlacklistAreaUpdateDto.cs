using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackingBle.Models.Dto.VisitorBlacklistAreaDtos
{
    public class VisitorBlacklistAreaUpdateDto
    {

        public Guid FloorplanId { get; set; }

        public Guid VisitorId { get; set; }

    }
}
