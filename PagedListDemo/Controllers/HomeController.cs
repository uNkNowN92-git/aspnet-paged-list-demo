using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedListDemo.Models;

namespace PagedListDemo.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View(new Book()
            {
                AcceptAndAgree = false 
            });
        }

        [HttpPost]
        public ActionResult Index(Book book)
        {
            if(!ModelState.IsValid)
            {
                return View(book);
            }

            TempData["Books"] = book;
            return RedirectToAction("NextPage", "Home");
        }

        public ActionResult NextPage()
        {
            ViewData["Books"] = (Book)TempData["Books"];

            return View();
        }
    }
}
