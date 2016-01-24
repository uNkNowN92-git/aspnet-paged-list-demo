using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;

namespace PagedListDemo.Common
{
    public static class MvcHelper
    {
        private static readonly string _contentPathTemplate = "~/Content/{0}";
        private static readonly string _scriptsPathTemplate = "~/Scripts/PagedListDemo/{0}";
        private static readonly string _bundlesPathTemplate = "~/bundles/{0}";
        private static readonly string _stylesPathTemplate = "~/Content/{0}Styles";

        public static MvcHtmlString RenderControllerStyleBundle(this HtmlHelper helper)
        {
            var controllerName = helper.ViewContext.RouteData.Values["controller"];
            var bundle = BundleTable.Bundles.GetBundleFor(string.Format(_stylesPathTemplate, controllerName));

            if (bundle != null)
                return new MvcHtmlString(Styles.Render(bundle.Path).ToString());

            return new MvcHtmlString(null);
        }

        public static MvcHtmlString RenderControllerScriptBundle(this HtmlHelper helper)
        {
            var controllerName = helper.ViewContext.RouteData.Values["controller"];
            var bundle = BundleTable.Bundles.GetBundleFor(string.Format(_bundlesPathTemplate, controllerName));

            if (bundle != null)
                return new MvcHtmlString(Scripts.Render(bundle.Path).ToString());

            return new MvcHtmlString(null);
        }

        public static void RegisterDynamicBundles(BundleCollection bundles)
        {
            GetControllerNames().ForEach(controllerName =>
            {
                var contentPath = string.Format(_contentPathTemplate, controllerName);
                var scriptsPath = string.Format(_scriptsPathTemplate, controllerName);
                var bundlesPath = string.Format(_bundlesPathTemplate, controllerName);
                var stylesPath = string.Format(_stylesPathTemplate, controllerName);

                var contentServerPath = HttpContext.Current.Server.MapPath(contentPath);
                var scriptsServerPath = HttpContext.Current.Server.MapPath(scriptsPath);

                if (Directory.Exists(contentServerPath))
                    bundles.Add(new StyleBundle(stylesPath).Include(
                        $"{contentPath}/*.css"));

                if (Directory.Exists(scriptsServerPath))
                    bundles.Add(new ScriptBundle(bundlesPath).Include(
                        $"{scriptsPath}/*.js"));
            });
        }

        private static List<string> GetControllerNames()
        {
            var controllerNames = new List<string>();

            GetSubClasses<Controller>().ForEach(
                type => controllerNames.Add(type.Name.RemoveFromEnd("Controller")));

            return controllerNames;
        }

        private static List<Type> GetSubClasses<T>()
        {
            return Assembly.GetCallingAssembly().GetTypes().Where(
                type => type.IsSubclassOf(typeof(T))).ToList();
        }

        private static string RemoveFromEnd(this string s, string suffix)
        {
            if (s.EndsWith(suffix, StringComparison.Ordinal))
                return s.Substring(0, s.Length - suffix.Length);

            return s;
        }
    }
}