using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SCv20.Tools.Core;
using SCv20.Tools.Core.Domain;


namespace SCv20.Tools.Web.Views {

    public partial class QualityEditor : UserControlBase {

        
        protected void Page_Init(object sender, EventArgs e) {
            cmd_save_new.Click  += new EventHandler(Save_Click);
            cmd_save.Click      += new EventHandler(Save_Click);
        }


        protected void Page_Load(object sender, EventArgs e) {
            if (!Page.IsPostBack) {
                EditRecord(Request["id"].SafeInt32());
            }
        }


        private void Save_Click(object sender, EventArgs e) {
            var command = (sender as Button).CommandName;
            
            var entity = new Quality {
                Name            = txt_name.Text,
                Description     = txt_desc.Text, 
                IsSeasonsOnly   = chk_season_only.Checked,
                BonusAD         = txt_bonus_ad.Text.SafeInt32(),
                BonusXP         = txt_bonus_xp.Text.SafeInt32(),
            };
            
            var repo = Repository<Quality>.GetInstance();
            repo.Create(entity);
            repo.Commit();

            if (command == "save-new")
                Clear();

            AddClientMessage("Operação Realizada com Sucesso", MessageType.Info);
        }


        public void EditRecord(int id) {
            CurrentRecord = id;

            if (id == 0) 
                return;

            var repo = Repository<Quality>.GetInstance();
            var data = repo.GetById(CurrentRecord);

            txt_name.Text           = data.Name;
            txt_desc.Text           = data.Description;
            chk_season_only.Checked = data.IsSeasonsOnly;
            txt_bonus_ad.Text       = data.BonusAD.ToString();
            txt_bonus_xp.Text       = data.BonusXP.ToString();
        }


        public int CurrentRecord {
            get {
                return current_id.Value.SafeInt32();
            }
            private set {
                current_id.Value = value.ToString();
            }
        }

        
        public bool Enabled {
            set {
                txt_name.Enabled        = value;
                txt_desc.Enabled        = value;
                chk_season_only.Enabled = value;
                txt_bonus_ad.Enabled    = value;
                txt_bonus_xp.Enabled    = value;
                formControls.Visible    = value;
            }
            get {
                return txt_name.Enabled;
            }
        }


        public void Clear() {
            txt_name.Text           = "";
            txt_desc.Text           = "";
            chk_season_only.Checked = false;
            txt_bonus_ad.Text       = "";
            txt_bonus_xp.Text       = "";
            txt_name.Focus();
        }
        
    }

}