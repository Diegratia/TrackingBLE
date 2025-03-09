using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TrackingBle.Models.Domain;
using TrackingBle.Models.Dto.VisitorBlacklistAreaDto;

namespace TrackingBle.MappingProfiles 
{
    public class VisitorBlacklistAreaProfile : Profile
    {

        public VisitorBlacklistAreaProfile()
        {
            CreateMap<VisitorBlacklistAreaCreateDto, VisitorBlacklistArea>();
            CreateMap<VisitorBlacklistAreaUpdateDto, VisitorBlacklistArea>();
            CreateMap<VisitorBlacklistArea, VisitorBlacklistAreaDto>();
               
        }
        
    }
}