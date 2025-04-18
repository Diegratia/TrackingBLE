using AutoMapper;
using TrackingBle.Models.Domain;
using TrackingBle.Models.Dto.MstOrganizationDtos;

namespace TrackingBle.src.MappingProfiles
{
    public class MstOrganizationProfile : Profile
    {
        public MstOrganizationProfile()
        {
            CreateMap<MstOrganizationCreateDto, MstOrganization>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Generate, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<MstOrganization, MstOrganizationDto>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status));

            CreateMap<MstOrganizationUpdateDto, MstOrganization>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Generate, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));
        }
    }
}