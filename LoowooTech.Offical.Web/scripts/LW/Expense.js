$(function () {
    $("a[name='Select']").click(function () {
        var href = $(this).attr("action");
        var target = $(this).attr("goal");
        var values = [];
        $("#Time input[name='" + target + "']").each(function () {
            values.push(target+"="+ $(this).val());
        });
        $(this).attr("href", href + values.join("&&"));

    });

});