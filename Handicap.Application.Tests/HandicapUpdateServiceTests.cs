using FluentAssertions;
using Handicap.Application.Interfaces;
using Handicap.Application.Services;
using Handicap.Domain.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Handicap.Application.Tests
{
    public class HandicapUpdateServiceTests
    {
        [Fact]
        public void HandicapIsUpdatedCorrectly()
        {
            var mockPlayerRepository = new Mock<IPlayerRepository>();
            var handicapUpdateService = new HandicapUpdateService(mockPlayerRepository.Object);
            var game = GetGame();

            game = handicapUpdateService.UpdatePlayerHandicap(game);
            game.PlayerOne.Handicap.Should().Be(50);
            game.PlayerTwo.Handicap.Should().Be(60);
        }

        [Fact]
        public void HandicapDoesNotGoBelowZero()
        {
            var mockPlayerRepository = new Mock<IPlayerRepository>();
            var handicapUpdateService = new HandicapUpdateService(mockPlayerRepository.Object);
            var game = GetGame();

            game.PlayerOne.Handicap = 0;

            game = handicapUpdateService.UpdatePlayerHandicap(game);
            game.PlayerOne.Handicap.Should().Be(0);
            game.PlayerTwo.Handicap.Should().Be(60);
        }

        [Fact]
        public void HandicapDoesNotGoAboveHundred()
        {
            var mockPlayerRepository = new Mock<IPlayerRepository>();
            var handicapUpdateService = new HandicapUpdateService(mockPlayerRepository.Object);
            var game = GetGame();

            game.PlayerTwo.Handicap = 100;

            game = handicapUpdateService.UpdatePlayerHandicap(game);
            game.PlayerOne.Handicap.Should().Be(50);
            game.PlayerTwo.Handicap.Should().Be(100);
        }

        [Fact]
        public void HandicapGoesToZero()
        {
            var mockPlayerRepository = new Mock<IPlayerRepository>();
            var handicapUpdateService = new HandicapUpdateService(mockPlayerRepository.Object);
            var game = GetGame();

            game.PlayerOne.Handicap = 5;

            game = handicapUpdateService.UpdatePlayerHandicap(game);
            game.PlayerOne.Handicap.Should().Be(0);
            game.PlayerTwo.Handicap.Should().Be(60);
        }

        [Fact]
        public void HandicapGoesToHundred()
        {
            var mockPlayerRepository = new Mock<IPlayerRepository>();
            var handicapUpdateService = new HandicapUpdateService(mockPlayerRepository.Object);
            var game = GetGame();

            game.PlayerTwo.Handicap = 95;

            game = handicapUpdateService.UpdatePlayerHandicap(game);
            game.PlayerOne.Handicap.Should().Be(50);
            game.PlayerTwo.Handicap.Should().Be(100);
        }

        [Fact]
        public async Task MatchDayIsUpdatedCorrectly()
        {
            var game = GetGame();
            var matchDay = GetMatchDay();

            var mockPlayerRepository = new Mock<IPlayerRepository>();
            mockPlayerRepository.SetupSequence(
                repo => repo.Find(It.IsAny<Expression<Func<Player, bool>>>()))
                .Returns(Task.FromResult(new List<Player>() { game.PlayerOne }.AsQueryable()))
                .Returns(Task.FromResult(new List<Player>() { game.PlayerTwo }.AsQueryable()));

            mockPlayerRepository.Setup(
                repo => repo.AddOrUpdate(It.IsAny<Player>()))
                .Returns(Task.FromResult(new Player()));

            var handicapUpdateService = new HandicapUpdateService(mockPlayerRepository.Object);

            matchDay = await handicapUpdateService.UpdateMatchDayHandicaps(matchDay);

            matchDay.Should().NotBeNull();
        }

        private Game GetGame(){
            return new Game{
                Date = DateTimeOffset.Now,
                Id = "1",
                TenantId = "tenant",
                IsFinished = true,
                MatchDayId = "matchDay",
                Type = GameType.Eightball,
                PlayerOne = new Player{
                    FirstName = "Hans",
                    LastName  =  "Maulwurf",
                    Handicap = 55,
                    Id = "p1",
                    MatchDayPlayers = null,
                    TenantId = "tenant"
                },
                PlayerTwo = new Player{
                    FirstName = "Alfred",
                    LastName  =  "Tetzlaf",
                    Handicap = 55,
                    Id = "p2",
                    MatchDayPlayers = null,
                    TenantId = "tenant"
                },
                PlayerOneId = "p1",
                PlayerTwoId = "p2",
                PlayerOnePoints = 5,
                PlayerOneRequiredPoints = 5,
                PlayerTwoPoints = 0,
                PlayerTwoRequiredPoints = 5
            };
        }

        private MatchDay GetMatchDay()
        {
            var game = GetGame();
            var game2 = GetGame();

            return new MatchDay
            {
                Id = "matchDay",
                Date = DateTimeOffset.Now,
                Games = new List<Game>()
                {
                    game,
                    game2
                },
                IsFinished = false,
                TenantId = "tenant",
                MatchDayPlayers = new List<MatchDayPlayer>()
            };
        }
    }
}
