﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PagedListDemo.Models
{
    public class JourneyModel
    {
        public int LocationId { get; set; }

        public string Address { get; set; }

        public double Longitude { get; set; }

        public double Latitude { get; set; }
    }
}