using System;
using System.Collections.Generic;
using System.Text;

namespace Handicap.Domain.Models
{
    public class GameUpdate
    {
        public string Id { get; set; }
        public int PlayerOnePoints { get; set; }
        public int PlayerTwoPoints { get; set; }
    }
}
