using System;
using System.Collections.Generic;
using System.Text;

namespace Handicap.Dto.Response
{
    public class MatchDayResponse : BaseResponse
    {
        public ICollection<PlayerResponse> Players { get; set; }
        public ICollection<PlayerResponse> PriorityQueue { get; set; }
        public ICollection<PlayerResponse> Queue { get; set; }
        public ICollection<GameResponse> Games { get; set; }
        //public ICollection<GameResponse> QueuedGames { get; set; }
    }
}
