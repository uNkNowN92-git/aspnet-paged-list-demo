using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedListDemo.Models.BooksModel;

namespace PagedListDemo.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            var message = new Models.NotificationMessage.NotificationMessageModel
            {
                Message = "test"
            };
            Session["test"] = "sd";
            
            TempData["NotificationMessage"] = message;

            var t = (Models.NotificationMessage.NotificationMessageModel)TempData["NotificationMessage"];
            return View(new BooksModel()
            {
                BookId = 1
                //AcceptAndAgree = false 
            });
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
