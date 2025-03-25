using AutoMapper;
using TrackingBle.src._16MstMember.Models.Domain;
using TrackingBle.src._16MstMember.Models.Dto.MstMemberDtos;
// using TrackingBle.src._16MstMember.Models.Dto.MstOrganizationDtos;
// using TrackingBle.src._16MstMember.Models.Dto.MstDepartmentDtos;
// using TrackingBle.src._16MstMember.Models.Dto.MstDistrictDtos;

namespace TrackingBle.src._16MstMember.MappingProfiles
{
    public class MstMemberProfile : Profile
    {
        public MstMemberProfile()
        {
            CreateMap<MstMember, MstMemberDto>()
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender.ToString()))
                .ForMember(dest => dest.StatusEmployee, opt => opt.MapFrom(src => src.StatusEmployee.ToString()))
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
                // .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender.ToString()))
                // .ForMember(dest => dest.StatusEmployee, opt => opt.MapFrom(src => src.StatusEmployee.ToString()));

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
                // .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender.ToString()))
                // .ForMember(dest => dest.StatusEmployee, opt => opt.MapFrom(src => src.StatusEmployee.ToString()));
            // CreateMap<MstMemberDto, MstMember>();
            // CreateMap<MstOrganization, MstOrganizationDto>();
            // CreateMap<MstDistrict, MstDistrictDto>();
            // CreateMap<MstDepartment, MstDepartmentDto>();
        }
    }
}