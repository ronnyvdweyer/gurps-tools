using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SCv20.Tools.Core.Domain.CampaignDesign;
using SCv20.Tools.Core.Domain;
using SCv20.Tools.Core;
using System.ComponentModel.DataAnnotations;


namespace SCv20.Tools.Web.Site.Campaigns {
    
    public partial class Create : PageBase {
        protected void Page_Load(object sender, EventArgs e) {
            if (!Page.IsPostBack) {
                LoadPageData();
                LoadData();
            }
        }


        protected void LoadData() {
            var id = Request["id"].SafeInt32();
            if (id == 0)
                return;

            var data = DataService.GetCampaign(id);
            if (data != null) {
                txt_name.Text           = data.Name;
                sel_year.SelectedValue  = data.YearId.ToString();
                sel_level.SelectedValue = data.StartingLevel.ToString();
                txt_year.Text           = data.YearDetails;
                txt_reputation.Text     = data.BaseReputation.ToString();
                txt_networth.Text       = data.BaseNetWorthFormatted;
                txt_concept.Text        = data.Concept;
                txt_summary.Text        = data.Summary;
                hid_campaign_id.Value   = id.ToString();
            }
        }


        protected override void LoadPageData() {
            sel_year.DataSource = DataService.GetAllHistoricalConversions();
            sel_year.DataBind();

            sel_level.DataSource = DataService.GetAllCareerLevels();
            sel_level.DataBind();
        }


        protected void Save_Click(object sender, EventArgs e) {
            var id = hid_campaign_id.Value.SafeInt32();
            var c = DataService.GetCampaign(id);
            
            if (c == null)
                c = new Campaign();

            c.Name           = txt_name.Text;
            c.YearId         = sel_year.SelectedValue.SafeInt32();
            c.YearDetails    = txt_year.Text;
            c.StartingLevel  = sel_level.SelectedValue.SafeInt32();
            c.BaseNetWorth   = txt_networth.Text.SafeDecimal();
            c.BaseReputation = txt_reputation.Text.SafeInt32();
            c.Concept        = txt_concept.Text;
            c.Summary        = txt_summary.Text;
            
            ValidateAnnotations(c);

            if (Page.IsValid) {
                var newId = DataService.SaveCampaign(c);
                hid_campaign_id.Value = newId.Id.ToString();
                AddClientMessage("Succes");
            }

            //Page.GetPostBackEventReference()
        }
    }
}