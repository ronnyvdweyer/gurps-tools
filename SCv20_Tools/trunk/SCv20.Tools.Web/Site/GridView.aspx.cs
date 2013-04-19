using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using SCv20.Tools.Core;
using SCv20.Tools.Core.Services;
using SCv20.Tools.Core.Domain;


namespace SCv20.Tools.Web.Site {

    public partial class GridView : PageBase {
        
        
        protected void Page_Init(object sender, EventArgs e) {
            Button1.Click               += new EventHandler(Button1_Click);
            Button2.Click               += new EventHandler(Button2_Click);
            
            GridView1.RowCancelingEdit  += new GridViewCancelEditEventHandler(GridView1_RowCancelingEdit);
            GridView1.RowEditing        += new GridViewEditEventHandler(GridView1_RowEditing);
            GridView1.RowUpdating       += new GridViewUpdateEventHandler(GridView1_RowUpdating);
        }

        
        void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e) {
            throw new NotImplementedException();
        }


        void GridView1_RowEditing(object sender, GridViewEditEventArgs e) {
            GridView1.EditIndex = e.NewEditIndex;
            BindData();  
        }


        void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e) {
            e.Cancel = true;
            GridView1.EditIndex = -1;
            BindData();  
        }


        protected void Page_PreRender(object sender, EventArgs e) {
            BindData();
        }


        protected void Page_Load(object sender, EventArgs e) {
            //BindData();
            
            if (!Page.IsPostBack) {
                BindData();
            }
        }

        
        private void BindData() {
            var repo = Repository<Quality>.GetInstance();
            var data = repo.FindAll().ToList();

            ListView1.DataSource = data;
            ListView1.DataBind();
        }


        protected void ClickMe(object sender, EventArgs e) {
            AddClientMessage("ok");
        }


        protected void Button1_Click(object sender, EventArgs e) {
            var svc = SerializationService.GetInstance();
            var file = Server.MapPath("/App_Data/qualities.json.js");
            var json = svc.DeserializeFile(file);

            var repo = Repository<Quality>.GetInstance();

            foreach (var x in json.data) {
                var enti = new Quality {
                    BonusAD       = x.ad,
                    BonusXP       = x.xp,
                    Description   = x.description,
                    IsSeasonsOnly = x.season,
                    Name          = x.name,
                    Dummy         = null
                };

                repo.Create(enti);
            }

            repo.Commit();
        }


        protected void Button2_Click(object sender, EventArgs e) {
            var svc = SerializationService.GetInstance();
            var obj = new Caliber { Id = 10, Value = "X", Reputation = 4, Networth = 1000000M };
            var str = svc.Serialize(obj);
            
            var ret = svc.Deserialize<Caliber>(str);
        }
    }
}