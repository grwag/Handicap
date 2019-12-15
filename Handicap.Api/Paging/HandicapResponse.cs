using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Handicap.Api.Paging
{
    public class HandicapResponse<T>
    {
        public int TotalCount { get; set; }
        public int Cursor { get; set; }
        public int PageSize { get; set; }
        public bool HasNext { get; set; }
        public bool HasPrevious { get; set; }
        public IList<T> Payload { get; set; }
        public HandicapError Error { get; set; }

        public HandicapResponse()
        {
        }
        public HandicapResponse(IQueryable<T> query, HandicapError error, int cursor, int pageSize)
        {
            TotalCount = query.Count();
            Cursor = cursor;
            PageSize = pageSize;
            HasNext = ((Cursor * PageSize) + PageSize) < TotalCount;
            HasPrevious = (Cursor * PageSize) < 0;
            Error = error;
            Payload = query.Skip(Cursor * PageSize).Take(PageSize).ToList<T>();
        }
    }
}
