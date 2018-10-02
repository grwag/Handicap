using FluentAssertions;
using Handicap.Application.Entities;
using Handicap.Application.Services;
using Moq;
using System;
using System.Collections;
using System.Collections.Generic;
using Xunit;

namespace Handicap.Application.Tests
{
    public class MatchDayTests
    {
        [Fact]
        public void GetNextGame_UsesFirstAvailableTable()
        {
            var mockCalculator = new Mock<IHandicapCalculator>();
            var mockQueueService = new Mock<IQueueService>();

            mockCalculator.Setup(calc => 
                calc.Calculate(It.IsAny<int>(), It.IsAny<GameType>())).Returns(25);

            mockQueueService.Setup(queue =>
                queue.NextPlayer()).Returns(new Player()
                {
                    FirstName = "alf",
                    LastName = "wurst",
                    IsBusy = false
                });

            mockQueueService.Setup(queue =>
                queue.IsQueueingPossible()).Returns(true);

            var md = new MatchDay(GetPlayers(), 4, mockCalculator.Object, mockQueueService.Object);

            var game = md.GetNextGame();

            game.Should().NotBe(null);
            game.Table.Should().Be(0);
        }

        [Fact]
        public void GetNextGame_ReturnsNullIfNoTablesAreAvailable()
        {
            var mockCalculator = new Mock<IHandicapCalculator>();
            var mockQueueService = new Mock<IQueueService>();

            mockCalculator.Setup(calc =>
                calc.Calculate(It.IsAny<int>(), It.IsAny<GameType>())).Returns(25);

            var md = new MatchDay(GetPlayers(), 4, mockCalculator.Object, mockQueueService.Object);
            md.Tables = new bool[]{ false, false, false, false};

            var game = md.GetNextGame();

            game.Should().Be(null);
        }

        private ICollection<Player> GetPlayers()
        {
            return new List<Player>()
            {
                new Player()
                {
                    FirstName = "alf",
                    LastName = "aaalf",
                    Handicap = 30,
                    IsBusy = false
                },
                new Player()
                {
                    FirstName = "ralf",
                    LastName = "raaalf",
                    Handicap = 50,
                    IsBusy = false
                }
            };
        }
    }
}
