using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Web.Routing;

namespace PagedListDemo.Common
{
    public static class HtmlHelpers
    {
        static HtmlHelpers()
        {
            HtmlHelperActionFunc = () => new HtmlHelperActionInvoker();
        }

        public static Func<HtmlHelperActionInvoker> HtmlHelperActionFunc { get; set; }

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

        private static string _template = "~/Views/Shared/_RadioButtonToggleTemplate.cshtml";

        public static MvcHtmlString RadioButtonToggleFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression)
        {
            string template = string.Empty;
            try
            {
                template = helper.Partial(_template).ToHtmlString();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            var data = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);

            Func<TModel, TProperty> method = expression.Compile();
            bool value = Convert.ToBoolean(method(helper.ViewData.Model));

            string radioTrueActive = value ? " active" : "";
            string radioFalseActive = !value ? " active" : "";
            string radioTrueId = data.PropertyName + "True";
            string radioFalseId = data.PropertyName + "False";
            MvcHtmlString radioTrue = helper.RadioButton(data.PropertyName, true, value, new { id = radioTrueId });
            MvcHtmlString radioFalse = helper.RadioButton(data.PropertyName, false, !value, new { id = radioFalseId });

            var result = string.Format(template, radioTrueActive, radioTrueId, radioTrue, radioFalseActive, radioFalseId, radioFalse);

            return new MvcHtmlString(result);
        }

        public static MvcHtmlString RadioButtonToggleFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression, string templateName)
        {
            string template = string.Empty;
            try
            {
                template = helper.Partial(templateName).ToHtmlString();
            }
            catch
            {
                template = helper.Partial(_template).ToHtmlString();
            }

            var data = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);

            Func<TModel, TProperty> method = expression.Compile();
            bool value = Convert.ToBoolean(method(helper.ViewData.Model));

            string radioTrueActive = value ? " active" : "";
            string radioFalseActive = !value ? " active" : "";
            string radioTrueId = data.PropertyName + "True";
            string radioFalseId = data.PropertyName + "False";
            MvcHtmlString radioTrue = helper.RadioButton(data.PropertyName, true, value, new { id = radioTrueId });
            MvcHtmlString radioFalse = helper.RadioButton(data.PropertyName, false, !value, new { id = radioFalseId });

            var result = string.Format(template, radioTrueActive, radioTrueId, radioTrue, radioFalseActive, radioFalseId, radioFalse);

            return new MvcHtmlString(result);
        }

        public static MvcHtmlString RadioButtonToggleFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes)
        {
            var data = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);

            Func<TModel, TProperty> method = expression.Compile();
            bool value = Convert.ToBoolean(method(helper.ViewData.Model));

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

            //buttonLabel["labelTrue"] = toggleButtons["labelTrue"] ?? buttonLabel["labelTrue"];
            //buttonLabel["labelFalse"] = toggleButtons["labelFalse"] ?? buttonLabel["labelFalse"];
            //labelTrueAttrs["class"] = labelTrueAttrs["class"] + (toggleButtons.Keys.Contains("class") ? " " + toggleButtons["class"] : "");
            //labelFalseAttrs["class"] = labelFalseAttrs["class"] + (toggleButtons.Keys.Contains("class") ? " " + toggleButtons["class"] : "");

            //attrs["class"] = defaultDivAttrs["class"] + (attrs.Keys.Contains("class") ? " " + attrs["class"] : "");
            //attrs["data-toggle"] = defaultDivAttrs["data-toggle"] + (attrs.Keys.Contains("data-toggle") ? " " + attrs["data-toggle"] : "");

            string radioTrueActive = (value ? " active" : "") + " radio-button-toggle-true";
            string radioFalseActive = (!value ? " active" : "") + " radio-button-toggle-false";
            string toggleTrueId = data.PropertyName + (string.IsNullOrEmpty(Convert.ToString(buttonLabel["labelTrue"])) ? "True" : buttonLabel["labelTrue"]);
            string toggleFalseId = data.PropertyName + (string.IsNullOrEmpty(Convert.ToString(buttonLabel["labelFalse"])) ? "False" : buttonLabel["labelFalse"]);

            MvcHtmlString radioTrue = helper.RadioButton(data.PropertyName, true, value, new { id = toggleTrueId });
            MvcHtmlString radioFalse = helper.RadioButton(data.PropertyName, false, !value, new { id = toggleFalseId });

            TagBuilder div = new TagBuilder("div");
            div.MergeAttributes(attrs);

            TagBuilder labelTrue = new TagBuilder("label");

            labelTrueAttrs["id"] = toggleTrueId;
            labelTrueAttrs["class"] += radioTrueActive;
            labelTrue.MergeAttributes(labelTrueAttrs);
            labelTrue.InnerHtml += radioTrue;
            labelTrue.InnerHtml += buttonLabel["labelTrue"];

            TagBuilder labelFalse = new TagBuilder("label");

            labelFalseAttrs["id"] = toggleFalseId;
            labelFalseAttrs["class"] += radioFalseActive;
            labelFalse.MergeAttributes(labelFalseAttrs);
            labelFalse.InnerHtml += radioFalse;
            labelFalse.InnerHtml += buttonLabel["labelFalse"];

            div.InnerHtml += labelTrue;
            div.InnerHtml += labelFalse;

            return new MvcHtmlString(div.ToString(TagRenderMode.Normal));
        }
    }

    public class HtmlHelperActionInvoker
    {
        public virtual MvcHtmlString InvokeAction(HtmlHelper helper, string action, string controller)
        {
            return helper.Action(action, controller);
        }
    }
}