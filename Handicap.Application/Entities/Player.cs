using System;
using System.Collections.Generic;

namespace Handicap.Application.Entities{
    public class Player : BaseEntity{
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public int Handicap { get; set; }
        public bool IsBusy { get; set; }
    }
}