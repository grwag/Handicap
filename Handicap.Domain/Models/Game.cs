using System;

namespace Handicap.Domain.Models
{
    public class Game : BaseEntity {
        public Player PlayerOne { get; set; }
        public Player PlayerTwo { get; set; }
        public GameType Type { get; set; }
        public int PlayerOneRequiredPoints { get; set; }
        public int PlayerOnePoints { get; set; }
        public int PlayerTwoRequiredPoints { get; set; }
        public int PlayerTwoPoints { get; set; }
        public string MatchDayId { get; set; }
        public DateTimeOffset Date { get; set; }
        public bool IsFinished { get; set; }

        public Game()
        {
            Id = Guid.NewGuid().ToString();
            PlayerOne = null;
            PlayerOnePoints = 0;
            PlayerOneRequiredPoints = 0;
            PlayerTwo = null;
            PlayerTwoPoints = 0;
            PlayerTwoRequiredPoints = 0;
            MatchDayId = string.Empty;
            Date = DateTimeOffset.Now;
            Type = GameType.Eightball;
            IsFinished = false;
        }

        public void SetGameType()
        {
            var values = Enum.GetValues(typeof(GameType));
            var rnd = new Random();

            var type = (GameType)values.GetValue(rnd.Next(values.Length));
            this.Type = type;
        }
    }
}