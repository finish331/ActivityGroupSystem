
$(document).ready(function () {
    debugger;
    var buttonObject = $("#confirm").kendoButton().data("kendoButton");

    $("#logout_button").kendoButton({ color: "#9baec8" });
    
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

    $("#Birthday").kendoDatePicker();
    $("#Sexuality").kendoComboBox();

    
   
});

$("#logout_button").click(function () {
    $("#register_form").data('kendoValidator').hideMessages();
    $("#register_form").data('kendoWindow').center().open();
});

$("#confirm").click(function () {
    var validator = $("#register_form").kendoValidator().data("kendoValidator");

    debugger;

    var errorNotification = $("#errorNotification").kendoNotification({ appendTo: "#Notification" }).data("kendoNotification");

    var insertMemberData = {
        MemberId: $("#MemberId").val(),
        Password: $("#Password").val(),
        Password2: $("#Password2").val(),
        MemberName: $("#MemberName").val(),
        Phone: $("#Phone").val(),
        Sexuality: $("#Sexuality").val(),
        Birthday: $("#Birthday").val()
    }

    if (validator.validate()) {
        $.ajax({
            url: "/Hall/Registers",
            dataType: "json",
            data: insertMemberData,
            type: "post"
        }).done(function (data) {
            if (data == "") {
                $("#register_form").data('kendoWindow').close();
                location.href = "Login";
            }
            else {
                errorNotification.show(data, "info");
            }
        }).fail(function (data) {
            alert("fail");
        });
    }
});