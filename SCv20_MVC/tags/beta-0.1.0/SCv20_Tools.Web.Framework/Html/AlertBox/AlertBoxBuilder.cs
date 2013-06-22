using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace SCv20_Tools.Web.Framework.Html {

    public class AlertBoxBuilder : IHtmlString {
        private readonly HtmlHelper _helper;
        private readonly string _text;

        public AlertBoxBuilder(HtmlHelper helper, string text) {
            this._helper = helper;
            this._text = text;
        }

        public string ToHtmlString() {
            return ToString();
        }

        public override string ToString() {
            return RenderControl();
        }

        private string RenderControl() {
            var wrapper = new TagBuilder("div");
            wrapper.AddCssClass("alert-box");

            //HtmlHelper.AnonymousObjectToHtmlAttributes(object);

            var closeButton = new TagBuilder("a");
            closeButton.AddCssClass("close");
            closeButton.Attributes.Add("href", "");
            closeButton.InnerHtml = "×";

            wrapper.InnerHtml = _text;
            wrapper.InnerHtml += closeButton.ToString();

            //return _helper.Partial("SpyCraftHtml/xpto").ToHtmlString();

            return wrapper.ToString();
        }
    }
}