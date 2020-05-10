$(document).ready(function () {
    debugger;
    $("#logout_button").kendoButton();
    $("#btn_create_activity").kendoButton();
    $("#btn_manage_activity").kendoButton();
    $("#btn_view_join_activity").kendoButton();
    $("#btn_cancel").kendoButton();
    $("#btn_search_activity").kendoButton();

    //創建活動window及內部元件初始化
    $("#btn_add_activity").kendoButton();
    $("#insert_form").kendoValidator();
    $("#insert_form").kendoWindow({
        width: "500px",
        title: "Update Book Window",
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
                    url: "/Hall/GetAllActivity",
                    type: "post",
                    dataType: "json",
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
            { field: "HomeOwnerId", title: "活動房主", width: "6%" },
            { field: "NumberOfPeople", title: "活動人數", width: "4%" },
            { field: "ActivityNote", title: "活動內容", width: "15%" },
            { field: "ActivityDate", title: "活動日期", width: "6%" },
            {
                command: { text: "參加", className: "" },
                title: " ",
                width: "5%"
            },
            {
                command: {
                    text: "進入",
                    className: "btn-enter-button",
                },
                title: " ",
                width: "5%"

            }
        ]
    });
});

//活動Grid中進入活動的按鈕
$("#activity_grid").on("click", ".btn-enter-button", function (e) {
    if ($("#label_memberId").text() != "") {
        var Item = $("#activity_grid").data("kendoGrid").dataItem($(e.currentTarget).closest('tr'));
        window.location.href = "/Hall/Room?activityId=" + Item.ActivityId + "&" + "userId=" + $("#label_memberId").text();
    }
    else {
        alert("請先登入會員！");
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

    debugger;

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
            data: insertActivityData,
            type: "post"
        }).done(function (data) {
            alert("success");
            $("#activity_grid").data("kendoGrid").dataSource.read();
        }).fail(function (data) {
            alert("fail");
        });
    }
});

$("#btn_manage_activity").click(function () {
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
    $("#activity_grid").data("kendoGrid").dataSource.transport.options.read = {
        url: "/Hall/GetAllActivity",
        type: "post",
        dataType: "json",
    }
    $("#activity_grid").data("kendoGrid").dataSource.read();
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
})