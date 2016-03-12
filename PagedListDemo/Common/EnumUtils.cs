using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace PagedListDemo.Common
{
    /// <summary>
    /// Enum Helpers Class
    /// </summary>
    public static class EnumHelpers
    {
        /// <summary>
        /// Converts Enum type to ValueText Model string
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<ValueTextModel> ToValueTextModel<T>()
        {
            Type t = typeof(T);
            if (t.IsEnum)
            {
                return from Enum e in Enum.GetValues(t)
                       select new ValueTextModel()
                       {
                           Value = Convert.ToInt16(e).ToString(),
                           Text = e.ToDescription()
                       };
            }
            return null;
        }

        /// <summary>
        /// Gets a list of <seealso cref="SelectListItem"/> objects corresponding to enum constants
        /// defined in the given type and uses the description attribute if defined as the text.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IList<SelectListItem> GetSelectList<T>()
        {
            Type t = typeof(T);
            if (t.IsEnum)
            {
                return (from Enum e in Enum.GetValues(t)
                       select new SelectListItem()
                       {
                           Value = Convert.ToInt16(e).ToString(),
                           Text = e.ToDescription()
                       }).ToList();
            }
            return null;
        }

        public static string ToDescription(this Enum en)
        {
            Type type = en.GetType();
            MemberInfo[] memInfo = type.GetMember(en.ToString());

            if (memInfo != null && memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attrs != null && attrs.Length > 0)
                    return ((DescriptionAttribute)attrs[0]).Description;
            }

            return en.ToString();

        }
    }

    public class ValueTextModel
    {
        public string Text { get; set; }

        public string Value { get; set; }

    }
}