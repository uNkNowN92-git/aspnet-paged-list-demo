using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Web;
using System.Web.Mvc;
using PagedListDemo.Models.BooksModel;
using Newtonsoft.Json;
using PagedListDemo.Common;
using System.Net;
using PagedListDemo.Models.NotificationMessage;

namespace PagedListDemo.Controllers
{
    public class HomeController : BaseController
    {
        private readonly Models.PagedListDemoEntities db = new Models.PagedListDemoEntities();

        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            var book = db.Books.Select(x => new BooksModel
            {
                BookId = x.BookId,
                Description = x.Description,
                Title = x.Title,
                PublishDate = x.PublishDate,
                Author = x.Author.FirstName + " " + x.Author.LastName
            }).FirstOrDefault();

            ViewBag.Book = book;
            return View();
        }

        public PartialViewResult IndexPartial()
        {
            ViewBag.Title = "Home Page";
            var book = db.Books.Select(x => new BooksModel
            {
                BookId = x.BookId,
                Description = x.Description,
                Title = x.Title,
                PublishDate = x.PublishDate,
                Author = x.Author.FirstName + " " + x.Author.LastName
            }).FirstOrDefault();

            ViewBag.Book = book;
            ViewBag.IsPartial = true;
            return PartialView("Index");
        }

        public JsonResult Get()
        {
            var book = db.Books.Select(x => new BooksModel
            {
                BookId = x.BookId,
                Description = x.Description,
                Title = x.Title,
                PublishDate = x.PublishDate,
                Author = x.Author.FirstName + " " + x.Author.LastName
            }).FirstOrDefault();

            return Json(null, JsonRequestBehavior.AllowGet);
        }

        [JsonHandler]
        public JsonResult GetJSON()
        {
            return Json(null, JsonRequestBehavior.AllowGet);
        }

        [JsonHandler]
        [HttpGet]
        public JsonResult Get(HttpStatusCode code, NotificationMessageModel message)
        {
            return CreateStatusResult(code, message.Message, message.Severity);
        }

        [HttpPost]
        public ActionResult Index(BooksModel book)
        {
            if(!ModelState.IsValid)
            {
                return View(book);
            }

            TempData["Books"] = book;
            return RedirectToRoute(new { @controller = "Components", @action = "Index", @id = book.BookId });
            //return RedirectToAction("NextPage", "Home");
        }

        public ActionResult NextPage()
        {
            ViewData["Books"] = (BooksModel)TempData["Books"];

            return View();
        }
    }
}
