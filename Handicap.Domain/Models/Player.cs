using System;
using System.Collections.Generic;

namespace Handicap.Domain.Models
{
    public class Player : BaseEntity{
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public int Handicap { get; set; }

        public Player()
        {
            Id = Guid.NewGuid();
            FirstName = string.Empty;
            LastName = string.Empty;
            Handicap = 0;
        }
    }
}