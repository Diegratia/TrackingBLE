using AutoMapper;
using TrackingBle.Models.Domain;
using TrackingBle.Models.Dto.MstDistrictDtos;

namespace TrackingBle.MappingProfiles
{
    public class MstDistrictProfile : Profile
    {
        public MstDistrictProfile()
        {
            CreateMap<MstDistrict, MstDistrictDto>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status));
            CreateMap<MstDistrictCreateDto, MstDistrict>()
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore()) 
                .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore()) 
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));
            CreateMap<MstDistrictUpdateDto, MstDistrict>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Generate, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore()) 
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));
        }
    }
}