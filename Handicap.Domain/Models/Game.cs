using System;

namespace Handicap.Domain.Models
{
    public class Game : BaseEntity {
        public Player PlayerOne { get; set; }
        public Player PlayerTwo { get; set; }
        public GameType Type { get; }
        public int PlayerOneRequiredPoints { get; set; }
        public int PlayerOnePoints { get; set; }
        public int PlayerTwoRequiredPoints { get; set; }
        public int PlayerTwoPoints { get; set; }
        public DateTimeOffset Date { get; set; }

        public Game()
        {
            Id = Guid.NewGuid();
            PlayerOne = new Player();
            PlayerOnePoints = 0;
            PlayerOneRequiredPoints = 0;
            PlayerTwo = new Player();
            PlayerTwoPoints = 0;
            PlayerTwoRequiredPoints = 0;
            Date = DateTimeOffset.Now;
            Type = GetGameType();
        }

        private GameType GetGameType()
        {
            var values = Enum.GetValues(typeof(GameType));
            var rnd = new Random();

            return (GameType)values.GetValue(rnd.Next(values.Length));
        }
    }
}