using System;
using System.Web.UI.WebControls;
using SCv20.Tools.Core.Services;
using SCv20.Tools.Core.Domain;
using System.Collections.Generic;
using SCv20.Tools.Core;

namespace SCv20.Tools.Web.Views.Shared {
    public partial class DataGrid_Tests : System.Web.UI.Page {
        protected void Page_Init(object sender, EventArgs e) {
            var col = new DynamicGrid.DynamicGridColumn();
            col.DataItemValue    = "Name";
            col.HeaderWidth      = new Unit("200px");
            col.HeaderText       = "Nome da Qualidade";
            col.DataItemStyle    = "padding-left:5px;";
            col.DataItemCssClass = "x-teste";
            
            myGrid.GridColum.Add(col);

            col = new DynamicGrid.DynamicGridColumn();
            col.DataItemValue    = "Description";
            col.HeaderText       = "Descrição";
            col.DataItemStyle    = "padding-left:5px;border-left:1px dotted silver;height:100px;";
            col.DataItemCssClass = "x-teste";
            
            myGrid.GridColum.Add(col);

            myGrid.ShowPager = true;
            myGrid.PageSize = 5;

            myGrid.PagerControl.PageIndexChanged += new Pager.PageIndexChangedHandler(GridControl_PageIndexChanged);

        }

        protected void Page_Load(object sender, EventArgs e) {
            if (!Page.IsPostBack) {
                var svc = DataService.GetInstance();
                var data = svc.GetAllQualities(false);

                //var paged = new PagedList<Quality>(data, 0, 5);
                myGrid.DataSource = data;
                myGrid.DataBind();
              
                //myGrid.ShowPager = false;
              
                
                //DataGrid1.DataSource = data;
                //DataGrid1.DataBind();
            }
        }

        protected void GridControl_PageIndexChanged(object sender, Pager.PageIndexChangedEventArgs e) {
            var svc = DataService.GetInstance();

            IList<Quality> data = svc.GetAllQualities(false);
            //var paged = new PagedList<Quality>(data, e.PageIndex, 10);

            myGrid.DataSource = data;
            myGrid.DataBind();
        }
    }
}