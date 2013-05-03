namespace SCv20_Tools.Web.Framework.Html {

    public class DropDownListOptions : BaseHtmlOptions {

        public DropDownListOptions() {
            Visible = true;
        }

        public bool     AutoFocus       { get; set; }

        public bool     Disabled        { get; set; }

        public bool     ReadOnly        { get; set; }

        public int      Size            { get; set; }

        public string   SelectedValue   { get; set; }

        public string   FirstItemValue  { get; set; }
        
        public string   FirstItemText   { get; set; }
    }
}