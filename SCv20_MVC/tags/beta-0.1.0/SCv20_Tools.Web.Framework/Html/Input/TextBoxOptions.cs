namespace SCv20_Tools.Web.Framework.Html {

    public class TextBoxOptions : BaseHtmlOptions {

        public TextBoxOptions() {
            Visible = true;
        }

        public bool     AutoFocus   { get; set; }

        public bool     Disabled    { get; set; }

        public int      MaxLength   { get; set; }

        public bool     ReadOnly    { get; set; }

        public int      Size        { get; set; }

        public string   Value       { get; set; }
    }
}