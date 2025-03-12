using AutoMapper;
using TrackingBle.Models.Dto.MstIntegrationDtos;
using TrackingBle.Models.Domain;
using TrackingBle.Models.Dto.MstBrandDtos;

namespace TrackingBle.MappingProfiles
{
    public class MstIntegrationProfile : Profile
    {
        public MstIntegrationProfile()
        {
            CreateMap<MstIntegration, MstIntegrationDto>()
                 .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                 .ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.BrandData)); // Map relasi Brand
            CreateMap<MstIntegrationCreateDto, MstIntegration>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Generate, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.BrandData, opt => opt.Ignore());
            CreateMap<MstIntegrationUpdateDto, MstIntegration>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Generate, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.BrandData, opt => opt.Ignore());
            CreateMap<MstIntegrationDto, MstIntegration>();
            CreateMap<MstBrandDto, MstBrand>();
            CreateMap<MstBrand, MstBrandDto>();
          
        }
    }
}