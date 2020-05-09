
$(document).ready(function () {
    debugger;
    $("#logout_button").kendoButton({ color: "#9baec8" });
    $("#confirm").kendoButton();
    $("#reset").kendoButton();
    $("#register_form").kendoValidator().data("kendoValidator");

    $("#register_form").kendoWindow({
        width: "600px",
        title: "Register",
        visible: false,
        actions: ["Minimize", "Maximize", "Close"],
        color: "#282c37",
        modal: true
    }).data("kendoWindow");

    $("#Datepicker").kendoDatePicker();
    $("#Sexuality").kendoComboBox();
    
});

$("#logout_button").click(function () {
    $("#register_form").data('kendoValidator').hideMessages();
    $("#register_form").data('kendoWindow').center().open();
});
