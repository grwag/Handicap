using System;

namespace Handicap.Domain.Models
{
    public class Game : BaseEntity {
        private readonly Random _rnd;
        public Player PlayerOne { get; set; }
        public Guid PlayerOneId { get; set; }
        public Player PlayerTwo { get; set; }
        public Guid PlayerTwoId { get; set; }
        public GameType Type { get; }
        public int PlayerOneRequiredPoints { get; set; }
        public int PlayerOnePoints { get; set; }
        public int PlayerTwoRequiredPoints { get; set; }
        public int PlayerTwoPoints { get; set; }
        public DateTimeOffset Date { get; set; }

        public Game()
        {
            Id = Guid.NewGuid();
            _rnd = new Random();
            PlayerOne = new Player();
            PlayerOnePoints = 0;
            PlayerOneRequiredPoints = 0;
            PlayerTwo = new Player();
            PlayerTwoPoints = 0;
            PlayerTwoRequiredPoints = 0;
            Date = DateTimeOffset.Now;
            Type = GameType.Eightball;
        }
    }
}