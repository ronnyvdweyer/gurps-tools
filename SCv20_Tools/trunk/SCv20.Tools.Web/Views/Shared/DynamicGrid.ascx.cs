using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.IO;
using System.Reflection;

namespace SCv20.Tools.Web.Views.Shared {

    public partial class DynamicGrid : UserControlBase {
        private ITemplate _footerTemplate;
        private IEnumerable _dataSource;
        private object _currentItem;


        public DynamicGrid() {
            this.GridColum = new List<DynamicGridColumn>();

        }

        protected void Page_Init(object sender, EventArgs e) {
            var path = AppRelativeVirtualPath.Replace("~", "");
            var baseStyles = string.Format("<link href='{0}.css' type='text/css' rel='stylesheet' />", path);
            Page.Header.Controls.Add(new LiteralControl(baseStyles));
        }


        protected void Page_Load(object sender, EventArgs e) {
            if (!Page.IsPostBack) {
                CreateGridHeader();
            }
        }


        public string Title {
            get {
                return gridTitle.InnerHtml;
            }
            set {
                gridTitle.InnerHtml = value;
            }
        }


        public bool ShowPager {
            get {
                return PagerContainer.Visible;
            }
            set {
                PagerContainer.Visible = value;
            }
        }


        public String TitleStyle {
            get {
                return gridTitle.Attributes["style"];
            }
            set {
                gridTitle.Attributes.Add("style", value);
            }
        }


        public string TitleCssClass {
            get {
                return gridTitle.Attributes["class"];
            }
            set {
                gridTitle.Attributes.Add("class", value);
            }
        }





        public string CssClass {
            get {
                return QualityGridContainer.CssClass;
            }
            set {
                QualityGridContainer.CssClass = value;
            }
        }


        public Pager PagerControl {
            get {
                return GridPager;
            }
        }


        public List<DynamicGridColumn> GridColum {
            get;
            set;
        }


        private void CreateGridHeader() {
            StringWriter stringWriter = new StringWriter();
            using (HtmlTextWriter html = new HtmlTextWriter(stringWriter, "  ")) {
                html.AddAttribute("style", "border-collapse:collapse;width:100%");
                html.RenderBeginTag("table");                                                           //## <table>
                html.RenderBeginTag("tr");                                                              //##    <tr>
                foreach (var col in this.GridColum) {                                                   //##        <td>...</td>
                    html.Write(col.GetHeaderColHtml());                                                 //##        <td>...</td>
                }                                                                                       //##        <td>...</td>
                html.RenderEndTag();                                                                    //##    </tr>
                html.RenderEndTag();                                                                    //## </table>

                var table = new LiteralControl();
                table.Text = stringWriter.ToString();
                gridHeaderContainer.Controls.Add(table);
            }
        }


        private void CreateGridRow(object currentItem) {
            StringWriter stringWriter = new StringWriter();
            using (HtmlTextWriter html = new HtmlTextWriter(stringWriter, "  ")) {
                html.AddAttribute("style", "border-collapse:collapse;width:100%");
                html.RenderBeginTag("table");                                                           //## <table>
                html.RenderBeginTag("tr");                                                              //##    <tr>
                foreach (var col in this.GridColum) {                                                   //##        <td>...</td>
                    var value = Convert.ToString(DataBinder.Eval(currentItem, col.DataItemValue));      //##        <td>...</td>
                    html.Write(col.GetItemColHtml(value));                                              //##        <td>...</td>
                }                                                                                       //##        <td>...</td>
                html.RenderEndTag();                                                                    //##    </tr>
                html.RenderEndTag();                                                                    //## </table>

                var table = new LiteralControl();
                table.Text = stringWriter.ToString();
                gridItemContainer.Controls.Add(table);
            }
        }


        private void SetPagedDatasource<T>(T datasource) {

        }


        public override void DataBind() {
            //TODO: http://stackoverflow.com/questions/812673/convert-cast-ienumerable-to-ienumerablet

            //MethodInfo method = this.GetType().GetMethod("DataBind2");
            //MethodInfo generic = method.MakeGenericMethod(_dataSource.Cast<object>().GetType());
            //generic.Invoke(this, new object[] {_dataSource});

            //if (this.ShowPager)
            //    throw new InvalidOperationException("You must suppy a Strongly Typed Collection (Generics) to call this method when ShowPager is true." +
            //        "Please, use the DataBind<T>() method.");
            //else
            DataBind2<Object>(_dataSource);
        }

        private void DataBind2<T>(IEnumerable ds) {
            IEnumerator ie = null;

            if (PagerControl.Visible)
                ie = PagerControl.PaginateDataSource<T>((IEnumerable<T>)ds).GetEnumerator();
            else
                ie = _dataSource.GetEnumerator();

            while (ie.MoveNext()) {
                CreateGridRow(ie.Current);
                _currentItem = ie.Current;
            }

            AddFooterTemplateAsControl(_footerTemplate, null);

            base.DataBind();
        }


