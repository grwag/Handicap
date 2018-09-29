using System.Collections.Generic;

namespace Handicap.Application.Entities{
    public class Player : BaseEntity{
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual ICollection<Game> Games { get; set; }
    }
}