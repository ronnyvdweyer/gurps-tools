/// <reference path="../../../Content/Scripts/jquery/jquery-1.7.1.js" />

var qualityApi = function () { }

// Get the details of specified quality id.
qualityApi.prototype.getDetails = function (id, fDone) {
    $.ajax('/api/quality/getdetails', {
        type: 'GET',
        dataType: 'json',
        data: { "id": id }
    }).done(fDone);
}

qualityApi.prototype.getAll = function (fDone) {
    $.ajax('/api/quality/getall', {
        type: 'GET',
        dataType: 'json'
    }).done(fDone);
}
