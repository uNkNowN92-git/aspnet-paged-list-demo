using System.Web.Mvc;

namespace PagedListDemo.Common
{
    /// <summary>
    /// Represents a class for Json Handler filter attributes
    /// </summary>
    public sealed class JsonNetResultAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <seealso cref="JsonNetResultAttribute"/> class.
        /// </summary>
        public JsonNetResultAttribute() { }

        /// <summary>
        /// Called by the ASP.NET MVC framework after the action method executes.
        /// </summary>
        /// <param name="filterContext">The filter context.</param>
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            //var type = ((ReflectedActionDescriptor)filterContext.ActionDescriptor).MethodInfo.ReturnType;
            //var data = type.GetType().GetProperties();
            //var jsonResult = filterContext.Result as JsonResult;

            //if (jsonResult != null)
            //{
                //filterContext.Result = new JsonNetResult
                //{
                //    //ContentEncoding = filterContext.ContentEncoding,
                //    //ContentType = jsonResult.ContentType,
                //    Data = data,
                //    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                //};
            //}

            base.OnActionExecuted(filterContext);
        }
    }
}