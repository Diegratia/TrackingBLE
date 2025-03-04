using AutoMapper;
using TrackingBle.Models.Domain;
using TrackingBle.Models.Dto.MstAccessCctvDto;

namespace TrackingBle.MappingProfiles
{
    public class MstAccessCctvProfile : Profile
    {
        public MstAccessCctvProfile()
        {
            CreateMap<MstAccessCctv, MstAccessCctvDto>();
            CreateMap<MstAccessCctvCreateDto, MstAccessCctv>();
            CreateMap<MstAccessCctvUpdateDto, MstAccessCctv>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Generate, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());
        }
    }
}