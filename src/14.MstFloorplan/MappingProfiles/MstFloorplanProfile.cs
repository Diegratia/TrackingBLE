using AutoMapper;
using TrackingBle.src._14MstFloorplan.Models.Domain;
using TrackingBle.src._14MstFloorplan.Models.Dto.MstFloorplanDtos;

namespace TrackingBle.src._14MstFloorplan.MappingProfiles
{
    public class MstFloorplanProfile : Profile
    {
        public MstFloorplanProfile()
        {
            // Mapping dari MstFloorplan ke MstFloorplanDto
            CreateMap<MstFloorplan, MstFloorplanDto>()
                .ForMember(dest => dest.Floor, opt => opt.Ignore()) 
                .ForMember(dest => dest.Generate, opt => opt.MapFrom(src => src.Generate));

            CreateMap<MstFloorplanCreateDto, MstFloorplan>();

            // Mapping dari MstFloorplanUpdateDto ke MstFloorplan
            CreateMap<MstFloorplanCreateDto, MstFloorplan>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null)); 
        }
    }
}