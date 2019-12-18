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
            CreateMap<Player, PlayerDbo>(MemberList.Source)
                .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dst => dst.TenantId, opt => opt.MapFrom(src => src.TenantId))
                .ForMember(dst => dst.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dst => dst.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dst => dst.Handicap, opt => opt.MapFrom(src => src.Handicap));

            CreateMap<Game, GameDbo>()
                .ForMember(dst => dst.PlayerOneId, opt => opt.MapFrom(
                    src => src.PlayerOne.Id))
                .ForMember(dst => dst.PlayerTwoId, opt => opt.MapFrom(
                    src => src.PlayerTwo.Id));

            CreateMap<MatchDay, MatchDayDbo>(MemberList.Source)
                //.ForMember(dst => dst.MatchDayPlayers, opt => opt.MapFrom(src => src.Players))
                .ForMember(dst => dst.Games, opt => opt.MapFrom(src => src.Games));

            CreateMap<Player, MatchDayPlayer>(MemberList.Destination)
                .ForMember(dst => dst.Player, opt => opt.MapFrom(src => src))
                .ForMember(dst => dst.PlayerId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dst => dst.MatchDay, opt => opt.Ignore())
                .ForMember(dst => dst.MatchDayId, opt => opt.Ignore())
                ;

            CreateMap<MatchDay, MatchDayPlayer>(MemberList.Destination)
                .ForMember(dst => dst.MatchDay, opt => opt.MapFrom(src => src))
                .ForMember(dst => dst.MatchDayId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dst => dst.Player, opt => opt.Ignore())
                .ForMember(dst => dst.PlayerId, opt => opt.Ignore());

            //CreateMap<MatchDay, MatchDayGame>(MemberList.Destination)
            //    .ForMember(dst => dst.MatchDay, opt => opt.MapFrom(src => src))
            //    .ForMember(dst => dst.MatchDayId, opt => opt.MapFrom(src => src.Id))
            //    .ForMember(dst => dst.Game, opt => opt.Ignore())
            //    .ForMember(dst => dst.GameId, opt => opt.Ignore());

            //CreateMap<Game, MatchDayGame>(MemberList.Destination)
            //    .ForMember(dst => dst.Game, opt => opt.MapFrom(src => src))
            //    .ForMember(dst => dst.GameId, opt => opt.MapFrom(src => src.Id))
            //    .ForMember(dst => dst.MatchDay, opt => opt.Ignore())
            //    .ForMember(dst => dst.MatchDayId, opt => opt.Ignore());

            CreateMap<PlayerDbo, Player>(MemberList.Destination)
                .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dst => dst.TenantId, opt => opt.MapFrom(src => src.TenantId))
                .ForMember(dst => dst.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dst => dst.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dst => dst.Handicap, opt => opt.MapFrom(src => src.Handicap));

            CreateMap<GameDbo, Game>()
                .ForMember(dst => dst.Type, opt => opt.MapFrom(
                    src => (GameType)src.Type));

            CreateMap<MatchDayPlayer, Player>(MemberList.Destination)
                .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.Player.Id))
                .ForMember(dst => dst.TenantId, opt => opt.MapFrom(src => src.Player.TenantId))
                .ForMember(dst => dst.FirstName, opt => opt.MapFrom(src => src.Player.FirstName))
                .ForMember(dst => dst.LastName, opt => opt.MapFrom(src => src.Player.LastName))
                .ForMember(dst => dst.Handicap, opt => opt.MapFrom(src => src.Player.Handicap))
                ;

            CreateMap<MatchDayPlayer, MatchDay>(MemberList.None)
                .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.MatchDay.Id))
                .ForMember(dst => dst.TenantId, opt => opt.MapFrom(src => src.MatchDay.TenantId));

            //CreateMap<MatchDayGame, Game>(MemberList.Destination)
            //    .ForMember(dst => dst.Date, opt => opt.MapFrom(src => src.Game.Date))
            //    .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.Game.Id))
            //    .ForMember(dst => dst.IsFinished, opt => opt.MapFrom(src => src.Game.IsFinished))
            //    .ForMember(dst => dst.PlayerOne, opt => opt.MapFrom(src => src.Game.PlayerOne))
            //    .ForMember(dst => dst.PlayerOnePoints, opt => opt.MapFrom(src => src.Game.PlayerOnePoints))
            //    .ForMember(dst => dst.PlayerOneRequiredPoints, opt => opt.MapFrom(src => src.Game.PlayerOneRequiredPoints))
            //    .ForMember(dst => dst.PlayerTwo, opt => opt.MapFrom(src => src.Game.PlayerTwo))
            //    .ForMember(dst => dst.PlayerTwoPoints, opt => opt.MapFrom(src => src.Game.PlayerTwoPoints))
            //    .ForMember(dst => dst.PlayerTwoRequiredPoints, opt => opt.MapFrom(src => src.Game.PlayerTwoRequiredPoints))
            //    .ForMember(dst => dst.TenantId, opt => opt.MapFrom(src => src.Game.TenantId))
            //    .ForMember(dst => dst.Type, opt => opt.MapFrom(src => (GameType)src.Game.Type));

            CreateMap<MatchDayDbo, MatchDay>(MemberList.Destination)
                //.ForMember(dst => dst.Players, opt => opt.MapFrom(src => src.MatchDayPlayers))
                .ForMember(dst => dst.Games, opt => opt.MapFrom(src => src.Games));
        }
    }
}
