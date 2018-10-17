using System;

namespace Handicap.Dbo
{
    public class GameDbo : BaseDbo
    {
        public Guid PlayerOneId { get; set; }
        public PlayerDbo PlayerOne { get; set; }
        public Guid PlayerTwoId { get; set; }
        public PlayerDbo PlayerTwo { get; set; }
        public int Type { get; }
        public int PlayerOneRequiredPoints { get; set; }
        public int PlayerOnePoints { get; set; }
        public int PlayerTwoRequiredPoints { get; set; }
        public int PlayerTwoPoints { get; set; }
    }
}