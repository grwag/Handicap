using System.Collections.Generic;

namespace Handicap.Dbo
{
    public class MatchDayDbo : BaseDbo
    {
        public virtual ICollection<MatchDayPlayer> MatchDayPlayers { get; set; }
        public virtual ICollection<MatchDayGame> MatchDayGames { get; set; }
    }
}
