using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PagedListDemo.Models.Locations
{
    public class LocationModel
    {
        public int LocationId { get; set; }

        public string Address { get; set; }

        public double? Longitude { get; set; }

        public double? Latitude { get; set; }

        public double? Distance { get; set; }

        public double? Miles { get; set; }

        public double? Ratio { get; set; }
    }
}