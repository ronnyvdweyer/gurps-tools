using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SCv20.Tools.Core.Services;

namespace SCv20.Tools.Web.Views.Shared
{
	public partial class DataGrid_Tests : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
            var svc = DataService.GetInstance();
            var data = svc.GetAllQualities(false);

            DataGrid1.DataSource = data;
            DataGrid1.DataBind();
		}
	}
}