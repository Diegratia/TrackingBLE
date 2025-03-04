using AutoMapper;
using TrackingBle.Models.Dto.MstBrandDto;
using TrackingBle.Models.Domain;

namespace TrackingBle.MappingProfiles
{
    public class MstBrandProfile : Profile
    {
        public MstBrandProfile()
        {
            CreateMap<MstBrand, MstBrandDto>();
            CreateMap<MstBrandCreateDto, MstBrand>();
            CreateMap<MstBrandUpdateDto, MstBrand>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Generate, opt => opt.Ignore());
        }
    }
}