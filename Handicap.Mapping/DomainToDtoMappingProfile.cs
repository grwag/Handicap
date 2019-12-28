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
            //DisableConstructorMapping();
            CreateMap<PlayerRequest, Player>(MemberList.Source)
                .ForMember(dst => dst.Id, opt => opt.Ignore())
                .ForMember(dst => dst.TenantId, opt => opt.Ignore());

            CreateMap<Player, PlayerResponse>();

            CreateMap<MatchDayPlayer, PlayerResponse>(MemberList.Destination)
                .ForMember(dst => dst.FirstName, opt => opt.MapFrom(src => src.Player.FirstName))
                .ForMember(dst => dst.LastName, opt => opt.MapFrom(src => src.Player.LastName))
                .ForMember(dst => dst.Handicap, opt => opt.MapFrom(src => src.Player.Handicap))
                .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.Player.Id))
                .ForMember(dst => dst.TenantId, opt => opt.MapFrom(src => src.Player.TenantId));

            CreateMap<MatchDayPlayer, MatchDayResponse>(MemberList.Destination)
                .ForMember(dst => dst.TenantId, opt => opt.MapFrom(src => src.MatchDay.TenantId))
                .ForMember(dst => dst.Date, opt => opt.MapFrom(src => src.MatchDay.Date))
                .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.MatchDay.Id))
                .ForMember(dst => dst.IsFinished, opt => opt.Ignore());

            CreateMap<GameType, GameTypeDto>();

            CreateMap<GameTypeDto, GameType>();

            CreateMap<Game, GameResponse>();

            CreateMap<GameUpdateDto, GameUpdate>();

            CreateMap<MatchDay, MatchDayResponse>();
        }
    }
}
