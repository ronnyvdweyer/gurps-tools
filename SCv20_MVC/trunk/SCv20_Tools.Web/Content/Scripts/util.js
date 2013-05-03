/// <reference path="jquery/jquery-1.7.1.js" />

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
        var data = $.parseJSON(jqxhr.responseText);

        $msg.attr("class", "").addClass("error");
        $msg.fadeIn('fast');

        $("#ajaxMessage .message").html(data.message);
    });
}