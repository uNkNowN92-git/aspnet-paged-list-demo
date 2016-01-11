using PagedListDemo.Common.PagedList;
using PagedListDemo.Models;
using PagedListDemo.Models.BooksModel;

namespace PagedListDemo.Repositories.BooksRepository
{
    public interface IBooksRepository
    {
        PagedListResult<Book> GetList(BooksFilterOptions filters, PagedListOptions pagedListOptions);
    }
}