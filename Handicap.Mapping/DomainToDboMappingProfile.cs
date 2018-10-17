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
                .ForMember(dst => dst.Handicap, opt => opt.MapFrom(src => src.Handicap));

            CreateMap<Game, GameDbo>(MemberList.Destination)
                .ForMember(dst => dst.Id, opt => opt.MapFrom(g => g.Id))
                .ForMember(dst => dst.PlayerOneId, opt => opt.MapFrom(g => g.PlayerOneId))
                .ForMember(dst => dst.PlayerOnePoints, opt => opt.MapFrom(g => g.PlayerOnePoints))
                .ForMember(dst => dst.PlayerOneRequiredPoints, opt => opt.MapFrom(g => g.PlayerOneRequiredPoints))
                .ForMember(dst => dst.PlayerTwoId, opt => opt.MapFrom(g => g.PlayerTwoId))
                .ForMember(dst => dst.PlayerTwoPoints, opt => opt.MapFrom(g => g.PlayerTwoPoints))
                .ForMember(dst => dst.PlayerTwoRequiredPoints, opt => opt.MapFrom(g => g.PlayerTwoRequiredPoints))
                .ForMember(dst => dst.Type, opt => opt.MapFrom(g => g.Type))
                .ForMember(dst => dst.PlayerOne, opt => opt.MapFrom(g => g.PlayerOne));

            CreateMap<GameDbo, Game>(MemberList.Destination)
                .ForMember(dst => dst.Id, opt => opt.MapFrom(g => g.Id))
                .ForMember(dst => dst.PlayerOneId, opt => opt.MapFrom(g => g.PlayerOneId))
                .ForMember(dst => dst.PlayerOnePoints, opt => opt.MapFrom(g => g.PlayerOnePoints))
                .ForMember(dst => dst.PlayerOneRequiredPoints, opt => opt.MapFrom(g => g.PlayerOneRequiredPoints))
                .ForMember(dst => dst.PlayerTwoId, opt => opt.MapFrom(g => g.PlayerTwoId))
                .ForMember(dst => dst.PlayerTwoPoints, opt => opt.MapFrom(g => g.PlayerTwoPoints))
                .ForMember(dst => dst.PlayerTwoRequiredPoints, opt => opt.MapFrom(g => g.PlayerTwoRequiredPoints))
                .ForMember(dst => dst.Type, opt => opt.MapFrom(g => g.Type))
                .ForMember(dst => dst.Date, opt => opt.Ignore())
                .ForMember(dst => dst.PlayerOne, opt => opt.MapFrom(g => g.PlayerOne));
        }
    }
}
