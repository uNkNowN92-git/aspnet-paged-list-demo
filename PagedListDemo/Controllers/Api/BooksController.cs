using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using PagedListDemo.Common.PagedList;
using PagedListDemo.Models;
using PagedListDemo.Models.BooksModel;

namespace PagedListDemo.Controllers.Api
{
    public class BooksController : ApiController
    {
        private readonly PagedListDemoEntities db = new PagedListDemoEntities();

        // GET: api/Books
        public IEnumerable<BooksModel> GetBooks()
        {
            return db.Books.Select(x => new BooksModel
            {
                BookId = x.BookId,
                Description = x.Description,
                Title = x.Title,
                PublishDate = x.PublishDate,
                AuthorFirstName = x.Author.FirstName,
                AuthorLastName = x.Author.LastName
            });
        }

        [Route("api/books/autocomplete", Name = "BooksAutocomplete")]
        public IEnumerable<BooksModel> GetAutocomplete([FromUri]SearchModel searchModel)
        {
            var booksQuery = db.Books.AsQueryable();
            searchModel.SearchTerm = searchModel.SearchTerm ?? "";

            switch (searchModel.SearchFor)
            {
                case SearchFor.Title:
                    booksQuery = booksQuery
                        .Where(x => x.Title.Contains(searchModel.SearchTerm));
                    break;

                case SearchFor.Description:
                    booksQuery = booksQuery
                        .Where(x => x.Description.Contains(searchModel.SearchTerm));
                    break;

                case SearchFor.AuthorName:
                    booksQuery = booksQuery
                        .Where(x => x.Author.FirstName.Contains(searchModel.SearchTerm)
                            || x.Author.LastName.Contains(searchModel.SearchTerm));
                    break;
            }

            var books = booksQuery.Take(searchModel.MaxResults)
                .Select(x => new BooksModel
                {
                    BookId = x.BookId,
                    Description = x.Description,
                    Title = x.Title,
                    PublishDate = x.PublishDate,
                    AuthorFirstName = x.Author.FirstName,
                    AuthorLastName = x.Author.LastName,
                    AuthorId = x.AuthorId,
                });

            return books;
        }

        [Route("api/books/paged", Name = "BooksPaged")]
        public PagedListResult<BooksModel> GetPagedBooks([FromUri]PagedListOptions pagedListOptions)
        {
            return db.Books.Select(x => new BooksModel
            {
                BookId = x.BookId,
                Description = x.Description,
                Title = x.Title,
                PublishDate = x.PublishDate,
                Author = x.Author.FirstName + " " + x.Author.LastName
            }).AsQueryable().ToPagedListResult(pagedListOptions);
        }

        // GET: api/Books/5
        [ResponseType(typeof(BooksModel))]
        public IHttpActionResult GetBook(long id)
        {
            var book = db.Books.Where(x => x.BookId == id).Select(x => new BooksModel
            {
                BookId = x.BookId,
                Description = x.Description,
                Title = x.Title,
                PublishDate = x.PublishDate,
                Author = x.Author.FirstName + " " + x.Author.LastName
            }).FirstOrDefault();

            if (book == null)
                return NotFound();

            return Ok(book);
        }

        // PUT: api/Books/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBook(long id, BooksModel booksModel)
        {
            if (id != booksModel.BookId)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var book = db.Books.Find(id);

            if (book == null)
                return StatusCode(HttpStatusCode.NotFound);

            // MAP using SERVICE
            book.BookId = booksModel.BookId;
            book.Description = booksModel.Description;
            book.Title = booksModel.Title;
            book.PublishDate = booksModel.PublishDate;

            book.Author.FirstName = booksModel.AuthorFirstName;
            book.Author.LastName = booksModel.AuthorLastName;

            try
            {
                // UPDATE using SERVICE
                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception)
            {
                return StatusCode(HttpStatusCode.InternalServerError);
            }

            return StatusCode(HttpStatusCode.Accepted);
        }

        // POST: api/Books
        [ResponseType(typeof(Book))]
        public IHttpActionResult PostBook(Models.BooksModel.BooksModel booksModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var book = new Book
            {
                Description = booksModel.Description,
                Title = booksModel.Title,
                PublishDate = booksModel.PublishDate
            };
            try
            {
                db.Books.Add(book);
                db.SaveChanges();

                return Created("DefaultApi", new { id = book.BookId });
            }
            catch (Exception)
            {
                return StatusCode(HttpStatusCode.InternalServerError);
            }
        }

        // DELETE: api/Books/5
        [ResponseType(typeof(Book))]
        public IHttpActionResult DeleteBook(long id)
        {
            var book = db.Books.Find(id);
            if (book == null)
            {
                return NotFound();
            }

            db.Books.Remove(book);
            db.SaveChanges();

            return Ok(book);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BookExists(long id)
        {
            return db.Books.Count(e => e.BookId == id) > 0;
        }
    }
}