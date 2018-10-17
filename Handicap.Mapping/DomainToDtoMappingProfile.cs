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

            CreateMap<GameRequest, Game>(MemberList.Destination)
                .ForMember(dst => dst.PlayerOneId, opt => opt.MapFrom(g => g.PlayerOneId))
                .ForMember(dst => dst.PlayerTwoId, opt => opt.MapFrom(g => g.PlayerTwoId))
                .ForMember(dst => dst.Id, opt => opt.Ignore())
                .ForMember(dst => dst.PlayerOne, opt => opt.Ignore())
                .ForMember(dst => dst.PlayerOnePoints, opt => opt.Ignore())
                .ForMember(dst => dst.PlayerOneRequiredPoints, opt => opt.Ignore())
                .ForMember(dst => dst.PlayerTwo, opt => opt.Ignore())
                .ForMember(dst => dst.PlayerTwoPoints, opt => opt.Ignore())
                .ForMember(dst => dst.PlayerTwoRequiredPoints, opt => opt.Ignore())
                .ForMember(dst => dst.Date, opt => opt.Ignore())
                .ForMember(dst => dst.Type, opt => opt.Ignore());

            CreateMap<Game, GameResponse>(MemberList.Destination)
                .ForMember(dst => dst.Id, opt => opt.MapFrom(g => g.Id))
                .ForMember(dst => dst.Date, opt => opt.MapFrom(g => g.Date))
                .ForMember(dst => dst.GameType, opt => opt.MapFrom(g => g.Type))
                .ForMember(dst => dst.PlayerOne, opt => opt.Ignore())
                .ForMember(dst => dst.PlayerOnePoints, opt => opt.Ignore())
                .ForMember(dst => dst.PlayerOneRequiredPoints, opt => opt.Ignore())
                .ForMember(dst => dst.PlayerTwo, opt => opt.Ignore())
                .ForMember(dst => dst.PlayerTwoPoints, opt => opt.Ignore())
                .ForMember(dst => dst.PlayerTwoRequiredPoints, opt => opt.Ignore());
        }
    }
}
