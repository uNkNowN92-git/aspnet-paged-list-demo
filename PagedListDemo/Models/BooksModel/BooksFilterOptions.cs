using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PagedListDemo.Models.BooksModel
{
    public class BooksFilterOptions
    {
        public string Author { get; set; }
        public string Description{ get; set; }
        public string Title{ get; set; }
        public DateTime? PublishYear { get; set; }
    }
}