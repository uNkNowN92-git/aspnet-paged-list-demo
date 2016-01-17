using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Resources;

namespace PagedListDemo.Common
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class MustBeTrueAttribute : ValidationAttribute, IClientValidatable
    {
        public override bool IsValid(object value)
        {
            return value != null && value is bool && (bool)value;
        }

        public override string FormatErrorMessage(string name)
        {
            return "The " + name + " field must be checked in order to continue.";
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            string errorMessage = String.Empty;
            if (String.IsNullOrWhiteSpace(ErrorMessage))
            {
                if (ErrorMessageResourceType != null && !String.IsNullOrWhiteSpace(ErrorMessageResourceName))
                {
                    var resMan = new ResourceManager(ErrorMessageResourceType.FullName, ErrorMessageResourceType.Assembly);
                    errorMessage = resMan.GetString(ErrorMessageResourceName);
                }
                else
                {
                    errorMessage = FormatErrorMessage(metadata.DisplayName);
                }
            }
            else
            {
                errorMessage = ErrorMessage;
            }

            yield return new ModelClientValidationRule
            {
                ErrorMessage = errorMessage,
                ValidationType = "mustbetrue"
            };
        }
    }
}