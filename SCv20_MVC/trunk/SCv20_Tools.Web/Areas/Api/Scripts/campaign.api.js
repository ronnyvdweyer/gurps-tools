/// <reference path="../../../Content/Scripts/jquery/jquery-1.7.1.js" />

var campaignApi = function () { }

campaignApi.prototype.addQuality = function (campaignId, qualityId, fDone) {
    $.ajax('/api/campaign/AddQuality', {
        type: 'POST',
        dataType: 'json',
        data: { "campaignId": campaignId, "qualityId": qualityId }
    }).done(fDone);
}

