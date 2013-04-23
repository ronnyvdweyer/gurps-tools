using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SCv20.Tools.Core;
using SCv20.Tools.Core.Domain;

namespace SCv20.Tools.Web.Views {
    public partial class QualityGrid : UserControlBase {
        public delegate void ItemSelectedHandler(object sender, ItemSelectedEventArgs e);
        public event ItemSelectedHandler ItemSelected;

        protected void Page_Init(object sender, EventArgs e) {
            GridPager.PageIndexChanged += new Views.Shared.Pager.PageIndexChangedHandler(GridPager_PageIndexChanged);
            SelectedItems.Click += new EventHandler(SelectedItems_Click);

        }


        protected void Page_Load(object sender, EventArgs e) {
            if (!Page.IsPostBack) {
                BindData();
            }
        }


        protected void SelectedItems_Click(object sender, EventArgs e) {
            if (string.IsNullOrWhiteSpace(txt_seletion.Text))
                return;

            var selection = txt_seletion.Text.Split(',');
            var list = selection.Select(n => int.Parse(n)).ToList();
            var args = new ItemSelectedEventArgs(list);
            OnItemSelected(args);
        }


        protected void GridPager_PageIndexChanged(object sender, Views.Shared.Pager.PageIndexChangedEventArgs e) {
            // Rebind Data
            var repo  = Repository<Quality>.GetInstance();
            var data  = repo.FindAll().OrderBy(m => m.Name);
            var paged = GridPager.PaginateDataSource<Quality>(data);

            // Rebind Datasource
            ListQuality.DataSource = paged;
            ListQuality.DataBind();
        }


        private void BindData() {
            var repo  = Repository<Quality>.GetInstance();
            var data  = repo.FindAll().OrderBy(m => m.Name);
            var paged = GridPager.PaginateDataSource<Quality>(data);

            ListQuality.DataSource = paged;
            ListQuality.DataBind();
        }


        private void OnItemSelected(ItemSelectedEventArgs args) {
            if (ItemSelected != null)
                ItemSelected(this, args);
        }


        /// <summary>
        /// Nested Class
        /// </summary>
        [Serializable]
        public class ItemSelectedEventArgs : EventArgs {
            public ItemSelectedEventArgs(IList<int> selectedItems) {
                this.SelectedItems = selectedItems;
            }

            public IList<int> SelectedItems {
                get;
                private set;
            }
        }
    }
}