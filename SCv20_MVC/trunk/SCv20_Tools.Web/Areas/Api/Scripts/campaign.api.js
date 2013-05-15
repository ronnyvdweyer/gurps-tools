/// <reference path="../../../Content/Scripts/jquery/jquery-1.7.1.js" />

var campaignApi = function () { }

campaignApi.prototype.addQuality = function (campaignId, qualityId, fDone) {
    $.ajax('/api/campaign/AddQuality', {
        type: 'POST',
        dataType: 'json',
        data: { "campaignId": campaignId, "qualityId": qualityId }
    }).done(fDone);
}

campaignApi.prototype.getQualities = function (campaignId, fDone) {
    $.ajax('/api/campaign/getQualities', {
        type: 'GET',
        dataType: 'json',
        data: { "campaignId": campaignId }
    }).done(fDone);
}

campaignApi.prototype.getAvaliable = function (campaignId, fDone) {
    $.ajax('/api/campaign/getAvaliableQualities', {
        type: 'GET',
        dataType: 'json',
        data: { "campaignId": campaignId }
    }).done(fDone);
}

campaignApi.prototype.removeQuality = function (campaignId, qualityId, fDone) {
    $.ajax('/api/campaign/removeQuality', {
        type: 'POST',
        dataType: 'json',
        data: { "campaignId": campaignId, "qualityId": qualityId }
    }).done(fDone);
}

campaignApi.prototype.save = function (formData, fDone) {
    $.ajax('/api/campaign/save', {
        type: 'POST',
        dataType: 'json',
        data: formData
    }).done(fDone);
}


