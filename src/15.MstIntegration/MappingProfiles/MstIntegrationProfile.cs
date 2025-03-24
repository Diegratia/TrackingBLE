using AutoMapper;
using TrackingBle.src._15MstIntegration.Models.Domain;
using TrackingBle.src._15MstIntegration.Models.Dto.MstIntegrationDtos;

namespace TrackingBle.src._15MstIntegration.MappingProfiles
{
    public class MstIntegrationProfile : Profile
    {
        public MstIntegrationProfile()
        {
            CreateMap<MstIntegrationCreateDto, MstIntegration>()
                .ForMember(dest => dest.IntegrationType, opt => opt.MapFrom(src => Enum.Parse<IntegrationType>(src.IntegrationType)))
                .ForMember(dest => dest.ApiTypeAuth, opt => opt.MapFrom(src => Enum.Parse<ApiTypeAuth>(src.ApiTypeAuth)));
            CreateMap<MstIntegrationUpdateDto, MstIntegration>()
                .ForMember(dest => dest.IntegrationType, opt => opt.MapFrom(src => Enum.Parse<IntegrationType>(src.IntegrationType)))
                .ForMember(dest => dest.ApiTypeAuth, opt => opt.MapFrom(src => Enum.Parse<ApiTypeAuth>(src.ApiTypeAuth)));
            CreateMap<MstIntegration, MstIntegrationDto>()
                .ForMember(dest => dest.Brand, opt => opt.Ignore()); // Hanya Brand yang di-ignore
        }
    }
}