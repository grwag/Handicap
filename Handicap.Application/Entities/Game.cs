namespace Handicap.Application.Entities{
    public class Game : BaseEntity {
        public GameTyp Type { get; }
        public Player PlayerOne { get; set; }
        public Player PlayerTwo { get; set; }

        public int PlayerOneDesiredPoints { get; set; }
        public int PlayerOnePoints { get; set; }
        public int PlayerTwoDesiredPoints { get; set; }
        public int PlayerTwoPoints { get; set; }

        public bool IsFinished { get; set; }
    }

    public enum GameTyp{
        EIGHTBALL,
        NINEBALL,
        TENBALL,
        STRAIGHTPOOL
    }
}