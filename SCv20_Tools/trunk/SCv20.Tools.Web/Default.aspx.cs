using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SCv20.Tools.Core;
using SCv20.Tools.Core.Domain;


namespace SCv20.Tools.Web {
    public partial class _Default : System.Web.UI.Page {
        
        
        protected void Page_Load(object sender, EventArgs e) {

        }


        protected void Button1_Click(object sender, EventArgs e) {
            System.Threading.Thread.Sleep(1000);
            Label1.Text = DateTime.Now.ToString();
            var repo = Repository<Caliber>.GetInstance();
        }

    }
}
