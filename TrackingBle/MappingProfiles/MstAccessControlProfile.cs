using AutoMapper;
using TrackingBle.Models.Dto.MstAccessControlDto;
using TrackingBle.Models.Domain;

namespace TrackingBle.MappingProfiles
{
    public class MstAccessControlProfile : Profile
    {
        public MstAccessControlProfile()
        {
            CreateMap<MstAccessControl, MstAccessControlDto>();
            CreateMap<MstAccessControlCreateDto, MstAccessControl>();
            CreateMap<MstAccessControlUpdateDto, MstAccessControl>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Generate, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());
        }
    }
}