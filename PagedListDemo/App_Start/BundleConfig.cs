using System.Web;
using System.Web.Optimization;
using DynamicBundles;

namespace PagedListDemo
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/scripts").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/bootstrap.js",
                        "~/Scripts/respond.js",
                        "~/Scripts/jquery.validate*",
#if DEBUG
                        "~/Scripts/knockout-3.4.0.debug.js",
                        "~/Scripts/knockout.mapping-latest.debug.js"
#else
                        "~/Scripts/knockout-3.4.0.js",
                        "~/Scripts/knockout.mapping-latest.js"
#endif
                        ).IncludeDirectory(
                        "~/Scripts/PagedListDemo/Common", "*.js", true));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                        "~/Content/bootstrap.css",
                        "~/Content/site.css",
                        "~/Content/Common/*.css"));

            bundles.RegisterDynamicBundles();

#if DEBUG
            foreach (var bundle in BundleTable.Bundles)
                // disable minification of the bundles
                bundle.Transforms.RemoveAt(0);
#endif

            BundleTable.EnableOptimizations = true;
        }
    }
}
