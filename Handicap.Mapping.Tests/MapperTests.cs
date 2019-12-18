using Microsoft.Extensions.DependencyInjection;
using Xunit;
using AutoMapper;
using System;
using Handicap.Dto.Request;
using Handicap.Domain.Models;
using FluentAssertions;
using Handicap.Dto.Response;
using Handicap.Mapping;
using System.Linq;
using System.Collections.Generic;
//using Handicap.Dbo;
using AutoMapper.QueryableExtensions;

namespace Handicap.Mapping.Tests
{
    public class MapperTests
    {
        private readonly IServiceProvider provider;
        private readonly string tenantId = "tenantId";
        private readonly Guid playerOneId = Guid.Parse("{9E923FF8-1EFE-4E13-B4CE-2BF3D3260244}");
        private readonly Guid playerTwoId = Guid.Parse("{9E923FF8-1EFE-4E13-B4CE-2BF3D3260244}");

        public MapperTests()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddAutoMapper(typeof(DomainToDtoMappingProfile));
            this.provider = services.BuildServiceProvider();
        }

        [Fact]
        public void ShouldAssertConfigurationIsValid()
        {
            var mapper = this.provider.GetService<IMapper>();
            mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }

        //[Fact]
        //public void GameIsMappedToGameResponse()
        //{
        //    var mapper = this.provider.GetService<IMapper>();
        //    var game = GetGame();
        //    var expected = GetGameResponse();

        //    var actual = mapper.Map<GameResponse>(game);

        //    actual.Should().BeEquivalentTo(expected);
        //}

        //[Fact]
        //public void QueryablesAreMappedCorrectly()
        //{
        //    var mapper = this.provider.GetService<IMapper>();
        //    var gameQuery = new List<Game> { GetGame() }.AsQueryable();
        //    var expected = new List<GameResponse> { GetGameResponse() }.AsQueryable();

        //    var actual = gameQuery.ProjectTo<GameResponse>(mapper.ConfigurationProvider);

        //    actual.Should().BeEquivalentTo(expected);
        //}

        //[Fact]
        //public void PlayerIsMappedToMatchDayPlayer()
        //{
        //    var mapper = this.provider.GetService<IMapper>();
        //    var player = new Player
        //    {
        //        TenantId = tenantId,
        //        FirstName = "a",
        //        LastName = "b",
        //        Handicap = 25,
        //        Id = "11"
        //    };

        //    var playerDbo = new PlayerDbo
        //    {
        //        TenantId = tenantId,
        //        FirstName = "a",
        //        LastName = "b",
        //        Handicap = 25,
        //        Id = "11"
        //    };

        //    var expectedMatchDayPlayer = new MatchDayPlayer
        //    {
        //        Player = playerDbo,
        //        PlayerId = player.Id,
        //        MatchDay = null,
        //        MatchDayId = null
        //    };

        //    var actual = mapper.Map<MatchDayPlayer>(player);

        //    actual.Should().BeEquivalentTo(expectedMatchDayPlayer);
        //}

        //[Fact]
        //public void MatchDayIsMappedToMatchDayDbo()
        //{
        //    var mapper = this.provider.GetService<IMapper>();
        //    //var players = GetPlayerQuery().ToList();
        //    var game = GetGame();
        //    var matchDay = new MatchDay
        //    {
        //        Id = "alf",
        //        TenantId = tenantId,
        //        //Players = players,
        //        Games = new List<Game>()
        //        {
        //            game
        //        }
        //    };

        //    var mapped = mapper.Map<MatchDayDbo>(matchDay);

        //    mapped.Games.Should().NotBeNull();
        //    //mapped.MatchDayPlayers.Should().NotBeNull();
        //    //mapped.MatchDayPlayers.Count.Should().Be(2);
        //}

        private IQueryable<Player> GetPlayerQuery()
        {
            var players = new List<Player>()
            {
                new Player
                {
                    FirstName = "alf",
                    Handicap = 25,
                    LastName = "ralf",
                    Id = "111",
                    TenantId = tenantId
                },
                new Player
                {
                    FirstName = "hans",
                    Handicap = 25,
                    LastName = "maulwurf",
                    Id = "222",
                    TenantId = tenantId
                }
            };

            return players.AsQueryable();
        }

        //private IQueryable<PlayerDbo> GetPlayerDboQuery()
        //{
        //    var players = new List<PlayerDbo>()
        //    {
        //        new PlayerDbo
        //        {
        //            FirstName = "alf",
        //            Handicap = 25,
        //            LastName = "ralf",
        //            Id = "111",
        //        },
        //        new PlayerDbo
        //        {
        //            FirstName = "hans",
        //            Handicap = 25,
        //            LastName = "maulwurf",
        //            Id = "222"
        //        }
        //    };

        //    return players.AsQueryable();
        //}

        //private Game GetGame()
        //{
        //    var players = GetPlayerQuery().ToList();

        //    return new Game
        //    {
        //        Id = "1",
        //        Date = new DateTimeOffset(),
        //        IsFinished = false,
        //        PlayerOnePoints = 0,
        //        PlayerOneRequiredPoints = 10,
        //        PlayerTwoPoints = 1,
        //        PlayerTwoRequiredPoints = 11,
        //        TenantId = tenantId,
        //        Type = GameType.Eightball,
        //        PlayerOne = players[0],
        //        PlayerTwo = players[1]
        //    };
        //}

        //private GameResponse GetGameResponse()
        //{
        //    return new GameResponse
        //    {
        //        Id = "1",
        //        Date = new DateTimeOffset(),
        //        IsFinished = false,
        //        PlayerOnePoints = 0,
        //        PlayerOneRequiredPoints = 10,
        //        PlayerTwoPoints = 1,
        //        PlayerTwoRequiredPoints = 11,
        //        TenantId = tenantId,
        //        Type = GameTypeDto.Eightball,
        //        PlayerOne = new PlayerResponse
        //        {
        //            TenantId = "123",
        //            FirstName = "a",
        //            LastName = "b",
        //            Handicap = 25,
        //            Id = "11"
        //        },
        //        PlayerTwo = new PlayerResponse
        //        {
        //            TenantId = "123",
        //            FirstName = "c",
        //            LastName = "d",
        //            Handicap = 55,
        //            Id = "22"
        //        }
        //    };
        //}
    }
}
