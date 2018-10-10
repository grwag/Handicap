using System;
using System.Collections.Generic;
using System.Text;

namespace Handicap.Dto.Response.Paging
{
    public class PagedListInfo
    {
        public bool Any { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
    }
}
