using System;
using System.Collections.Generic;
using System.Text;

namespace Handicap.Dbo
{
    public class MatchDayGame
    {
        public string MatchDayId { get; set; }
        public MatchDayDbo MatchDay { get; set; }
        public string GameId { get; set; }
        public GameDbo Game { get; set; }
    }
}
