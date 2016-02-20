using System.Web.Mvc;

namespace PagedListDemo.Common
{
    /// <summary>
    /// Represents a class for Json Handler filter attributes
    /// </summary>
    public sealed class JsonHandlerAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <seealso cref="JsonHandlerAttribute"/> class.
        /// </summary>
        public JsonHandlerAttribute() { }

        /// <summary>
        /// Called by the ASP.NET MVC framework after the action method executes.
        /// </summary>
        /// <param name="filterContext">The filter context.</param>
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var jsonResult = filterContext.Result as JsonResult;

            if (jsonResult != null)
            {
                filterContext.Result = new JsonNetResult
                {
                    ContentEncoding = jsonResult.ContentEncoding,
                    ContentType = jsonResult.ContentType,
                    Data = jsonResult.Data,
                    JsonRequestBehavior = jsonResult.JsonRequestBehavior
                };
            }

            base.OnActionExecuted(filterContext);
        }
    }
}