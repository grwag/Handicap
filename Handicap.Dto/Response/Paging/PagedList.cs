using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Handicap.Dto.Response.Paging
{
    public class PagedList<T> : List<T>
    {
        public int CurrentPage { get; private set; }
        public int TotalPages { get; private set; }
        public int PageSize { get; private set; }
        public int TotalCount { get; private set; }
        public PagedListInfo Info { get; set; }

        public bool HasPrevious
        {
            get
            {
                return (CurrentPage > 1);
            }
        }

        public bool HasNext
        {
            get
            {
                return (CurrentPage < TotalPages);
            }
        }

        private PagedList(List<T> items, int count, int pageNumber, int pageSize)
        {
            TotalCount = count;
            PageSize = pageSize;
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            AddRange(items);
            Info = new PagedListInfo()
            {
                Any = items.Any(),
                CurrentPage = CurrentPage,
                TotalPages = TotalPages
            };
        }

        private PagedList(List<T> items, int count, int pageNumber, int pageSize, int totalPages, int totalCount)
        {
            TotalCount = totalCount;
            PageSize = pageSize;
            CurrentPage = pageNumber;
            TotalPages = totalPages;
            AddRange(items);
            Info = new PagedListInfo()
            {
                Any = items.Any(),
                CurrentPage = CurrentPage,
                TotalPages = TotalPages
            };
        }

        public static PagedList<T> Create(IQueryable<T> source, int pageNumber, int pageSize)
        {
            var count = source != null ? source.Count() : 0;
            var items = source != null ? source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList() : new List<T>();
            return new PagedList<T>(items, count, pageNumber, pageSize);
        }

        public static PagedList<T> CreateFromPagedList(IQueryable<T> source, int pageNumber, int pageSize, int totalPages, int totalCount)
        {
            var items = source != null ? source.ToList() : new List<T>();
            return new PagedList<T>(items, totalPages, pageNumber, pageSize, totalPages, totalCount);
        }
    }
}
