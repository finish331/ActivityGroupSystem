$(document).ready(function () {
    debugger;
    $("#logout_button").kendoButton();
    $("#btn_create_activity").kendoButton();
    $("#btn_manage_activity").kendoButton();
    $("#btn_view_join_activity").kendoButton();

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
                    url: "GetAllActivity",
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
            { field: "ActivityId", title: "活動代號", width: "10%" },
            { field: "ActivityName", title: "活動名稱", width: "15%" },
            { field: "HomeOwnerId", title: "活動房主", width: "8%" },
            {
                command: { text: "參加", className: "" },
                title: " ",
                width: "5%"
            },
            {
                command: {
                    text: "進入",
                    className: "",
                },
                title: " ",
                width: "5%"

            }
        ]
    });
});

//開啟新增活動window之按鈕動作
$("#btn_create_activity").click(function () {
    $("#insert_form").data('kendoValidator').hideMessages();
    $("#insert_form").data('kendoWindow').center().open();
});

//新增活動window內的"新增"按鈕動作
$("#btn_add_activity").click(function () {
    var validator = $("#insert_form").kendoValidator().data("kendoValidator");

    var insertBookData = {
        ActivityName: $("#add_activity_name").val(),
        AvtivityNumber: $("#add_activity_number").val(),
        AvtivityNote: $("#add_activity_note").val(),
        AvtivityDate: $("#add_activity_date").val(),
    }

    if (validator.validate()) {
        $.ajax({
            url: "InsertBookToDB",
            dataType: "json",
            data: insertBookData,
            type: "post"
        }).done(function (data) {
            alert("success");
            ClearView();
        }).fail(function (data) {
            alert("fail");
        });
    }
});

$("#btn_manage_activity").click(function () {
    $("#activity_grid").data("kendoGrid").dataSource.transport.options.read = {
        url: "GetAllActivity2",
        type: "post",
        dataType: "json",
    }
    $("#activity_grid").data("kendoGrid").dataSource.read();
});

function Logout() //test
{
    var cookies = document.cookie;
    if (cookies != null) {
        document.cookie = 'userName=; max-age=0; path=/';
        alert(document.cookie);
        //location.href = "Index";
    }
}