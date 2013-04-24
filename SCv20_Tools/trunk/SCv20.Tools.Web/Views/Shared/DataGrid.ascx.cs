using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;

namespace SCv20.Tools.Web.Views.Shared {
    public partial class DataGrid : UserControlBase {
        private ITemplate   _headerTemplate;
        private ITemplate   _itemTemplate;
        private ITemplate   _footerTemplate;
        private IEnumerable _dataSource;
        private object      _currentItem;

        protected void Page_Load(object sender, EventArgs e) {


        }


        [TemplateContainer(typeof(SimpleTemplateItem))]
        public ITemplate HeaderTemplate {
            get {
                return _headerTemplate;
            }
            set {
                _headerTemplate = value;
            }
        }


        [TemplateContainer(typeof(SimpleTemplateItem))]
        public ITemplate ItemTemplate {
            get {
                return _itemTemplate;
            }
            set {
                _itemTemplate = value;
            }
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


        public override void DataBind() {
            // Adiciona o HeaderTemplate
            AddHeaderTemplateAsControl(_headerTemplate, null);
            AddFooterTemplateAsControl(_footerTemplate, null);

            if (_dataSource == null) {
                AddItemTemplateAsControl(null, null);
            }
            else {
                IEnumerator ie = _dataSource.GetEnumerator();

                while (ie.MoveNext()) {
                    var x = DataBinder.Eval(_currentItem, "Name");
                    
                    if (_itemTemplate != null) {
                        AddItemTemplateAsControl(_itemTemplate, ie.Current);
                    }
                }
            }
            base.DataBind();
        }


        private void AddHeaderTemplateAsControl(ITemplate anyTemplate, object currentItem) {
            if (anyTemplate != null) {
                gridHeaderTemplate.Controls.Clear();
                SimpleTemplateItem templateContentHolder = new SimpleTemplateItem(currentItem, 0);
                anyTemplate.InstantiateIn(templateContentHolder);
                gridHeaderTemplate.Controls.Add(templateContentHolder);
            }
            else {
                gridHeaderTemplate.Controls.Add(new LiteralControl("HeaderTemplate Not Defined<br/>")); 
            }
        }
        

        private void AddItemTemplateAsControl(ITemplate anyTemplate, object currentItem) {
            if (anyTemplate != null) {
                gridHeaderTemplate.Controls.Clear();

                SimpleTemplateItem templateContentHolder = new SimpleTemplateItem(currentItem, 0);
                anyTemplate.InstantiateIn(templateContentHolder);
                gridItemTemplate.Controls.Add(templateContentHolder);
            }
            else {
                gridHeaderTemplate.Controls.Add(new LiteralControl("<br/>ItemTemplate Not Defined<br/>")); 
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
                gridFooterTemplate.Controls.Add(new LiteralControl("FooterTemplate not Defined<br/>"));
            }
        }


        #region --- Nested Classes ----------------------------------------------------------------

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
    }
}