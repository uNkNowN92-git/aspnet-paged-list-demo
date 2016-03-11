using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PagedListDemo.Common
{
    /// <summary>
    /// Enum Helpers Class
    /// </summary>
    public class EnumHelpers
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
                           Text = e.ToString()
                       };
            }
            return null;
        }
    }

    public class ValueTextModel
    {
        public string Text { get; set; }

        public string Value { get; set; }

    }
}