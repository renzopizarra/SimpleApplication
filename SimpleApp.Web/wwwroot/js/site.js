// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {
    $('.btn-primary').on('click', function (e) {
        var user = $('#username').val();
        var pass = $('#password').val();
        var credentials = { username: user, password: pass };

        $.ajax({
            url: "/Account/Validate",
            data: credentials,
            type: 'POST',
            //async: false,
            success: function (result) {
                if (result.status == "error") {
                    $('.alert-danger #alert-div').text("Invalid username or password");
                    $('.alert-danger ').css("display", "block");
                    return;
                } else {
                    $('.alert-danger #alert-div').text("");
                    $('.alert-danger ').css("display", "none");
                    window.location = "/Home/Index";
                }
            }
        });
    });

});