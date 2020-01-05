using FluentAssertions;
using Handicap.Application.Services;
using Handicap.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Handicap.Application.Tests
{
    public class HandicapUpdateServiceTests
    {
        [Fact]
        public void HandicapIsUpdatedCorrectly()
        {
            var handicapUpdateService = new HandicapUpdateService();
            var game = GetGame();

            game = handicapUpdateService.UpdatePlayerHandicap(game);
            game.PlayerOne.Handicap.Should().Be(50);
            game.PlayerTwo.Handicap.Should().Be(60);
        }

        [Fact]
        public void HandicapDoesNotGoBelowZero()
        {
            var handicapUpdateService = new HandicapUpdateService();
            var game = GetGame();

            game.PlayerOne.Handicap = 0;

            game = handicapUpdateService.UpdatePlayerHandicap(game);
            game.PlayerOne.Handicap.Should().Be(0);
            game.PlayerTwo.Handicap.Should().Be(60);
        }

        [Fact]
        public void HandicapDoesNotGoAboveHundred()
        {
            var handicapUpdateService = new HandicapUpdateService();
            var game = GetGame();

            game.PlayerTwo.Handicap = 100;

            game = handicapUpdateService.UpdatePlayerHandicap(game);
            game.PlayerOne.Handicap.Should().Be(50);
            game.PlayerTwo.Handicap.Should().Be(100);
        }

        [Fact]
        public void HandicapGoesToZero()
        {
            var handicapUpdateService = new HandicapUpdateService();
            var game = GetGame();

            game.PlayerOne.Handicap = 5;

            game = handicapUpdateService.UpdatePlayerHandicap(game);
            game.PlayerOne.Handicap.Should().Be(0);
            game.PlayerTwo.Handicap.Should().Be(60);
        }

        [Fact]
        public void HandicapGoesToHundred()
        {
            var handicapUpdateService = new HandicapUpdateService();
            var game = GetGame();

            game.PlayerTwo.Handicap = 95;

            game = handicapUpdateService.UpdatePlayerHandicap(game);
            game.PlayerOne.Handicap.Should().Be(50);
            game.PlayerTwo.Handicap.Should().Be(100);
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
    }
}
