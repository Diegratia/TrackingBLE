using AutoMapper;
using TrackingBle.src._9MstBrand.Models.Domain;
using TrackingBle.src._9MstBrand.Models.Dto.MstBrandDtos;

namespace TrackingBle.src._9MstBrand.MappingProfiles
{
    public class MstBrandProfile : Profile
    {
        public MstBrandProfile()
        {
            CreateMap<MstBrand, MstBrandDto>()
                .ForMember(dest => dest.Generate, opt => opt.MapFrom(src => src.Generate));
            CreateMap<MstBrandCreateDto, MstBrand>();
            CreateMap<MstBrandUpdateDto, MstBrand>();
        }
    }
}