$(function () {
    //删除操作
    $("a[name='Delete']").click(function () {
        var href = $(this).attr("href");
        var url = $(this).attr("Url");
        swal({
            title: "您确定要删除吗？",
            text: "您将不能恢复这项操作！",
            type: "warning",
            showCancelButton: true,
            confirmButtonColor: "#DD6B55",
            confirmButtonText: "是的，删除！",
            cancelButtonText:"不，取消！",
            closeOnConfirm: false,
            closeOnCancel:false
        }, function (isConfirm) {
            if (isConfirm) {
                $.getJSON(href, function (data) {
                    if (data.result == 1) {
                        swal({
                            title: "成功删除！",
                            text: "成功删除,点击确定完成删除！",
                            type:"success"
                        }, function () {
                            location.href = url;
                        })
                        //swal("成功删除", "成功删除", "success");
                    } else {
                        swal("删除失败", json.content, "error");
                    }
                })
            } else {
                swal("取消", "取消删除操作", "error");
            }
        });
        return false;
    });

    $("a[name='Commit']").click(function () {
        var href = $(this).attr("href");
        var url = $(this).attr("Url");
        var $btn = $(this);
        $btn.attr("disabled", "disabled");
        $.getJSON(href, function (data) {
            if (data.result == 1) {
                swal({
                    title: "成功提交",
                    text: "成功提交，点击确定完成提交！",
                    type:"success"
                }, function () {
                    location.href = url;
                });
            } else {
                swal("删除失败", json.content, "error");
                $btn.removeAttr("disabled");
            }
        });

        return false;
    });
    //点赞事件
    $("a[name='Good']").click(function () {

        swal({
            title: "点赞成功!",
            text: "谢谢您的支持！",
            type:"success"
        });

        return false;
    });
});