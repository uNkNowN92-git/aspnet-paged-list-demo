using PagedListDemo.Models.BooksModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedListDemo.Common;
using PagedListDemo.Models.NotificationMessage;

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

            //this.SetNotificationMessage("test", Severity.Success);

            return View();
        }

        public PartialViewResult IndexPartial()
        {
            return PartialView("Index");
        }

        [Route("Components/ErrorTooltip")]
        public PartialViewResult ErrorTooltip()
        {
            return PartialView("_ErrorTooltip");
        }
    }
}