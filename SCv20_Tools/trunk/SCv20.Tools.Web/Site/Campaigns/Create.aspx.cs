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
                BindData();
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


        protected override void BindData() {
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


            //  http://stackoverflow.com/questions/3089760/using-asp-net-mvc-data-annotation-outside-of-mvc
            //  http://stackoverflow.com/questions/777889/on-postback-how-can-i-add-a-error-message-to-validation-summary
            //  http://blog.webmastersam.net/post/Adding-custom-error-message-to-ValidationSummary-without-validators.aspx
            //  http://stackoverflow.com/questions/7149899/custom-validation-not-executing


            //var results = new List<ValidationResult>();
            //var context = new ValidationContext(c, null, null);
            //var xpto = Validator.TryValidateObject(c, context, results, true);


            //foreach (var v in results) {
            //    var err = new CustomValidator();
            //    err.ValidationGroup = "DataAnotationsValidator";
            //    err.IsValid = false;
            //    err.ErrorMessage = v.ErrorMessage;
            //    errorSummary.ValidationGroup = "DataAnotationsValidator";
            //    Page.Validators.Add(err);
            //}


            //var newId = DataService.SaveCampaign(c);
            //hid_campaign_id.Value = newId.Id.ToString();
            //AddClientMessage("Succes");
        }
    }
}