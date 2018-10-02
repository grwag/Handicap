using System;

namespace Handicap.Domain.Models
{
    public class Game : BaseEntity {
        private readonly Player _playerOne;
        private readonly Player _playerTwo;
        private readonly Random _rnd;

        public GameType Type { get; }
        public int PlayerOneRequiredPoints { get; set; }
        public int PlayerOnePoints { get; set; }
        public int PlayerTwoRequiredPoints { get; set; }
        public int PlayerTwoPoints { get; set; }
        public DateTimeOffset Date { get; set; }

        public Game(Player playerOne, Player playerTwo, Random rnd)
        {
            _playerOne = playerOne;
            _playerTwo = playerTwo;
            _rnd = rnd;

            Type = GetGameType();

            PlayerOnePoints = 0;
            PlayerTwoPoints = 0;

            Date = DateTimeOffset.Now;
        }

        private GameType GetGameType()
        {
            var types = Enum.GetValues(typeof(GameType));
            return (GameType)types.GetValue(_rnd.Next(types.Length));
        }
    }
}