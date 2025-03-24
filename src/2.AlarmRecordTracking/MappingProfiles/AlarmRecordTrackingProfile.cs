using AutoMapper;
using TrackingBle.src._2AlarmRecordTracking.Models.Domain;
using TrackingBle.src._2AlarmRecordTracking.Models.Dto.AlarmRecordTrackingDtos;

namespace TrackingBle.src._2AlarmRecordTracking.MappingProfiles
{
    public class AlarmRecordTrackingProfile : Profile
    {
        public AlarmRecordTrackingProfile()
        {
            CreateMap<AlarmRecordTrackingCreateDto, AlarmRecordTracking>()
                .ForMember(dest => dest.Alarm, opt => opt.MapFrom(src => Enum.Parse<AlarmRecordStatus>(src.AlarmRecordStatus)))
                .ForMember(dest => dest.Action, opt => opt.MapFrom(src => Enum.Parse<ActionStatus>(src.ActionStatus)));

            CreateMap<AlarmRecordTrackingUpdateDto, AlarmRecordTracking>()
                .ForMember(dest => dest.Alarm, opt => opt.MapFrom(src => Enum.Parse<AlarmRecordStatus>(src.AlarmRecordStatus)))
                .ForMember(dest => dest.Action, opt => opt.MapFrom(src => Enum.Parse<ActionStatus>(src.ActionStatus)));

            CreateMap<AlarmRecordTracking, AlarmRecordTrackingDto>()
                .ForMember(dest => dest.AlarmRecordStatus, opt => opt.MapFrom(src => src.Alarm.ToString()))
                .ForMember(dest => dest.ActionStatus, opt => opt.MapFrom(src => src.Action.ToString()))
                .ForMember(dest => dest.Visitor, opt => opt.Ignore())
                .ForMember(dest => dest.Reader, opt => opt.Ignore())
                .ForMember(dest => dest.FloorplanMaskedArea, opt => opt.Ignore());
        }
    }
}