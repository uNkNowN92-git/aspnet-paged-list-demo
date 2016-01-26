namespace PagedListDemo.Common.PagedList
{
    public class PagedListOptions
    {
        public PagedListOptions()
        {
            // Set default values
            Page = 1;
            PerPage = 5;
            SortAsc = true;
        }

        /* Values passed by the getJSON request */

        // For paging

        public int CurrentEntries { get; set; }

        public int Page { get; set; }

        public int PerPage { get; set; }

        public bool ShowAll { get; set; }

        // For sorting

        public bool SortOnly { get; set; }

        public bool SortAsc { get; set; }

        public string SortBy { get; set; }


        // Computed values to produce the desired output

        public int Start
        {
            get { return SortOnly ? 0 : PerPage * (Page - 1); }
        }

        public int Entries
        {
            get { return SortOnly && CurrentEntries > 0 ? CurrentEntries : PerPage; }
        }

        public string OrderBy
        {
            get
            {
                return string.IsNullOrEmpty(SortBy) ? null :
                    IsMultipleSort ? SortBy : $"{SortBy} {(SortAsc ? "ASC" : "DESC")}";
            }
        }

        public bool IsMultipleSort { get; set; }
    }
}