        [TemplateContainer(typeof(SimpleTemplateItem))]
        public ITemplate FooterTemplate {
            get {
                return _footerTemplate;
            }
            set {
                _footerTemplate = value;
            }
        }


        public IEnumerable DataSource {
            get {
                return _dataSource;
            }
            set {
                _dataSource = value;
            }
        }


        public object CurrentItem {
            get {
                return _currentItem;
            }
            set {
                _currentItem = value;
            }
        }


        private void AddFooterTemplateAsControl(ITemplate anyTemplate, object currentItem) {
            if (anyTemplate != null) {
                gridFooterTemplate.Controls.Clear();
                SimpleTemplateItem templateContentHolder = new SimpleTemplateItem(currentItem, 0);
                anyTemplate.InstantiateIn(templateContentHolder);
                gridFooterTemplate.Controls.Add(templateContentHolder);
            }
            else {
                gridFooterTemplate.Controls.Add(new LiteralControl("&nbsp;"));
            }
        }


        #region --- Nested Classes ----------------------------------------------------------------


        public class DynamicGridColumn {
            public string HeaderText { get; set; }

            public string HeaderStyle { get; set; }

            public string HeaderCssClass { get; set; }

            public Unit HeaderWidth { get; set; }

            public string DataItemStyle { get; set; }

            public string DataItemCssClass { get; set; }

            public string DataItemValue { get; set; }

            public string GetHeaderColHtml() {
                StringWriter stringWriter = new StringWriter();
                using (HtmlTextWriter html = new HtmlTextWriter(stringWriter, "  ")) {
                    var cssClazz = new string[] { "tb-header-item", string.Empty };

                    //-- <th>...</th>
                    html.AddAttribute("style", "padding:0;margin:0");
                    if (this.HeaderWidth != null)
                        html.AddStyleAttribute(HtmlTextWriterStyle.Width, this.HeaderWidth.ToString());
                    if (HeaderStyle != null)
                        html.AddAttribute("style", this.HeaderStyle);
                    if (HeaderCssClass != null)
                        cssClazz[1] = this.DataItemCssClass;
                    html.AddAttribute("class", string.Join(" ", cssClazz).Trim());
                    html.RenderBeginTag("th");


                    //-- <span>...</span>
                    html.AddAttribute("style", "display:block;word-wrap:break-word;overflow:hidden");
                    if (this.HeaderWidth != null)
                        html.AddStyleAttribute(HtmlTextWriterStyle.Width, this.HeaderWidth.ToString());
                    html.RenderBeginTag("span");
                    html.Write(this.HeaderText);


                    html.RenderEndTag();

                    html.RenderEndTag();

                    return stringWriter.ToString();
                }
            }

            public string GetItemColHtml(string value) {
                StringWriter stringWriter = new StringWriter();
                using (HtmlTextWriter html = new HtmlTextWriter(stringWriter, "  ")) {
                    var cssClazz = new string[] { "tb-row-item", string.Empty };

                    //-- <td>...</td>
                    html.AddAttribute("style", "padding:0;margin:0");
                    if (this.HeaderWidth != null)
                        html.AddStyleAttribute(HtmlTextWriterStyle.Width, this.HeaderWidth.ToString());
                    if (this.DataItemStyle != null)
                        html.AddAttribute("style", this.DataItemStyle);
                    if (this.DataItemCssClass != null)
                        cssClazz[1] = this.DataItemCssClass;

                    html.AddAttribute("class", string.Join(" ", cssClazz).Trim());
                    html.RenderBeginTag("td");


                    //-- <span>...</span>
                    html.AddAttribute("style", "display:block;word-wrap:break-word;overflow:hidden");
                    if (this.HeaderWidth != null)
                        html.AddStyleAttribute(HtmlTextWriterStyle.Width, this.HeaderWidth.ToString());
                    html.RenderBeginTag("span");
                    html.Write(value);


                    html.RenderEndTag();

                    html.RenderEndTag();

                    return stringWriter.ToString();
                }
            }
        }






        public class SimpleTemplateItem : Control, INamingContainer, IDataItemContainer {
            private object _currentItem;
            private int _index;

            public SimpleTemplateItem(object currentItem, int index) {
                _currentItem = currentItem;
                _index = index;
            }

            public object DataItem {
                get {
                    return _currentItem;
                }
            }

            public int DataItemIndex {
                get {
                    return _index;
                    //throw new Exception("The method or operation is not implemented.");
                }
            }

            public int DisplayIndex {
                get {
                    return _index;
                    //throw new Exception("The method or operation is not implemented.");
                }
            }
        }

        #endregion

        public int PageSize {
            get {
                return PagerControl.PageSize;
            }
            set {
                PagerControl.PageSize = value;
            }
        }
    }

}