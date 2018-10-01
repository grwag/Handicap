using Handicap.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Handicap.Application.Entities
{
    public class MatchDay : BaseEntity
    {
        private readonly ICollection<Player> _players;
        private readonly int _numberOfTables;
        private readonly IHandicapCalculator _handicapCalculator;
        private readonly IQueueService _queueService;

        private Random _rnd;

        public DateTime Date { get; set; }

        public bool[] Tables { get; set; }


        public MatchDay(ICollection<Player> players, int numberOfTables, IHandicapCalculator handicapCalculator, IQueueService queueService)
        {
            _players = players;
            _numberOfTables = numberOfTables;
            _handicapCalculator = handicapCalculator;
            _queueService = queueService;

            _queueService.Setup(_players);

            _rnd = new Random();

            Date = DateTime.Now;

            Tables = new bool[numberOfTables];
            for (var i = 0; i < Tables.Length; i++)
            {
                Tables[i] = true;
            }
        }

        public Game GetNextGame()
        {
            var table = GetTable();
            if (table == -1)
            {
                return null;
            }

            if (!_queueService.IsQueueingPossible())
            {
                return null;
            }

            var playerOne = _queueService.GetNextPlayer();
            var playerTwo = _queueService.GetNextPlayer();

            var game = new Game(playerOne, playerTwo, _rnd);

            game.PlayerOneRequiredPoints = _handicapCalculator.Calculate(playerOne.Handicap, game.Type);
            game.PlayerTwoRequiredPoints = _handicapCalculator.Calculate(playerTwo.Handicap, game.Type);

            return game;
        }

        private int GetTable()
        {
            if (!Tables.Contains(true))
            {
                return -1;
            }

            var table = 0;
            for (var i = 0; i < Tables.Length; i++)
            {
                if (Tables[i])
                {
                    table = i;
                    Tables[i] = false;

                    break;
                }
            }

            return table;
        }
    }
}
