using System;
using System.Collections.Generic;
using System.Text;

namespace Handicap.Domain.Models
{
    public class HandicapConfiguration : BaseEntity
    {
        public bool UpdatePlayersImmediately { get; set; }

        public HandicapConfiguration()
        {
            Id = Guid.NewGuid().ToString();
            UpdatePlayersImmediately = true;
        }
    }
}
