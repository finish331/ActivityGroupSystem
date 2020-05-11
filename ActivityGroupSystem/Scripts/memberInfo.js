
$(document).ready(function () {
    debugger;
    $("#manage_btn").kendoButton();
    $("#memberInfo_form").kendoValidator();
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

    $("#friendList").kendoGrid({
        dataSource: {
            serverPaging: true,
            serverSorting: true,
            pageSize: 100,
            transport: {
                read: {
                    url: "/Hall/GetFriendList",
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
                    className: "DeleteFriend",
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
                    url: "/Hall/GetBlackList",
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
            { field: "MemberId", title: "Member Id", width: "10%" },
            { field: "MemberName", title: "Member Name", width: "10%" },
            {
                command: {
                    text: "刪除",
                    className: "DeleteBlack",
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
                    url: "/Hall/GetInvitationList",
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
            { field: "MemberId", title: "Member Id", width: "10%" },
            { field: "MemberName", title: "Member Name", width: "10%" },
            {
                command: {
                    text: "同意",
                    className: "Agree",
                },
                title: " ",
                width: "5%"
            },
            {
                command: {
                    text: "拒絕",
                    className: "Reject",
                },
                title: " ",
                width: "5%"
            }
        ]
    });

   

    
});

$("#friendList").on("click", ".DeleteFriend", function (e) {
    var Item = $("#friendList").data("kendoGrid").dataItem($(e.currentTarget).closest('tr'));
    var errorNotification = $("#errorNotification").kendoNotification().data("kendoNotification");
    $.ajax({
        url: "/Hall/DeleteFriend",
        dataType: "json",
        data: { MemberId: Item.MemberId, },
        type: "post"
    }).done(function (data) {
        errorNotification.show(data, "info");
        $("#friendList").data("kendoGrid").dataSource.read();
    }).fail(function (data) {
        errorNotification.show("修改資料失敗", "info");
    });
});

$("#blackMemberList").on("click", ".DeleteBlack", function (e) {
    var Item = $("#blackMemberList").data("kendoGrid").dataItem($(e.currentTarget).closest('tr'));
    var errorNotification = $("#errorNotification").kendoNotification().data("kendoNotification");
    $.ajax({
        url: "/Hall/DeleteBlack",
        dataType: "json",
        data: { MemberId: Item.MemberId, },
        type: "post"
    }).done(function (data) {
        errorNotification.show(data, "info");
        $("#blackMemberList").data("kendoGrid").dataSource.read();
    }).fail(function (data) {
        errorNotification.show("修改資料失敗", "info");
    });
});

$("#friendInvitation").on("click", ".Agree", function (e) {
    var Item = $("#friendInvitation").data("kendoGrid").dataItem($(e.currentTarget).closest('tr'));
    var errorNotification = $("#errorNotification").kendoNotification().data("kendoNotification");
    $.ajax({
        url: "/Hall/AgreeInvitation",
        dataType: "json",
        data: { MemberId: Item.MemberId, },
        type: "post"
    }).done(function (data) {
        errorNotification.show(data, "info");
        $("#friendInvitation").data("kendoGrid").dataSource.read();
        $("#friendList").data("kendoGrid").dataSource.read();
    }).fail(function (data) {
        errorNotification.show("修改資料失敗", "info");
    });
});

$("#friendInvitation").on("click", ".Reject", function (e) {
    var Item = $("#friendInvitation").data("kendoGrid").dataItem($(e.currentTarget).closest('tr'));
    var errorNotification = $("#errorNotification").kendoNotification().data("kendoNotification");
    $.ajax({
        url: "/Hall/RejectInvitation",
        dataType: "json",
        data: { MemberId: Item.MemberId, },
        type: "post"
    }).done(function (data) {
        errorNotification.show(data, "info");
        $("#friendInvitation").data("kendoGrid").dataSource.read();
    }).fail(function (data) {
        errorNotification.show("修改資料失敗", "info");
    });
});

$("#manage_btn").click(function () {
    var validator = $("#memberInfo_form").kendoValidator().data("kendoValidator");

    debugger;

    var errorNotification = $("#errorNotification").kendoNotification({ appendTo: "#Notification" }).data("kendoNotification");

    var insertMemberData = {
        MemberId: $("#MemberId").val(),
        Password: $("#Password").val(),
        MemberName: $("#MemberName").val(),
        Phone: $("#Phone").val(),
        Sexuality: $("#Sexuality").val(),
        Birthday: $("#Birthday").val()
    }

    if (validator.validate()) {
        $.ajax({
            url: "/Hall/UpdateMemberInfo",
            dataType: "json",
            data: insertMemberData,
            type: "post"
        }).done(function (data) {
            if (data == "") {
                errorNotification.show("修改資料成功", "info");
            }
        }).fail(function (data) {
            errorNotification.show("修改資料失敗", "info");
        });
    }
});




