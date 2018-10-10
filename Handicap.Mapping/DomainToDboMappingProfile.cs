using AutoMapper;
using Handicap.Dbo;
using Handicap.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Handicap.Mapping
{
    public class DomainToDboMappingProfile : Profile
    {
        public DomainToDboMappingProfile()
        {
            CreateMap<Player, PlayerDbo>(MemberList.Destination)
                .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dst => dst.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dst => dst.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dst => dst.Handicap, opt => opt.MapFrom(src => src.Handicap))
                .ForMember(dst => dst.Games, opt => opt.Ignore());

            CreateMap<PlayerDbo, Player>(MemberList.Destination)
                .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dst => dst.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dst => dst.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dst => dst.Handicap, opt => opt.MapFrom(src => src.Handicap))
                .ForMember(dst => dst.HasFinishedMatchDay, opt => opt.Ignore());
        }
    }
}
