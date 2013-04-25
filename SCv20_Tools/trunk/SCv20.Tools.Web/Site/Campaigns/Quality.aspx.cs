using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SCv20.Tools.Web.Views;
using SCv20.Tools.Web.Views.Shared;

namespace SCv20.Tools.Web.Site.Campaigns {

    public partial class Quality : PageBase {

        protected void Page_Init(object sender, EventArgs e) {
            ucCampaignDisplay.ItemSelected += new CampaignDisplay.ItemSelectedHandler(ucCampaignDisplay_ItemSelected);
            gridQualities.PageIndexChanged += new DynamicGrid.PageIndexChangedHandler(gridQualities_PageIndexChanged);
            cmdSave.Click += new EventHandler(cmdSave_Click);
            SetupGridControl();

            var ctl = gridQualities.FindControl("cmdRemove");
        }

        protected void cmdRemove_Click(object sender, EventArgs e) {
            var x = gridQualities.SelectedItemCollection;

            throw new NotImplementedException();
        }


        protected void Page_Load(object sender, EventArgs e) {
            if (!Page.IsPostBack) {
                CarregarDropDownQualities();
                CarregarGridQualities();
            }
        }


        protected void ucCampaignDisplay_ItemSelected(object sender, CampaignDisplay.ItemSelectedEventArgs e) {
            if (e.SelectedItem == 0) {
                LimparPagina();
                cboQualities.Enabled = false;
            }
            else {
                cboQualities.Enabled = true;
                cboQualities.Focus();
            }

            CarregarDropDownQualities();
            CarregarGridQualities();
        }


        protected void cboQualities_SelectedIndexChanged(object sender, EventArgs e) {
            var id = cboQualities.SelectedValue.SafeInt32();
            var data = DataService.GetQuality(id);
            if (data != null) {
                txtDescription.Text = data.Description;
                txtBonusAD.Text = data.BonusAD.ToString();
                txtBonusXP.Text = data.BonusXP.ToString();
            }
            else {
                LimparPagina();
            }
            CarregarGridQualities();
        }


        protected void gridQualities_PageIndexChanged(object sender, Pager.PageIndexChangedEventArgs e) {
            CarregarGridQualities();
        }


        protected void cmdSave_Click(object sender, EventArgs e) {
            var quality = DataService.GetQuality(cboQualities.SelectedValue.SafeInt32());
            var campaign = DataService.GetCampaign(ucCampaignDisplay.SelectedCampaignID);

            campaign.Qualities.Add(quality);
            DataService.SaveCampaign(campaign);

            AddClientMessage("Campaign Quality Added");

            // Recarrega a tela para que os dados sejam atualizados.
            CarregarDropDownQualities();
            CarregarGridQualities();
        }
        
        
        protected override void LoadPageData() {
            throw new NotImplementedException();
        }
        
        
        private void CarregarDropDownQualities() {
            var id = ucCampaignDisplay.SelectedCampaignID;

            if (id == 0) {
                LimparPagina();
            }
            else {
                var data = DataService.GetAllQualitiesExcludingExistingFromCampaign(id);
                cboQualities.DataSource = data;
                cboQualities.DataBind();
                cboQualities.Items.Insert(0, new ListItem("-- Please Select a Quality --", "0"));
            }
        }


        private void CarregarGridQualities() {
            var id = ucCampaignDisplay.SelectedCampaignID;

            if(id == 0) {
                LimparPagina();
                gridQualities.Visible = false;
            }
            else {
                var data = DataService.GetCampaign(id);
                if(data != null) {
                    gridQualities.DataSource = data.Qualities.OrderBy(e=>e.Name);
                    gridQualities.DataBind();
                    gridQualities.Visible = true;
                }
                else {
                    gridQualities.Visible = false;
                }
            }
        }


        private void LimparPagina() {
            cboQualities.SelectedIndex = 0;
            txtDescription.Text = string.Empty;
            txtBonusAD.Text = string.Empty;
            txtBonusXP.Text = string.Empty;
        }


        private void SetupGridControl() {
            gridQualities.PaginateResults = true;
            gridQualities.PageSize = 2;

            var col                 = new DynamicGrid.DynamicColumn();
            col.HeaderText          = "Selecione";
            col.HeaderWidth         = new Unit("30px");
            col.HeaderType          = DynamicGrid.DynamicHeaderType.Checkbox;
            col.DataItemValue       = "Id";
            col.DataItemValueID     = "Id";
            col.DataItemStyle       = "padding:3px 3px 3px 3px;";
            col.HeaderStyle         = "padding:0 3px 0 3px;";
            gridQualities.DynamicColumns.Add(col);

            col                     =  new DynamicGrid.DynamicColumn();
            col.HeaderText          = "Campaign Quality";
            col.HeaderWidth         =  new Unit("150px");
            col.HeaderType          =  DynamicGrid.DynamicHeaderType.Default;
            col.DataItemValue       = "Name";
            col.DataItemValueID     = "Id";
            col.DataItemStyle       = "padding:3px 3px 3px 3px;";
            col.HeaderStyle         = "padding:0 3px 0 3px;";
            gridQualities.DynamicColumns.Add(col);

            col                     =  new DynamicGrid.DynamicColumn();
            col.HeaderText          = "Description";
            col.HeaderType          =  DynamicGrid.DynamicHeaderType.Default;
            col.DataItemValue       = "Description";
            col.DataItemValueID     = "Id";
            col.DataItemStyle       = "padding:3px 3px 3px 3px;";
            col.HeaderStyle         = "padding:0 3px 0 3px;";
            gridQualities.DynamicColumns.Add(col);
        }
    }
}