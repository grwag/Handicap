using System;
using System.Collections.Generic;
using System.Text;

namespace Handicap.Application.Exceptions
{
    public class EntityClosedForUpdateException : Exception
    {
        public EntityClosedForUpdateException(string message) : base(message) { }
    }
}
