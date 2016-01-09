using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;

namespace PagedListDemo.Common.PagedList
{
    public static class PagedListExtension
    {
        public static PagedListResult<T> ToPagedListResult<T>(this IEnumerable<T> data, PagedListOptions pagedListOptions)
        {
            if (!string.IsNullOrEmpty(pagedListOptions.OrderBy))
                data = data.OrderBy(pagedListOptions.OrderBy);

            var pagedList = data
                .Skip(pagedListOptions.Start)
                .Take(pagedListOptions.ShowAll ? data.Count() : pagedListOptions.Entries);

            var details = new PagedListDetails()
            {
                TotalEntries = data.Count()
            };

            return new PagedListResult<T>(pagedList, details);
        }
    }
}