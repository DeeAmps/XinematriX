"use strict";
var XApi = apiUrl + "/api/Movies";
var ratings = {};
var showtimes = [];
displayLoadingDialog();

var ratingsAjax = $.ajax({
    url: XApi + '/GetMovieRating',
    type: 'Get',
    contentType: 'application/json'

});

function LoadData() {
    $.when(ratingsAjax)
        .done(function (ratingsData) {
            ratings = ratingsData
        });
    renderConfimedGrid();
}

$(function () {
    LoadData();
});

function renderConfimedGrid()
{
    $("#movietitle").width('70%').kendoMaskedTextBox();
    $("#trailer").width('100%').kendoMaskedTextBox();
    $("#cast").width('80%').kendoMaskedTextBox();
    $("#synopsis").kendoEditor({
        resizable: {
            content: true,
            toolbar: false
        }
    });
    $("#runningtime").kendoTimePicker({
        format: "HH:mm"
    });
    $("#staus").kendoComboBox({
        dataTextField: "text",
        dataValueField: "text",
        dataSource: [
            { text: "Premiering this week" },
            { text: "Showing Now" },
            { text: "Coming Soon" },
        ],
        filter: "contains",
    });
    $("#movierating").width("70%")
        .kendoDropDownList({
            dataSource: ratings,
            filter: "contains",
            suggest: true,
            dataValueField: "MovieRatingId",
            dataTextField: "MovieRatingType",
            highlightFirst: true,
            ignoreCase: true,
            optionLabel: "---Select Option---",
        });
    $("#image").kendoUpload({
        validation: {
            allowedExtensions: [".png", ".jpg"]
        },
        async: {
            saveUrl: "save",
            removeUrl: "remove"
        },
    });
    $("#genre").kendoMultiSelect({
        placeholder: "Select genres...",
        dataTextField: "MovieGenreType",
        dataValueField: "MovieGenreId",
        autoBind: false,
        dataSource: {
            type: "odata",
            serverFiltering: true,
            transport: {
                read: {
                    url: XApi + "/GetMovieGenres",
                }
            }
        },
    });
    renderShowTimeGrid();
    dismissLoadingDialog();
}

function renderShowTimeGrid() {
    $("#showtimeGrid").kendoGrid({
        dataSource: {
            transport: {
                read: function (entries) {
                    entries.success(showtimes);
                },
                create: function (entries) {
                    entries.success(entries.data);
                },
                update: function (entries) {
                    entries.success(entries.data);
                },
                destroy: function (entries) {
                    entries.success(entries.data);
                },
                parameterMap: function (data) { return JSON.stringify(data); }
            },
            schema: {
                model: {
                    id: "showtimeid",
                    fields: {
                        date: { editable: true, validation: { required: true } },
                        showtimes: { editable: true, validation: { required: true } }

                    }
                }
            },
        },

        columns: [
            { field: 'date', title: 'Date', filterable: { cell: { operator: "contains" } } },
            { field: 'showtimes', title: 'Show Times', filterable: { cell: { operator: "contains" } } },
            { command: [{ name: "edit", text: "Edit" }, { name: "destroy", text: "Delete" }] }



        ],
        editable: "popup",
        edit: function (e) {
            var editWindow = this.editable.element.data("kendoWindow");
            editWindow.title("Edit Show Time");
        },
        filterable: true,
        sortable: {
            mode: "multiple",
        },
        selectable: true,
        pageable: {
            pageSize: 10,
            pageSizes: [10, 50, 75, 100, 1000],
            previousNext: true,
            buttonCount: 5,
        },
        toolbar: [
            {
                name: "addShowTime",
                className: 'addShowTime',
                text: '+ Add Show Time',
            },


        ],
        mobile: true
    });
    $(".addShowTime").click(function () {
        addShowTime();
    });
}

function addShowTime(data) {
    return showShowTimeWindow('#newShowTime', data)
};

function showShowTimeWindow(template, data) {
    var dfd = new jQuery.Deferred();
    var result = false;
    $("<div id='popupWindow'></div>")
        .appendTo("body")
        .kendoWindow({
            width: "500px",
            height: "300px",
            modal: true,
            title: "New Show Time",
            visible: false,
            close: function (e) {
                this.destroy();
                dfd.resolve(result);
            }
        }).data('kendoWindow').content($(template).html()).center().open();
        applyUserAccountKendoStyle(data);

    $('#confirmShowTime').click(function () {
        var res = {
            date: $('#showdate').data("kendoDatePicker").value(),
            showtimes: $('#showtimes').data("kendoMaskedTextBox").value(),
            
        };
        showtimes.push(res);
        $('#popupWindow').data('kendoWindow').close();
        renderShowTimeGrid();
        //saveFAQResponse(res);

    });
    $('#cancel').click(function () {
        $('#popupWindow').data('kendoWindow').close();
    });
    return dfd.promise();
};

function applyUserAccountKendoStyle(data)
{
    $("#showdate").kendoDatePicker();
    $("#showtimes").width('100%').kendoMaskedTextBox();
}