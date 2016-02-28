using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PagedListDemo.Models.Locations
{
    public class ResultModel
    {
        public int LocationId { get; set; }

        public string Address { get; set; }

        public double Miles { get; set; }
    }
}