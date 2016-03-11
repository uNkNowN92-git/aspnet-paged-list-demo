using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PagedListDemo.Models.Booking
{
    public class RepeatBookingModel
    {
        public RepeatType RepeatType { get; set; }

        public int Every { get; set; }

        public DayOfWeek DayOfWeek { get; set; }
    }

    public enum RepeatType
    {
        [Display(Name = "Daily")]
        Daily,
        [Display(Name = "Weekly")]
        Weekly,
        [Display(Name = "Monthly")]
        Monthly
    }
}