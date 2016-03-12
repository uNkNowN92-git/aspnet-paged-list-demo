using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace PagedListDemo.Models.BooksModel
{
    public class SearchModel
    {
        public SearchModel()
        {
            MaxResults = 10;
        }

        public string SearchTerm { get; set; }

        public int MaxResults { get; set; }

        public SearchFor SearchFor { get; set; }
    }

    public enum SearchFor
    {
        Title,
        Description,
        [Description("Author Name")]
        AuthorName
    }
}