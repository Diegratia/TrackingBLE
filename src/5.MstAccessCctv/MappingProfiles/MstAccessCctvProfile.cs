using AutoMapper;
using TrackingBle.src._5MstAccessCctv.Models.Domain;
using TrackingBle.src._5MstAccessCctv.Models.Dto.MstAccessCctvDtos;

namespace TrackingBle.src._5MstAccessCctv.MappingProfiles
{
    public class MstAccessCctvProfile : Profile
    {
        public MstAccessCctvProfile()
        {
            CreateMap<MstAccessCctvCreateDto, MstAccessCctv>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Generate, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<MstAccessCctvUpdateDto, MstAccessCctv>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Generate, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<MstAccessCctv, MstAccessCctvDto>()
                .ForMember(dest => dest.Integration, opt => opt.Ignore());
        }
    }
}