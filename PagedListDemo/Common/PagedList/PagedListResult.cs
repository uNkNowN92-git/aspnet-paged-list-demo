using System.Collections.Generic;
using Newtonsoft.Json;

namespace PagedListDemo.Common.PagedList
{
    public class PagedListResult<T>
    {
        public PagedListResult(IEnumerable<T> data, PagedListDetails details)
        {
            Data = data;
            Details = details;
        }

        //[JsonProperty(PropertyName = "data")]
        public IEnumerable<T> Data { get; private set; }

        //[JsonProperty(PropertyName = "details")]
        public PagedListDetails Details { get; private set; }
    }
}