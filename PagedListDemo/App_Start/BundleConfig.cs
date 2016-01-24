using System.Web;
using System.Web.Optimization;
using PagedListDemo.Common;

namespace PagedListDemo
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                     "~/Scripts/jquery.validate*",
                     "~/Scripts/PagedListDemo/jquery.custom-validation.js",
                     "~/Scripts/PagedListDemo/error-tooltip.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/knockout").Include(
                      "~/Scripts/knockout-3.4.0.js",
                      "~/Scripts/knockout.mapping-latest.debug.js"));

            bundles.Add(new StyleBundle("~/Content/paged-list-demo").Include(
                      "~/src/css/PagedListDemo/*.css"));

            bundles.Add(new StyleBundle("~/Content/components").Include(
                      "~/src/css/Components/*.css"));
        
            RegisterDynamicBundles(bundles);
        }
		
        public static void RegisterDynamicBundles(BundleCollection bundles)
        {
            var controllers = MvcHelper.GetControllerNames();
            
            controllers.ForEach(controllerName => {
		var contentPath = string.Format(MvcHelper.contentPathTemplate, controllerName);
		var scriptsPath = string.Format(MvcHelper.scriptsPathTemplate, controllerName);
		var bundlesPath = string.Format(MvcHelper.bundlesPathTemplate, controllerName);
	
		var contentServerPath = HttpContext.Current.Server.MapPath(contentPath);
		var scriptsServerPath = HttpContext.Current.Server.MapPath(scriptsPath);
		
		if (System.IO.Directory.Exists(contentServerPath))
			bundles.Add(new StyleBundle(contentPath)
			    .Include(string.Format("{0}/*.css", contentPath)));
		
		if (System.IO.Directory.Exists(scriptsServerPath))
			bundles.Add(new ScriptBundle(bundlesPath)
			    .Include(string.Format("{0}/*.js", scriptsPath)));
		});
        }
    }
}
