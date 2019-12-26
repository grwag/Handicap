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

        [Fact]
        public void MatchDayPlayerMapsToPlayerResponse()
        {
            var mapper = this.provider.GetService<IMapper>();
            var player = GetPlayerQuery().ToList()[0];

            var matchDayPlayer = new MatchDayPlayer
            {
                MatchDay = new MatchDay(),
                MatchDayId = "1",
                Player = player,
                PlayerId = player.Id
            };

            var playerResponse = mapper.Map<PlayerResponse>(matchDayPlayer);

            playerResponse.Should().NotBeNull();
            playerResponse.FirstName.Should().Be(player.FirstName);
            playerResponse.LastName.Should().Be(player.LastName);
            playerResponse.Handicap.Should().Be(player.Handicap);
            playerResponse.Id.Should().Be(player.Id);
            playerResponse.TenantId.Should().Be(player.TenantId);
        }

        [Fact]
        public void MatchDayPlayerMapsToMatchDayResponse()
        {
            var mapper = this.provider.GetService<IMapper>();
            var player = GetPlayerQuery().ToArray()[0];
            var matchDay = GetMatchDay();

            var matchDayPlayer = GetMatchDayPlayer();

            var matchDayResponse = mapper.Map<MatchDayResponse>(matchDayPlayer);

            matchDayResponse.Should().NotBeNull();
            matchDayResponse.Id.Should().Be(matchDay.Id);
            matchDayResponse.TenantId.Should().Be(matchDay.TenantId);
        }

        [Fact]
        public void GameMapsToGameResponse()
        {
            var mapper = this.provider.GetService<IMapper>();
            var game = GetGame();

            var mappedGameResponse = mapper.Map<GameResponse>(game);

            mappedGameResponse.Should().NotBeNull();
            mappedGameResponse.IsFinished.Should().Be(game.IsFinished);
            mappedGameResponse.Id.Should().Be(game.Id);
            mappedGameResponse.MatchDayId.Should().Be(game.MatchDayId);
            mappedGameResponse.PlayerOnePoints.Should().Be(game.PlayerOnePoints);
            mappedGameResponse.PlayerOneRequiredPoints.Should().Be(game.PlayerOneRequiredPoints);
            mappedGameResponse.PlayerTwoPoints.Should().Be(game.PlayerTwoPoints);
            mappedGameResponse.PlayerTwoRequiredPoints.Should().Be(game.PlayerTwoRequiredPoints);
        }

        [Fact]
        public void PlayerMapsToPlayerResponse()
        {
            var mapper = this.provider.GetService<IMapper>();
            var player = GetPlayerQuery().ToArray()[0];

            var mappedPlayerResponse = mapper.Map<PlayerResponse>(player);

            mappedPlayerResponse.Should().NotBeNull();
            mappedPlayerResponse.FirstName.Should().Be(player.FirstName);
            mappedPlayerResponse.LastName.Should().Be(player.LastName);
            mappedPlayerResponse.Handicap.Should().Be(player.Handicap);
            mappedPlayerResponse.Id.Should().Be(player.Id);
            mappedPlayerResponse.TenantId.Should().Be(player.TenantId);
        }

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
                    TenantId = tenantId,
                    MatchDayPlayers = new List<MatchDayPlayer>()
                },
                new Player
                {
                    FirstName = "hans",
                    Handicap = 25,
                    LastName = "maulwurf",
                    Id = "222",
                    TenantId = tenantId,
                    MatchDayPlayers = new List<MatchDayPlayer>()
                }
            };

            return players.AsQueryable();
        }

        private Game GetGame()
        {
            var players = GetPlayerQuery().ToList();

            return new Game
            {
                Id = "1",
                Date = new DateTimeOffset(),
                IsFinished = false,
                PlayerOnePoints = 0,
                PlayerOneRequiredPoints = 10,
                PlayerTwoPoints = 1,
                PlayerTwoRequiredPoints = 11,
                TenantId = tenantId,
                Type = GameType.Eightball,
                PlayerOne = players[0],
                PlayerTwo = players[1]
            };
        }

        private MatchDayPlayer GetMatchDayPlayer()
        {
            var players = GetPlayerQuery().ToArray();
            var matchDay = GetMatchDay();

            var matchDayPlayer = new MatchDayPlayer
            {
                Player = players[0],
                PlayerId = players[0].Id,
                MatchDay = matchDay,
                MatchDayId = matchDay.Id
            };


            return matchDayPlayer;
        }

        private MatchDay GetMatchDay()
        {
            var players = GetPlayerQuery().ToArray();
            var matchDay = new MatchDay
            {
                Date = new DateTimeOffset(),
                Games = new List<Game> { GetGame() },
                Id = "123",
                MatchDayPlayers = new List<MatchDayPlayer>
                {
                    new MatchDayPlayer
                    {
                        Player = players[0],
                        PlayerId = players[0].Id,
                        MatchDay = null,
                        MatchDayId = null
                    }
                },
                TenantId = tenantId
            };

            return matchDay;
        }

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
