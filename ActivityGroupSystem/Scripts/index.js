
$(document).ready(function () {
    debugger;
    $("#logout_button").kendoButton();
    $("#detail_form").kendoValidator().data("kendoValidator");

    $("#detail_form").kendoWindow({
        width: "600px",
        title: "Book Detail Window",
        visible: false,
        actions: ["Pin", "Minimize", "Maximize", "Close"],
        modal: true
    }).data("kendoWindow");
});

$("#logout_button").click(function () {
    $("#detail_form").data('kendoValidator').hideMessages();
    $("#detail_form").data('kendoWindow').center().open();
});