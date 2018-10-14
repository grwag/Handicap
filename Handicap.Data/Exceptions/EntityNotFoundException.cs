using System;
using System.Collections.Generic;
using System.Text;

namespace Handicap.Data.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(string message) : base(message) { }
    }
}
