using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SCv20.Tools.Web.Views {

    public partial class CampaignDisplay : UserControlBase {
        public event ItemSelectedHandler ItemSelected;
        public delegate void ItemSelectedHandler(object sender, ItemSelectedEventArgs e);
                

        public int SelectedCampaign {
            get {
                return SelectCampaignField.SelectedValue.SafeInt32();
            }
            set {
                SelectCampaignField.SelectedValue = value.ToString();
                SessionVariables.CampaignID = value;
            }
        }


        protected void Page_Load(object sender, EventArgs e) {
            if (!Page.IsPostBack) {
                LoadControlData();
            }
        }


        protected void SelectCampaignField_SelectedIndexChanged(object sender, EventArgs e) {
            var value = SelectCampaignField.SelectedValue.SafeInt32();
            LoadControlData( value );
        }


        private void ClearControl() {
            txtCreatedOn.Text = string.Empty;
            txtConcept.Text = string.Empty;
        }


        private void LoadControlData() {
            var list = DataService.GetAllCampaigns();

            SelectCampaignField.DataSource = list;
            SelectCampaignField.DataBind();
            SelectCampaignField.Items.Insert(0, new ListItem("-- Please Select a Campaign --", "0"));
            SelectCampaignField.SelectedValue = SessionVariables.CampaignID.ToString();

            if (SessionVariables.CampaignID > 0)
                LoadControlData(SessionVariables.CampaignID);
        }


        private void LoadControlData(int id) {
            var e = DataService.GetCampaign(id);

            if (e != null) {
                txtCreatedOn.Text = e.CreatedOn.ToShortDateString();
                txtConcept.Text = e.Concept;
            }
            else {
                ClearControl();
            }

            SessionVariables.CampaignID = id;

            OnItemSelected(new ItemSelectedEventArgs(id));
        }


        private void OnItemSelected(ItemSelectedEventArgs args) {
            if (ItemSelected != null)
                ItemSelected(this, args);
            else
                throw new NullReferenceException("Evento {0}.OnItemSelected() não registrado.".FormatWith(this.ID));
        }


        /// <summary>
        /// Nested Class
        /// </summary>
        [Serializable]
        public class ItemSelectedEventArgs : EventArgs {
            public ItemSelectedEventArgs(int selectedItem) {
                this.SelectedItem = selectedItem;
            }

            public int SelectedItem {
                get;
                private set;
            }
        }
    }
}