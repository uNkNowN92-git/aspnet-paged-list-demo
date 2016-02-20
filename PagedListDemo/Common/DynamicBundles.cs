using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;

namespace DynamicBundles
{
    /// <summary>
    /// MVC Helper to dynamically add a bundle to the BundleCollection
    /// based on the MVC Controller names within the project.
    /// Folders in Content and Scripts directory that matches the controller name
    /// will be added automatically to the bundle.
    /// </summary>
    public static class DynamicBundles
    {
        /// <summary>
        /// Template used to generate the path of the styles together with the controller name
        /// </summary>
        static string _contentPathTemplate;

        /// <summary>
        /// Template used to generate the path of the scripts together with the controller name
        /// </summary>
        static string _scriptsPathTemplate;

        /// <summary>
        /// Template used to generate the style bundle together with the controller name
        /// </summary>
        static readonly string _stylesPathTemplate = "~/Content/{0}Styles";

        /// <summary>
        /// Template used to generate the script bundle together with the controller name
        /// </summary>
        static readonly string _bundlesPathTemplate = "~/bundles/{0}";

        static void Initialize()
        {
            var stylesFolder = ConfigurationManager.AppSettings["DynamicBundlesStylesFolder"];
            var scriptsFolder = ConfigurationManager.AppSettings["DynamicBundlesScriptsFolder"];

            _contentPathTemplate = stylesFolder + "/{0}";
            _scriptsPathTemplate = scriptsFolder + "/{0}";
        }

        /// <summary>
        /// Renders the style bundle based on the controller name.
        /// </summary>
        /// <param name="helper">The HtmlHelper</param>
        /// <returns>Returns the link tags for the controller bundle.</returns>
        public static MvcHtmlString RenderControllerStyleBundle(this HtmlHelper helper)
        {
            var controllerName = helper.ViewContext.RouteData.Values["controller"].ToString();
            var bundle = BundleTable.Bundles.GetBundleFor(controllerName.GetPathFromTemplate(_stylesPathTemplate));

            if (bundle != null)
                return new MvcHtmlString(Styles.Render(bundle.Path).ToString());

            return new MvcHtmlString(null);
        }

        /// <summary>
        /// Renders the script bundle based on the controller name.
        /// </summary>
        /// <param name="helper">The HtmlHelper</param>
        /// <returns>Returns the script tags for the controller bundle.</returns>
        public static MvcHtmlString RenderControllerScriptBundle(this HtmlHelper helper)
        {
            var controllerName = helper.ViewContext.RouteData.Values["controller"].ToString();
            var bundle = BundleTable.Bundles.GetBundleFor(controllerName.GetPathFromTemplate(_bundlesPathTemplate));

            if (bundle != null)
                return new MvcHtmlString(Scripts.Render(bundle.Path).ToString());

            return new MvcHtmlString(null);
        }

        /// <summary>
        /// Dynamically adds a script and style bundle for every controller name.
        /// </summary>
        /// <param name="bundles">The BundleCollection.</param>
        public static void RegisterDynamicBundles(this BundleCollection bundles)
        {
            Initialize();

            GetControllerNames().ForEach(controllerName =>
            {
                var contentPath = controllerName.GetPathFromTemplate(_contentPathTemplate);
                var scriptsPath = controllerName.GetPathFromTemplate(_scriptsPathTemplate);
                var bundlesPath = controllerName.GetPathFromTemplate(_bundlesPathTemplate);
                var stylesPath = controllerName.GetPathFromTemplate(_stylesPathTemplate);

                var contentServerPath = HttpContext.Current.Server.MapPath(contentPath);
                var scriptsServerPath = HttpContext.Current.Server.MapPath(scriptsPath);

                if (Directory.Exists(contentServerPath))
                    bundles.Add(new StyleBundle(stylesPath)
                        .IncludeDirectory(contentPath, "*.css", true));

                if (Directory.Exists(scriptsServerPath))
                    bundles.Add(new ScriptBundle(bundlesPath)
                        .IncludeDirectory(scriptsPath, "*.js", true));
            });
        }

        static List<string> GetControllerNames()
        {
            var controllerNames = new List<string>();

            GetSubClasses<Controller>().ForEach(
                type => controllerNames.Add(type.Name.RemoveFromEnd("Controller")));

            return controllerNames;
        }

        static List<Type> GetSubClasses<T>()
        {
            return Assembly.GetCallingAssembly().GetTypes().Where(
                type => type.IsSubclassOf(typeof(T))).ToList();
        }

        static string GetPathFromTemplate(this string s, string template)
        {
            return string.Format(template, s);
        }

        static string RemoveFromEnd(this string s, string suffix)
        {
            if (s.EndsWith(suffix, StringComparison.Ordinal))
                return s.Substring(0, s.Length - suffix.Length);

            return s;
        }
    }
}