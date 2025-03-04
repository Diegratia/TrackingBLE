using AutoMapper;
using TrackingBle.Models.Dto.MstAreaDto;
using TrackingBle.Models.Domain;

namespace TrackingBle.MappingProfiles
{
    public class MstAreaProfile : Profile
    {
        public MstAreaProfile()
        {
            CreateMap<MstArea, MstAreaDto>();
            CreateMap<MstAreaCreateDto, MstArea>();
            CreateMap<MstAreaUpdateDto, MstArea>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Generate, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());
        }
    }
}