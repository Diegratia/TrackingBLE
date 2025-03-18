using AutoMapper;
using TrackingBle.src._14MstFloorplan.Models.Domain;
using TrackingBle.src._14MstFloorplan.Models.Dto.MstFloorplanDtos;
using TrackingBle.src._14MstFloorplan.Services;

namespace TrackingBle.src._14MstFloorplan.MappingProfiles
{
    public class MstFloorplanProfile : Profile
    {
        public MstFloorplanProfile()
        {
            CreateMap<MstFloorplan, MstFloorplanDto>();
            CreateMap<MstFloorplanCreateDto, MstFloorplan>();
            CreateMap<MstFloorplanUpdateDto, MstFloorplan>();
        }
    }
}