using System.Collections.Generic;

namespace Handicap.Dbo
{
    public class MatchDayDbo : BaseDbo
    {
        public ICollection<PlayerDbo> Players { get; set; }
        public ICollection<PlayerDbo> PriorityQueue { get; set; }
        public ICollection<PlayerDbo> Queue { get; set; }
        public ICollection<GameDbo> Games { get; set; }
        //public ICollection<GameDbo> QueuedGames { get; set; }
        //public string Tables { get; set; }
    }
}
