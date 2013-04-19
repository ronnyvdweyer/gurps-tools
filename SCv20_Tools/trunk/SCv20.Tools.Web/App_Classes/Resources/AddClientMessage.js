/// <reference path="../../Content/Scripts/jquery-1.9.1.js" />
(function () {
    var messageText = '{#message}'
    var messageType = '{#messageType}';

    if (messageType == 'info')
        $('.right-head-message').fadeIn(500).delay(250).fadeOut(500).addClass(messageType).text(messageText);
    else
        $('.right-head-message').fadeIn(500).addClass(messageType).text(messageText);
})();