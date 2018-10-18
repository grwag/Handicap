using System.Collections.Generic;

namespace Handicap.Dbo
{
    public class PlayerDbo : BaseDbo
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Handicap { get; set; }
    }
}
