
$(document).ready(function () {
    debugger;
    var buttonObject = $("#confirm").kendoButton().data("kendoButton");

    $("#Birthday").kendoDatePicker();
    $("#Sexuality").kendoComboBox();

   
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
                location.href = "Hall\Login";
            }
            else {
                errorNotification.show(data, "info");
            }
        }).fail(function (data) {
            alert("fail");
        });
    }
});