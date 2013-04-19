/// <reference path="http://localhost:7777/Content/Scripts/jquery-1.9.1.js" />

$(function () {
    var split_marginLeft_expanded = $(".splitter").css("margin-left");
    var right_marginLeft_expanded = $(".right").css("margin-left");
    var right_marginLeft_page = $(".right .page").css("margin-left");
    var right_marginLeft_content = $(".right-content").css("margin-left");


    //    console.log(split_marginLeft_expanded);
    //    console.log(right_marginLeft_expanded);

    $(".splitter-icon").click(function () {
        var s = $(".splitter");
        var l = $(".left");
        var r = $(".right");
        var rc = $(".right-content");   //-220px margin-left
        var rp = $(".right .page");     // 220px margin-left
        var i = $(".splitter-icon");

        var action = s.data("action");

        if (action == "contract") {
            s.css("margin-left", "0px");     //  0px
            r.css("margin-left", "6px");     //  6px
            rc.css("margin-left", "-20px");   // -20px (scroll bar)
            rp.css("margin-left", "20px");    // 220px
            l.hide();

            i.css("background-image", "url(/content/images/split-expander.gif)");
            s.data("action", "expand");
        }
        else {
            s.css("margin-left", split_marginLeft_expanded);   //  200px
            r.css("margin-left", right_marginLeft_expanded);   //  207px
            rp.css("margin-left", right_marginLeft_page);       //  220px
            rc.css("margin-left", right_marginLeft_content);    // -220px

            l.show();

            i.css("background-image", "url(/content/images/split-reducer.gif)");
            s.data("action", "contract");
        }
    })


    //-----------------------------------------------------------------------------
    // Reamarra todos os eventos jquery aos Pedaços atualizados da página.
    //-----------------------------------------------------------------------------
    
    $(".update-panel")
        .on("focus", ".check-box input", function (event) {
            $(this).parent(".check-box").addClass("focus");
        })
        .on("blur", ".check-box input", function (event) {
            $(this).parent(".check-box").removeClass("focus");
        });

    // Document ShortCuts;
    $(document).keypress(function (e) {
        if (e.keyCode == 27)
            $('#right-head-message').fadeOut('1000').removeClass('error, info, warn');
    });


    //-- ENTER AS TABS
    $('body').on('keydown', '.text, .text-number, .text-area, .check-box input, .btn.default', function (e) {
        var self = $(this);
        var form = self.parents('form:eq(0)');
        var focusable, next, prev;

        if (e.keyCode == 13) {
            focusable = form.find('.text, .text-number, .text-area, .link, .check-box input, .btn.default').filter(':visible');
            next = focusable.eq(focusable.index(this) + 1);
            prev = focusable.eq(focusable.index(this) - 1);

            if (next.length) {
                if (e.shiftKey) prev.focus();
                else next.focus();
            } else {
                if (e.shiftKey) prev.focus();
                else {
                    if (self.is(".btn.default"))
                        return true;
                }
            }
            return false;
        }
    });





    var ajaxError = function (sender, args) {
        //if UpdatePanel error occurs...
        if (args.get_error() != undefined) {
            var error = args.get_error().message;
            //            var Error = "3-add further description client side... : [Code]:" +
            //                    args.get_response().get_statusCode() + "  [Message]: " +
            //                    args.get_error().message;
            //Show your custom popup or...
            console.log(args.get_response());
            //alert(error);
            $(".right-head-message").fadeIn(200).addClass("error").text(error);
            //Hide default ajax error popup
            args.set_errorHandled(true);
            //...redirect error to your Error Panel on page
            //document.getElementById("Label1").innerText = Error;
        }
    }

    //    if (typeof (Sys) !== 'undefined') { Sys.Application.notifyScriptLoaded(); alert('xx'); };
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(ajaxError);
});