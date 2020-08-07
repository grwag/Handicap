using System;
using System.Collections.Generic;
using System.Text;

namespace Handicap.Dto.Request
{
    public class HandicapConfigurationUpdate
    {
        public bool UpdatePlayersImmediately { get; set; }
        public int EightBallMax {get; set;}
        public int NineBallMax {get; set;}
        public int TenBallMax {get; set;}
        public int StraigntPoolMax {get; set;}
    }
}
