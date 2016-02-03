using System;
using PagedListDemo.Common.PagedList;
using PagedListDemo.Models;
using System.Collections.Generic;
using System.Linq;
using PagedListDemo.Models.BooksModel;
using System.Linq.Dynamic;
using System.Data.Entity;

namespace PagedListDemo.Repositories.BooksRepository
{
    internal class BooksRepository : IBooksRepository
    {
        private readonly PagedListDemoEntities db = new PagedListDemoEntities();

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CC0068:Unused Method", Justification = "<Pending>")]
        private void Seed()
        {
            var r = new Random();

            //// Add person
            //var person = new Person()
            //{
            //    PersonId = r.Next(1, 10000),
            //    FirstName = "First Name " + r.Next(1, 10),
            //    LastName = "Last Name " + r.Next(1, 10),
            //    BirthDate = DateTime.Now.AddYears(r.Next(1, 20)).AddMonths(r.Next(1, 12)).AddDays(r.Next(1, 31))
            //    //, new Person()
            //    //{
            //    //    FirstName = "First Name 2",
            //    //    LastName = "Last Name 2",
            //    //    BirthDate = DateTime.Now.AddYears(-20).AddMonths(2)
            //    //},
            //};

            //db.Persons.Add(person);


            // Add author
            var author = new Author()
            {
                AuthorId = r.Next(1, 10000),
                //PersonId = person.PersonId
            };
            db.Authors.Add(author);

            // Add books

            switch (r.Next(1, 4))
            {
                case 1:
                    var books = new List<Book>() {
                new Book()
                {
                    BookId = r.Next(1, 10000),
                    Author = author,
                    AuthorId = author.AuthorId,
                    Description = "Description",
                    Title = "Title"
                },
                new Book()
                {
                    BookId = r.Next(1, 10000),
                    Author = author,
                    AuthorId = author.AuthorId,
                    Description = "Descridsaasns",
                    Title = "Tiasffsle"
                },
                new Book()
                {
                    BookId = r.Next(1, 10000),
                    Author = author,
                    AuthorId = author.AuthorId,
                    Description = "asdagw",
                    Title = "dasdfgsd"
                }
            };
                    db.Books.AddRange(books);

                    break;
                case 2:
                    var books2 = new List<Book>() {
                new Book()
                {
                    BookId = r.Next(1, 10000),
                    Author = author,
                    AuthorId = author.AuthorId,
                    Description = "sdfasdfasdf",
                    Title = "asdfasdfasdf"
                },
                new Book()
                {
                    BookId = r.Next(1, 10000),
                    Author = author,
                    AuthorId = author.AuthorId,
                    Description = "asdfasdfasd",
                    Title = "asdfasdfsdf"
                },
                new Book()
                {
                    BookId = r.Next(1, 10000),
                    Author = author,
                    AuthorId = author.AuthorId,
                    Description = "fsdfsdfhfg",
                    Title = "jtyryheraef"
                }
            };
                    db.Books.AddRange(books2);
                    break;
                case 3:
                    var book2 = new List<Book>() {
                new Book()
                {
                    BookId = r.Next(1, 10000),
                    Author = author,
                    AuthorId = author.AuthorId,
                    Description = "hsfghhshse",
                    Title = "sdhrthsdsgfd"
                },
                new Book()
                {
                    BookId = r.Next(1, 10000),
                    Author = author,
                    AuthorId = author.AuthorId,
                    Description = "trhrthdrth",
                    Title = "jyukktgerre"
                },
                new Book()
                {
                    BookId = r.Next(1, 10000),
                    Author = author,
                    AuthorId = author.AuthorId,
                    Description = "regsrtjdtyhsre",
                    Title = "iluytgsrthsrtgs"
                },
                new Book()
                {
                    BookId = r.Next(1, 10000),
                    Author = author,
                    AuthorId = author.AuthorId,
                    Description = "dytjuklythd",
                    Title = "strh6kyt"
                }
            };
                    db.Books.AddRange(book2);
                    break;
                default:

                    var books3 = new List<Book>() {
                new Book()
                {
                    BookId = r.Next(1, 10000),
                    Author = author,
                    AuthorId = author.AuthorId,
                    Description = "hdrhyjdyjdtyj",
                    Title = "ilktrhgfsd"
                },
                new Book()
                {
                    BookId = r.Next(1, 10000),
                    Author = author,
                    AuthorId = author.AuthorId,
                    Description = "hrdthtkytdj",
                    Title = "gsegesrgergr"
                }
            };
                    db.Books.AddRange(books3);

                    break;
            }

            db.SaveChanges();
        }

        public void Test()
        {
            var authors = new List<Author>
            {
                new Author
                {
                    BirthDate = DateTime.Now,
                    FirstName = "First Name"
                },
                new Author
                {
                    BirthDate = DateTime.Now,
                    FirstName = "First Name 2"
                },
            };

            var books = new List<Book>();
            authors.ForEach(x =>
            {
                var book = new Book
                {
                    Description = "Description " + x.AuthorId,
                    AuthorId = x.AuthorId
                };

                //db.Authors.Attach(x);
                //db.Entry(x).State = EntityState.Added;

                x.Books.Add(book);
            });

            db.Authors.AddRange(authors);
            db.SaveChanges();

            var id = authors.FirstOrDefault().AuthorId;
        }

        public PagedListResult<BooksModel> GetList(BooksFilterOptions filters, PagedListOptions pagedListOptions)
        {
            //try
            //{
            //    //Seed();
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}

            Test();

            var data = db.Books.AsQueryable();

            // filter Description
            if (!string.IsNullOrEmpty(filters.Title))
            {
                data = data.Where("Title.Contains(@0)", filters.Title);
            }

            // filter Author
            if (!string.IsNullOrEmpty(filters.Author))
            {
                //data = data.Where("Author.Person.LastName.Contains(@0) OR Author.Person.FirstName.Contains(@0)", filters.Author);
                //data = data.Where("Author.Person.FirstName.Contains(@0)", filters.Author);
            }

            // filter Description
            if (!string.IsNullOrEmpty(filters.Description))
            {
                data = data.Where("Description.Contains(@0)", filters.Description);
            }

            // REQUIRED
            pagedListOptions.SortBy = pagedListOptions.SortBy ?? "BookId";

            var pagedListResult = data
                .Select(x => new BooksModel
                {
                    Author = x.Author.FirstName,
                    BookId = x.BookId,
                    Description = x.Description,
                    PublishDate = x.PublishDate
                }).ToPagedListResult(pagedListOptions);

            return pagedListResult;

            //var pagedListResult = data
            //    .OrderBy(pagedListOptions.OrderBy)
            //    .Skip(pagedListOptions.Start)
            //    .Take(pagedListOptions.ShowAll ? data.Count() : pagedListOptions.Entries)
            //    .Select(x => new BooksModel()
            //    {
            //        BookId = x.BookId,
            //        Title = x.Title,
            //        Description = x.Description,
            //        Author = x.Author.Person.FirstName + " " + x.Author.Person.LastName,
            //        PublishDate = x.PublishDate
            //    });//.ToPagedListResult(pagedListOptions);

            //var result = new PagedListResult<BooksModel>(pagedListResult, new PagedListDetails()
            //{
            //    TotalEntries = data.Count()
            //});
            //return result;


            //return pagedListResult;
            //// sample data



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