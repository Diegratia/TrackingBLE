using AutoMapper;
using TrackingBle.Models.Domain;
using TrackingBle.Models.Dto.TrackingTransactionDtos;

namespace TrackingBle.MappingProfiles
{
    public class TrackingTransactionProfile : Profile
    {
        public TrackingTransactionProfile()
        {
            CreateMap<TrackingTransactionCreateDto, TrackingTransaction>()
                .ForMember(dest => dest.AlarmStatus, opt => opt.MapFrom(src => Enum.Parse<AlarmStatus>(src.AlarmStatus, true)));

            CreateMap<TrackingTransaction, TrackingTransactionDto>()
                .ForMember(dest => dest.AlarmStatus, opt => opt.MapFrom(src => src.AlarmStatus.ToString()));
        }
    }
}