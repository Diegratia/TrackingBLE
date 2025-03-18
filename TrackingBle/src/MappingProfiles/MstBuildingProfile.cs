using AutoMapper;
using TrackingBle.Models.Domain;
using TrackingBle.Models.Dto.MstBuildingDtos;

namespace TrackingBle.src.MappingProfiles
{
    public class MstBuildingProfile : Profile
    {
        public MstBuildingProfile()
        {
            // Mapping dari Domain ke DTO
            CreateMap<MstBuilding, MstBuildingDto>();

            // Mapping dari Create DTO ke Domain
            CreateMap<MstBuildingCreateDto, MstBuilding>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.Status, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore());

            // Mapping dari Update DTO ke Domain
            CreateMap<MstBuildingUpdateDto, MstBuilding>()
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore());
        }
    }
}