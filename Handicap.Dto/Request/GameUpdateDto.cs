using System;
using System.Collections.Generic;
using System.Text;

namespace Handicap.Dto.Request
{
    public class GameUpdateDto
    {
        public string Id { get; set; }
        public int PlayerOnePoints { get; set; }
        public int PlayerTwoPoints { get; set; }
        public bool IsFinished { get; set; }
    }
}
