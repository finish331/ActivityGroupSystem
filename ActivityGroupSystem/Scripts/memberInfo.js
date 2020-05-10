
$(document).ready(function () {
    debugger;
    $("#logout_button").kendoButton();
    $("#confirm").kendoButton();
    $("#reset").kendoButton();
    $("#memberInfo_form").kendoValidator();

    $("#memberInfo_form").kendoWindow({
        width: "500px",
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

    $("#blackMemberList").kendoGrid({
        dataSource: {
            serverPaging: true,
            serverSorting: true,
            pageSize: 100,
            transport: {
                read: {
                    url: "GetFriendList",
                    type: "post",
                    dataType: "json",
                }
            },
        },
        height: 500,
        scrollable: {
            virtual: true
        },
        sortable: true,
        columns: [
            { field: "MemberName", title: "Member", width: "10%" },
            
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

