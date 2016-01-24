using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

namespace PagedListDemo.Common
{
	public static class MvcHelper
	{
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
