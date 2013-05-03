using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace SCv20_Tools.Web.Framework.Html {
    public class SpycraftControlsFactory<TModel> {
        private HtmlHelper<TModel> _helper;

        public SpycraftControlsFactory(HtmlHelper<TModel> helper) {
            _helper = helper;
        }


        public AlertBoxBuilder AlertBox(string text) {
            return new AlertBoxBuilder(_helper, text);
        }


        public AlertBoxBuilder AlertBoxFor<TTextProperty>(Expression<Func<TModel, TTextProperty>> textExpression) {
            var text = (string)ModelMetadata.FromLambdaExpression(textExpression, _helper.ViewData).Model;
            return new AlertBoxBuilder(_helper, text);
        }
    }
}