using System;
using System.Collections;
using System.Linq.Expressions;
using System.Web.Mvc;
using SCv20_Tools.Web.Framework.Html.Input;

namespace SCv20_Tools.Web.Framework.Html {

    public class CustomHelperFactory<TModel> {
        private HtmlHelper<TModel> _helper;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomHelperFactory{TModel}"/> class.
        /// </summary>
        /// <param name="helper">The HtmlHelper reference.</param>
        public CustomHelperFactory(HtmlHelper<TModel> helper) {
            _helper = helper;
        }

        /// <summary>
        /// Renders a HTML ComboBox element.
        /// </summary>
        public DropDownListBuilder DropDownListFor<TProp, TSource>(Expression<Func<TModel, TProp>> targetModelProperty,
            Expression<Func<TModel, TSource>> dataSource, string dataValueField, string dataTextField, DropDownListOptions options) where TSource : IEnumerable {
            if (options == null)
                options = new DropDownListOptions() { FirstItemText = "-- Please Select --", FirstItemValue = "0" };

            var props = Extract(targetModelProperty);
            var source = Extract(dataSource);

            options.ID = props.FullId;
            options.Name = props.FullName;
            options.SelectedValue = props.ValueAsText;

            var builder = new DropDownListBuilder(_helper, (IEnumerable)source.ValueAsObject, dataValueField, dataTextField, options);

            return builder;
        }

        /// <summary>
        /// Renders a HTML TextArea element .
        /// </summary>
        public TextAreaBuilder TextAreaFor<TProp>(Expression<Func<TModel, TProp>> targetModelProperty, TextBoxOptions options) {
            if (options == null)
                options = new TextBoxOptions();

            var meta = Extract(targetModelProperty);

            options.ID = meta.FullId;
            options.Name = meta.FullName;
            options.Value = meta.ValueAsText;

            return new TextAreaBuilder(_helper, options);
        }

        /// <summary>
        /// Renders a HTML Input type text element.
        /// </summary>
        public TextBoxBuilder TextBoxFor<TProp>(Expression<Func<TModel, TProp>> targetModelProperty, TextBoxOptions options = null) {
            if (options == null)
                options = new TextBoxOptions();

            var field_value = Convert.ToString(ModelMetadata.FromLambdaExpression(targetModelProperty, _helper.ViewData).Model);
            var field_name = ExpressionHelper.GetExpressionText(targetModelProperty);

            var full_id = _helper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(field_name);
            var full_name = _helper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(field_name);

            options.ID = string.IsNullOrWhiteSpace(options.ID) ? full_id : options.ID;
            options.Name = string.IsNullOrWhiteSpace(options.Name) ? full_name : options.Name;
            options.Value = string.IsNullOrWhiteSpace(options.Value) ? field_value : options.Value;

            return new TextBoxBuilder(_helper, options);
        }

        /// <summary>
        /// Helper function to extract ModelMetadata information of given expresion;
        /// </summary>
        /// <returns>Metadata Information of givel expression.</returns>
        private Metadata Extract<TProp>(Expression<Func<TModel, TProp>> expression) {
            var meta = new Metadata();

            meta.ValueAsText = Convert.ToString(ModelMetadata.FromLambdaExpression(expression, _helper.ViewData).Model);
            meta.FieldName = ExpressionHelper.GetExpressionText(expression);
            meta.FullId = _helper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(meta.FieldName);
            meta.FullName = _helper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(meta.FieldName);
            meta.ValueAsObject = ModelMetadata.FromLambdaExpression(expression, _helper.ViewData).Model;

            return meta;
        }
    }

    #region -- Support Classes --------------------------------------------------------------------

    /// <summary>
    /// Class to store model metadata in friendly format.
    /// </summary>
    internal class Metadata {

        public string FieldName { get; set; }

        public string FullId { get; set; }

        public string FullName { get; set; }

        public object ValueAsObject { get; set; }

        public string ValueAsText { get; set; }
    }

    #endregion -- Support Classes --------------------------------------------------------------------
}