using System;
using TrackingBle.Models.Dto.MstApplicationDtos;
using TrackingBle.Models.Dto.VisitorDtos;
using TrackingBle.Models.Dto.MstBleReaderDtos;
using TrackingBle.Models.Dto.FloorplanMaskedAreaDtos;

namespace TrackingBle.src._2AlarmRecordTracking.Models.Dto.AlarmRecordTrackingDtos
{
    public class AlarmRecordTrackingDto
    {
        public long Generate { get; set; }
        public Guid Id { get; set; }
        public DateTime Timestamp { get; set; }
        public Guid VisitorId { get; set; }
        public Guid ReaderId { get; set; }
        public Guid FloorplanMaskedAreaId { get; set; }
        public Guid ApplicationId { get; set; }
        public string AlarmRecordStatus { get; set; }
        public string ActionStatus { get; set; }
        public DateTime IdleTimestamp { get; set; }
        public DateTime DoneTimestamp { get; set; }
        public DateTime CancelTimestamp { get; set; }
        public DateTime WaitingTimestamp { get; set; }
        public DateTime InvestigatedTimestamp { get; set; }
        public DateTime InvestigatedDoneAt { get; set; }
        public string IdleBy { get; set; }
        public string DoneBy { get; set; }
        public string CancelBy { get; set; }
        public string WaitingBy { get; set; }
        public string InvestigatedBy { get; set; }
        public string InvestigatedResult { get; set; }

        public VisitorDto Visitor { get; set; }
        public MstBleReaderDto Reader { get; set; }
        public FloorplanMaskedAreaDto FloorplanMaskedArea { get; set; }
    }
}