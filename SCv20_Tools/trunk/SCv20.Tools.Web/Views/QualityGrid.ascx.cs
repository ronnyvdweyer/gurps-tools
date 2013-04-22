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
        protected void Page_Init(object sender, EventArgs e) {
            GridPager.PageIndexChanged += new Views.Shared.Pager.PageIndexChangedHandler(GridPager_PageIndexChanged);
        }


        protected void Page_Load(object sender, EventArgs e) {
            if (!Page.IsPostBack) {
                BindData();
            }
        }


        protected void GridPager_PageIndexChanged(object sender, Views.Shared.Pager.PageIndexChangedEventArgs e) {
            // Rebind Data
            var repo  = Repository<Quality>.GetInstance();
            var data  = repo.FindAll().OrderBy(m => m.Name);
            var paged = GridPager.PaginateDataSource<Quality>(data);

            // Rebind Datasource
            ListView1.DataSource = paged;
            ListView1.DataBind();
        }


        private void BindData() {
            var repo  = Repository<Quality>.GetInstance();
            var data  = repo.FindAll().OrderBy(m => m.Name);
            var paged = GridPager.PaginateDataSource<Quality>(data);

            ListView1.DataSource = paged;
            ListView1.DataBind();
        }
    }
}