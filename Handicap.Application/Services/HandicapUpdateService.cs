using Handicap.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Handicap.Application.Services
{
    public class HandicapUpdateService : IHandicapUpdateService
    {
        public Game UpdatePlayerHandicap(Game game)
        {
            if (game.PlayerOnePoints >= game.PlayerOneRequiredPoints)
            {
                game.PlayerOne.Handicap = (game.PlayerOne.Handicap - 5 < 5)
                    ? game.PlayerOne.Handicap
                    : game.PlayerOne.Handicap - 5;
                game.PlayerTwo.Handicap = (game.PlayerTwo.Handicap + 5 > 100)
                    ? game.PlayerTwo.Handicap
                    : game.PlayerTwo.Handicap + 5;
            }
            else
            {
                game.PlayerOne.Handicap = (game.PlayerOne.Handicap + 5 > 100)
                    ? game.PlayerOne.Handicap
                    : game.PlayerOne.Handicap + 5;
                game.PlayerTwo.Handicap = (game.PlayerTwo.Handicap - 5 < 5)
                    ? game.PlayerTwo.Handicap
                    : game.PlayerTwo.Handicap - 5;
            }

            return game;
        }
    }
}
