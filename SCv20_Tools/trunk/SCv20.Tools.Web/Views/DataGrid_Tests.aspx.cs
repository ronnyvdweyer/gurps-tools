using System;
using System.Web.UI.WebControls;
using SCv20.Tools.Core.Services;
using SCv20.Tools.Core.Domain;
using System.Collections.Generic;
using SCv20.Tools.Core;

namespace SCv20.Tools.Web.Views.Shared {
    public partial class DataGrid_Tests : System.Web.UI.Page {
        
        protected void Page_Init(object sender, EventArgs e) {
            var col                 = new DynamicGrid.DynamicColumn();
            col.HeaderText          = "Selecione";
            col.HeaderWidth         = new Unit("30px");
            col.HeaderType          = DynamicGrid.DynamicHeaderType.Checkbox;
            col.DataItemValue       = "Id";
            col.DataItemValueID     = "Id";
            col.DataItemStyle       = "padding:0 3px 0 3px;";
            col.HeaderStyle         = "padding:0 3px 0 3px;";
            myGrid.DynamicColumns.Add(col);

            col                     =  new DynamicGrid.DynamicColumn();
            col.HeaderText          = "Campaign Quality";
            col.HeaderWidth         =  new Unit("150px");
            col.HeaderType          =  DynamicGrid.DynamicHeaderType.Default;
            col.DataItemValue       = "Name";
            col.DataItemValueID     = "Id";
            col.DataItemStyle       = "padding:0 3px 0 3px;";
            col.HeaderStyle         = "padding:0 3px 0 3px;";
            myGrid.DynamicColumns.Add(col);

            col                     =  new DynamicGrid.DynamicColumn();
            col.HeaderText          = "Description";
            col.HeaderType          =  DynamicGrid.DynamicHeaderType.Default;
            col.DataItemValue       = "Description";
            col.DataItemValueID     = "Id";
            col.DataItemStyle       = "padding:0 3px 0 3px;";
            col.HeaderStyle         = "padding:0 3px 0 3px;";
            myGrid.DynamicColumns.Add(col);

        //  myGrid.PaginateResults = true;
        //  myGrid.PageSize = 3;

            myGrid.PageIndexChanged += new DynamicGrid.PageIndexChangedHandler(myGrid_PageIndexChanged);
            myGrid.SelectedItems    += new DynamicGrid.SelectedItemsHandler(myGrid_SelectedItems);
        }

        
        protected void Page_Load(object sender, EventArgs e) {
            if (!Page.IsPostBack) {
                var svc = DataService.GetInstance();
                var data = svc.GetAllQualities(false);

                //var paged = new PagedList<Quality>(data, 0, 5);
                myGrid.DataSource = data;
                myGrid.DataBind();
            }
        }


        protected void myGrid_PageIndexChanged(object sender, Pager.PageIndexChangedEventArgs e) {
            var svc = DataService.GetInstance();

            IList<Quality> data = svc.GetAllQualities(false);
            //var paged = new PagedList<Quality>(data, e.PageIndex, 10);

            myGrid.DataSource = data;
            myGrid.DataBind();
        }


        protected void myGrid_SelectedItems(object sender, DynamicGrid.SelectedItemsEventArgs e) {
            var items = e.SelectedItems;
        }
        
        
        protected void Button1_Click(object sender, EventArgs e) {
            var x = myGrid.SelectedItemCollection;
            BindGrid();
        }


        private void BindGrid() {
            var svc = DataService.GetInstance();
            var data = svc.GetAllQualities(false);

            myGrid.DataSource = data;
            myGrid.DataBind();
        }
    
    }
}