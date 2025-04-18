using AutoMapper;
using TrackingBle.Models.Domain;
using TrackingBle.Models.Dto.MstFloorDtos;

namespace TrackingBle.src.MappingProfiles
{
    public class MstFloorProfile : Profile
    {
        public MstFloorProfile()
        {
            CreateMap<MstFloor, MstFloorDto>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status));
            CreateMap<MstFloorCreateDto, MstFloor>()
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow)) 
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.FloorImage, opt => opt.Ignore());
            CreateMap<MstFloorUpdateDto, MstFloor>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Generate, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.FloorImage, opt => opt.Ignore());
        }
    }
}