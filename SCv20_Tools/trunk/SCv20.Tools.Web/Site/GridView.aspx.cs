using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using SCv20.Tools.Core;
using SCv20.Tools.Core.Services;
using SCv20.Tools.Core.Domain;
using System.Reflection;
using System.Diagnostics;


namespace SCv20.Tools.Web.Site {

    public partial class GridView : PageBase {
        
        
        void Page_Init(object sender, EventArgs e) {
            Button1.Click               += new EventHandler(Button1_Click);
            Button2.Click               += new EventHandler(Button2_Click);
            
            //GridView1.RowCancelingEdit  += new GridViewCancelEditEventHandler(GridView1_RowCancelingEdit);
            //GridView1.RowEditing        += new GridViewEditEventHandler(GridView1_RowEditing);
            //GridView1.RowUpdating       += new GridViewUpdateEventHandler(GridView1_RowUpdating);
            Pager1.PageIndexChanged     += new Views.Shared.Pager.PageIndexChangedHandler(Pager1_PageIndexChanged);
            //Pager1.PageSize = 10;
        }


        void Page_Load(object sender, EventArgs e) {
            if (!Page.IsPostBack) {
                BindData();
            }
        }



        void Pager1_PageIndexChanged(object sender, Views.Shared.Pager.PageIndexChangedEventArgs e) {
            // Rebind Data
            var repo = Repository<Quality>.GetInstance();
            var data = repo.FindAll().OrderBy(m => m.Name);
            var xxxx = Pager1.PaginateDataSource<Quality>(data);
            
            // Rebind Datasource
            ListView1.DataSource = xxxx;
            ListView1.DataBind();

            AddClientMessage("Você está na página {0}!".FormatWith(e.PageIndex));
        }

        
        


        



        
        private void BindData() {
            var repo = Repository<Quality>.GetInstance();
            var data = repo.FindAll().OrderBy(m => m.Name);
            var xxxx = Pager1.PaginateDataSource<Quality>(data);

            ListView1.DataSource = xxxx;//new PagedList<Quality>(data, CurrentPageIndex, 10);
            ListView1.DataBind();
        }




        //protected void Next_Click(object sender, EventArgs e) {
        //    CurrentPageIndex++;
        //    BindData();
        //}

        //protected void Prev_Click(object sender, EventArgs e) {
        //    CurrentPageIndex--;
        //    BindData();
        //}

        //protected void CurrentPage_Change(object sender, EventArgs e) {
        //    CurrentPageIndex = CurrentPage.Text.SafeInt32()-1;
        //    BindData();
        //}
        
        //public int CurrentPageIndex {
        //    get {
        //        var name = "CurrentPageIndex";
        //        var value = ViewState[name];
        //        return Convert.ToString(value).SafeInt32();
            
        //    } set {
        //        var name = "CurrentPageIndex";
        //        CurrentPage.Text = value.ToString();
        //        ViewState[name] = value;
        //    }
        //}


        #region -- Reference Methods --------------------------------------------------------------

        
        void Page_PreRender(object sender, EventArgs e) {
            //BindData();
        }


        //void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e) {
        //    throw new NotImplementedException();
        //}


        //void GridView1_RowEditing(object sender, GridViewEditEventArgs e) {
        //    GridView1.EditIndex = e.NewEditIndex;
        //    BindData();
        //}


        //void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e) {
        //    e.Cancel = true;
        //    GridView1.EditIndex = -1;
        //    BindData();
        //}


        void ClickMe(object sender, EventArgs e) {
            AddClientMessage("ok");
        }


        void Button1_Click(object sender, EventArgs e) {
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


        void Button2_Click(object sender, EventArgs e) {
            var svc = SerializationService.GetInstance();
            var obj = new Caliber { Id = 10, Value = "X", Reputation = 4, Networth = 1000000M };
            var str = svc.Serialize(obj);
            
            var ret = svc.Deserialize<Caliber>(str);
        }


        #endregion
    }
}