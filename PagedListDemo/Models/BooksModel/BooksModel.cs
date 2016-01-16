using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PagedListDemo.Common;

namespace PagedListDemo.Models.BooksModel
{
    public class BooksModel
    {
        [JsonProperty(PropertyName = "bookId")]
        public long BookId { get; set; }

        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "author")]
        public string Author { get; set; }

        [JsonProperty(PropertyName = "publishDate")]
        public DateTime? PublishDate { get; set; }

        [Display(Name = "I Accept and Agree to the terms and conditions")]
        [MustBeTrue(ErrorMessageResourceName = "BooksAcceptAndAgree", ErrorMessageResourceType = typeof(ErrorMessages))]
        public bool AcceptAndAgree { get; set; }

        [Required]
        public bool ShareTransport { get; set; }

        public string SharingMessage { get; set; }

    }
}