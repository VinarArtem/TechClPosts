"use strict";

$(document).ready(function () {

    //#region User

    $("#logout").click(function () {
        document.cookie += "expires=Thu, 01 Jan 1970 00:00:00 GMT";
        location.href = "/";
    });


    //#endregion

    //#region Subject

    $("#subjectsListFilter").on("input", function () {
        let coll = $("#subjectsList").children();

        if ($(this).val() != "") {
            for (let el of coll) {
                if (el.innerText.toUpperCase().includes($(this).val().toUpperCase().replace(/ /g, ""))) {
                    $(el).show();
                } else {
                    $(el).hide();
                }
            }
        } else {
            for (let el of coll) {
                $(el).show();
            }
        }
    });

    //#endregion

})