using AutoMapper;
using TrackingBle.src._6MstAccessControl.Models.Domain;
using TrackingBle.src._6MstAccessControl.Models.Dto.MstAccessControlDtos;

namespace TrackingBle.src._6MstAccessControl.MappingProfiles
{
    public class MstAccessControlProfile : Profile
    {
        public MstAccessControlProfile()
        {
            CreateMap<MstAccessControlCreateDto, MstAccessControl>();
            CreateMap<MstAccessControlUpdateDto, MstAccessControl>();
            CreateMap<MstAccessControl, MstAccessControlDto>()
                .ForMember(dest => dest.Brand, opt => opt.Ignore())
                .ForMember(dest => dest.Integration, opt => opt.Ignore());
        }
    }
}