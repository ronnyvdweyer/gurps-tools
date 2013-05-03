using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace SCv20_Tools.Web.Framework.Html {
    internal static class TagBuilderHelper {
        
        public static void ProcessBase(TagBuilder builder, BaseHtmlOptions options) {
            var attr = builder.Attributes;

            if (options.CssClass != null)
                attr.Add("class", options.CssClass);

            if (options.ID != null)
                attr.Add("id", options.ID);


            if (options.Name != null)
                attr.Add("name", options.Name);

            if (options.Style != null)
                attr.Add("style", options.Style);

            if (options.TabIndex.HasValue)
                attr.Add("tabindex", options.TabIndex.Value.ToString());

            if (options.Title != null)
                attr.Add("title", options.Title);

            if (options.HtmlAttributes != null)
                builder.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(options.HtmlAttributes), true);


            //RouteValueDictionary attributes = new RouteValueDictionary(options);
            //foreach (var attr in attributes) {
            //    builder.Attributes.Add(attr.Key, (string)attr.Value);
            //}
        }


        public static void Process(TagBuilder builder, TextBoxOptions options) {
            ProcessBase(builder, options);
            
            var attr = builder.Attributes;

            if (options.AutoFocus)
                attr.Add("autofocus", "autofocus");


            if (options.Disabled)
                attr.Add("disabled", "disabled");


            if (options.MaxLength > 0)
                attr.Add("maxlength", options.MaxLength.ToString());


            if (options.ReadOnly)
                attr.Add("readonly", "readonly");


            if (options.Size > 0)
                attr.Add("size", options.Size.ToString());
            

            if (options.Value != null)
                attr.Add("value", options.Value);
        }



        public static void Process(TagBuilder builder, DropDownListOptions options) {
            ProcessBase(builder, options);

            var attr = builder.Attributes;


            if (options.AutoFocus)
                attr.Add("autofocus", "autofocus");


            if (options.Disabled)
                attr.Add("disabled", "disabled");


            if (options.ReadOnly)
                attr.Add("readonly", "readonly");


            if (options.Size > 0)
                attr.Add("size", options.Size.ToString());
        }

        public static void AddValidation(HtmlHelper htmlHelper, TagBuilder builder) {
            ModelState state = null;

            var elementName = builder.Attributes["name"];

            if (string.IsNullOrWhiteSpace(elementName))
                throw new InvalidOperationException("Tag Name not defined");

            if (htmlHelper.ViewData.ModelState.TryGetValue(elementName, out state)) {
                if (state.Errors.Count > 0)
                    builder.AddCssClass(HtmlHelper.ValidationInputCssClassName);
            }
        }
    }
}
