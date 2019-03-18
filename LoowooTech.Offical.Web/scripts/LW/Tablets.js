$(function () {
    $(".i-checks").iCheck({
        checkboxClass: 'icheckbox_square-green',
        radioClass: 'iradio_square-green',
    });

    $('.chosen-select').chosen({ width: "100%" });

    $(".datetime").datepicker({
        keyboardNavigation: false,
        forceParse: false,
        autoclose: true
    });

});