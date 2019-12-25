using System;
using System.Collections.Generic;
using System.Text;

namespace Handicap.Application.Exceptions
{
    public class NotEnoughPlayersException : Exception
    {
        public NotEnoughPlayersException(string message) : base(message) { }
    }
}
