using System;
using System.Collections.Generic;
using System.Text;

namespace Handicap.Dto.Response
{
    public class MatchDayResponse : BaseResponse
    {
        public DateTimeOffset Date { get; set; }
        public bool IsFinished { get; set; }
    }
}
