
$(document).ready(function () {
    debugger;
    $("#addFriend_btn").kendoButton();
    $("#black_btn").kendoButton();
    $("#checkInfo_form").kendoValidator();
    $("#Birthday").kendoDatePicker();

    $("#tabstrip").kendoTabStrip({
        tabPosition: "left",
        animation: {
            open: {
                effects: "fadeIn"
            }
        }
    });

    $("#Sexuality").kendoComboBox({
        dataTextField: "text",
        dataValueField: "value",
        dataSource: [
            { text: "男", value: "男" },
            { text: "女", value: "女" },
        ],
        filter: "contains",
        suggest: true,
        index: 1
    });


    
});

$("#addFriend_btn").click(function () {
    debugger;
    var errorNotification = $("#CheckErrorNotification").kendoNotification().data("kendoNotification");
    $.ajax({
        url: "/Hall/AddFriend",
        dataType: "json",
        data: { MemberId: $("#CheckId").val(), },
        type: "post"
    }).done(function (data) {
        errorNotification.show(data, "info");
        $("#friendInvitation").data("kendoGrid").dataSource.read();
    }).fail(function (data) {
        errorNotification.show("修改資料失敗", "info");
    });
});

$("#black_btn").click(function () {
    debugger;
    var errorNotification = $("#CheckErrorNotification").kendoNotification().data("kendoNotification");
    $.ajax({
        url: "Hall/BlackMember",
        dataType: "json",
        data: { MemberId: $("#CheckId").val(), },
        type: "post"
    }).done(function (data) {
        errorNotification.show(data, "info");
        $("#blackMemberList").data("kendoGrid").dataSource.read();
    }).fail(function (data) {
        errorNotification.show("修改資料失敗", "info");
    });
});



