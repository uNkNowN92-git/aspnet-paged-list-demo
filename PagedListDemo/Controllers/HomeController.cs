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

            return View(new BooksModel()
            {
                AcceptAndAgree = false 
            });
        }

        [HttpPost]
        public ActionResult Index(BooksModel books)
        {
            if(!ModelState.IsValid)
            {
                return View(books);
            }

            return RedirectToAction("NextPage", "Home");
        }

        public ActionResult NextPage()
        {
            return View();
        }
    }
}
