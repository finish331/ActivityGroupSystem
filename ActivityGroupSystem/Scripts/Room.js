$(document).ready(function () {
    $("#memberInfo_button").kendoButton();
    $("#test").kendoButton();
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

$("#memberInfo_button").click(function () {
    $("#memberInfo_form").kendoValidator().data("kendoValidator");
    $("#memberInfo_form").kendoWindow({
        width: "900px",
        title: "Member Information",
        visible: false, //設定此介面一開始是否可看見
        actions: ["Minimize", "Maximize", "Close"],
        content: "MemberInfo",
        position: {
            top: "20%",
            left: "30%"
        },
        modal: true, //操作kendoWindow時，其他元件無法操作
    }).data("kendoWindow");

    //打開window
    $("#memberInfo_form").data('kendoValidator').hideMessages();
    $("#memberInfo_form").data('kendoWindow').open();
});

$("#test").click(function () {
    $("#checkInfo_form").kendoValidator().data("kendoValidator");
    $("#checkInfo_form").kendoWindow({
        width: "500px",
        title: "Member Information",
        visible: false, //設定此介面一開始是否可看見
        actions: ["Minimize", "Maximize", "Close"],
        content: "OtherMemberInfo",
        position: {
            top: "20%",
            left: "35%"
        },
        modal: true, //操作kendoWindow時，其他元件無法操作
    }).data("kendoWindow");
    //打開window
    $("#checkInfo_form").data('kendoValidator').hideMessages();
    $("#checkInfo_form").data('kendoWindow').open();
});