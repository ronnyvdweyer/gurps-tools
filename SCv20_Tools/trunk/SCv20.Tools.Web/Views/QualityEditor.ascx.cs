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
            cmd_save_new.Click += new EventHandler(SaveClick);
            cmd_save.Click += new EventHandler(SaveClick);
        }


        protected void Page_Load(object sender, EventArgs e) {

        }


        public void LoadData(int id) {
            LoadedDataId = id;
            if (id == 0) return;
        }


        public int LoadedDataId {
            get {
                return Convert.ToInt32(ViewState["LoadedDataId"]);
            }
            private set {
                ViewState["LoadedDataId"] = value;
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


        private void SaveClick(object sender, EventArgs e) {
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
    }

}