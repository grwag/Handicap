using System;
using System.Collections.Generic;
using System.Text;

namespace Handicap.Dto.Response
{
    public class PlayerResponse : BaseResponse
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Handicap { get; set; }
    }
}
