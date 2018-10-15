using AutoMapper;
using Handicap.Domain.Models;
using Handicap.Dto.Request;
using Handicap.Dto.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace Handicap.Mapping
{
    public class DomainToDtoMappingProfile : Profile
    {
        public DomainToDtoMappingProfile()
        {
            CreateMap<PlayerRequest, Player>(MemberList.Destination)
                .ForMember(dst => dst.Id, opt => opt.Ignore())
                .ForMember(dst => dst.FirstName, opt => opt.MapFrom(p => p.FirstName))
                .ForMember(dst => dst.LastName, opt => opt.MapFrom(p => p.LastName))
                .ForMember(dst => dst.Handicap, opt => opt.MapFrom(p => p.Handicap));

            CreateMap<Player, PlayerResponse>(MemberList.Destination)
                .ForMember(dst => dst.Id, opt => opt.MapFrom(p => p.Id))
                .ForMember(dst => dst.FirstName, opt => opt.MapFrom(p => p.FirstName))
                .ForMember(dst => dst.LastName, opt => opt.MapFrom(p => p.LastName))
                .ForMember(dst => dst.Handicap, opt => opt.MapFrom(p => p.Handicap));
        }
    }
}
