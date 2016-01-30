using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PagedListDemo.Common
{
    public class DayMonthYearDateConverter : IsoDateTimeConverter
    {
        public DayMonthYearDateConverter()
        {
            DateTimeFormat = "dd/MM/yyyy";
        }
    }
}