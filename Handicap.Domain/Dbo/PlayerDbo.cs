using Handicap.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Handicap.Domain.Dbo
{
    public class PlayerDbo : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Handicap { get; set; }
        public virtual ICollection<GameDbo> Games { get; set; }
    }
}
