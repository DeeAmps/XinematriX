"use strict";
var XApi = apiUrl + "/api/Promos";
var daysOfWeeks = {};

displayLoadingDialog();

var daysAjax = $.ajax({
    url: XApi + '/GetDaysOfWeek',
    type: 'Get',
    contentType: 'application/json'

});

function LoadData() {
    $.when(daysAjax)
        .done(function (Data) {
            daysOfWeeks = Data
        });
    renderGrid();
    dismissLoadingDialog();
}

$(function () {
    LoadData();
});

function renderGrid()
{
    $("#tabs").kendoTabStrip();
    $("#prdate").kendoGrid({
        dataSource:
        {
            transport: {
                read: {
                    url: XApi + '/GetDatePromos/',
                    type: 'POST',
                    dataType: "json",
                    contentType: "application/json",
                },
                update: {
                    url: XApi + '/UpdateDatePromos/',
                    type: 'POST',
                    dataType: "json",
                    contentType: "application/json",
                },

                parameterMap: function (data, type) {
                    return kendo.stringify(data);
                },
            },
            requestEnd: function (e) {
                var response = e.response;
                var type = e.type;
                if (type === 'update' && e.response) {
                    $('#prdate').data('kendoGrid').dataSource.read();
                }
            },
            pageSize: 10,
            schema: {
                data: "Data",

                total: "Count",
                model: {
                    id: 'DatePromoId',
                    fields: {
                        DatePromoId: { type: 'number', editable: false },
                        PromoDate: { editable: false, validation: { required: true } },
                        PromoDescription: { editable: false, validation: { required: true } },
                    } //fields
                },
            },
            serverPaging: true,
            serverFiltering: true,
            serverSorting: false,
        },
        columns: [
            //{ field: 'DivisionId', title: 'S/N', filterable: { cell: { operator: "contains" } } },
            { field: 'PromoDate', title: 'Promo Date', filterable: { cell: { operator: "contains" } } },
            {
                command: [
                    {
                        name: "PromoDescription",
                        text: "View Promo Description",
                        click: function (e) {
                            // e.target is the DOM element representing the button
                            var tr = $(e.target).closest("tr"); // get the current table row (tr)
                            // get the data bound to the current table row
                            var currentRecord = this.dataItem(tr);
                            showPromoDescriptionWindow("#datepromo", currentRecord.PromoDescription);

                        },
                    }
                ],
                width: 120
            },
            { command: ["edit"], title: "&nbsp;", width: "110px" },
            {
                command: [
                    {
                        name: "del",
                        text: "Delete",
                        click: function (e) {
                            // e.target is the DOM element representing the button
                            var tr = $(e.target).closest("tr"); // get the current table row (tr)
                            // get the data bound to the current table row
                            var currentRecord = this.dataItem(tr);
                            var result = confirm("Delete Promo on " + currentRecord.PromoDate + " ?")
                            if (result) {
                                window.location = "/Home/DeleteDatePromo/" + currentRecord.DatePromoId;

                            }
                        },
                    }
                ],
                width: 110
            },


        ],
        filterable: true,
        sortable: {
            mode: "multiple",
        },
        editable: "popup",
        selectable: true,
        toolbar: [
            {
                name: "addDatePromo",
                className: 'addDatePromo',
                text: '+ Add Promo(Date)',
            },
        ],
        pageable: {
            pageSize: 10,
            pageSizes: [10, 50, 75, 100, 1000],
            previousNext: true,
            buttonCount: 5,
        },

        mobile: true
    });
    $(".addDatePromo").click(function () {
        showDatePromoWindow();
    });

    //Day Promos Start Here
    $("#tabs").kendoTabStrip();
    $("#prday").kendoGrid({
        dataSource:
        {
            transport: {
                read: {
                    url: XApi + '/GetDayPromos/',
                    type: 'POST',
                    dataType: "json",
                    contentType: "application/json",
                },
                update: {
                    url: XApi + '/UpdateDayPromos/',
                    type: 'POST',
                    dataType: "json",
                    contentType: "application/json",
                },

                parameterMap: function (data, type) {
                    return kendo.stringify(data);
                },
            },
            requestEnd: function (e) {
                var response = e.response;
                var type = e.type;
                if (type === 'update' && e.response) {
                    $('#prday').data('kendoGrid').dataSource.read();
                }
            },
            pageSize: 10,
            schema: {
                data: "Data",

                total: "Count",
                model: {
                    id: 'DayPromoId',
                    fields: {
                        DayPromoId: { type: 'number', editable: false },
                        PromoDay: { editable: false, validation: { required: true } },
                        PromoDDescription: { editable: false, validation: { required: true } },
                    } //fields
                },
            },
            serverPaging: true,
            serverFiltering: true,
            serverSorting: false,
        },
        columns: [
            //{ field: 'DivisionId', title: 'S/N', filterable: { cell: { operator: "contains" } } },
            { field: 'PromoDay', title: 'Promo Day', filterable: { cell: { operator: "contains" } } },
            {
                command: [
                    {
                        name: "PromoDDescription",
                        text: "View Promo Description",
                        click: function (e) {
                            // e.target is the DOM element representing the button
                            var tr = $(e.target).closest("tr"); // get the current table row (tr)
                            // get the data bound to the current table row
                            var currentRecord = this.dataItem(tr);
                            showPromoDDescriptionWindow("#daypromo", currentRecord.PromoDDescription);

                        },
                    }
                ],
                width: 120
            },
            { command: ["edit"], title: "&nbsp;", width: "110px" },
            {
                command: [
                    {
                        name: "del",
                        text: "Delete",
                        click: function (e) {
                            // e.target is the DOM element representing the button
                            var tr = $(e.target).closest("tr"); // get the current table row (tr)
                            // get the data bound to the current table row
                            var currentRecord = this.dataItem(tr);
                            var result = confirm("Delete Promo on " + currentRecord.PromoDay + " ?")
                            if (result) {
                                window.location = "/Home/DeleteDayPromo/" + currentRecord.DayPromoId;

                            }
                        },
                    }
                ],
                width: 110
            },


        ],
        filterable: true,
        sortable: {
            mode: "multiple",
        },
        toolbar: [
            {
                name: "addDayPromo",
                className: 'addDayPromo',
                text: '+ Add Promo(Day)',
            },
        ],
        editable: "popup",
        selectable: true,
        pageable: {
            pageSize: 10,
            pageSizes: [10, 50, 75, 100, 1000],
            previousNext: true,
            buttonCount: 5,
        },

        mobile: true
    });
    $(".addDayPromo").click(function () {
        showDayPromoWindow();
    });
    //Day Promos End Here

    
    function showDatePromoWindow(data) {
        return showDatePrWindow('#newDatePromo', data)
    };

    function showDayPromoWindow(data) {
        return showDayPrWindow('#newDayPromo', data)
    };
}

