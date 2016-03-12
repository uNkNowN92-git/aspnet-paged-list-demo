using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
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

            return helper.RenderStyleBundle(controllerName);
        }

        /// <summary>
        /// Renders the style bundle based on the controller name.
        /// </summary>
        /// <param name="helper">The HtmlHelper</param>
        /// <param name="controllerName">The controller name</param>
        /// <returns>Returns the link tags for the controller bundle.</returns>
        public static MvcHtmlString RenderControllerStyleBundle(this HtmlHelper helper, string controllerName)
        {
            return helper.RenderStyleBundle(controllerName);
        }

        /// <summary>
        /// Renders the script bundle based on the controller name.
        /// </summary>
        /// <param name="helper">The HtmlHelper</param>
        /// <returns>Returns the script tags for the controller bundle.</returns>
        public static MvcHtmlString RenderControllerScriptBundle(this HtmlHelper helper)
        {
            var controllerName = helper.ViewContext.RouteData.Values["controller"].ToString();

            return helper.RenderScriptBundle(controllerName);
        }

        /// <summary>
        /// Renders the script bundle based on the controller name.
        /// </summary>
        /// <param name="helper">The HtmlHelper</param>
        /// <param name="controllerName">The controller name</param>
        /// <returns>Returns the script tags for the controller bundle.</returns>
        public static MvcHtmlString RenderControllerScriptBundle(this HtmlHelper helper, string controllerName)
        {
            return helper.RenderScriptBundle(controllerName);
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
                    bundles.Add(new Bundle(bundlesPath, new JsMinify(), new JsSourceUrlTransform())
                        .IncludeDirectory(scriptsPath, "*.js", true));
            });
        }
        static MvcHtmlString RenderScriptBundle(this HtmlHelper helper, string controllerName)
        {
            var bundle = BundleTable.Bundles.GetBundleFor(controllerName.GetPathFromTemplate(_bundlesPathTemplate));

            if (bundle != null)
                return new MvcHtmlString(Scripts.Render(bundle.Path).ToString());

            return new MvcHtmlString(null);
        }

        static MvcHtmlString RenderStyleBundle(this HtmlHelper helper, string controllerName)
        {
            var bundle = BundleTable.Bundles.GetBundleFor(controllerName.GetPathFromTemplate(_stylesPathTemplate));

            if (bundle != null)
                return new MvcHtmlString(Styles.Render(bundle.Path).ToString());

            return new MvcHtmlString(null);
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

    /// <summary>
    /// Javascript Source Url Transform
    /// </summary>
    public class JsSourceUrlTransform : IBundleTransform
    {
        /// <summary>
        /// Adds '//# sourceURL=pathToJS'  at the end of the script to be able to map the script
        /// on the sources tab of the browser for debugging.
        /// </summary>
        public JsSourceUrlTransform() { }

        /// <summary>
        /// Process the transform
        /// </summary>
        /// <param name="context"></param>
        /// <param name="response"></param>
        public void Process(BundleContext context, BundleResponse response)
        {
            var sb = new StringBuilder(response.Content);
            sb.AppendLine();
            sb.AppendLine(string.Format(@"//# sourceURL={0}{1}",
                HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority),
                VirtualPathUtility.ToAbsolute(context.BundleVirtualPath)));

            response.Content = sb.ToString();
        }
    }
}