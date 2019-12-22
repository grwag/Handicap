using System;
using System.Collections.Generic;
using System.Text;

namespace Handicap.Domain.Models
{
    public class MatchDay : BaseEntity
    {
        public DateTimeOffset Date { get; set; }
        public ICollection<MatchDayPlayer> MatchDayPlayers { get; set; }
        public ICollection<Game> Games { get; set; }

        public MatchDay()
        {
            Id = Guid.NewGuid().ToString();
            Date = DateTimeOffset.Now;
            MatchDayPlayers = new List<MatchDayPlayer>();
            Games = new List<Game>();
        }
    }
}
