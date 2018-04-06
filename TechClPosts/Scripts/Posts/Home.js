"use strict";

$(document).ready(function () {

    //#region User

    $("#logout").click(function () {
        document.cookie += "expires=Thu, 01 Jan 1970 00:00:00 GMT";
        location.href = "/";
    });

    $("#userLogin, #userPassword").on("input", function () {
        $(this).val($(this).val().replace(/^ /, "").replace(/  $/, " ").replace(/[^a-zA-Z0-9 ]/g, ""));

        if ($(this).val().length > 50) {
            $(this).val($(this).val().substring(0, 50));
        }

        if ($("#userLogin").val() != "" && $("#userPassword").val() != "") {
            $("#loginBtn").removeAttr('disabled');
        } else {
            $("#loginBtn").prop("disabled", true);
        }
    })

    $("#loginBtn").click(function () {
        if ($("#userLogin").val() !== "" && $("#userPassword").val() !== "") {
            let login = $("#userLogin").val();
            let password = $("#userPassword").val();
            
            $.post(
                "/User/Login",
                {
                    userLogin: login,
                    userPassword: password
                },
                function (data, status) {
                    if (data === "login failed") {
                        alert("Wrong login or password!");
                    } else if (data === "login successful") {
                        location.href = location.href;
                    } else {
                        alert("Authentication error please try again!");
                    }
                }
                )
        }
    })

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