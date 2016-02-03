using PagedListDemo.Common.PagedList;
using PagedListDemo.Models;
using PagedListDemo.Models.BooksModel;

namespace PagedListDemo.Repositories.BooksRepository
{
    [ServiceInterface]
    public interface IBooksRepository
    {
        PagedListResult<BooksModel> GetList(BooksFilterOptions filters, PagedListOptions pagedListOptions);
    }
}