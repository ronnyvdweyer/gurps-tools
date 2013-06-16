/// <reference path="/scripts/jquery-1.10.1.js" />
/// <reference path="/areas/api/scripts/quality.api.js" />
/// <reference path="/areas/api/scripts/campaign.api.js" />
/// <reference path="/content/scripts/doT.js" />
/// <reference path="/content/scripts/util.js" />
var page = window.page || {};

page.module = (function () {
    var fn_avaliableQualitiesTmpl;      // DropDownList
    var fn_campaignQualitiesTmpl;       // Table
    var _campaignId = $("#Id").val();
    var _lastSelIndex = 0;
    var _qualityId = 0;

    // **************************************************************
    // initializes all required data and events
    // **************************************************************
    init = function () {
        $.get('/views/campaigns/tmpl/avaliableQualitiesTmpl.shtml', function (tmpl) {
            fn_avaliableQualitiesTmpl = doT.template(tmpl);
        });

        $.get('/views/campaigns/tmpl/qualityListTmpl.shtml', function (tmpl) {
            fn_campaignQualitiesTmpl = doT.template(tmpl);
        });

        //$('#avaliableQualities').on("keyup", "#SelectedQualityId", function (e) {
        //    console.debug(e);
        //    getQualityDetail(this);
        //});

        $('#avaliableQualities').on("change", "#SelectedQualityId", getQualityDetail);
        $('#qualityList').on("click", ".remove", removeQuality);
        $('#btnAddQuality').click(addQuality);

        getAllQualities();
        getAllCampaignQualities();
    },

    // **************************************************************
    // finalize all user data and events
    // **************************************************************
    finalize = function () {
    },

    // **************************************************************
    // Loads all avaliabe qualities
    // **************************************************************
    getAllQualities = function (fDoneCallback) {
        var api = new campaignApi();
        api.getAvaliable(_campaignId, function (data) {
            if (data.done) {
                var o = $(fn_avaliableQualitiesTmpl(data.ds));
                $('option[value=' + _qualityId + ']', o).attr('selected', 'selected')
                $("#avaliableQualities").html(o);
                //o.focus();
                //o.change();
                if (fDoneCallback !== undefined)
                    fDoneCallback();
            }
            
        });
    },

    // **************************************************************
    // Load existing campaign qualities
    // **************************************************************
    getAllCampaignQualities = function () {
        var api = new campaignApi();
        api.getQualities(_campaignId, function (data) {
            if (data.done) {
                $("#qualityList").html(fn_campaignQualitiesTmpl(data.ds));
            }
        });
    },

    // **************************************************************
    // Get the selected quality detail.
    // **************************************************************
    getQualityDetail = function (e) {
        var id = $(this).val();
        if (id == 0) {
            $("#btnAddQuality").attr("disabled", "disabled");
        } else {
            $("#btnAddQuality").removeAttr("disabled");
        }
        var api = new qualityApi();
        api.getDetails(id, function (data) {
            $("#txtQualityBonusAD").val(data.bonusADFormated);
            $("#txtQualityBonusXP").val(data.bonusXPFormated);
            $("#txtQualityDetails").html(data.description);
        });
    },

    // **************************************************************
    // Adds the selected quality to campaign qualities
    // **************************************************************
    addQuality = function (e) {
        var api = new campaignApi();
        var sel = $('#SelectedQualityId');
        var qid = sel.val();

        api.addQuality(_campaignId, qid, function (data) {
            if (data.done) {
                _lastSelIndex = sel[0].selectedIndex; ;

                $('#SelectedQualityId option:selected').remove();

                getAllCampaignQualities();

                sel[0].selectedIndex = sel[0].length == 1 ? 0 : _lastSelIndex;
                sel.focus();
                sel.change();
            }
        });
    },

    // **************************************************************
    // Removes the selected quality.
    // **************************************************************
    removeQuality = function (e) {
        var qid = $(this).data('id');
        var row = $(this).closest('tr');
        var sel = $('#SelectedQualityId');

        _lastSelIndex = sel[0].selectedIndex; ;
        _qualityId = qid;

        $("#btnAddQuality").attr("disabled", "disabled");

        var api = new campaignApi();
        api.removeQuality(_campaignId, qid, function (data) {
            if (data.done) {
                row.remove();
                getAllQualities(function () {
                    $('#SelectedQualityId').focus();
                    $('#SelectedQualityId').change();
                });
            }
        });
    }

    return { init: init };
});

// Inicializa o módulo da página.
$(function () {
    page.module().init();
});