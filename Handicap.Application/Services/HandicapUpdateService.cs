using Handicap.Application.Interfaces;
using Handicap.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handicap.Application.Services
{
    public class HandicapUpdateService : IHandicapUpdateService
    {
        private readonly IPlayerRepository _playerRepository;

        public HandicapUpdateService(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        public Game UpdatePlayerHandicap(Game game)
        {
            if (game.PlayerOnePoints >= game.PlayerOneRequiredPoints)
            {
                game.PlayerOne.Handicap = (game.PlayerOne.Handicap - 5 < 0)
                    ? game.PlayerOne.Handicap
                    : game.PlayerOne.Handicap - 5;
                game.PlayerTwo.Handicap = (game.PlayerTwo.Handicap + 5 > 95)
                    ? game.PlayerTwo.Handicap
                    : game.PlayerTwo.Handicap + 5;
            }
            else
            {
                game.PlayerOne.Handicap = (game.PlayerOne.Handicap + 5 > 95)
                    ? game.PlayerOne.Handicap
                    : game.PlayerOne.Handicap + 5;
                game.PlayerTwo.Handicap = (game.PlayerTwo.Handicap - 5 < 0)
                    ? game.PlayerTwo.Handicap
                    : game.PlayerTwo.Handicap - 5;
            }

            return game;
        }

        public async Task<MatchDay> UpdateMatchDayHandicaps(MatchDay matchDay)
        {
            var playerResults = GetPlayersResults(matchDay);

            foreach (var playerResult in playerResults)
            {
                var player = (await _playerRepository.Find(p => p.Id == playerResult.PlayerId)).FirstOrDefault();
                if (player != null)
                {
                    if (player.Handicap + playerResult.HandicapToAdd > 95)
                    {
                        player.Handicap = 95;
                    }
                    else if (player.Handicap + playerResult.HandicapToAdd < 0)
                    {
                        player.Handicap = 0;
                    }
                    else
                    {
                        player.Handicap += playerResult.HandicapToAdd;
                    }

                    await _playerRepository.AddOrUpdate(player);
                }
            }

            return matchDay;
        }

        private List<(string PlayerId, int HandicapToAdd)> GetPlayersResults(MatchDay matchDay)
        {
            var results = new List<(string PlayerId, int HandicapToAdd)>();

            var playerIds = matchDay.Games.Select(g => g.PlayerOneId).ToList();
            playerIds.AddRange(matchDay.Games.Select(g => g.PlayerTwoId).ToList());

            playerIds = playerIds.Distinct().ToList();



            foreach (var playerId in playerIds)
            {
                var handicapToAdd = 0;
                foreach (var game in matchDay.Games)
                {
                    handicapToAdd += HandicapToAdd(playerId, game);
                }

                results.Add((PlayerId: playerId, HandicapToAdd: handicapToAdd));
            }

            return results;
        }

        private int HandicapToAdd(string playerId, Game game)
        {
            if (game.PlayerOneId != playerId && game.PlayerTwoId != playerId) { return 0; }

            if (game.PlayerOnePoints >= game.PlayerOneRequiredPoints)
            {
                if (playerId == game.PlayerOneId)
                {
                    return -5;
                }

                return 5;
            }
            else
            {
                if (playerId == game.PlayerTwoId)
                {
                    return -5;
                }

                return 5;
            }
        }
    }
}
