using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SCv20.Tools.Web.Views;

namespace SCv20.Tools.Web.Site.Campaigns {

    public partial class Quality : PageBase {

        protected void Page_Init(object sender, EventArgs e) {
            CampaignDisplay.ItemSelected += new CampaignDisplay.ItemSelectedHandler(CampaignDisplay_ItemSelected);
        }

        
        protected void Page_Load(object sender, EventArgs e) {
            if (!Page.IsPostBack)
                LoadPageData();
        }

        
        protected void SelectQuality_SelectedIndexChanged(object sender, EventArgs e) {
            var value = SelectQuality.SelectedValue.SafeInt32();
            LoadPageData(value);
        }

        
        protected void CampaignDisplay_ItemSelected(object sender, CampaignDisplay.ItemSelectedEventArgs e) {
            if (e.SelectedItem == 0) {
                ClearControl();
                SelectQuality.Enabled = false;
            }
            else {
                SelectQuality.Enabled = true;
                SelectQuality.Focus();
            }
        }
        
        
        protected override void LoadPageData() {
            var list = DataService.GetAllQualities(null);
            SelectQuality.DataSource = list;
            SelectQuality.DataBind();
            SelectQuality.Items.Insert(0, new ListItem("-- Please Select a Quality --", "0"));
        }
        
        
        private void LoadPageData(int id) {
            var data = DataService.GetQuality(id);

            if (data != null) {
                txtDescription.Text = data.Description;
                txtBonusAD.Text = data.BonusAD.ToString();
                txtBonusXP.Text = data.BonusXP.ToString();
                SelectQuality.Focus();
            }
            else {
                ClearControl();
                SelectQuality.Focus();
            }
        }

        
        private void ClearControl() {
            SelectQuality.SelectedIndex = 0;
            txtDescription.Text = string.Empty;
            txtBonusAD.Text = string.Empty;
            txtBonusXP.Text = string.Empty;
        }
    }
}