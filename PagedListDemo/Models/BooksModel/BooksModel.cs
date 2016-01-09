using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace PagedListDemo.Models.BooksModel
{
    public class BooksModel
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "author")]
        public string Author { get; set; }

        [JsonIgnore]
        [Display(Name = "I Accept and Agree to the terms and conditions")]
        public bool AcceptAndAgree { get; set; }
    }
}