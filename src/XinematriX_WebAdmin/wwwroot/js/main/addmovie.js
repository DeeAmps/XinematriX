<<<<<<< HEAD
﻿"use strict";
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
=======
﻿
>>>>>>> a3fe7956ee5c9787d48f99dd2738c77e0b90f6bd
