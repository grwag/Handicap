using System;
using System.Collections.Generic;

namespace Handicap.Domain.Models
{
    public class Player : BaseEntity{
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public int Handicap { get; set; }
        public bool HasFinishedMatchDay { get; set; }
        public ICollection<DateTimeOffset> PlayedDates { get; set; }
    }
}