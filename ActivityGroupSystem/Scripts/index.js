$(document).ready(function () {
    $("#btn_create_activity").kendoButton();
    $("#btn_manage_activity").kendoButton();
    $("#btn_view_join_activity").kendoButton();
    $("#btn_cancel").kendoButton();
    $("#btn_search_activity").kendoButton();
    $("#btn_view_invite_activity").kendoButton();
    $("#register_button").kendoButton();
    $("#memberInfo_button").kendoButton();
    $("#test").kendoButton();


    //創建活動window及內部元件初始化
    $("#btn_add_activity").kendoButton();
    $("#insert_form").kendoValidator();
    $("#insert_form").kendoWindow({
        width: "500px",
        title: "Create Activity Window",
        visible: false, //設定此介面一開始是否可看見
        actions: ["Pin", "Minimize", "Maximize", "Close"],
        modal: true, //操作kendoWindow時，其他元件無法操作
    }).data("kendoWindow");
    $("#add_activity_date").kendoDatePicker({
        format: "yyyy/MM/dd",
        value: new Date(),
        dateInput: true
    });

    $("#activity_grid").kendoGrid({
        height: 400,
        dataSource: {
            transport: {
                read: {
                    url: "/Hall/GetUnJoinActivity",
                    type: "post",
                    dataType: "json",
                    data: { "memberId": $("#label_memberId").text() }
                }
            },
            pageSize: 20
        },
        pageable: {
            input: true,
            numeric: false
        },
        columns: [
            { hidden: true, field: "ActivityId" },
            { field: "ActivityName", title: "活動名稱", width: "10%" },
            { field: "HomeOwnerId", title: "活動房主", width: "10%" },
            { field: "NumberOfPeople", title: "活動人數", width: "10%" },
            { field: "ActivityNote", title: "活動內容", width: "30%" },
            { field: "ActivityDate", title: "活動日期", width: "10%" },
            {
                command: {
                    text: "進入",
                    className: "btn-enter-button",
                },
                title: " ",
                width: "10%",
                attributes: {
                    style: "text-align: center"
                }
            }
        ]
    }); 
});

//活動Grid中進入活動的按鈕
$("#activity_grid").on("click", ".btn-enter-button", function (e) {
    if ($("#label_memberId").text() != "") {
        var Item = $("#activity_grid").data("kendoGrid").dataItem($(e.currentTarget).closest('tr'));

        $.ajax({
            url: "/Hall/IsParticipant",
            dataType: "json",
            data: { "activityId": Item.ActivityId, "memberId": $("#label_memberId").text() },
            type: "post"
        }).done(function (data) {
            if (data.responseText == "True")
                window.location.href = "/Hall/Room?activityId=" + Item.ActivityId + "&" + "userId=" + $("#label_memberId").text();
            else {
                if (confirm("您尚未參加\"" + Item.ActivityName + "\"活動，請問您是否要參加?"))
                    window.location.href = "/Hall/Room?activityId=" + Item.ActivityId + "&" + "userId=" + $("#label_memberId").text();
            }
        }).fail(function (data) {
            alert("進入失敗");
        });
    }
    else {
        alert("請先登入會員！");
    }
});

$("#activity_grid").on("click", ".btn-accept-button", function (e) {

});

$("#activity_grid").on("click", ".btn-reject-button", function (e) {
    var Item = $("#activity_grid").data("kendoGrid").dataItem($(e.currentTarget).closest('tr'));
    if (confirm("您是否要拒絕參加此活動?")) {
        $.ajax({
            url: "/Hall/DeleteInvitedList",
            dataType: "json",
            data: { "activityId": Item.ActivityId, "memberId": $("#label_memberId").text() },
            type: "post"
        }).done(function (data) {
            alert("success");
            $("#btn_cancel").click();
        }).fail(function (data) {
            alert("fail");
        });
    }
});

//開啟新增活動window之按鈕動作
$("#btn_create_activity").click(function () {
    $("#insert_form").data('kendoValidator').hideMessages();
    $("#insert_form").data('kendoWindow').center().open();
});

