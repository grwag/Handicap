using System;
using System.Collections.Generic;
using System.Text;

namespace Handicap.Dto.Request
{
    public class GameRequest
    {
        public Guid PlayerOneId { get; set; }
        public Guid PlayerTwoId { get; set; }
    }
}
