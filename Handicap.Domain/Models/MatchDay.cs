using System;
using System.Collections.Generic;
using System.Text;

namespace Handicap.Domain.Models
{
    public class MatchDay : BaseEntity
    {
        public DateTimeOffset Date { get; set; }
        //public ICollection<Player> Players { get; set; }
        public ICollection<Game> Games { get; set; }

        public MatchDay()
        {
            Id = Guid.NewGuid().ToString();
            //Players = new List<Player>();
            Games = new List<Game>();
        }
    }
}
