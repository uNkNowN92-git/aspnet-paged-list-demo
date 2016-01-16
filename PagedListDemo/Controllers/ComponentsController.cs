using PagedListDemo.Models.BooksModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PagedListDemo.Controllers
{
    public class ComponentsController : Controller
    {
        // GET: Components
        [Route("Components/{id?}")]
        public ActionResult Index(string id)
        {
            ViewData["Books"] = (BooksModel)TempData["Books"];
            ViewBag.Id = id;
            return View();
        }

        [Route("Components/ErrorTooltip")]
        public PartialViewResult ErrorTooltip()
        {
            return PartialView("_ErrorTooltip");
        }
    }
}