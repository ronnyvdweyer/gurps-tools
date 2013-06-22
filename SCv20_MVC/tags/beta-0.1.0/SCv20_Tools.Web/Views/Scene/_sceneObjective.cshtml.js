/// <reference path="/scripts/jquery-1.10.1.min.js" />
/// <reference path="/content/scripts/util.js" />
$(function () {
    $(document, ".portlet.scene-objective")
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
        })
        
        .on("click", "#up", function () {
            reorder(this, -1);
        })
        
        .on("click", "#down", function () {
            reorder(this, +1);
        });

    function reorder(source, offset) {
        var $part = $(source).closest(".portlet");
        var model = {
            "sceneid"     : $part.find("#SceneID").val(),
            "objectiveid" : $part.find("#ID").val(),
            "offset"      : offset
        }

        if (model.objectiveid == 0) {
            return;
        }
        else {
            $.post("/scene/reorderObjective", model).done(function (data) {
                $("#list").html(data);
            });
        }

        return model;
    }
});