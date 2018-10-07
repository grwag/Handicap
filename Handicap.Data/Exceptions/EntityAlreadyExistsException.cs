using System;
using System.Collections.Generic;
using System.Text;

namespace Handicap.Data.Exceptions
{
    public class EntityAlreadyExistsException : Exception
    {
        public EntityAlreadyExistsException(string message) : base(message) { }
    }
}
