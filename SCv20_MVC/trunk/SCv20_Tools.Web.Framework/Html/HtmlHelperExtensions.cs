using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;

namespace SCv20_Tools.Web.Framework.Html {
    public static class HtmlHelperExtensions {

        public static SpycraftControlsFactory<TModel> SpyCraft<TModel>(this HtmlHelper<TModel> helper) {
            return new SpycraftControlsFactory<TModel>(helper);
        }


        public static CustomHelperFactory<TModel> MeHZ<TModel>(this HtmlHelper<TModel> helper) {
            return new CustomHelperFactory<TModel>(helper);
        }

        public static string IdFor<TModel, TResult>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TResult>> targetModelProperty) {

            var name = ExpressionHelper.GetExpressionText(targetModelProperty);

            //--> Bloco Necessário quando estiver usando um EditorFor.
            string fullName = helper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(name);
            string fullId = helper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(name);
            //<--

            return fullId;
        }
    }
}
