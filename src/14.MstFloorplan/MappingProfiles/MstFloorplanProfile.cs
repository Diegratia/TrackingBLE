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
                .ForMember(dest => dest.Floor, opt => opt.Ignore()) // Diisi manual via service
                .ForMember(dest => dest.Generate, opt => opt.MapFrom(src => src.Generate));

            // Mapping dari MstFloorplanCreateDto ke MstFloorplan
            CreateMap<MstFloorplanCreateDto, MstFloorplan>();

            // Mapping dari MstFloorplanUpdateDto ke MstFloorplan
            // Catatan: Anda belum memberikan MstFloorplanUpdateDto, jadi saya asumsikan mirip dengan CreateDto
            CreateMap<MstFloorplanCreateDto, MstFloorplan>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null)); // Hanya update field yang tidak null
        }
    }
}