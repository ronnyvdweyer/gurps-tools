/// <reference path="/content/scripts/jquery/jquery-1.7.1.min.js" />
/// <reference path="/content/scripts/util.js" />

$(".portlet.scene-objective")
    .on("change", "#selType, #selGrade", function () {
        var $form = $(this).closest(".form");
        var model = {
            "typeid": $form.find("#selType").val(),
            "gradeid": $form.find("#selGrade").val()
        }

        $.get("/common/GetObjectiveTypeDetail", model).done(function (data) {
            if (data.done) {
                $form.find("#sample").html(data.ds.description);
                $form.find("#sample").attr("title", data.ds.description);
                $form.find("#basexp").html(data.ds.gradeXPFormated);
            }
        });
    })
    .on("click", "#save", function () {
        var $part = $(this).closest(".portlet");
        var model = $part.find(":input").serialize();

        $.post("/scene/saveObjetive", model).done(function (data) {
            if (!validate($part, data))
                return;
            $part.find("#ID").val(data.model.id);
        });
    })
    .on("click", "#delete", function () {
        alert('delete-me');
    });