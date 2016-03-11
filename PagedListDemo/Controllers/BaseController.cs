using System.Web.Mvc;
using System.Net;
using PagedListDemo.Models.NotificationMessage;

namespace PagedListDemo.Controllers
{
    /// <summary>
    /// Base Controller
    /// </summary>
    public class BaseController : Controller
    {

        ///// <summary>
        ///// Security User Inteface
        ///// </summary>
        //protected readonly ISecurityUser SecurityUser;

        ////public ISecurityUser SecurityUser { get; set; }
        ///// <summary>
        ///// Base controller
        ///// </summary>
        ///// <param name="securityUser">Security user information</param>
        //public BaseController(ISecurityUser securityUser)
        //{

        //    this.SecurityUser = securityUser;
        //    ViewBag.FullName = securityUser.UserInfo.FullName;
        //}

        ///// <summary>
        ///// Log error
        ///// </summary>
        ///// <param name="ex"></param>
        ///// <param name="contextualMessage"></param>
        //protected void LogError(Exception ex, string contextualMessage = null)
        //{
        //    try
        //    {
        //        // log error to Elmah
        //        if (contextualMessage != null)
        //        {
        //            // log exception with contextual information that's visible when 
        //            // clicking on the error in the Elmah log
        //            var annotatedException = new Exception(contextualMessage, ex);
        //            ErrorSignal.FromCurrentContext().Raise(annotatedException, System.Web.HttpContext.Current);
        //        }
        //        else
        //        {
        //            ErrorSignal.FromCurrentContext().Raise(ex, System.Web.HttpContext.Current);
        //        }
        //    }
        //    catch { }
        //}

        /// <summary>
        /// Create success status result for json result
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        protected JsonResult CreateStatusResult(HttpStatusCode code, string message, Severity severity = Severity.Info)
        {
            Response.StatusCode = (int)code;
            var response = new NotificationMessageModel
            {
                Message = message,
                Severity = severity
            };

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        ///// <summary>
        ///// Create error status result for json result
        ///// </summary>
        ///// <param name="code"></param>
        ///// <returns></returns>
        //protected JsonResult CreateErrorStatusResult(HttpStatusCode code)
        //{
        //    Response.StatusCode = (int)code;
        //    return Json(Model.ErrorMessage.NotificationMessageError);
        //}

        /// <summary>
        /// Create http status code result
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        protected HttpStatusCodeResult StatusCode(HttpStatusCode code)
        {
            return new HttpStatusCodeResult(code);
        }

        /// <summary>
        /// Returns an action result that redirects to the home page
        /// </summary>
        /// <returns></returns>
        protected ActionResult RedirectToHome()
        {
            return RedirectToAction("Index", "Home");
        }
    }
}