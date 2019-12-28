using System;
using System.Collections.Generic;
using System.Text;

namespace Handicap.Application.Exceptions
{
    public class TenantMissmatchException : Exception
    {
        public TenantMissmatchException() : base($"Wrong tenant.") { }
    }
}
