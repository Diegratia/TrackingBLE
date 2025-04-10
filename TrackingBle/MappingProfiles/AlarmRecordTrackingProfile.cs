using AutoMapper;
using TrackingBle.Models.Domain;
using TrackingBle.Models.Dto.AlarmRecordTrackingDtos;
using TrackingBle.Models.Dto.VisitorDtos;
using TrackingBle.Models.Dto.MstBleReaderDtos;
using TrackingBle.Models.Dto.FloorplanMaskedAreaDtos;

namespace TrackingBle.MappingProfiles
{
    public class AlarmRecordTrackingProfile : Profile
    {
        public AlarmRecordTrackingProfile()
        {
            CreateMap<AlarmRecordTracking, AlarmRecordTrackingDto>()
                .ForMember(dest => dest.Visitor, opt => opt.MapFrom(src => src.Visitor))
                .ForMember(dest => dest.Reader, opt => opt.MapFrom(src => src.Reader))
                .ForMember(dest => dest.ActionStatus, opt => opt.MapFrom(src => src.Action.ToString()))
                .ForMember(dest => dest.AlarmRecordStatus, opt => opt.MapFrom(src => src.Alarm.ToString()))
                .ForMember(dest => dest.FloorplanMaskedArea, opt => opt.MapFrom(src => src.FloorplanMaskedArea));

            CreateMap<AlarmRecordTrackingCreateDto, AlarmRecordTracking>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Generate, opt => opt.Ignore());

            CreateMap<AlarmRecordTrackingUpdateDto, AlarmRecordTracking>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Generate, opt => opt.Ignore());
               

            CreateMap<Visitor, VisitorDto>();
            CreateMap<MstBleReader, MstBleReaderDto>();
            CreateMap<FloorplanMaskedArea, FloorplanMaskedAreaDto>();
        }
    }
}