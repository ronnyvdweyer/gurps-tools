using Cassette;
using Cassette.Scripts;
using Cassette.Stylesheets;

namespace SCv20_Tools.Web {
    /// <summary>
    /// Configures the Cassette asset bundles for the web application.
    /// </summary>
    public class CassetteBundleConfiguration : IConfiguration<BundleCollection> {
        public void Configure(BundleCollection bundles) {
            bundles.Add<StylesheetBundle>("templateStyles", new[] { 
                "content/template/css/style.css",
                "content/template/css/bootstrap.css",
                "content/template/css/jquery-ui-1.8.16.custom.css"  ,
                "content/template/css/fullcalendar.css"             ,
                "content/template/css/chosen.css"                   ,
                "content/template/css/glisse.css"                   ,
                "content/template/css/jquery.jgrowl.css"            ,
                "content/template/css/demo_table.css"               ,
                "content/template/css/jquery.fancybox.css"          ,
                "content/template/css/icon/font-awesome.css"        ,
                "content/template/css/bootstrap-responsive.css"     
              //"content/template/css/color/green.css"              ,
              //"content/template/css/color/red.css"                ,
              //"content/template/css/color/blue.css"               ,
              //"content/template/css/color/orange.css"             ,
              //"content/template/css/color/purple.css"
            });


            bundles.Add<ScriptBundle>("templateScripts", new[] {
                "content/template/js/jquery.min.js"                     ,                 
                "content/template/js/jquery-ui.min.js"                  ,         
                "content/template/js/bootstrap.min.js"                  ,        
                "content/template/js/google-code-prettify/prettify.js"  ,
                "content/template/js/jquery.flot.js"                    ,           
                "content/template/js/jquery.flot.pie.js"                ,        
                "content/template/js/jquery.flot.orderBars.js"          ,     
                "content/template/js/jquery.flot.resize.js"             ,     
                "content/template/js/jquery.flot.categories.js"         ,   
                "content/template/js/graphtable.js"                     ,        
                "content/template/js/fullcalendar.min.js"               ,
                "content/template/js/chosen.jquery.min.js"              ,
                "content/template/js/autoresize.jquery.min.js"          ,
                "content/template/js/jquery.autotab.js"                 ,
                "content/template/js/jquery.jgrowl_minimized.js"        ,
                "content/template/js/jquery.dataTables.min.js"          ,
                "content/template/js/jquery.stepy.min.js"               ,
                "content/template/js/jquery.validate.min.js"            ,
                "content/template/js/raphael.2.1.0.min.js"              ,
                "content/template/js/justgage.1.0.1.min.js"             ,
                "content/template/js/glisse.js"                         ,
                "content/template/js/styleswitcher.js"                  ,
                "content/template/js/jquery.sparkline.min.js"           ,
                "content/template/js/slidernav-min.js"                  ,
                "content/template/js/jquery.fancybox.js"                ,
                "content/template/js/application.js"
            });

            // TODO: Configure your bundles here...
            // Please read http://getcassette.net/documentation/configuration

            // This default configuration treats each file as a separate 'bundle'.
            // In production the content will be minified, but the files are not combined.
            // So you probably want to tweak these defaults!

            //  bundles.AddPerIndividualFile<StylesheetBundle>("Content");
            //  bundles.AddPerIndividualFile<ScriptBundle>("Scripts");

            // To combine files, try something like this instead:
            //   bundles.Add<StylesheetBundle>("Content");
            // In production mode, all of ~/Content will be combined into a single bundle.

            // If you want a bundle per folder, try this:
            //   bundles.AddPerSubDirectory<ScriptBundle>("Scripts");
            // Each immediate sub-directory of ~/Scripts will be combined into its own bundle.
            // This is useful when there are lots of scripts for different areas of the website.
        }
    }
}