using AutoMapper;
using TrackingBle.Models.Domain;
using TrackingBle.Models.Dto.MstFloorDto;

namespace TrackingBle.MappingProfiles
{
    public class MstFloorProfile : Profile
    {
        public MstFloorProfile()
        {
            CreateMap<MstFloor, MstFloorDto>();
            CreateMap<MstFloorCreateDto, MstFloor>()
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.Now)) // Waktu lokal Jakarta
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.Now)); // Waktu lokal Jakarta
            CreateMap<MstFloorUpdateDto, MstFloor>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Generate, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.Now)); // Waktu lokal Jakarta
        }
    }
}