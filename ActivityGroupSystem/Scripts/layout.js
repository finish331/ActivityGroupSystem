
$(document).ready(function () {
    debugger;
    $("#register_button").kendoButton();
    $("#memberInfo_button").kendoButton();
    $("#test").kendoButton();
});

$("#register_button").click(function () {
    $("#register_form").kendoValidator().data("kendoValidator");
    $("#register_form").kendoWindow({
        width: "600px",
        top: "100px",
        title: "Register",
        visible: false,
        actions: ["Minimize", "Maximize", "Close"],
        content: "Hall/Register",
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

$("#memberInfo_button").click(function () {
    $("#memberInfo_form").kendoValidator().data("kendoValidator");
    $("#memberInfo_form").kendoWindow({
        width: "900px",
        title: "Member Information",
        visible: false, //設定此介面一開始是否可看見
        actions: ["Minimize", "Maximize", "Close"],
        content: "Hall/MemberInfo",
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
        content: "Hall/OtherMemberInfo",
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