using System.Web;
using System.Web.Mvc;
using PagedListDemo.Common;

namespace PagedListDemo
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new JsonHandlerAttribute());
        }
    }
}
