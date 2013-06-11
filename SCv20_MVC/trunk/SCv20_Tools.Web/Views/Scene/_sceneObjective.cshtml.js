/// <reference path="/content/scripts/jquery/jquery-1.7.1.min.js" />
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

        //var $form = $(this).closest(".form");
        var model = $part.find(":input").serialize();

        console.log(model);
        console.log( $part.html() );

        $.post("/scene/saveObjetive", model).done(function (data) {
            $part.find("#ID").val(data.id);
        });

    })
    .on("click", "#delete", function () {
        alert('delete-me');
    });