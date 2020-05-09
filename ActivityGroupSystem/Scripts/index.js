
$(document).ready(function () {
    debugger;
    $("#logout_button").kendoButton();
    $("#detail_form").kendoValidator().data("kendoValidator");

    $("#detail_form").kendoWindow({
        width: "600px",
        title: "Book Detail Window",
        visible: false,
        actions: ["Pin", "Minimize", "Maximize", "Close"],
        modal: true
    }).data("kendoWindow");

    debugger;
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

$("#logout_button").click(function () {
    $("#detail_form").data('kendoValidator').hideMessages();
    $("#detail_form").data('kendoWindow').center().open();
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