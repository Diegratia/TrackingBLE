using System;

namespace TrackingBle.Models.Dto.AlarmRecordTrackingDtos
{
    public class AlarmRecordTrackingCreateDto
    {
       
        public Guid VisitorId { get; set; }
        public Guid ReaderId { get; set; }
        public Guid FloorplanMaskedAreaId { get; set; }
        public Guid ApplicationId { get; set; }
        public string AlarmRecordStatus { get; set; }
        public string ActionStatus { get; set; }
        public string InvestigatedResult { get; set; }
    }
}