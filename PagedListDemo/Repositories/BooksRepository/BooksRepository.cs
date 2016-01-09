using PagedListDemo.Common.PagedList;
using PagedListDemo.Models.BooksModel;
using System.Collections.Generic;
using System.Linq;

namespace PagedListDemo.Repositories.BooksRepository
{
    internal class BooksRepository : IBooksRepository
    {
        public PagedListResult<BooksModel> GetList(BooksFilterOptions filters, PagedListOptions pagedListOptions)
        {

            // sample data

            var sampleData = new List<BooksModel>() {

                new BooksModel()
                {
                    Id = 1,
                    Author = "Author",
                    Description = "Description",
                    Title = "Title"
                },
                new BooksModel()
                {
                    Id = 2,
                    Author = "Fasdkds",
                    Description = "Descridsaasns",
                    Title = "Tiasffsle"
                },
                new BooksModel()
                {
                    Id = 3,
                    Author = "gwerwe",
                    Description = "asdagw",
                    Title = "dasdfgsd"
                },
                new BooksModel()
                {
                    Id = 4,
                    Author = "Autwefasfhor",
                    Description = "sdfasdfasdf",
                    Title = "asdfasdfasdf"
                },
                new BooksModel()
                {
                    Id = 5,
                    Author = "Authasdfasdfor",
                    Description = "asdfasdfasd",
                    Title = "asdfasdfsdf"
                },
                new BooksModel()
                {
                    Id = 6,
                    Author = "athgfsdfsd",
                    Description = "fsdfsdfhfg",
                    Title = "jtyryheraef"
                },
                new BooksModel()
                {
                    Id = 7,
                    Author = "hyjwrfsdfag",
                    Description = "hsfghhshse",
                    Title = "sdhrthsdsgfd"
                },
                new BooksModel()
                {
                    Id = 8,
                    Author = "grtjijhsergreg",
                    Description = "trhrthdrth",
                    Title = "jyukktgerre"
                },
                new BooksModel()
                {
                    Id = 9,
                    Author = "sergergrjs",
                    Description = "regsrtjdtyhsre",
                    Title = "iluytgsrthsrtgs"
                },
                new BooksModel()
                {
                    Id = 10,
                    Author = "hsrthukjjsrtyhs",
                    Description = "dytjuklythd",
                    Title = "strh6kyt"
                },
                new BooksModel()
                {
                    Id = 11,
                    Author = "hsrthtyikdtyj",
                    Description = "hdrhyjdyjdtyj",
                    Title = "ilktrhgfsd"
                },
                new BooksModel()
                {
                    Id = 12,
                    Author = "mghjfdhfgh",
                    Description = "hrdthtkytdj",
                    Title = "gsegesrgergr"
                }
            };

            // sample data end

            // convert to queryable
            var data = sampleData.AsQueryable();

            // filter data
            if (!string.IsNullOrEmpty(filters.Author))
            {
                data = data.Where(x => x.Author.ToLower().Contains(filters.Author.ToLower()));
                //data = data.Where("Author.Contains(@0)", filters.Author);
            }

            // filter data
            if (!string.IsNullOrEmpty(filters.Description))
            {
                data = data.Where(x => x.Description.ToLower().Contains(filters.Description.ToLower()));
            }

            // set default sort field
            pagedListOptions.SortBy = pagedListOptions.SortBy ?? "Id";

            // get paged list result
            var pagedListResult = data.ToPagedListResult(pagedListOptions);

            return pagedListResult;
        }
    }
}