"use strict";
displayLoadingDialog();

$(function () {
    renderConfimedGrid();
});

function renderConfimedGrid()
{
    $("#movietitle").width('70%').kendoMaskedTextBox();
    $("#runningtime").kendoTimePicker({
        format: "HH:mm"
    });
    dismissLoadingDialog();
}