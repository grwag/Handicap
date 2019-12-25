using Handicap.Application.Exceptions;
using Handicap.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Handicap.Application.Extensions
{
    public static class MatchDayExtensions
    {
        public static (string PlayerOneId, string PlayerTwoId) GetNextPlayers(this MatchDay matchDay, ICollection<Game> games)
        {
            var nextPlayers = (PlayerOneId: "", PlayerTwoId: "");
            var players = matchDay.GetNextRoundPlayers(games);

            if (players.Count < 2)
            {
                throw new NotEnoughPlayersException($"MatchDay {matchDay.Id} has not enough Players to start a match. Add some players to the MatchDay.");
            }

            if (players.Count == 2)
            {
                nextPlayers.PlayerOneId = players.ElementAt(0).Id;
                nextPlayers.PlayerTwoId = players.ElementAt(1).Id;
                return nextPlayers;
            }

            if (matchDay.Games.Count == 0)
            {
                nextPlayers.PlayerOneId = players.ElementAt(0).Id;
                nextPlayers.PlayerTwoId = players.ElementAt(1).Id;
                return nextPlayers;
            }

            players = players.GroupBy(p => p.Id)
                .OrderBy(g => g.Count())
                .SelectMany(p => p)
                .ToList();

            nextPlayers.PlayerOneId = players.ElementAt(0).Id;
            players = players.Where(p => p.Id != nextPlayers.PlayerOneId).ToList();

            var groups = players.GroupBy(p => p.Id)
                .OrderBy(g => g.Count());

            nextPlayers.PlayerTwoId = GetRandomSecondPlayer(groups);

            return nextPlayers;
        }

        private static ICollection<Player> GetAvailablePlayers(this MatchDay matchDay)
        {
            var players = matchDay.MatchDayPlayers.Select(md => md.Player).ToList();

            return players;
        }

        private static ICollection<Player> GetPlayersByGames(this MatchDay matchDay, ICollection<Game> games)
        {
            var players = games.Select(g => g.PlayerOne).ToList();
            players.AddRange(games.Select(g => g.PlayerTwo).ToList());

            return players;
        }

        private static ICollection<Player> GetNextRoundPlayers(this MatchDay matchDay, ICollection<Game> games)
        {
            var availablePlayers = matchDay.GetAvailablePlayers();
            var players = matchDay.GetPlayersByGames(games);

            foreach (var player in players)
            {
                var playerExists = availablePlayers.FirstOrDefault(p => p.Id == player.Id) != null;
                if (playerExists)
                {
                    availablePlayers.Add(player);
                }
            }

            return availablePlayers;
        }

        private static string GetRandomSecondPlayer(IOrderedEnumerable<IGrouping<string, Player>> groups)
        {
            if (groups.ElementAt(0).Count() == 1)
            {
                return groups.ElementAt(0).ElementAt(0).Id;
            }

            var rnd = new Random();
            var combinedGroups = groups.Where(g => g.Count() == groups.ElementAt(0).Count());

            var players = combinedGroups.SelectMany(p => p).ToArray();

            var player = players[rnd.Next(players.Length)];

            return player.Id;
        }
    }
}
