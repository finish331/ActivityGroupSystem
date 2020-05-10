$(document).ready(function () {
    // 管理活動視窗
    $("#manage_button").kendoButton();
    $("#manage_form").kendoValidator();
    $("#manage_form").kendoWindow({
        width: "500px",
        title: "管理活動",
        visible: false,
        actions: ["Pin", "Minimize", "Maximize", "Close"],
        modal: true,
    }).data("kendoWindow");
    $("#activity_date").kendoDatePicker({
        format: "yyyy/MM/dd",
        value: new Date(),
        dateInput: true
    });

    // 查看與管理看參加者名單視窗
    $("#view_participants_button").kendoButton();
    $("#view_participants_form").kendoValidator();
    $("#view_participants_form").kendoWindow({
        width: "500px",
        title: "查看與管理看參加者名單",
        visible: false,
        actions: ["Pin", "Minimize", "Maximize", "Close"],
        modal: true,
    }).data("kendoWindow");

    // 邀請好友視窗
    $("#invite_friend_button").kendoButton();
    $("#invite_form").kendoValidator();
    $("#invite_form").kendoWindow({
        width: "500px",
        title: "查看與管理看參加者名單",
        visible: false,
        actions: ["Pin", "Minimize", "Maximize", "Close"],
        modal: true,
    }).data("kendoWindow");
});

$("#manage_button").click(function () {
    $("#manage_form").data('kendoValidator').hideMessages();
    $("#manage_form").data('kendoWindow').center().open();
});

$("#view_participants_button").click(function () {
    $("#view_participants_form").data('kendoValidator').hideMessages();
    $("#view_participants_form").data('kendoWindow').center().open();
});

$("#invite_friend_button").click(function () {
    $("#invite_form").data('kendoValidator').hideMessages();
    $("#invite_form").data('kendoWindow').center().open();
});