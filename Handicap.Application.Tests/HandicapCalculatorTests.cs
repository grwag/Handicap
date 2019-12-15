using FluentAssertions;
using Handicap.Application.Services;
using Handicap.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Handicap.Application.Tests
{
    public class HandicapCalculatorTests
    {
        [Fact]
        public void HandicapIsFive()
        {
            var calc = new HandicapCalculator();
            var handicap = 60;
            var gameType = GameType.Nineball;
            var expected = 4;

            var actual = calc.Calculate(handicap, gameType);

            actual.Should().Be(expected);
        }
    }
}
