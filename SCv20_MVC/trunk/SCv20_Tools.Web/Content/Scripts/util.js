/// <reference path="jquery/jquery-1.7.1.js" />
/// <reference path="doT.js" />

$(function (doc) {
    ajaxSetup(doc);
    setupPortlet();
    processValidationMessages();
});


function ajaxSetup(doc) {
    var $msg = $("#ajaxMessage");

    $(document).ajaxSend(function () {
        $('#ajaxLoader').show();
        //$('#ajaxLoader').fadeIn('fast');
        //$msg.fadeOut('fast');
    });

    $(document).ajaxComplete(function () {
        $('#ajaxLoader').hide();
    });

    $(document).ajaxError(function (event, jqxhr, settings, exception) {
        $('#ajaxLoader').hide();

        console.debug(jqxhr);

        if (jqxhr.status === 404) {
            $("#ajaxMessage .message").html("AJAX Request to invalid URL!");
            $msg.attr("class", "").addClass("error");
            $msg.fadeIn('fast');
            return;
        }

        var data = $.parseJSON(jqxhr.responseText);
        if (data == null || data == undefined) {
            $("#ajaxMessage .message").html("Internal Server Error. Unknown reason.");
            $msg.attr("class", "").addClass("error");
            $msg.fadeIn('fast');
            return;
        }

        if (data.isvalidation) {
            var msg = "";
            for (var i = 0; i < data.errors.length; i++) {
                msg += "- " + data.errors[i].value + "<br/>";
                var key = data.errors[i].key;
                $(":input[name=" + key + "]").css("background-color", "yellow");
            }
            $("#ajaxMessage .message").html(data.message + "<br/>" + msg);
            $msg.attr("class", "").addClass("warn");
            $msg.fadeIn('fast');
        }
        else {
            $("#ajaxMessage .message").html(data.message + (data.stack !== "" ? "<br/><pre>" + data.stack + "</pre>" : ""));
            $msg.attr("class", "").addClass("error");
            $msg.fadeIn('fast');
        }
    });

    //$(document).ajaxSuccess(function () {
    //    $msg.attr("class", "").addClass("info");
    //    $("#ajaxMessage .message").html("Operação realizada com sucesso");
    //    $msg.fadeIn('fast');//.fadeOut(2000);
    //});
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
    }
});

function require(pathToScript) {
    var script = document.createElement("script");
    script.type = "text/javascript";
    script.src = pathToScript;       // use this for linked script
    //script.text = "alert('voila!');"    // use this for inline script
    document.head.appendChild(script);
}

function processValidationMessages() {
    $(document).on("focus", ".input-validation-error", function (e) {
        var $this = $(this);
        var $icon = $this.parent().find(".icon-error");
        var position = $this.position(); //$this.position();
        var messages = $this.data("validation-messages");
        var scrollPos = $("#main .content").scrollTop();

        if ($icon.length > 0) {
            $icon.show();
        }
        else {
            $icon = $("<span>").addClass("icon-error");//.appendTo(".form");
            $icon.attr("title", messages.text[0]);
            $this.parent().append($icon);
        }

        /*+ $this.width() - 5*/;
        var x = (position.left) - 05;
        var y = (position.top + scrollPos) - 15 + ($this.height() - $icon.height()) / 2;

        $icon.css("left", x + "px").css("top", y + "px");
    });

    $(document).on("blur", ".input-validation-error", function (e) {
        var $this = $(this);
        var $icon = $this.parent().find(".icon-error");
        if ($icon.length > 0) {
            $icon.hide();
        }
    });

    $(window).resize(function (e) {
        var scrollPos = $("#main .content").scrollTop();
        var $errors = $(".input-validation-error").each(function (index) {
            var $this = $(this);
            var $icon = $this.parent().find(".icon-error");
            var position = $this.position();
            if ($icon.length > 0) {
                var x = (position.left) - 05;
                var y = (position.top + scrollPos) - 15 + ($this.height() - $icon.height()) / 2;
                $icon.css("left", x + "px").css("top", y + "px");
            }
        });
    });
}

function setupPortlet() {
    $(document).on("click", ".portlet .h-title", function (e) {
        var $body = $(this).closest(".portlet").find(".body");
        $body.toggleClass("show").toggleClass("hide");
    });
}


function validate(target, model) {
    if (model === null || model === undefined) {
        throw "\n Source: [util.js]" +
              "\n Method: validate() :: No model data supplied. Please check.";
        return;
    }

    if (target === null || target === undefined) {
        throw "\n Source: [util.js]" +
              "\n Method: validate() :: No target defined. Please check."
    }

    validateDefaultModel(model);
    removeError(target);

    if (model.valid) return true;

    for (var i = 0; i < model.errors.length; i++) {
        var str = { text: [] };
        var fld = model.errors[i].field;    //Name
        var msg = model.errors[i].error;    //Error message;
        str.text.push(msg);
        attachError(target, fld, JSON.stringify(str));
    }

    function attachError(target, field, message) {
        $targets = $(target).find(":input[name=" + field + "]");
        $targets.addClass("input-validation-error");
        $targets.attr("data-validation-messages", message);
    }

    function removeError(target) {
        $targets = $(target).find(":input") ;//.find(":input[name=" + field + "]");
        $targets.removeClass("input-validation-error");
    }

    return false;
}


function validateDefaultModel(model) {
    var i = 0;
    for (prop in model) {
        i += model.hasOwnProperty("valid")   ? 0 : 1;
        i += model.hasOwnProperty("errors")  ? 0 : 1;
        i += model.hasOwnProperty("message") ? 0 : 1;
        i += model.hasOwnProperty("model")   ? 0 : 1;
      //i += model.hasOwnProperty("html")    ? 0 : 1;
    }

    if (i == 0 && model.errors !== null) {
        for (prop in model.errors[0]) {
            i += model.errors[0].hasOwnProperty("field") ? 0 : 1;
            i += model.errors[0].hasOwnProperty("error") ? 0 : 1;
        }
    }

    if (i > 0)
        throw "\n Source: [util.js]" +
              "\n Method: validate() :: Invalid JSON model supplied. Please check." +
              '\n Expected: {"valid": "<true | false>", "errors": [{"field": "<FieldName>", "error": "<ErrorDescription>"}], "message": "<SomeMessage>", "model": {<SomeObject>}"}' +
              "\n Received: " + JSON.stringify(model);
}

window.onerror = function (msg, url, line) {
    $('#ajaxLoader').hide();
    //alert("Error: " + msg + "\nurl: " + url + "\nline #: " + line);
    //TODO: Report this error via ajax so you can keep track of what pages have JS issues.
    //If you return true, then error alerts (like in older versions of Internet Explorer) will be suppressed.

    return false;
};