using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;

namespace PagedListDemo.Common.PagedList
{
    public static class PagedListExtension
    {
        public static PagedListResult<T> ToPagedListResult<T>(this IQueryable<T> data, PagedListOptions pagedListOptions)
        {
            pagedListOptions = pagedListOptions ?? new PagedListOptions();

            if (string.IsNullOrEmpty(pagedListOptions.OrderBy))
            {
                return null;
            }

            var pagedList = data.ToPagedListData(pagedListOptions);

            var details = data.ToPagedListDetails();

            return new PagedListResult<T>(pagedList, details);
        }

        public static PagedListDetails ToPagedListDetails<T>(this IQueryable<T> data)
        {
            // TODO: search for COUNT(*) equivalent for LINQ
            return new PagedListDetails
            {
                TotalEntries = data.Count()
            };
        }

        public static IEnumerable<T> ToPagedListData<T>(this IQueryable<T> data, PagedListOptions pagedListOptions)
        {
            return data
                .OrderBy(pagedListOptions.OrderBy)
                .Skip(pagedListOptions.Start)
                .Take(pagedListOptions.ShowAll ? data.Count() : pagedListOptions.Entries);
        }
    }
}