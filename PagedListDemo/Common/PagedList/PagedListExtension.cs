using System.Linq;
using System.Linq.Dynamic;

namespace PagedListDemo.Common.PagedList
{
    public static class PagedListExtension
    {
        public static PagedListResult<T> ToPagedListResult<T>(this IQueryable<T> data, PagedListOptions pagedListOptions)
        {
            if (string.IsNullOrEmpty(pagedListOptions.OrderBy))
            {
                return null;
            }

            var pagedList = data
                .OrderBy(pagedListOptions.OrderBy)
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