//新增活動window內的"新增"按鈕動作
$("#btn_add_activity").click(function () {
    var validator = $("#insert_form").kendoValidator().data("kendoValidator");

    var insertActivityData = {
        ActivityName: $("#add_activity_name").val(),
        NumberOfPeople: $("#add_activity_number").val(),
        ActivityNote: $("#add_activity_note").val(),
        ActivityDate: $("#add_activity_date").val(),
        HomeOwnerId: $("#label_memberId").text()
    }

    if (validator.validate()) {
        $.ajax({
            url: "/Hall/CreateActivity",
            dataType: "json",
            data: { "activityInfo": insertActivityData, "memberId": $("#label_memberId").text()},
            type: "post"
        }).done(function (data) {
            alert("success");
            $("#add_activity_name").val("");
            $("#add_activity_number").val("");
            $("#add_activity_note").val("");
            $("#activity_grid").data("kendoGrid").dataSource.read();
        }).fail(function (data) {
            alert("fail");
        });
    }
});

$("#btn_manage_activity").click(function () {
    resetColumn();
    var memberId = $("#label_memberId").text()
    $("#activity_grid").data("kendoGrid").dataSource.transport.options.read = {
        url: "/Hall/GetManageActivity",
        type: "post",
        dataType: "json",
        data: { "memberId": memberId }
    }
    $("#activity_grid").data("kendoGrid").dataSource.read();
});

$("#btn_cancel").click(function () {
    resetColumn();
    var memberId = $("#label_memberId").text()
    $("#activity_grid").data("kendoGrid").dataSource.transport.options.read = {
        url: "/Hall/GetUnJoinActivity",
        type: "post",
        dataType: "json",
        data: { "memberId": memberId }
    }
    $("#activity_grid").data("kendoGrid").dataSource.read();
});

$("#btn_view_join_activity").click(function () {
    resetColumn();
    var memberId = $("#label_memberId").text()
    $("#activity_grid").data("kendoGrid").dataSource.transport.options.read = {
        url: "/Hall/GetJoinActivity",
        type: "post",
        dataType: "json",
        data: { "memberId": memberId }
    }
    $("#activity_grid").data("kendoGrid").dataSource.read();
});


$("#btn_view_invite_activity").click(function () {
    var memberId = $("#label_memberId").text()

    var options = $("#activity_grid").data("kendoGrid").getOptions()

    options.columns = [
        { hidden: true, field: "ActivityId" },
        { field: "ActivityName", title: "活動名稱", width: "10%" },
        { field: "HomeOwnerId", title: "活動房主", width: "8%" },
        { field: "NumberOfPeople", title: "活動人數", width: "7%" },
        { field: "ActivityNote", title: "活動內容", width: "18%" },
        { field: "ActivityDate", title: "活動日期", width: "8%" },
        { field: "InviteMember", title: "邀請人", width: "8%" },
        {
            command: {
                text: "接受",
                className: "btn-enter-button",
            },
            title: " ",
            width: "7%",
            attributes: {
                style: "text-align: center"
            }
        },
        {
            command: {
                text: "拒絕",
                className: "btn-reject-button",
            },
            title: " ",
            width: "7%",
            attributes: {
                style: "text-align: center"
            }
        }
    ]

    $("#activity_grid").data("kendoGrid").setOptions(options)

    $("#activity_grid").data("kendoGrid").dataSource.transport.options.read = {
        url: "/Hall/GetInvitedActivity",
        type: "post",
        dataType: "json",
        data: { "memberId": memberId }
    }
    $("#activity_grid").data("kendoGrid").dataSource.read();

    $("#activity_grid").data("kendoGrid").refresh();
});

$("#btn_search_activity").click(function () {
    var activityKeyWord = $("#search_activity").val()
    $("#activity_grid").data("kendoGrid").dataSource.transport.options.read = {
        url: "/Hall/InputAcivityKeyWord",
        type: "post",
        dataType: "json",
        data: { keyWord: activityKeyWord }
    }
    $("#activity_grid").data("kendoGrid").dataSource.read();
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

function resetColumn() {
    var options = $("#activity_grid").data("kendoGrid").getOptions()

    options.columns = [
        { hidden: true, field: "ActivityId" },
        { field: "ActivityName", title: "活動名稱", width: "10%" },
        { field: "HomeOwnerId", title: "活動房主", width: "10%" },
        { field: "NumberOfPeople", title: "活動人數", width: "10%" },
        { field: "ActivityNote", title: "活動內容", width: "30%" },
        { field: "ActivityDate", title: "活動日期", width: "10%" },
        {
            command: {
                text: "進入",
                className: "btn-enter-button",
            },
            title: " ",
            width: "10%",
            attributes: {
                style: "text-align: center"
            }
        }
    ]

    $("#activity_grid").data("kendoGrid").setOptions(options)
}

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