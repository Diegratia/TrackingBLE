using AutoMapper;
using TrackingBle.Models.Domain;
using TrackingBle.Models.Dto.MstMemberDto;

namespace TrackingBle.MappingProfiles
{
    public class MstMemberProfile : Profile
    {
        public MstMemberProfile()
        {
            CreateMap<MstMember, MstMemberDto>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status));
           
            CreateMap<MstMemberCreateDto, MstMember>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Generate, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.FaceImage, opt => opt.Ignore()) // Ditangani manual
                .ForMember(dest => dest.UploadFr, opt => opt.Ignore())
                .ForMember(dest => dest.UploadFrError, opt => opt.Ignore());

            CreateMap<MstMemberUpdateDto, MstMember>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Generate, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.FaceImage, opt => opt.Ignore()) // Ditangani manual
                .ForMember(dest => dest.UploadFr, opt => opt.Ignore())
                .ForMember(dest => dest.UploadFrError, opt => opt.Ignore());
        }
    }
}