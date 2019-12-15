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
            CreateMap<Player, PlayerDbo>();

            CreateMap<PlayerDbo, Player>();

            CreateMap<Game, GameDbo>()
                .ForMember(dst => dst.PlayerOneId, opt => opt.MapFrom(
                    src => src.PlayerOne.Id))
                .ForMember(dst => dst.PlayerTwoId, opt => opt.MapFrom(
                    src => src.PlayerTwo.Id));

            CreateMap<GameDbo, Game>()
                .ForMember(dst => dst.Type, opt => opt.MapFrom(
                    src => (GameType)src.Type));

            CreateMap<MatchDay, MatchDayDbo>();

            CreateMap<MatchDayDbo, MatchDay>();
        }
    }
}
