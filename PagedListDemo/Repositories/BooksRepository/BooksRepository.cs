using System;
using PagedListDemo.Common.PagedList;
using PagedListDemo.Models;
using System.Collections.Generic;
using System.Linq;
using PagedListDemo.Models.BooksModel;
using System.Linq.Dynamic;

namespace PagedListDemo.Repositories.BooksRepository
{
    internal class BooksRepository : IBooksRepository
    {
        private Models.PagedListDemoEntities db = new Models.PagedListDemoEntities();

        public PagedListResult<BooksModel> GetList(BooksFilterOptions filters, PagedListOptions pagedListOptions)
        {
            var data = db.Books
                .Select(x => new BooksModel()
                {
                    BookId = x.BookId,
                    Title = x.Author,
                    Description = x.Description,
                    Author = x.Author
                });

            // filter Description
            if (!string.IsNullOrEmpty(filters.Title))
            {
                data = data.Where("Title.Contains(@0)", filters.Title);
            }

            // filter Author
            if (!string.IsNullOrEmpty(filters.Author))
            {
                data = data.Where("Author.Contains(@0)", filters.Author);
            }

            // filter Description
            if (!string.IsNullOrEmpty(filters.Description))
            {
                data = data.Where("Description.Contains(@0)", filters.Description);
            }

            // REQUIRED
            pagedListOptions.SortBy = pagedListOptions.SortBy ?? "BookId";

            return data.ToPagedListResult(pagedListOptions);
            
            //return pagedListResult;
            //// sample data

            //var sampleData = new List<Book>() {

            //    new Book()
            //    {
            //        BookId = 1,
            //        Author = "Author",
            //        Description = "Description",
            //        Title = "Title"
            //    },
            //    new Book()
            //    {
            //        BookId = 2,
            //        Author = "Fasdkds",
            //        Description = "Descridsaasns",
            //        Title = "Tiasffsle"
            //    },
            //    new Book()
            //    {
            //        BookId = 3,
            //        Author = "gwerwe",
            //        Description = "asdagw",
            //        Title = "dasdfgsd"
            //    },
            //    new Book()
            //    {
            //        BookId = 4,
            //        Author = "Autwefasfhor",
            //        Description = "sdfasdfasdf",
            //        Title = "asdfasdfasdf"
            //    },
            //    new Book()
            //    {
            //        BookId = 5,
            //        Author = "Authasdfasdfor",
            //        Description = "asdfasdfasd",
            //        Title = "asdfasdfsdf"
            //    },
            //    new Book()
            //    {
            //        BookId = 6,
            //        Author = "athgfsdfsd",
            //        Description = "fsdfsdfhfg",
            //        Title = "jtyryheraef"
            //    },
            //    new Book()
            //    {
            //        BookId = 7,
            //        Author = "hyjwrfsdfag",
            //        Description = "hsfghhshse",
            //        Title = "sdhrthsdsgfd"
            //    },
            //    new Book()
            //    {
            //        BookId = 8,
            //        Author = "grtjijhsergreg",
            //        Description = "trhrthdrth",
            //        Title = "jyukktgerre"
            //    },
            //    new Book()
            //    {
            //        BookId = 9,
            //        Author = "sergergrjs",
            //        Description = "regsrtjdtyhsre",
            //        Title = "iluytgsrthsrtgs"
            //    },
            //    new Book()
            //    {
            //        BookId = 10,
            //        Author = "hsrthukjjsrtyhs",
            //        Description = "dytjuklythd",
            //        Title = "strh6kyt"
            //    },
            //    new Book()
            //    {
            //        BookId = 11,
            //        Author = "hsrthtyikdtyj",
            //        Description = "hdrhyjdyjdtyj",
            //        Title = "ilktrhgfsd"
            //    },
            //    new Book()
            //    {
            //        BookId = 12,
            //        Author = "mghjfdhfgh",
            //        Description = "hrdthtkytdj",
            //        Title = "gsegesrgergr"
            //    }
            //};

            //try
            //{
            //    db.Books.AddRange(sampleData);
            //    db.SaveChanges();
            //}
            //catch (Exception ex)
            //{

            //    throw ex;
            //}

            // sample data end

            // convert to queryable
            //var data = books.AsQueryable();

            //// filter data
            //if (!string.IsNullOrEmpty(filters.Author))
            //{
            //    data = data.Where(x => x.Author.ToLower().Contains(filters.Author.ToLower()));
            //    //data = data.Where("Author.Contains(@0)", filters.Author);
            //}

            //// filter data
            //if (!string.IsNullOrEmpty(filters.Description))
            //{
            //    data = data.Where(x => x.Description.ToLower().Contains(filters.Description.ToLower()));
            //}

            //// set default sort field
            //pagedListOptions.SortBy = pagedListOptions.SortBy ?? "BookId";

            //// get paged list result
            //var pagedListResult = data.ToPagedListResult(pagedListOptions);

            //return pagedListResult;
        }
    }
}