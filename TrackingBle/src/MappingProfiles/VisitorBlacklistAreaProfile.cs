using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TrackingBle.Models.Domain;
using TrackingBle.Models.Dto.VisitorBlacklistAreaDtos;
using TrackingBle.Models.Dto.VisitorDtos;
using TrackingBle.Models.Dto.FloorplanMaskedAreaDtos;

namespace TrackingBle.src.MappingProfiles
{
    public class VisitorBlacklistAreaProfile : Profile
    {

        public VisitorBlacklistAreaProfile()
        {
            CreateMap<VisitorBlacklistAreaCreateDto, VisitorBlacklistArea>();
            CreateMap<VisitorBlacklistAreaUpdateDto, VisitorBlacklistArea>();
            CreateMap<VisitorBlacklistArea, VisitorBlacklistAreaDto>();
            CreateMap<VisitorBlacklistAreaDto, VisitorBlacklistArea>();
            CreateMap<FloorplanMaskedArea, FloorplanMaskedAreaDto>();
            CreateMap<Visitor, VisitorDto>();
        }
        
    }
}