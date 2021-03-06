﻿using Newtonsoft.Json;
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

        [JsonProperty(PropertyName = "authorId")]
        public long AuthorId { get; set; }

        /// <summary>
        /// Author of the book
        /// </summary>
        [JsonProperty(PropertyName = "authorName")]
        public string AuthorFullName
        {
            get
            {
                return AuthorFirstName + " " + AuthorLastName;
            }
        }

        [JsonIgnore]
        public string Author { get; set; }

        [JsonProperty(PropertyName = "authorFirstName")]
        public string AuthorFirstName { get; set; }

        [JsonProperty(PropertyName = "authorLastName")]
        public string AuthorLastName { get; set; }

        /// <summary>
        /// Pusblish date of the book
        /// </summary>
        [JsonProperty(PropertyName = "publishDate")]
        [JsonConverter(typeof(DayMonthYearDateConverter))]
        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? PublishDate { get; set; }

        /// <summary>
        /// Determines whether accepted and agreed or not
        /// </summary>
        [Display(Name = "I Accept and Agree to the terms and conditions")]
        [MustBeTrue(ErrorMessageResourceName = "BooksAcceptAndAgree", ErrorMessageResourceType = typeof(ErrorMessages))]
        public bool AcceptAndAgree { get; set; }

        [MustBeTrue(ErrorMessage = "You must select yes")]
        public bool Conferencing { get; set; }

        /// <summary>
        /// Determines whether to share the vehicle or not
        /// </summary>
        //[MustBeTrue(ErrorMessageResourceName = "BooksAcceptAndAgree", ErrorMessageResourceType = typeof(ErrorMessages))]
        //[Required]
        public bool ShareTransport { get; set; }

        private string sharingMessage;
        /// <summary>
        /// The sharing message
        /// </summary>
        public string SharingMessage
        {
            get
            {
                return ShareTransport ? sharingMessage : null;
            }
            set
            {
                sharingMessage = value;
            }
        }
    }
}