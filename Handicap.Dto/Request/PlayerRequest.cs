using System;
using System.Collections.Generic;
using System.Text;

namespace Handicap.Dto.Request
{
    public class PlayerRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Handicap { get; set; }
    }
}
