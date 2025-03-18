using AutoMapper;
using TrackingBle.src._4FloorplanMaskedArea.Models.Domain;
using TrackingBle.src._4FloorplanMaskedArea.Models.Dto.FloorplanMaskedAreaDtos;

namespace TrackingBle.src._4FloorplanMaskedArea.MappingProfiles
{
    public class FloorplanMaskedAreaProfile : Profile
    {
        public FloorplanMaskedAreaProfile()
        {
            CreateMap<FloorplanMaskedArea, FloorplanMaskedAreaDto>()
                .ForMember(dest => dest.RestrictedStatus, opt => opt.MapFrom(src => src.RestrictedStatus.ToString().ToLower()));
            CreateMap<FloorplanMaskedAreaCreateDto, FloorplanMaskedArea>()
                .ForMember(dest => dest.RestrictedStatus, opt => opt.MapFrom(src => Enum.Parse<RestrictedStatus>(src.RestrictedStatus, true)));
            CreateMap<FloorplanMaskedAreaUpdateDto, FloorplanMaskedArea>()
                .ForMember(dest => dest.RestrictedStatus, opt => opt.MapFrom(src => Enum.Parse<RestrictedStatus>(src.RestrictedStatus, true)));
        }
    }
}