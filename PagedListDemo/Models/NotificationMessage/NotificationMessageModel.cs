using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PagedListDemo.Models.NotificationMessage
{
    public class NotificationMessageModel
    {
        public string Message { get; set; }

        public Severity Severity { get; set; }
    }

    public enum Severity
    {
        Info,
        Success,
        Error,
        Warning
    }
}