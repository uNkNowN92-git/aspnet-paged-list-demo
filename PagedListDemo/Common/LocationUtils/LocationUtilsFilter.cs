using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Web;

namespace PagedListDemo.Common.LocationUtils
{
    public class LocationUtilsFilter
    {
        public DbGeography Origin { get; set; }

        public double Radius { get; set; }
    }
}