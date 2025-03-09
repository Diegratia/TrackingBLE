using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TrackingBle.Models.Domain;
using TrackingBle.Models.Dto.VisitorDto;

namespace TrackingBle.MappingProfiles 
{
    public class VisitorProfile : Profile
    {

        public VisitorProfile()
        {
            CreateMap<VisitorCreateDto, Visitor>();
            CreateMap<VisitorUpdateDto, Visitor>();
            CreateMap<Visitor, VisitorDto>();
               
        }
        
    }
}