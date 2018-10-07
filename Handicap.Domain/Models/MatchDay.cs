using System;
using System.Collections.Generic;
using System.Text;

namespace Handicap.Domain.Models
{
    public class MatchDay : BaseEntity
    {
        public ICollection<Player> Players { get; set; }
        public ICollection<Player> PriorityQueue { get; set; }
        public ICollection<Player> Queue { get; set; }
        public ICollection<Game> Games { get; set; }
        public ICollection<Game> QueuedGames { get; set; }
        public bool[] Tables { get; set; }
    }
}
