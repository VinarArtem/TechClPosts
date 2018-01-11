$(document).ready(function () {

    // #region Authorize

    $("#userName, #userPassword").on("input", function () {

        if ($(this).val().length > 20) {
            $(this).val($(this).val().substring(0, 21));
        }

        if ($("userName").val() != "" && $("userPassword") != "") {
            $("#authSubmit").prop("disabled", false);
        } else {
            $("#authSubmit").prop("disabled", true);
        }
    })

    // #endregion

    // #region AddPost

    $("#showPostCreator").click(function () {

        if ($("#cke_editor").length == 0) {
            CKEDITOR.replace("editor");
        }
        
        $("#subjectDropdown").load("/Admin/SubjectsDropDown");

        $("#addPost").show();
    })

    $("#postDescription").on("input", function () {
        $(this).val($(this).val().replace(/^ /, "").replace(/  $/, " ").replace(/( ){2,}/g, " ").replace(/[^a-zA-Z0-9 ]/g, ""));

        if ($(this).val() != "") {
            $("#addNewPost").removeAttr('disabled');
        } else {
            $("#addNewPost").prop("disabled", true);
        }
    })

    $("#addNewPost").click(function () {
        let desc = $("#postDescription").val();
        let subj = $("#subjectDropdown").val();
        let cont = encodeURIComponent(CKEDITOR.instances["editor"].getData());

        if (cont == "") {
            alert("The content of the post is empty");
            return;
        }

        $.post(
            "/Admin/AddPost",
            {
                description: desc,
                subject: subj,
                content: cont,
            }
            )
    })

    $("#closeAddPostForm").click(function () {
        $("#addPost").hide();
    })

    //#endregion

    //#region Subjects

    $("#subjects").click(function () {
        $("#subjList").load("/Admin/SubjectsList")

        $("#subjectsContainer").show();
    })

    $("#newSubjName").on("input", function () {
        $(this).val($(this).val().replace(/^ /, "").replace(/  $/, " ").replace(/( ){2,}/g, " ").replace(/[^a-zA-Z0-9 ]/g, ""));

        if ($(this).val().length > 50) {
            $(this).val($(this).val().substring(0, 50));
        }

        if ($(this).val() != "") {
            $("#addSubject").removeAttr('disabled');
        } else {
            $("#addSubject").prop("disabled", true);
        }
    })

    $("#addSubject").click(function () {
        if ($("#newSubjName").val() != "") {
            let newSubj = $("#newSubjName").val().replace(/ $/, "");

            $.post(
                "/Admin/AddSubject",
                {
                    subjectName: newSubj
                },
                function (data, status) {
                    if (status != 'success') {
                        alert("Error occurred during creation of new subject!\nPlease try again!");
                    } else {
                        $("#subjList").load("/Admin/SubjectsList");
                        $("#newSubjName").val("");
                    }
                }
                )
        }
    })

    $("#closeSubjLisr").click(function () {
        $("#subjectsContainer").hide();
        $("#newSubjName").val("");
    })

    //#endregion

    //#region Posts

    $("#posts").click(function () {
        $("#listOfPosts").load("/Admin/ListOfPosts")

        $(".posts-list").show();
    })

    $("#closePostjLisr").click(function () {
        $(".posts-list").hide();
    })

    //#endregion
})

