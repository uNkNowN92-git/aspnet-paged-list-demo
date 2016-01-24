using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Newtonsoft.Json;
using PagedListDemo.Models.NotificationMessage;

namespace PagedListDemo.Common
{
    public static class HtmlHelpers
    {
        //public static NotificationMessageModel NotificationMessage
        //{
        //    get
        //    {
        //        return Session["sdf"];
        //    }
        //}

        public static MvcHtmlString RenderToastNotification(this HtmlHelper helper)
        {
            var notificationMessageModel = (NotificationMessageModel)helper.ViewContext.TempData["NotificationMessage"]
                ?? new NotificationMessageModel();

            return helper.Partial("~/Views/Shared/_NotificationMessage.cshtml", notificationMessageModel);
        }

        public static string GetJsonPropertyName<TModel, TValue>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TValue>> expression)
        {
            var data = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);

            var jsonPropertyName = typeof(TModel).GetProperty(data.PropertyName)
                    .GetCustomAttributes(typeof(JsonPropertyAttribute))
                    .Cast<JsonPropertyAttribute>()
                    .Select(p => p.PropertyName)
                    .FirstOrDefault();

            return jsonPropertyName;
        }

        private static readonly string _koPagedListPagerTemplate = "~/Views/Shared/_KoPagedListPager.cshtml";

        public static MvcHtmlString KoPagedListPager(this HtmlHelper helper, string template = null)
        {
            var result = new MvcHtmlString(string.Empty);
            try
            {
                result = helper.Partial(template);
            }
            catch (Exception)
            {
                throw;
            }

            return helper.Partial(_koPagedListPagerTemplate);
        }

        private static object _radioButtonToggleDivDefaultAttrs = new
        {
            @class = "radio-button-toggle btn-group",
            data_toggle = "buttons"
        };

        private static object _radioButtonToggleDefaultAttrs = new
        {
            @class = "btn"
        };

        private static object _radioButtonToggleDefaultLabel = new
        {
            labelTrue = "Yes",
            labelFalse = "No"
        };

        private static string _template = @"<div class='radio-button-toggle btn-group' data-toggle='buttons'>
            <label class='btn btn-default radio-button-toggle-true{0}' for='{1}'>
                {2}Yes
            </label>
            <label class='btn btn-default radio-button-toggle-false{3}' for='{4}'>
                {5}No
            </label>
        </div>";

        public static MvcHtmlString RadioButtonToggleFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression)
        {
            var data = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);

            var method = expression.Compile();
            var value = helper.ViewData.Model == null ? false : Convert.ToBoolean(method(helper.ViewData.Model));

            var radioTrueActive = value ? " active" : "";
            var radioFalseActive = !value ? " active" : "";
            var radioTrueId = data.PropertyName + "True";
            var radioFalseId = data.PropertyName + "False";
            var radioTrue = helper.RadioButton(data.PropertyName, true, value, new { id = radioTrueId });
            var radioFalse = helper.RadioButton(data.PropertyName, false, !value, new { id = radioFalseId });

            var result = string.Format(_template, radioTrueActive, radioTrueId, radioTrue, radioFalseActive, radioFalseId, radioFalse);

            return new MvcHtmlString(result);
        }

        public static MvcHtmlString RadioButtonToggleFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression, string templateName)
        {
            var template = string.Empty;
            try
            {
                template = helper.Partial(templateName).ToHtmlString();
            }
            catch (Exception)
            {
                template = _template;
            }

            var data = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);

            var method = expression.Compile();
            var value = helper.ViewData.Model == null ? false : Convert.ToBoolean(method(helper.ViewData.Model));

            var radioTrueActive = value ? " active" : "";
            var radioFalseActive = !value ? " active" : "";
            var radioTrueId = data.PropertyName + "True";
            var radioFalseId = data.PropertyName + "False";
            var radioTrue = helper.RadioButton(data.PropertyName, true, value, new { id = radioTrueId });
            var radioFalse = helper.RadioButton(data.PropertyName, false, !value, new { id = radioFalseId });

            var result = string.Format(template, radioTrueActive, radioTrueId, radioTrue, radioFalseActive, radioFalseId, radioFalse);

            return new MvcHtmlString(result);
        }

        public static MvcHtmlString RadioButtonToggleFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes)
        {
            var data = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);

            var method = expression.Compile();
            var value = helper.ViewData.Model == null ? false : Convert.ToBoolean(method(helper.ViewData.Model));

            var _attrs = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
            var _defaultDivAttrs = HtmlHelper.AnonymousObjectToHtmlAttributes(_radioButtonToggleDivDefaultAttrs);
            var _buttonLabel = HtmlHelper.AnonymousObjectToHtmlAttributes(_radioButtonToggleDefaultLabel);
            var _labelTrueAttrs = HtmlHelper.AnonymousObjectToHtmlAttributes(_radioButtonToggleDefaultAttrs);
            var _labelFalseAttrs = HtmlHelper.AnonymousObjectToHtmlAttributes(_radioButtonToggleDefaultAttrs);

            var toggleButtons = HtmlHelper.AnonymousObjectToHtmlAttributes(_attrs["toggleButtons"]);

            _attrs.Remove("toggleButtons");
            toggleButtons.Remove("id");

            var attrs = _attrs.Concat(_defaultDivAttrs)
                .GroupBy(kvp => kvp.Key, kvp => kvp.Value)
                .ToDictionary(g => g.Key, g => (object)string.Join(" ", g.ToList()));

            var labelTrueAttrs = _labelTrueAttrs.Concat(toggleButtons)
                .GroupBy(kvp => kvp.Key, kvp => kvp.Value)
                .ToDictionary(g => g.Key, g => (object)string.Join(" ", g.ToList()));

            var labelFalseAttrs = _labelFalseAttrs.Concat(toggleButtons)
                .GroupBy(kvp => kvp.Key, kvp => kvp.Value)
                .ToDictionary(g => g.Key, g => (object)string.Join(" ", g.ToList()));

            var buttonLabel = _buttonLabel.Concat(toggleButtons)
                .GroupBy(kvp => kvp.Key, kvp => kvp.Value)
                .ToDictionary(g => g.Key, g => g.LastOrDefault());

            var radioTrueActive = (value ? " active" : "") + " radio-button-toggle-true";
            var radioFalseActive = (!value ? " active" : "") + " radio-button-toggle-false";
            var toggleTrueId = data.PropertyName + (string.IsNullOrEmpty(Convert.ToString(buttonLabel["labelTrue"])) ? "True" : buttonLabel["labelTrue"]);
            var toggleFalseId = data.PropertyName + (string.IsNullOrEmpty(Convert.ToString(buttonLabel["labelFalse"])) ? "False" : buttonLabel["labelFalse"]);

            var radioTrue = helper.RadioButton(data.PropertyName, true, value, new { id = toggleTrueId });
            var radioFalse = helper.RadioButton(data.PropertyName, false, !value, new { id = toggleFalseId });

            var div = new TagBuilder("div");
            div.MergeAttributes(attrs);

            var labelTrue = new TagBuilder("label");

            labelTrueAttrs["id"] = toggleTrueId;
            labelTrueAttrs["class"] += radioTrueActive;
            labelTrue.MergeAttributes(labelTrueAttrs);
            labelTrue.InnerHtml += radioTrue;
            labelTrue.InnerHtml += buttonLabel["labelTrue"];

            var labelFalse = new TagBuilder("label");

            labelFalseAttrs["id"] = toggleFalseId;
            labelFalseAttrs["class"] += radioFalseActive;
            labelFalse.MergeAttributes(labelFalseAttrs);
            labelFalse.InnerHtml += radioFalse;
            labelFalse.InnerHtml += buttonLabel["labelFalse"];

            div.InnerHtml += labelTrue;
            div.InnerHtml += labelFalse;

            return new MvcHtmlString(div.ToString(TagRenderMode.Normal));
        }

        //public static MvcHtmlString RadioButtonToggleFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression)
        //{
        //    var data = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);

        //    Func<TModel, TProperty> method = expression.Compile();
        //    bool value = Convert.ToBoolean(method(helper.ViewData.Model));

        //    IDictionary<string, object> attrs = HtmlHelper.AnonymousObjectToHtmlAttributes(new { });
        //    IDictionary<string, object> defaultDivAttrs = HtmlHelper.AnonymousObjectToHtmlAttributes(_radioButtonToggleDivDefaultAttrs);
        //    IDictionary<string, object> buttonLabel = HtmlHelper.AnonymousObjectToHtmlAttributes(_radioButtonToggleDefaultLabel);
        //    IDictionary<string, object> labelTrueAttrs = HtmlHelper.AnonymousObjectToHtmlAttributes(_radioButtonToggleDefaultAttrs);
        //    IDictionary<string, object> labelFalseAttrs = HtmlHelper.AnonymousObjectToHtmlAttributes(_radioButtonToggleDefaultAttrs);

        //    buttonLabel["labelTrue"] = buttonLabel["labelTrue"];
        //    buttonLabel["labelFalse"] = buttonLabel["labelFalse"];

        //    attrs["class"] = defaultDivAttrs["class"];
        //    attrs["data-toggle"] = defaultDivAttrs["data-toggle"] + (attrs.Keys.Contains("data-toggle") ? " " + attrs["data-toggle"] : "");

        //    string radioTrueActive = (value ? " active" : "") + " radio-button-toggle-true" + " btn-default";
        //    string radioFalseActive = (!value ? " active" : "") + " radio-button-toggle-false" + " btn-default";

        //    MvcHtmlString radioTrue = helper.RadioButton(data.PropertyName, true, value,
        //        new { id = data.PropertyName + (string.IsNullOrEmpty(Convert.ToString(buttonLabel["labelTrue"])) ? "True" : buttonLabel["labelTrue"]) });
        //    MvcHtmlString radioFalse = helper.RadioButton(data.PropertyName, false, !value,
        //        new { id = data.PropertyName + (string.IsNullOrEmpty(Convert.ToString(buttonLabel["labelFalse"])) ? "False" : buttonLabel["labelFalse"]) });

        //    TagBuilder div = new TagBuilder("div");
        //    div.MergeAttributes(attrs);

        //    TagBuilder labelTrue = new TagBuilder("label");

        //    labelTrueAttrs["class"] += radioTrueActive;
        //    labelTrue.MergeAttributes(labelTrueAttrs);
        //    labelTrue.InnerHtml += radioTrue;
        //    labelTrue.InnerHtml += buttonLabel["labelTrue"];

        //    TagBuilder labelFalse = new TagBuilder("label");

        //    labelFalseAttrs["class"] += radioFalseActive;
        //    labelFalse.MergeAttributes(labelFalseAttrs);
        //    labelFalse.InnerHtml += radioFalse;
        //    labelFalse.InnerHtml += buttonLabel["labelFalse"];

        //    div.InnerHtml += labelTrue;
        //    div.InnerHtml += labelFalse;

        //    return new MvcHtmlString(div.ToString(TagRenderMode.Normal));
        //    //return helper.RadioButtonToggleFor(expression, new {});
        //    //_radioButtonToggleTemplate = @"<div class='radio-button-toggle btn-group' data-toggle='buttons'>
        //    //    <label class='btn btn-default{0}'>
        //    //        {1}Yes
        //    //    </label>
        //    //    <label class='btn btn-default{2}'>
        //    //        {3}No
        //    //    </label>
        //    //</div>";

        //    //var data = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);

        //    //Func<TModel, TProperty> method = expression.Compile();
        //    //bool value = Convert.ToBoolean(method(helper.ViewData.Model));

        //    //string radioTrueActive = value ? " active" : "";
        //    //string radioFalseActive = !value ? " active" : "";
        //    //MvcHtmlString radioTrue = helper.RadioButton(data.PropertyName, true, value);
        //    //MvcHtmlString radioFalse = helper.RadioButton(data.PropertyName, false, !value);

        //    //var result = string.Format(_radioButtonToggleTemplate, radioTrueActive, radioTrue, radioFalseActive, radioFalse);

        //    //return new MvcHtmlString(result);
        //}
    }
}