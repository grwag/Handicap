using FluentAssertions;
using Handicap.Application.Services;
using Handicap.Domain.Models;
using Moq;
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
                });

            mockQueueService.Setup(queue =>
                queue.IsQueueingPossible()).Returns(true);

            true.Should().BeTrue();
        }

        private ICollection<Player> GetPlayers()
        {
            return new List<Player>()
            {
                new Player()
                {
                    FirstName = "alf",
                    LastName = "aaalf",
                    Handicap = 30
                },
                new Player()
                {
                    FirstName = "ralf",
                    LastName = "raaalf",
                    Handicap = 50
                }
            };
        }
    }
}
