$(document).ready(function () {
    $("#register_button").kendoButton();
});

$("#register_button").click(function () {
    $("#register_form").kendoValidator().data("kendoValidator");
    $("#register_form").kendoWindow({
        width: "600px",
        top: "100px",
        title: "Register",
        visible: false,
        actions: ["Minimize", "Maximize", "Close"],
        content: "Register",
        position: {
            top: "5%",
            left: "35%"
        },
        modal: true
    });
    //打開window
    $("#register_form").data('kendoValidator').hideMessages();
    $("#register_form").data('kendoWindow').open();
});

