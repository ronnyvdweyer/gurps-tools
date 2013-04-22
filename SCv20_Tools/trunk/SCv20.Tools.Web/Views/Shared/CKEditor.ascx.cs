using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace SCv20.Tools.Web.Views.Shared {

    public partial class CKEditor : System.Web.UI.UserControl {
        
        
        protected void Page_Load(object sender, EventArgs e) {
            CompactToolbar = true;
        }


        public bool Enabled {
            get { return ck.Enabled; }
            set { ck.Enabled = value; }
        }

        
        public bool Resizable { 
            get { return ck.ResizeEnabled; }
            set { ck.ResizeEnabled = value; }
        }


        public int Height {
            get { return Convert.ToInt32(ck.Height.Value); }
            set { ck.Height = new Unit(value, UnitType.Pixel); }
        }

        
        public int Width {
            get { return Convert.ToInt32(ckEditorContainer.Height.Value); }
            set { ckEditorContainer.Width = new Unit(value, UnitType.Pixel); }
        }


        public string Text {
            get { return ck.Text; }
            set { ck.Text = value; }
        }


        public bool CompactToolbar {
            set {
                ck.Toolbar = @"
                    Bold|Italic|Underline|Strike|-|Subscript|Superscript
                    JustifyLeft|JustifyCenter|JustifyRight|JustifyBlock|TextColor|BGColor|FontSize
                    NewPage|Preview
                ";

            }
        }


        public void Clear() {
            ck.Text = "";
        }
    }

}