using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PagedListDemo.Common;

namespace PagedListDemo.Models.BooksModel
{
    public class BooksModel
    {
        /// <summary>
        /// Book ID
        /// </summary>
        [JsonProperty(PropertyName = "bookId")]
        public long BookId { get; set; }

        /// <summary>
        /// Title of the book
        /// </summary>
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        /// <summary>
        /// Description of the book
        /// </summary>
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        /// <summary>
        /// Author of the book
        /// </summary>
        [JsonProperty(PropertyName = "author")]
        public string Author { get; set; }

        /// <summary>
        /// Pusblish date of the book
        /// </summary>
        [JsonProperty(PropertyName = "publishDate")]
        public DateTime? PublishDate { get; set; }

        /// <summary>
        /// Determines whether accepted and agreed or not
        /// </summary>
        [Display(Name = "I Accept and Agree to the terms and conditions")]
        [MustBeTrue(ErrorMessageResourceName = "BooksAcceptAndAgree", ErrorMessageResourceType = typeof(ErrorMessages))]
        public bool AcceptAndAgree { get; set; }

        /// <summary>
        /// Determines whether to share the vehicle or not
        /// </summary>
        [Required]
        public bool ShareTransport { get; set; }

        /// <summary>
        /// The sharing message
        /// </summary>
        public string SharingMessage { get; set; }

    }
}