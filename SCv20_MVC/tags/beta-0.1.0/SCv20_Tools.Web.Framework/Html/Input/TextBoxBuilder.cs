using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace SCv20_Tools.Web.Framework.Html.Input {
    public class TextBoxBuilder: IHtmlString {
        private readonly HtmlHelper     _helper;
        private readonly TextBoxOptions _options;


        public TextBoxBuilder(HtmlHelper helper, TextBoxOptions options) {
            _helper  = helper;
            _options = options;
        }


        public string ToHtmlString() {
            return ToString();
        }


        public override string ToString() {
            return RenderControl();
        }

                
        private string RenderControl() {
            if (!_options.Visible)
                return null;

            var builder = new TagBuilder("input");
            
            builder.Attributes.Add("type", "text");

            TagBuilderHelper.Process(builder, _options);

            TagBuilderHelper.AddValidation(_helper, builder);

            return builder.ToString(TagRenderMode.SelfClosing);
        }
    }
}
