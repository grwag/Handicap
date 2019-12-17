using System.Collections.Generic;

namespace Handicap.Dbo
{
    public class MatchDayDbo : BaseDbo
    {
        public virtual ICollection<PlayerDbo> Players { get; set; }
        public virtual ICollection<GameDbo> Games { get; set; }
    }
}
