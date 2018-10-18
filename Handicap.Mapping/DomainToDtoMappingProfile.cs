﻿using AutoMapper;
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
            CreateMap<PlayerRequest, Player>()
                .ForMember(dst => dst.Id, opt => opt.Ignore());

            CreateMap<Player, PlayerResponse>();

            CreateMap<GameType, GameTypeDto>();

            CreateMap<GameTypeDto, GameType>();

            CreateMap<Game, GameResponse>(MemberList.Destination);
        }
    }
}
