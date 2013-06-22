using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using System.Web.UI;

namespace SCv20_Tools.Web.Framework.Html.Repeater {
    public class RepeaterBuilder: IHtmlString {
        private readonly HtmlHelper          _helper;
        private readonly IEnumerable         _dataSource;


        public RepeaterBuilder(HtmlHelper helper, IEnumerable dataSource) {
            _helper         = helper;
            _dataSource     = dataSource;
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

            var builder = new TagBuilder("select");
            
            TagBuilderHelper.Process(builder, _options);
            TagBuilderHelper.AddValidation(_helper, builder);
            
            builder.InnerHtml = RenderOptions();

            return builder.ToString();
        }


        private string RenderOptions() {
            StringBuilder sb = new StringBuilder("\n");

            //Adiciona a primeira opção se existente
            if (_options.FirstItemText != null || _options.FirstItemValue != null) {
                var firstOption = new TagBuilder("option");
                firstOption.Attributes.Add("value", _options.FirstItemValue);
                firstOption.SetInnerText(_options.FirstItemText);
                sb.AppendLine(firstOption.ToString());
            }

            if (_dataSource != null) {
                var enumerator = _dataSource.GetEnumerator();

                while (enumerator.MoveNext()) {
                    var builder = new TagBuilder("option");
                    var value = Convert.ToString(DataBinder.Eval(enumerator.Current, _dataValueField));
                    var text = Convert.ToString(DataBinder.Eval(enumerator.Current, _dataTextField));

                    builder.Attributes.Add("value", value);
                    builder.SetInnerText(text);

                    if (_options.SelectedValue == value)
                        builder.Attributes.Add("selected", "selected");

                    sb.AppendLine(builder.ToString());
                }
            }

            return sb.ToString();
        }
    }
}
