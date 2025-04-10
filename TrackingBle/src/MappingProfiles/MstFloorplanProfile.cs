using AutoMapper;
using TrackingBle.Models.Dto.MstFloorplanDtos;
using TrackingBle.Models.Domain;

namespace TrackingBle.src.MappingProfiles
{
    public class MstFloorplanProfile : Profile
    {
        public MstFloorplanProfile()
        {
            CreateMap<MstFloorplan, MstFloorplanDto>();

            CreateMap<MstFloorplanCreateDto, MstFloorplan>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.Status, opt => opt.Ignore());

            CreateMap<MstFloorplanUpdateDto, MstFloorplan>()
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());
        }
    }
}