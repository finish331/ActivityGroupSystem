
$(document).ready(function () {
    debugger;
    $("#logout_button").kendoButton();
    $("#confirm").kendoButton();
    $("#reset").kendoButton();
    $("#register_form").kendoValidator().data("kendoValidator");

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
            type: "odata",
            serverPaging: true,
            serverSorting: true,
            pageSize: 100,
            transport: {
                read: "https://demos.telerik.com/kendo-ui/service/Northwind.svc/Orders"
            }
        },
        height: 500,
        scrollable: {
            virtual: true
        },
        sortable: true,
        columns: [
            { field: "OrderID", title: "Order ID"},
            { field: "CustomerID", title: "Customer ID"}
        ]
    });

    $("#Datepicker").kendoDatePicker();
});


