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

    $("a[name='Select2']").click(function () {
        var href = $(this).attr("action");
        var target = $(this).attr("goal");
        var str = $("input[name='" + target + "']").val();
        if (str != undefined) {
            var values = str.split(',');
            var vall = [];
            $.each(values, function (i, val) {
                vall.push("UserId=" + val);
            });
            $(this).attr("href", href + vall.join("&&"));
        }
 

    });

    //计算出差人员差补详情
    function CalculatorErrand() {
        var price = 0;
        var errandType = $("input[name='ErrandType']:checked").val();
        if (errandType === "" || errandType === undefined) {
            price = 0;
        } else {
            if (errandType === "Locale") {
                price = 100;
            } else {
                price = 80;
            }
        }
        var start = [];
        var end = [];
        $("input[type='text'][name='startTime']").each(function () {
            start.push($(this).val());
        });

        $("input[type='text'][name='endTime']").each(function () {
            end.push($(this).val());
        });
        var sum = .0;
        if (start.length === 0 || end.length === 0 || start.length !== end.length) {
           // swal("计算差补失败", "未获取出差人员时间详情", "error");
            return;
        } else {
            $.each(start, function (i, value) {
                if (value !== "") {
                    var d1 = new Date(value);
                    var d2 = new Date(end[i]);
                    if (d1 === undefined || d2 === undefined) {
                        swal("计算差补失败", "对比用户时间失败！", "error");
                        return;
                    }
                    var day_d1 = d1.getTime() / 1000 / 3600 / 24;
                    var day_d2 = d2.getTime() / 1000 / 3600 / 24;
                    var days = day_d2 - day_d1 + 1;
                    sum += days * price;
                }

            });
        }
        $("input[type='text'][name='ErrandSum']").val(sum);
    }
    //交通费
    function CalculatorTraffic() {
        CalculatorCompany();
        CalculatorPersonal();

        var vehicles = [];
        var cost = [];
        $("input:checkbox[name='Vehicles']").each(function () {
            vehicles.push($(this));
        });

        $("input:text[name='Cost']").each(function () {
            cost.push($(this).val());
        });
        var sum = .0;
        if (vehicles.length == 0 || cost.length == 0 || vehicles.length != cost.length) {

        } else {
            $.each(vehicles, function (i, value) {
                if (value.is(":checked")) {
                    var type = value.val();
                    if (type != "InternetCar") {
                        var val = parseFloat(cost[i]);
                        if (!isNaN(val)) {
                            sum += val;
                        }
                    }
                  
                }
            });

        }
        $("input[name='TrafficSum']").val(sum);
    }

 
    //公司车
    function CalculatorCompany() {
        var sum = .0;
        var kilo = parseFloat($("input[name='KiloMeter']")[0].value);
        if (!isNaN(kilo)) {
            sum += kilo * 0.5;
        }
        var petrol = parseFloat($("input[name='Petrol']")[0].value);
        if (!isNaN(petrol)) {
            sum += petrol;
        }
        var toll = parseFloat($("input[name='Toll']")[0].value);
        if (!isNaN(toll)) {
            sum += toll;
        }
        $("input[name='Cost'][readonly]:first").val(sum);
    }

    ///自备车计算
    function CalculatorPersonal() {
        var sum = .0;
        var kilo = parseFloat($("input[name='KiloMeter']")[1].value);
        if (!isNaN(kilo)) {
            sum += kilo * 1.5;
        }
        $("input[name='Cost'][readonly]:eq(1)").val(sum);
    }

    $("button[type='button'][name='Errand']").click(function () {
        CalculatorErrand();
    });

    $("input[type='radio'][name='ErrandType']").change(function () {
        CalculatorErrand();
    });

    $("button[type='button'][name='Traffic']").click(function () {
        CalculatorTraffic();
    });

    $("button[type='button'][name='Company']").click(function () {
        CalculatorCompany();
    });
    $("button[type='button'][name='Personal']").click(function () {
        CalculatorPersonal();
    });
    
    $("button[name='Remove']").click(function () {
        $(this).parent().parent().remove();
    });

    //日常报销
    var appendCateHtml = $("#Cate").html();

    $("button[name='PlusCategory']").click(function () {
        $("tbody[name='Category']").append(appendCateHtml);
        $(".chosen-select").chosen({ width: "100%" });
        $("button[name='Remove']").unbind("click").bind("click", function () {
            $(this).parent().parent().remove();
        });
    });
    //计算日常报销合计金额
    function CalculatorDaily() {
        var sum = .0;
        $("input:text[name='Cost']").each(function () {
            var val = parseFloat($(this).val());
            if (!isNaN(val) && val !== undefined) {
                sum += val;
            }
        });
        //sum = Math.round(sum, 2);
        return sum;
    }
 

    $("button[name='Daily']").click(function () {
        var sum = CalculatorDaily();
        $("input:text[name='Fee-Category']").val(sum.toFixed(2));
    });

    //招待报销
    var appendREhtml = $("#RE").html();
    $("button[name='PlusItem']").click(function () {
        $("tbody[name='Reception']").append(appendREhtml);
        $("button[name='Remove']").unbind("click").bind("click", function () {
            $(this).parent().parent().remove();
        });
    });

    //计算招待报销的分项金额
    function CalculatorReception() {
        var sum = .0;
        var coins = [];
        var ways = [];
        $("input[name='Cost']").each(function () {
            coins.push($(this).val());
        });

        $("select[name='Way']").each(function () {
            ways.push($(this).val());
        });
        if (coins.length === ways.length) {
            for (x in ways) {
                if (ways[x] === "Card" || ways[x] === "Bill" || ways[x] === "Self") {
                    //不计金额
                } else {
                    if (coins[x] != "") {
                        sum += parseFloat(coins[x]);
                    }
                }
            }
        }
        return sum;
    }
    $("button[name='Reception']").click(function () {
        var sum = CalculatorReception();
        $("input[name='Fee-Reception']").val(sum.toFixed(2));
    });

    $("a[name='Cancel']").click(function () {
        var href = $(this).attr("href");
        var url = $(this).attr("Url");
        var $btn = $(this);
        $btn.attr("disabled", "disabled");
        $.getJSON(href, function (data) {
            if (data.result === 1) {
                swal({
                    title: '成功撤回',
                    text: "成功撤回，点击确定完成撤回操作！",
                    type:'success'
                }, function () {
                    location.href = url;
                });
            } else {
                swal("撤回失败", data.content, "error");
                $btn.removeAttr("disabled");
            }
        });
        return false;
    });
});