using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
//using Foolproof;

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

        //[Is(Operator.EqualTo, "MustBeTrue")]
        [Display(Name = "I Accept and Agree to the terms and conditions")]
        //[Range(typeof(bool), "true", "true", ErrorMessage = "You must Accept and Agree to the terms and conditions.")]
        //[Compare("MustBeTrue", ErrorMessage = "You must Accept and Agree to the terms and conditions.")]
        [MustBeTrue(ErrorMessage = "You must accept the terms and conditions")]
        public bool AcceptAndAgree { get; set; }

        [Required]
        public bool ShareTransport { get; set; }

        //[RequiredIfTrue("ShareTransport")]
        public string SharingMessage { get; set; }
        
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class MustBeTrueAttribute : ValidationAttribute, IClientValidatable
    {
        public override bool IsValid(object value)
        {
            return value != null && value is bool && (bool)value;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            yield return new ModelClientValidationRule
            {
                ErrorMessage = this.ErrorMessage,
                ValidationType = "mustbetrue"
            };
        }
    }
}