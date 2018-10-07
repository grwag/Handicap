using Handicap.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Handicap.Domain.Dbo
{
    public class MatchDayDbo : BaseEntity
    {
        public ICollection<PlayerDbo> Players { get; set; }
        public ICollection<PlayerDbo> PriorityQueue { get; set; }
        public ICollection<PlayerDbo> Queue { get; set; }
        public ICollection<GameDbo> Games { get; set; }
        public ICollection<GameDbo> QueuedGames { get; set; }
        public int NumberOfTables { get; set; }
    }
}
