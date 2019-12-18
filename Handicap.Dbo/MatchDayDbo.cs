using System;
using System.Collections.Generic;

namespace Handicap.Dbo
{
    public class MatchDayDbo : BaseDbo
    {
        public DateTimeOffset Date { get; set; }
        //public virtual ICollection<MatchDayPlayer> MatchDayPlayers { get; set; }
        public virtual ICollection<GameDbo> Games { get; set; }
    }
}
