using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

namespace PagedListDemo.Common
{
	public static class MvcHelper
	{
		public const string contentPathTemplate = "~/Content/{0}";
		public const string bundlesPathTemplate = "~/bundles/{0}";
		public const string scriptsPathTemplate = "~/Scripts/{0}";
		
		public static MvcHtmlString RenderControllerStyleBundle(this HtmlHelper helper)
		{
			var controllerName = helper.ViewContext.RouteData.Values["controller"];
			var bundle = BundleTable.Bundles.GetBundleFor(string.Format(contentPathTemplate, controllerName));
			
			if (bundle != null)
				return new MvcHtmlString(Styles.Render(bundle.Path).ToString());
			
			return new MvcHtmlString(null);
		}
		
		public static MvcHtmlString RenderControllerScriptBundle(this HtmlHelper helper)
		{
			var controllerName = helper.ViewContext.RouteData.Values["controller"];
			var bundle = BundleTable.Bundles.GetBundleFor(string.Format(bundlesPathTemplate, controllerName));
			
			if (bundle != null)
				return new MvcHtmlString(Scripts.Render(bundle.Path).ToString());
			
			return new MvcHtmlString(null);
		}
		
		public static List<string> GetControllerNames()
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
			if (s.EndsWith(suffix, StringComparison.Ordinal)) {
				return s.Substring(0, s.Length - suffix.Length);
			} else {
				return s;
			}
		}
	}
}
