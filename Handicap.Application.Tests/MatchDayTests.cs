using FluentAssertions;
using Handicap.Application.Extensions;
using Handicap.Application.Services;
using Handicap.Domain.Models;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace Handicap.Application.Tests
{
    public class MatchDayTests
    {
        [Fact]
        public void GetNextPlayersReturnsFirstPlayersIfNoGamesExist()
        {
            var matchDay = new MatchDay
            {
                Date = DateTimeOffset.Now,
                Id = "md",
                Games = new List<Game>(),
                TenantId = "tenant",
                MatchDayPlayers = new List<MatchDayPlayer>()
                {
                    new MatchDayPlayer
                    {
                        PlayerId = "p1",
                        Player = new Player
                        {
                            FirstName = "a",
                            LastName = "b",
                            Handicap = 5,
                            TenantId = "tenant",
                            MatchDayPlayers = new List<MatchDayPlayer>(),
                            Id = "1",
                        },
                        MatchDay = null,
                        MatchDayId = null
                    },
                    new MatchDayPlayer
                    {
                        PlayerId = "p2",
                        Player = new Player
                        {
                            FirstName = "c",
                            LastName = "d",
                            Handicap = 5,
                            TenantId = "tenant",
                            MatchDayPlayers = new List<MatchDayPlayer>(),
                            Id = "2",
                        },
                        MatchDay = null,
                        MatchDayId = null
                    }
                }
            };

            var nextPlayers = matchDay.GetNextPlayers(matchDay.Games);

            nextPlayers.Should().NotBeNull();
            nextPlayers.PlayerOneId.Should().Be("1");
            nextPlayers.PlayerTwoId.Should().Be("2");
        }

        [Fact]
        public void GetNextPlayersPreferesPlayersWithLessGames()
        {
            var matchDay = new MatchDay
            {
                Date = DateTimeOffset.Now,
                Id = "md",
                Games = new List<Game>()
                { 
                    new Game
                    {
                        Id = "game",
                        Date = DateTimeOffset.Now,
                        IsFinished = true,
                        MatchDayId = "md",
                        PlayerOne = new Player
                        {
                            FirstName = "a",
                            LastName = "b",
                            Handicap = 5,
                            TenantId = "tenant",
                            MatchDayPlayers = new List<MatchDayPlayer>(),
                            Id = "1",
                        },
                        PlayerOnePoints = 1,
                        PlayerOneRequiredPoints = 1,
                        PlayerTwo = new Player
                        {
                            FirstName = "c",
                            LastName = "d",
                            Handicap = 5,
                            TenantId = "tenant",
                            MatchDayPlayers = new List<MatchDayPlayer>(),
                            Id = "2",
                        },
                        PlayerTwoPoints = 0,
                        PlayerTwoRequiredPoints = 5,
                        TenantId = "tenant",
                        Type = GameType.Eightball
                    }
                },
                TenantId = "tenant",
                MatchDayPlayers = new List<MatchDayPlayer>()
                {
                    new MatchDayPlayer
                    {
                        PlayerId = "p1",
                        Player = new Player
                        {
                            FirstName = "a",
                            LastName = "b",
                            Handicap = 5,
                            TenantId = "tenant",
                            MatchDayPlayers = new List<MatchDayPlayer>(),
                            Id = "1",
                        },
                        MatchDay = null,
                        MatchDayId = null
                    },
                    new MatchDayPlayer
                    {
                        PlayerId = "p2",
                        Player = new Player
                        {
                            FirstName = "c",
                            LastName = "d",
                            Handicap = 5,
                            TenantId = "tenant",
                            MatchDayPlayers = new List<MatchDayPlayer>(),
                            Id = "2",
                        },
                        MatchDay = null,
                        MatchDayId = null
                    },
                    new MatchDayPlayer
                    {
                        PlayerId = "p3",
                        Player = new Player
                        {
                            FirstName = "e",
                            LastName = "f",
                            Handicap = 5,
                            TenantId = "tenant",
                            MatchDayPlayers = new List<MatchDayPlayer>(),
                            Id = "3",
                        },
                        MatchDay = null,
                        MatchDayId = null
                    }
                }
            };

            var nextPlayers = matchDay.GetNextPlayers(matchDay.Games);

            nextPlayers.Should().NotBeNull();
            Assert.True(nextPlayers.PlayerOneId == "3" || nextPlayers.PlayerTwoId == "3");
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
