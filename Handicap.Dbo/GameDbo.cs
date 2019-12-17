using System;

namespace Handicap.Dbo
{
    public class GameDbo : BaseDbo
    {
        public string PlayerOneId { get; set; }
        public virtual PlayerDbo PlayerOne { get; set; }
        public string PlayerTwoId { get; set; }
        public virtual PlayerDbo PlayerTwo { get; set; }
        public int Type { get; set; }
        public int PlayerOneRequiredPoints { get; set; }
        public int PlayerOnePoints { get; set; }
        public int PlayerTwoRequiredPoints { get; set; }
        public int PlayerTwoPoints { get; set; }
        public DateTimeOffset Date { get; set; }
        public bool IsFinished { get; set; }
    }
}