
$(document).ready(function () {
    debugger;
    $("#logout_button").kendoButton();
    $("#confirm").kendoButton();
    $("#reset").kendoButton();
    $("#memberInfo_form").kendoValidator();

    $("#memberInfo_form").kendoWindow({
        width: "600px",
        title: "Member Information",
        visible: false, //設定此介面一開始是否可看見
        actions: ["Minimize", "Maximize", "Close"],
        modal: true, //操作kendoWindow時，其他元件無法操作
    }).data("kendoWindow");

    $("#checkInfo_form_form").kendoWindow({
        width: "500px",
        title: "Member Information",
        visible: false, //設定此介面一開始是否可看見
        actions: ["Minimize", "Maximize", "Close"],
        modal: true, //操作kendoWindow時，其他元件無法操作
    }).data("kendoWindow");

    $("#tabstrip").kendoTabStrip({
        tabPosition: "left",
        animation: {
            open: {
                effects: "fadeIn"
            }
        }
    });

    $("#friendList").kendoGrid({
        dataSource: {
            serverPaging: true,
            serverSorting: true,
            pageSize: 100,
            transport: {
                read: {
                    url: "GetFriendList",
                    type: "post",
                    dataType: "json",
                },
            },
        },
        height: 500,
        scrollable: {
            virtual: true
        },
        sortable: true,
        columns: [
            { hidden: true, field: "MemberId" },
            { field: "MemberId", title: "Member Id", width: "10%" },
            { field: "MemberName", title: "Member Name", width: "10%" },
            {
                command: {
                    text: "刪除",
                    className: "",
                    click: function (e) {
                        alert("1");
                    }
                },
                title: " ",
                width: "5%"
            }
        ]
    });
    $("#blackMemberList").kendoGrid({
        dataSource: {
            serverPaging: true,
            serverSorting: true,
            pageSize: 100,
            transport: {
                read: {
                    url: "GetAllActivity",
                    type: "post",
                    dataType: "json",
                },
                test: function (e) {
                    alert("1");
                }
            },
        },
        height: 500,
        scrollable: {
            virtual: true
        },
        sortable: true,
        columns: [
            { field: "ActivityName", title: "活動名稱", width: "10%" },
            {
                command: {
                    text: "刪除",
                    className: "test",
                },
                title: " ",
                width: "5%"
            }
        ]
    });
    $("#friendInvitation").kendoGrid({
        dataSource: {
            serverPaging: true,
            serverSorting: true,
            pageSize: 100,
            transport: {
                read: {
                    url: "GetAllActivity",
                    type: "post",
                    dataType: "json",
                },
            },
        },
        height: 500,
        scrollable: {
            virtual: true
        },
        sortable: true,
        columns: [
            { field: "ActivityName", title: "活動名稱", width: "10%" },
            {
                command: {
                    text: "同意",
                    className: "test",
                },
                click: function (e) {
                    console.log("1");
                },
                title: " ",
                width: "5%"
            },
            {
                command: {
                    text: "拒絕",
                    className: "test",
                },
                title: " ",
                width: "5%"
            }
        ]
    });

   

    $("#Datepicker").kendoDatePicker();
});

$("#memberInfo").click(function () {
    $("#memberInfo_form").data('kendoValidator').hideMessages();
    $("#memberInfo_form").data('kendoWindow').center().open();
});

$("#checkInfo").click(function () {
    $("#checkInfo_form").data('kendoValidator').hideMessages();
    $("#checkInfo_form").data('kendoWindow').center().open();
});

function test() //test
{
    
}