function showPromoDescriptionWindow(template, data) {
    var dfd = new jQuery.Deferred();
    var result = false;
    $("<div id='popupWindow'></div>")
        .appendTo("body")
        .kendoWindow({
            width: "500px",
            height: "600px",
            modal: true,
            title: "Promo Description",
            visible: false,
            close: function (e) {
                this.destroy();
                dfd.resolve(result);
            }
        }).data('kendoWindow').content($(template).html()).center().open();
    $('#popupWindow').kendoEditor({
        value: data,
        encoded: false
    }).attr('contenteditable', false);

}

function showPromoDDescriptionWindow(template, data) {
    var dfd = new jQuery.Deferred();
    var result = false;
    $("<div id='popupWindow'></div>")
        .appendTo("body")
        .kendoWindow({
            width: "400px",
            height: "400px",
            modal: true,
            title: "Promo Description",
            visible: false,
            close: function (e) {
                this.destroy();
                dfd.resolve(result);
            }
        }).data('kendoWindow').content($(template).html()).center().open();
    $('#popupWindow').kendoEditor({
        value: data,
        encoded: false
    }).attr('contenteditable', false);

}

function showDatePrWindow(template, data) {
    var dfd = new jQuery.Deferred();
    var result = false;
    $("<div id='popupWindow'></div>")
        .appendTo("body")
        .kendoWindow({
            width: "500px",
            height: "450px",
            modal: true,
            title: "New Promo(Date)",
            visible: false,
            close: function (e) {
                this.destroy();
                dfd.resolve(result);
            }
        }).data('kendoWindow').content($(template).html()).center().open();
    applyDatePromoKendoStyle(data);
    $('#confirmDatePromo').click(function () {
        var res = {
            
        };
        saveDatePromoResponse(res);

    });
    $('#cancel').click(function () {
        $('#popupWindow').data('kendoWindow').close();
    });
    return dfd.promise();
};

function showDayPrWindow(template, data) {
    var dfd = new jQuery.Deferred();
    var result = false;
    $("<div id='popupWindow'></div>")
        .appendTo("body")
        .kendoWindow({
            width: "500px",
            height: "450px",
            modal: true,
            title: "New Promo(Day)",
            visible: false,
            close: function (e) {
                this.destroy();
                dfd.resolve(result);
            }
        }).data('kendoWindow').content($(template).html()).center().open();
    applyDayPromoKendoStyle(data);
    $('#confirmDayPromo').click(function () {
        var res = {

        };
        saveDayPromoResponse(res);

    });
    $('#cancell').click(function () {
        $('#popupWindow').data('kendoWindow').close();
    });
    return dfd.promise();
};

function applyDatePromoKendoStyle(data) {
    $('#dateforpromo').kendoDatePicker();
    $('#descriptionpromo').kendoEditor({
        resizable: {
            content: true,
            toolbar: false
        }
    });
}

function applyDayPromoKendoStyle(data) {
    $('#dayforpromo').width("100%")
        .kendoDropDownList({
            dataSource: daysOfWeeks,
            filter: "contains",
            suggest: true,
            dataValueField: "Day",
            dataTextField: "Day",
            highlightFirst: true,
            ignoreCase: true,
            optionLabel: "---Select Option---",
        });
    $('#ddescriptionpromo').kendoEditor({
        resizable: {
            content: true,
            toolbar: false
        }
    });
}