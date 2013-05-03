namespace SCv20_Tools.Web.Framework.Html {

    public abstract class BaseHtmlOptions {
        
        public BaseHtmlOptions() {
            Visible = true;
        }

        /// <summary>
        /// Universal HTML Attribute.
        /// </summary>
        public string   CssClass        { get; set; }

        /// <summary>
        /// Universal HTML Attribute.
        /// </summary>
        public object   HtmlAttributes  { get; set; }

        /// <summary>
        /// Universal HTML Attribute.
        /// </summary>
        public string   ID              { get; set; }

        /// <summary>
        /// Universal HTML Attribute.
        /// </summary>
        public string   Name            { get; set; }

        /// <summary>
        /// Universal HTML Attribute.
        /// </summary>
        public string   Style           { get; set; }

        /// <summary>
        /// Universal HTML Attribute.
        /// </summary>
        public int?     TabIndex        { get; set; }

        /// <summary>
        /// Universal HTML Attribute.
        /// </summary>
        public string   Title           { get; set; }

        /// <summary>
        /// Universal HTML Attribute.
        /// </summary>
        public bool     Visible         { get; set; }
    }
}