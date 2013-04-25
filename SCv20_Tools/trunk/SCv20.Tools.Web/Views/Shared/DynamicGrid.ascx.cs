using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SCv20.Tools.Web.Views.Shared {

    public partial class DynamicGrid : UserControlBase {

        private object      _currentItem;
        
        private IEnumerable _dataSource;
        
        private ITemplate   _footerTemplate;


        #region -- Event Model --------------------------------------------------------------------

        public event PageIndexChangedHandler PageIndexChanged;

        public event SelectedItemsHandler    SelectedItems;

        public delegate void PageIndexChangedHandler(object sender, Pager.PageIndexChangedEventArgs e);

        public delegate void SelectedItemsHandler(object sender, SelectedItemsEventArgs e);

        #endregion


        public DynamicGrid() {
            this.DynamicColumns      = new List<DynamicColumn>();
            this.DataNotFoundMessage = "No Records Found";
            this.ViewStateMode       = ViewStateMode.Enabled;
        }


        public string CssClass {
            get {
                return QualityGridContainer.CssClass;
            }
            set {
                QualityGridContainer.CssClass = value;
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


        public Pager PagerControl {
            get {
                return GridPager;
            }
        }


        public int PageSize {
            get {
                return PagerControl.PageSize;
            }
            set {
                PagerControl.PageSize = value;
            }
        }


        public bool PaginateResults {
            get {
                var o = ViewState["PaginateResults"];
                if(o!=null)
                    return (bool)ViewState["PaginateResults"];
                
                return false;
            }
            set {
                ViewState["PaginateResults"] = value;
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


        public string TitleCssClass {
            get {
                return gridTitle.Attributes["class"];
            }
            set {
                gridTitle.Attributes.Add("class", value);
            }
        }


        public string TitleStyle {
            get {
                return gridTitle.Attributes["style"];
            }
            set {
                gridTitle.Attributes.Add("style", value);
            }
        }


        public string DataNotFoundMessage {
            get;
            set;
        }

        public List<Object> SelectedItemCollection {
            get;
            private set;
        }


        public List<DynamicColumn> DynamicColumns {
            get;
            set;
        }


        [TemplateContainer(typeof(SimpleTemplateItem)), PersistenceMode(PersistenceMode.InnerProperty), TemplateInstance(TemplateInstance.Single)]
        public ITemplate FooterTemplate {
            get {
                return _footerTemplate;
            }
            set {
                _footerTemplate = value;
            }
        }


        private object CurrentItem {
            get {
                return _currentItem;
            }
            set {
                _currentItem = value;
            }
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
            DataBindTyped<Object>(_dataSource);
        }
        
        
        #region -- Event Handlers -----------------------------------------------------------------


        protected void Page_PreInit(object sender, EventArgs e) {
            gridTitle.InnerHtml = "No Title Defined";
        }


        protected void Page_Init(object sender, EventArgs e) {
            var path = AppRelativeVirtualPath.Replace("~", "");
            var baseStyles = string.Format("<link href='{0}.css' type='text/css' rel='stylesheet' />", path);
            Page.Header.Controls.Add(new LiteralControl(baseStyles));
            PagerControl.PageIndexChanged += new Pager.PageIndexChangedHandler(PagerControl_PageIndexChanged);

            var checks = Request[this.ID + "$check"];
            if (checks != null) {
                var list = checks.Split(',').Cast<Object>().ToList();
                OnItemSelected(new SelectedItemsEventArgs(list));
                SelectedItemCollection = list;
            }
        }


        protected void Page_Load(object sender, EventArgs e) {
            if (!Page.IsPostBack) {
                CreateGridHeader();
            }
        }


        protected void PagerControl_PageIndexChanged(object sender, Pager.PageIndexChangedEventArgs e) {
            if (PageIndexChanged != null)
                PageIndexChanged(this, e);
        }


        #endregion


        #region -- Private Methods ----------------------------------------------------------------


        /// <summary>
        /// Cria a linha de Cabeçalho da Tabela.
        /// </summary>
        private void CreateGridHeader() {
            StringWriter stringWriter = new StringWriter();
            using (HtmlTextWriter html = new HtmlTextWriter(stringWriter, "  ")) {
                gridHeaderContainer.Controls.Clear();

                html.AddAttribute("style", "border-collapse:collapse;width:100%");
                html.RenderBeginTag("table");                                                           //## <table>
                html.RenderBeginTag("tr");                                                              //##    <tr>
                foreach (var col in this.DynamicColumns) {                                                   //##        <td>...</td>
                    html.Write(col.GetHeaderColHtml());                                                 //##        <td>...</td>
                }                                                                                       //##        <td>...</td>
                html.RenderEndTag();                                                                    //##    </tr>
                html.RenderEndTag();                                                                    //## </table>

                var table = new LiteralControl();
                table.Text = stringWriter.ToString();
                gridHeaderContainer.Controls.Add(table);
            }
        }
        

        /// <summary>
        /// Cria a linha de dados da tabela.
        /// </summary>
        /// <param name="currentItem">Item corrente da coleção sendo processado.</param>
        private void CreateGridRow(object currentItem) {
            StringWriter stringWriter = new StringWriter();
            using (HtmlTextWriter html = new HtmlTextWriter(stringWriter, "  ")) {
                html.AddAttribute("style", "border-collapse:collapse;width:100%");
                html.RenderBeginTag("table");                                                               //## <table>
                html.RenderBeginTag("tr");                                                                  //##    <tr>

                //if (currentItem == null)
                //    html.Write("<td>" + DataNotFoundMessage + "</td>");
                //else {
                    foreach (var col in this.DynamicColumns) {                                              //##        <td>...</td>
                        var value = Convert.ToString(DataBinder.Eval(currentItem, col.DataItemValue));      //##        <td>...</td>
                        var itemId = string.Empty;                                                          //##        <td>...</td>
                        var isChecked = false;

                        if (!string.IsNullOrWhiteSpace(col.DataItemValueID))                                //##        <td>...</td>
                            itemId = Convert.ToString(DataBinder.Eval(currentItem, col.DataItemValueID));   //##        <td>...</td>

                        if (this.SelectedItemCollection != null)
                            isChecked = this.SelectedItemCollection.Where(c => (string)c == itemId).Any();

                        html.Write(col.GetItemColHtml(value, itemId, this.ID, isChecked));                  //##        <td>...</td>
                    }                                                                                       //##        <td>...</td>
                //}
                html.RenderEndTag();                                                                        //##    </tr>
                html.RenderEndTag();                                                                        //## </table>

                var table = new LiteralControl();
                table.Text = stringWriter.ToString();
                gridItemContainer.Controls.Add(table);
            }
        }


        /// <summary>
        /// Cria o template do rodapé de acordo com o template definido pelo usuário.
        /// </summary>
        private void CreateFooterTemplate(ITemplate anyTemplate, object currentItem) {
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


        /// <summary>
        /// Executa o databind paginado de uma coleção genérica;
        /// </summary>
        private void DataBindTyped<T>(IEnumerable ds) {
            IEnumerator ie = null;

            CreateGridHeader();
            CreateFooterTemplate(_footerTemplate, null);

            if (_dataSource != null) {
                if (this.PaginateResults)
                    ie = PagerControl.PaginateDataSource<T>((IEnumerable<T>)ds).GetEnumerator();
                else
                    ie = _dataSource.GetEnumerator();

                while (ie.MoveNext()) {
                    CreateGridRow(ie.Current);
                    _currentItem = ie.Current;
                }
            }

            base.DataBind();
        }


        /// <summary>
        /// Dispara o evento de items selecionados quando solicitado.
        /// </summary>
        private void OnItemSelected(SelectedItemsEventArgs args) {
            if (SelectedItems != null)
                SelectedItems(this, args);
        }


        #endregion


        #region --- Nested Classes ----------------------------------------------------------------


        /// <summary>
        /// Classe que represnta os itens selecionados na grid.
        /// </summary>
        public class SelectedItemsEventArgs : EventArgs {
            public SelectedItemsEventArgs(IList<Object> selectedItems) {
                this.SelectedItems = selectedItems;
            }

            public IList<Object> SelectedItems {
                get;
                private set;
            }
        }


        /// <summary>
        /// Class que representa um UserTemplate para o rodapé da grid.
        /// </summary>
        public class SimpleTemplateItem : Control, INamingContainer, IDataItemContainer {
            private object  _currentItem;
            private int     _index;

            public SimpleTemplateItem(object currentItem, int index) {
                _currentItem = currentItem;
                _index = index;
            }

            public object DataItem      {
                get {
                    return _currentItem;
                }
            }

            public int DataItemIndex    {
                get {
                    return _index;
                    //throw new Exception("The method or operation is not implemented.");
                }
            }

            public int DisplayIndex     {
                get {
                    return _index;
                    //throw new Exception("The method or operation is not implemented.");
                }
            }
        }

        /// <summary>
        /// Class que representa uma coluna da grid inlcuindo o cabeçalho da coluna.
        /// </summary>
        public class DynamicColumn {

            public string               DataItemCssClass { get; set; }

            public string               DataItemStyle    { get; set; }

            public string               DataItemValue    { get; set; }

            public string               HeaderCssClass   { get; set; }

            public string               HeaderStyle      { get; set; }

            public string               HeaderText       { get; set; }
            
            public Unit                 HeaderWidth      { get; set; }

            public DynamicHeaderType    HeaderType       { get; set; }
            
            public string               DataItemValueID  { get; set; }


            /// <summary>
            /// Cria o HTML da Coluna de Cabeçalho
            /// </summary>
            public string GetHeaderColHtml() {
                StringWriter stringWriter = new StringWriter();
                using (HtmlTextWriter html = new HtmlTextWriter(stringWriter, "  ")) {
                    var cssClazz = new string[] { "tb-header-item", string.Empty };

                    //-- <th>...</th>
                    html.AddAttribute("style", "padding:0;margin:0");   //width:100px; border:1px solid red; word-wrap:break-word
                                                                        //http://stackoverflow.com/questions/1258416/word-wrap-in-a-html-table
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

                    if (HeaderType == DynamicHeaderType.Checkbox) {
                        html.AddAttribute("type", "checkbox");
                        html.AddAttribute("class", "tb-row-check");

                        html.RenderBeginTag("input");
                        html.RenderEndTag();

                    }
                    else {
                        html.Write(this.HeaderText);
                    }

                    html.RenderEndTag();

                    html.RenderEndTag();

                    return stringWriter.ToString();
                }
            }


            /// <summary>
            /// Cria o HTML da Coluna de Item
            /// </summary>
            public string GetItemColHtml(string value, string itemId, string controlId, bool isChecked) {
                StringWriter stringWriter = new StringWriter();
                using (HtmlTextWriter html = new HtmlTextWriter(stringWriter, "  ")) {
                    var cssClazz = new string[] { "tb-row-item", string.Empty };

                    //-- <td>...</td>
                    html.AddAttribute("style", "padding:0;margin:0");   //width:100px; border:1px solid red; word-wrap:break-word
                                                                        //http://stackoverflow.com/questions/1258416/word-wrap-in-a-html-table
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

                    if (HeaderType == DynamicHeaderType.Checkbox) {
                        html.AddAttribute("type",        "checkbox");
                        html.AddAttribute("value",       itemId);
                        html.AddAttribute("name",        controlId + "$check");
                        
                        if(isChecked)
                            html.AddAttribute("checked", "checked");
                        
                        html.AddAttribute("class",       "tb-row-check");
                        html.AddAttribute("data-id",     itemId);

                        html.RenderBeginTag("input");
                        html.RenderEndTag();
                    }
                    else {
                        html.Write(value);
                    }
                    
                    html.RenderEndTag();

                    html.RenderEndTag();

                    return stringWriter.ToString();
                }
            }
        }


        /// <summary>
        /// Enum que define os diversos tipos de cabeçalho da colunas da tabela.
        /// </summary>
        public enum DynamicHeaderType {
            Default = 0,
            Checkbox = 1
        }


        #endregion --- Nested Classes ----------------------------------------------------------------

    }
}