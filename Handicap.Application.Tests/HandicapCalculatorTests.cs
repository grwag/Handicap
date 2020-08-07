using FluentAssertions;
using Handicap.Application.Services;
using Handicap.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;
using System.Threading.Tasks;

namespace Handicap.Application.Tests
{
    public class HandicapCalculatorTests
    {
        [Fact]
        public void HandicapIsFive()
        {
            var calc = new HandicapCalculator();
            var handicap = 60;
            var gameType = (int)GameType.Nineball;
            var expected = 4;

            var actual = calc.Calculate(handicap, gameType);

            actual.Should().Be(expected);
        }

        [Fact]
        public void StraightPoolMaxIs75HandicapIs5(){
            var config = new HandicapConfiguration();
            config.StraigntPoolMax = 75;
            var mockConfigservice = new Mock<IHandicapConfigurationService>();
            mockConfigservice.Setup(svc => svc.Get(It.IsAny<string>())).Returns(
                Task.FromResult(config)
            );

            var calc = new HandicapCalculator();
            var handicap = 5;
            
            var actual = calc.Calculate(handicap, config.StraigntPoolMax);

            actual.Should().Be(72);
        }

        [Fact]
        public void StraightPoolMaxIs75HandicapIs0(){
            var config = new HandicapConfiguration();
            config.StraigntPoolMax = 75;
            var mockConfigservice = new Mock<IHandicapConfigurationService>();
            mockConfigservice.Setup(svc => svc.Get(It.IsAny<string>())).Returns(
                Task.FromResult(config)
            );

            var calc = new HandicapCalculator();
            var handicap = 0;
            
            var actual = calc.Calculate(handicap, config.StraigntPoolMax);

            actual.Should().Be(75);
        }

        [Fact]
        public void StraightPoolMaxIs75HandicapIs85(){
            var config = new HandicapConfiguration();
            config.StraigntPoolMax = 75;
            var mockConfigservice = new Mock<IHandicapConfigurationService>();
            mockConfigservice.Setup(svc => svc.Get(It.IsAny<string>())).Returns(
                Task.FromResult(config)
            );

            var calc = new HandicapCalculator();
            var handicap = 85;
            
            var actual = calc.Calculate(handicap, config.StraigntPoolMax);

            actual.Should().Be(19);
        }

        [Fact]
        public void StraightPoolMaxIs75HandicapIs100(){
            var config = new HandicapConfiguration();
            config.StraigntPoolMax = 75;
            var mockConfigservice = new Mock<IHandicapConfigurationService>();
            mockConfigservice.Setup(svc => svc.Get(It.IsAny<string>())).Returns(
                Task.FromResult(config)
            );

            var calc = new HandicapCalculator();
            var handicap = 100;
            
            var actual = calc.Calculate(handicap, config.StraigntPoolMax);

            actual.Should().Be(8);
        }
    }
}
