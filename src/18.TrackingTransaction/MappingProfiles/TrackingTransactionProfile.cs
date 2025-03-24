using AutoMapper;
using TrackingBle.src._18TrackingTransaction.Models.Domain;
using TrackingBle.src._18TrackingTransaction.Models.Dto.TrackingTransactionDtos;

namespace TrackingBle.src._18TrackingTransaction.MappingProfiles
{
    public class TrackingTransactionProfile : Profile
    {
        public TrackingTransactionProfile()
        {
            CreateMap<TrackingTransactionCreateDto, TrackingTransaction>()
                .ForMember(dest => dest.AlarmStatus, opt => opt.MapFrom(src => Enum.Parse<AlarmStatus>(src.AlarmStatus, true)));

            CreateMap<TrackingTransaction, TrackingTransactionDto>()
                .ForMember(dest => dest.Reader, opt => opt.Ignore()) // Diisi via HttpClient
                .ForMember(dest => dest.FloorplanMaskedArea, opt => opt.Ignore())
                .ForMember(dest => dest.AlarmStatus, opt => opt.MapFrom(src => src.AlarmStatus.ToString()));
        }
    }
}