using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using PagedListDemo.Models;

namespace PagedListDemo.Controllers
{
    public class BooksController : ApiController
    {
        private readonly Models.PagedListDemoEntities1 db = new Models.PagedListDemoEntities1();

        // GET: api/Books
        public IQueryable<Book> GetBooks()
        {
            return db.Books.Select(x => new Book
            {
                Author = x.Author,
                BookId = x.BookId,
                Description = x.Description,
                AuthorId = x.AuthorId,
                PublishDate = x.PublishDate,
                Title = x.Title
            });
        }

        // GET: api/Books/5
        [ResponseType(typeof(Book))]
        public IHttpActionResult GetBook(long id)
        {
            //var book = db.Books.Find(id);
            //if (book == null)
            //{
            if (id == 0)
                return NotFound();
            //}

            return Ok(new Book());
        }

        // PUT: api/Books/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBook(long id, Models.BooksModel.BooksModel booksModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var book = new Book
            {
                BookId = id,
                Description = booksModel.Description,
                Title = booksModel.Title,
                PublishDate = booksModel.PublishDate
            };

            if (id != book.BookId)
            {
                return BadRequest();
            }

            db.Entry(book).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
                {
                    return NotFound();
                }
                throw;
            }

            return StatusCode(HttpStatusCode.Accepted);
        }

        // POST: api/Books
        [ResponseType(typeof(Book))]
        public IHttpActionResult PostBook(Models.BooksModel.BooksModel booksModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

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

                //return CreatedAtRoute("DefaultApi", new { id = book.BookId });
                return Created("DefaultApi", new { id = book.BookId });
                //return Ok(new { id = book.BookId });
            }
            catch (Exception)
            {
                return StatusCode(HttpStatusCode.InternalServerError);
            }

            //return StatusCode(HttpStatusCode.NoContent);

            //return CreatedAtRoute("DefaultApi", new { id = book.BookId }, book);
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