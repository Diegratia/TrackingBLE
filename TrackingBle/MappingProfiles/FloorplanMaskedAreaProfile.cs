using AutoMapper;
using TrackingBle.Models.Dto.FloorplanMaskedAreaDtos;
using TrackingBle.Models.Domain;

namespace TrackingBle.MappingProfiles
{
    public class FloorplanMaskedAreaProfile : Profile
    {
        public FloorplanMaskedAreaProfile()
        {
            CreateMap<FloorplanMaskedArea, FloorplanMaskedAreaDto>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status));
            CreateMap<FloorplanMaskedAreaCreateDto, FloorplanMaskedArea>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Generate, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));
            CreateMap<FloorplanMaskedAreaUpdateDto, FloorplanMaskedArea>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Generate, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));
        }
    }
}