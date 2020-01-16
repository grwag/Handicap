using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Handicap.Api.Paging
{
    public class HandicapResponse<T,S>
    {
        public int TotalCount { get; set; }
        public int Cursor { get; set; }
        public int PageSize { get; set; }
        public bool HasNext { get; set; }
        public bool HasPrevious { get; set; }
        public IList<T> Payload { get; set; }
        public HandicapError Error { get; set; }

        private HandicapResponse()
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

        public static HandicapResponse<T,S> Create(IQueryable<S> query, HandicapError error, int cursor, int pageSize, IMapper mapper) 
        {
            var response = new HandicapResponse<T,S>();
            var list = query.Skip(cursor * pageSize).Take(pageSize).ToList();
            var responseList = mapper.Map<List<T>>(list);
            var totalCount = query.Count();

            response.TotalCount = totalCount;
            response.Cursor = cursor;
            response.PageSize = pageSize;
            response.HasNext = ((cursor * pageSize) + pageSize) < totalCount;
            response.HasPrevious = (cursor * pageSize) < 0;
            response.Error = error;
            response.Payload = responseList;

            return response;
        }

        public static HandicapResponse<T, S> CreateErrorResponse(HandicapError error)
        {
            return new HandicapResponse<T, S>
            {
                Cursor = 0,
                Error = error,
                HasNext = false,
                HasPrevious = false,
                PageSize = 0,
                Payload = null,
                TotalCount = 0
            };
        }
    }
}
