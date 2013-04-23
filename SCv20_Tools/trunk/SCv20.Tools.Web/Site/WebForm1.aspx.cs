using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SCv20.Tools.Web.Site {
    public partial class WebForm1 : PageBase {
        protected void Page_Load(object sender, EventArgs e) {
            this.QualityGrid1.ItemSelected += new Views.QualityGrid.ItemSelectedHandler(QualityGrid1_ItemSelected);

        }

        void QualityGrid1_ItemSelected(object sender, Views.QualityGrid.ItemSelectedEventArgs e) {
            var idList = e.SelectedItems;

            string o = "";
            foreach (var id in idList) {
                o = o + id + " :: ";
            }
            AddClientMessage(o);
        }

        protected override void LoadPageData() {
            throw new NotImplementedException();
        }
    }
}