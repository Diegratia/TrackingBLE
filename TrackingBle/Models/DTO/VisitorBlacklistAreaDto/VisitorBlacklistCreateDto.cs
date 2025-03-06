using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackingBle.Models.DTO.VisitorBlacklistAreaDto
{
    public class VisitorBlacklistAreaCreateDto
    {

        public Guid FloorplanId { get; set; }

        public Guid VisitorId { get; set; }

    }
}
