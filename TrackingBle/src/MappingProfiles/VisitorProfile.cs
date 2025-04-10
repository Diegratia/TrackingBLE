using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TrackingBle.Models.Domain;
using TrackingBle.Models.Dto.VisitorDtos;

namespace TrackingBle.src.MappingProfiles
{
    public class VisitorProfile : Profile
    {

        public VisitorProfile()
        {
            CreateMap<VisitorCreateDto, Visitor>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Generate, opt => opt.Ignore())
                .ForMember(dest => dest.FaceImage, opt => opt.Ignore()) // Ditangani manual
                .ForMember(dest => dest.UploadFr, opt => opt.Ignore())
                .ForMember(dest => dest.UploadFrError, opt => opt.Ignore());
            CreateMap<VisitorUpdateDto, Visitor>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Generate, opt => opt.Ignore())
                .ForMember(dest => dest.FaceImage, opt => opt.Ignore()) // Ditangani manual
                .ForMember(dest => dest.UploadFr, opt => opt.Ignore())
                .ForMember(dest => dest.UploadFrError, opt => opt.Ignore());
            CreateMap<Visitor, VisitorDto>();
               
        }
        
    }
}