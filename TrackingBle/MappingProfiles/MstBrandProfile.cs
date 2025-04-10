using AutoMapper;
using TrackingBle.Models.Dto.MstBrandDtos;
using TrackingBle.Models.Domain;

namespace TrackingBle.MappingProfiles
{
    public class MstBrandProfile : Profile
    {
        public MstBrandProfile()
        {
            CreateMap<MstBrand, MstBrandDto>() 
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status));
            CreateMap<MstBrandCreateDto, MstBrand>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Generate, opt => opt.Ignore());
            CreateMap<MstBrandUpdateDto, MstBrand>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Generate, opt => opt.Ignore());
                 
        }
    }
}