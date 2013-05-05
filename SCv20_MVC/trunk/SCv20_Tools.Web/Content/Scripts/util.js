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
        $msg.fadeOut('fast');
    });


    $(document).ajaxComplete(function () {
        $('#ajaxLoader').hide();
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
    tmpl: function (data) {
        ///	<summary>
        ///     &#10;This method is an Adapter to doT.js client template engine. This uses the jquery selector element (this) as 
        ///     &#10;the template spec. This is an part of MyCustomExtensions. Go to http://olado.github.io/doT/ for more information.
        ///	</summary>
        ///	<param name="data" type="Json">A json object contaning the template data.</param>
        ///	<returns type="jQuery" />
        if (doT == undefined || doT == null || doT == false)
            console.log("doT.js not found. Please include it in your references.")

        var template = $(this).html();
        var func = doT.template(template);
        $(this).html(func(data));

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