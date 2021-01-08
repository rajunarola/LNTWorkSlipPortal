using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using LNTSlipPortal_Repository.DTO;

namespace LNTSlipPortal
{
    public static class HtmlExtensions
    {


        public static MvcHtmlString DropdownListForSelect2Remote<TModel, TValue>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TValue>> expression, IEnumerable<SelectListItem> itemList, object htmlAttributes = null, bool renderFormControlClass = true, string placeholder = "--select--", string text = "", string value = "")
        {
            var result = new StringBuilder();
            var attrs = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
            if (renderFormControlClass)
                attrs = AddFormControlClassToHtmlAttributes1(attrs);

            result.Append(helper.DropDownListFor(expression, itemList, htmlAttributes));
            var script = BindSelect2FromRemote(attrs["mode"].ToString(), attrs["id"].ToString(), placeholder, text, value);
            result.AppendLine("<script type='text/javascript'>" + script + "</script>");
            return MvcHtmlString.Create(result.ToString());
        }

        public static MvcHtmlString DropdownListForSelect2<TModel, TValue>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TValue>> expression, List<AutoCompleteResponse> itemList, object htmlAttributes = null, bool renderFormControlClass = true)
        {
            var result = new StringBuilder();
            var attrs = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
            if (renderFormControlClass)
                attrs = AddFormControlClassToHtmlAttributes1(attrs);
            // IEnumerable < SelectListItem >
            List<SelectListItem> lst = new List<SelectListItem>();
            if (itemList != null)
            {
                foreach (AutoCompleteResponse obj in itemList)
                {
                    lst.Add(new SelectListItem { Text = obj.text, Value = obj.id });
                }
            }

            result.Append(helper.DropDownListFor(expression, lst, attrs));
            var script = BindSelect2FromModel(attrs["id"].ToString());
            result.AppendLine("<script type='text/javascript'>" + script + "</script>");
            return MvcHtmlString.Create(result.ToString());
        }

        public static string BindSelect2FromModel(string id)
        {
            var data = "$('#" + id + "').select2()";
            return data;
        }

        public static RouteValueDictionary AddFormControlClassToHtmlAttributes1(IDictionary<string, object> htmlAttributes)
        {
            if (htmlAttributes["class"] == null || string.IsNullOrEmpty(htmlAttributes["class"].ToString()))
                htmlAttributes["class"] = "form-control js-example-data-array";
            else
                if (!htmlAttributes["class"].ToString().Contains("form-control"))
                htmlAttributes["class"] += " form-control js-example-data-array";

            return htmlAttributes as RouteValueDictionary;
        }
        public static string BindSelect2FromRemote(string mode, string domid, string placeholder, string text, string value)
        {
            var data = @"$('#" + domid + "').createAutocomplete({Mode: '" + mode + "',SelectedText:'" + text + "',SelectedValue:'" + value + "',PlaceHolder:'" + placeholder + "'})";
            return data;
        }


    }
}