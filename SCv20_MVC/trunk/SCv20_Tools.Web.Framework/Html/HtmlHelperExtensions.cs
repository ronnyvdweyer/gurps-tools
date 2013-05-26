using System;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace SCv20_Tools.Web.Framework.Html {

    public static class HtmlHelperExtensions {

        /// <summary>
        /// Gets a Reference to SpyCraft Custom Control Factory
        /// </summary>
        public static SpycraftControlsFactory<TModel> SpyCraft<TModel>(this HtmlHelper<TModel> helper) {
            return new SpycraftControlsFactory<TModel>(helper);
        }

        /// <summary>
        /// Gets a Reference to MeHZ Custom Helper Factory.
        /// </summary>
        public static CustomHelperFactory<TModel> MeHZ<TModel>(this HtmlHelper<TModel> helper) {
            return new CustomHelperFactory<TModel>(helper);
        }

        #region -- ASP.Net MVC Base Helper Extensions -----------------------------------------------

        public static string IdFor<TModel, TResult>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TResult>> targetModelProperty) {
            var name = ExpressionHelper.GetExpressionText(targetModelProperty);
            //--> Bloco Necessário quando estiver usando um EditorFor.
            string fullName = helper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(name);
            string fullId = helper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(name);
            //<--
            return fullId;
        }

        public static IHtmlString ValueFor<TModel, TResult>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TResult>> targetModelProperty) {
            var value = ModelMetadata.FromLambdaExpression(targetModelProperty, helper.ViewData).Model;
            var html = Convert.ToString(value).Replace(Environment.NewLine, "<br/>");
            
            return new HtmlString(html);
        }

        #endregion -- ASP.Net MVC Base Helper Extensions -----------------------------------------------
    }
}