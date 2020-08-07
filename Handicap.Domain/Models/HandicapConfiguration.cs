using System;
using System.Collections.Generic;
using System.Text;

namespace Handicap.Domain.Models
{
    public class HandicapConfiguration : BaseEntity
    {
        public bool UpdatePlayersImmediately { get; set; }
        public int EightBallMax {get; set;}
        public int NineBallMax {get; set;}
        public int TenBallMax {get; set;}
        public int StraigntPoolMax {get; set;}

        public HandicapConfiguration()
        {
            Id = Guid.NewGuid().ToString();
            UpdatePlayersImmediately = true;
            EightBallMax = 6;
            NineBallMax = 8;
            TenBallMax = 7;
            StraigntPoolMax = 100;
        }
    }
}
