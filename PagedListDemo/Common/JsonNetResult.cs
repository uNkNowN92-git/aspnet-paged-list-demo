using System;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace PagedListDemo.Common
{
    /// <summary>
    /// Represents a class that is used to send JSON-formatted content to the 
    /// response using Newtonsoft <seealso cref="JsonSerializer"/>
    /// </summary>
    public class JsonNetResult : JsonResult
    {
        /// <summary>
        /// Initializes a new instance of the <seealso cref="JsonNetResult"/> class.
        /// </summary>
        public JsonNetResult()
        {
            Settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Error
            };
        }

        /// <summary>
        /// Gets the settings of the <seealso cref="JsonNetResult"/>.
        /// </summary>
        public JsonSerializerSettings Settings { get; private set; }

        /// <summary>
        /// Serializes the specified <see cref="System.Object"/> and writes the Json structure to a Stream
        /// using the specified <seealso cref="System.IO.TextWriter"/>.
        /// </summary>
        /// <param name="context">The context within which the result is executed.</param>
        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");
            if (JsonRequestBehavior == JsonRequestBehavior.DenyGet &&
                string.Equals(context.HttpContext.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase))
                throw new InvalidOperationException("JSON GET is not allowed");

            HttpResponseBase response = context.HttpContext.Response;
            response.ContentType = string.IsNullOrEmpty(ContentType) ? "application/json" : ContentType;

            if (ContentEncoding != null)
                response.ContentEncoding = ContentEncoding;
            if (Data == null)
                return;

            var scriptSerializer = JsonSerializer.Create(Settings);
            scriptSerializer.Serialize(response.Output, Data);
        }
    }
}