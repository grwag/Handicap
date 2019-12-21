using System;
using System.Collections.Generic;
using System.Text;

namespace Handicap.Domain.Models
{
    public class MatchDayPlayer
    {
        public MatchDay MatchDay { get; set; }
        public string MatchDayId { get; set; }
        public Player Player { get; set; }
        public string PlayerId { get; set; }
    }
}
