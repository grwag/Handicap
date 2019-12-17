using System;
using System.Collections.Generic;
using System.Text;

namespace Handicap.Dbo
{
    public class MatchDayPlayer
    {
        public string MatchDayId { get; set; }
        public MatchDayDbo MatchDay { get; set; }
        public string PlayerId { get; set; }
        public PlayerDbo Player { get; set; }

        public MatchDayPlayer()
        {

        }
    }
}
