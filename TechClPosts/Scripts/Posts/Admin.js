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

        $("#subjectsContainer, #postsListContainar, #postDetails, #usersContainer").hide();

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
        $("#addPost, #postsListContainar, #postDetails, #usersContainer").hide();

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

    $("#closeSubjList").click(function () {
        $("#subjectsContainer").hide();
        $("#newSubjName").val("");
    })

    //#endregion

    //#region Posts

    $("#posts").click(function () {
        $("#addPost, #subjectsContainer, #postDetails, #usersContainer").hide();

        $("#listOfPosts").load("/Admin/ListOfPosts");

        $("#postsListContainar").show();
    });

    $("body").on("click", "input.post-details-btn", function () {
        let postKey = $(this).attr("data-post-id");

        $("#postContent").load("/Admin/PostDetails?postKey=" + postKey);

        $("#postsListContainar").hide();
        $("#postDetails").show();
    });

    $("#closePostjDetails").click(function () {
        $("#postDetails").hide();
        $("#postsListContainar").show();
    })

    $("#closePostjList").click(function () {
        $("#postsListContainar").hide();
    });

    //#endregion

    //#region Users

    $("#users").click(function () {
        $("#addPost, #subjectsContainer, #postDetails, #postsListContainar").hide();

        $("#usersContainer").show();
    })

    $("#newUserName, #newUserLogin, #newUserPassword").on("input", function () {
        $(this).val($(this).val().replace(/^ /, "").replace(/  $/, " ").replace(/( ){2,}/g, " ").replace(/[^a-zA-Z0-9 ]/g, ""));

        if ($(this).val().length > 50) {
            $(this).val($(this).val().substring(0, 50));
        }

        if ($("#newUserName").val() != "" && $("#newUserLogin").val() != "" && $("#newUserPassword").val() != "" && ["0", "1"].includes($("#newUserRole").val())) {
            $("#createNewUser").removeAttr('disabled');
        } else {
            $("#createNewUser").prop("disabled", true);
        }
    })

    $("#createNewUser").click(function () {
        if ($("#newUserName").val() != "" && $("#newUserLogin").val() != "" && $("#newUserPassword").val() != "" && ["0", "1"].includes($("#newUserRole").val())) {
            let newUser = {
                name: $("#newUserName").val().replace(/ $/, ""),
                login: $("#newUserLogin").val().replace(/ $/, ""),
                password: $("#newUserPassword").val().replace(/ $/, ""),
                role: $("#newUserRole").val()
            }

            $.post(
                "/Admin/AddUser",
                newUser,
                function (data, status) {
                    //TODO: Hadle request result of adding new User
                }
                )
        }
    })

    $("#closeUsers").click(function () {
        $("#usersContainer").hide();
    })

    //#endregion

})
