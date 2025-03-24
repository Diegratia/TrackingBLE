using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TrackingBle.src._20VisitorBlacklistArea.Models.Domain;
using TrackingBle.src._20VisitorBlacklistArea.Models.Dto.VisitorBlacklistAreaDtos;


namespace TrackingBle.src._20VisitorBlacklistArea.MappingProfiles
{
    public class VisitorBlacklistAreaProfile : Profile
    {

        public VisitorBlacklistAreaProfile()
        {
            CreateMap<VisitorBlacklistAreaCreateDto, VisitorBlacklistArea>();
            CreateMap<VisitorBlacklistAreaUpdateDto, VisitorBlacklistArea>();
            CreateMap<VisitorBlacklistArea, VisitorBlacklistAreaDto>()
                .ForMember(dest => dest.FloorplanMaskedArea, opt => opt.Ignore())
                .ForMember(dest => dest.Visitor, opt => opt.Ignore());
            // CreateMap<VisitorBlacklistAreaDto, VisitorBlacklistArea>();
            // CreateMap<FloorplanMaskedArea, FloorplanMaskedAreaDto>();
            // CreateMap<Visitor, VisitorDto>();
        }
        
    }
}