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
using Handicap.Dbo;
using AutoMapper.QueryableExtensions;

namespace Handicap.Mapping.Tests
{
    public class MapperTests
    {
        private readonly IServiceProvider provider;
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
        public void GameIsMappedToGameResponse()
        {
            var mapper = this.provider.GetService<IMapper>();
            var game = GetGame();
            var expected = GetGameResponse();

            var actual = mapper.Map<GameResponse>(game);

            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void QueryablesAreMappedCorrectly()
        {
            var mapper = this.provider.GetService<IMapper>();
            var gameQuery = new List<Game> { GetGame() }.AsQueryable();
            var expected = new List<GameResponse> { GetGameResponse() }.AsQueryable();

            var actual = gameQuery.ProjectTo<GameResponse>(mapper.ConfigurationProvider);

            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void ExpressionsAreMappedCorrectly()
        {

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
                    Id = "111"
                },
                new Player
                {
                    FirstName = "hans",
                    Handicap = 25,
                    LastName = "maulwurf",
                    Id = "222"
                }
            };

            return players.AsQueryable();
        }

        private IQueryable<PlayerDbo> GetPlayerDboQuery()
        {
            var players = new List<PlayerDbo>()
            {
                new PlayerDbo
                {
                    FirstName = "alf",
                    Handicap = 25,
                    LastName = "ralf",
                    Id = "111",
                },
                new PlayerDbo
                {
                    FirstName = "hans",
                    Handicap = 25,
                    LastName = "maulwurf",
                    Id = "222"
                }
            };

            return players.AsQueryable();
        }

        private Game GetGame()
        {
            return new Game
            {
                Id = "1",
                Date = new DateTimeOffset(),
                IsFinished = false,
                PlayerOnePoints = 0,
                PlayerOneRequiredPoints = 10,
                PlayerTwoPoints = 1,
                PlayerTwoRequiredPoints = 11,
                MatchDayId = "a",
                TenantId = "123",
                Type = GameType.Eightball,
                PlayerOne = new Player
                {
                    TenantId = "123",
                    FirstName = "a",
                    LastName = "b",
                    Handicap = 25,
                    Id = "11"
                },
                PlayerTwo = new Player
                {
                    TenantId = "123",
                    FirstName = "c",
                    LastName = "d",
                    Handicap = 55,
                    Id = "22"
                }
            };
        }

        private GameResponse GetGameResponse()
        {
            return new GameResponse
            {
                Id = "1",
                Date = new DateTimeOffset(),
                IsFinished = false,
                PlayerOnePoints = 0,
                PlayerOneRequiredPoints = 10,
                PlayerTwoPoints = 1,
                PlayerTwoRequiredPoints = 11,
                MatchDayId = "a",
                TenantId = "123",
                Type = GameTypeDto.Eightball,
                PlayerOne = new PlayerResponse
                {
                    TenantId = "123",
                    FirstName = "a",
                    LastName = "b",
                    Handicap = 25,
                    Id = "11"
                },
                PlayerTwo = new PlayerResponse
                {
                    TenantId = "123",
                    FirstName = "c",
                    LastName = "d",
                    Handicap = 55,
                    Id = "22"
                }
            };
        }
    }
}
