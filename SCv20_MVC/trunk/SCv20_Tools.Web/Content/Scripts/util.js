/// <reference path="jquery/jquery-1.7.1.js" />
/// <reference path="doT.js" />

//require('/content/scripts/doT.js');
//require('/areas/api/scripts/quality.api.js');

$(function (doc) {
    init(doc)
});


function init(doc) {
    ajaxSetup(doc);
}


function ajaxSetup(doc) {
    var $msg = $("#ajaxMessage");

    $(document).ajaxSend(function () {
        $('#ajaxLoader').show();
        //$('#ajaxLoader').fadeIn('fast');
        $msg.fadeOut('fast');
    });


    $(document).ajaxComplete(function () {
        $('#ajaxLoader').fadeOut('slow');
    })


    $(document).ajaxError(function (event, jqxhr, settings, exception) {
        $('#ajaxLoader').hide();

        var data = $.parseJSON(jqxhr.responseText);
        $msg.attr("class", "").addClass("error");
        $msg.fadeIn('fast');

        if (data == null || data == undefined)
            $("#ajaxMessage .message").html("Internal Server Error. Unknown reason.");
        else 
            $("#ajaxMessage .message").html(data.message + "<br/><pre>" + data.stack + "</pre>");
    });
}


$.fn.extend({
    tmpl: function (tmplSource, tmplData) {
        ///	<summary>
        ///     &#10; This method is an Adapter to doT.js client template engine. This uses the jquery selected element (this) as the template 
        ///     &#10; target element. This is an part of MyCustomExtensions. Go to http://olado.github.io/doT/ for more information.
        ///	</summary>
        ///	<param name="tmplSource" type="jQuery">jQuery selector that contains the template specification.</param>
        ///	<param name="tmplData" type="Json">A json object containing the data that will be used by template specification.</param>
        ///	<returns type="jQuery" />
        if (doT == undefined || doT == null || doT == false) {
            console.log("doT.js not found. Please include it in your references.")
            return;
        }

        var tmpl = $(tmplSource).html();    // Gets the tmplSource Specification.
        var func = doT.template(tmpl);      // Compiles the template into a function based on tmplData.
        var html = func(tmplData);          // Executes the template function and stores the result HTML.
        console.log(func.toString());


        // Replaces the jQuery selected element HTML with the result HTML.
        $(this).html(html);

        return this;
    },
});


function require(pathToScript) {
    var script  = document.createElement("script");
    script.type = "text/javascript";
    script.src  = pathToScript;       // use this for linked script
  //script.text = "alert('voila!');"    // use this for inline script
    document.head.appendChild(script);
}