using System;
using System.Collections.Generic;
using System.Text;

namespace Handicap.Domain.Models
{
    public class GameUpdate
    {
        public Guid Id { get; set; }
        public int PlayerOnePoints { get; set; }
        public int PlayerTwoPoints { get; set; }
        public bool IsFinished { get; set; }
    }
}
