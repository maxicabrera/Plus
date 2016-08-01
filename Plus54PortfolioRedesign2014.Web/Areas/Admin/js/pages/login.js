$(function () {
    $('#forgotPassword').on('submit', function (e) {
        e.preventDefault();
        var valid = true;
        var errorMessage = "";
        //MAGIC LINE TO GET VALIDATION ERROR LIST
        jQuery.validator.unobtrusive.parse("#forgotPassword");
        var val = $("#forgotPassword").validate();
        valid = val.valid();

        if (!valid) {
            var errorMessage = "";

            if (val.errorList.length > 0) {
                var errors = val.errorList;
                $.each(errors, function (index, item) {
                    errorMessage += item.message + "</BR>";
                });
                //TODO:remove last BR
            }
            displayAlert(errorMessage, '#errorPlaceHolderResetPassword');
        } else {
            resetPassword();
        }
        return valid;
    });

    $('#btn-forgot-pass').on('click', function (e) {
        e.preventDefault();

        $('#loginBox').hide();
        $('#resetPass').fadeIn();
    });

    $('#btn-cancel-recover').on('click', function (e) {
        e.preventDefault();

        $('#resetPass').hide();
        $('#loginBox').fadeIn();
    });

    //login submit
    $('#loginForm').on('submit', function (e) {

        e.preventDefault();
        var valid = true;
        var errorMessage = "";
        //MAGIC LINE TO GET VALIDATION ERROR LIST
        jQuery.validator.unobtrusive.parse("#loginForm");
        var val = $("#loginForm").validate();
        valid = val.valid();

        if (!valid) {
            var errorMessage = "";

            if (val.errorList.length > 0) {
                var errors = val.errorList;
                $.each(errors, function (index, item) {
                    errorMessage += item.message + "</BR>";
                });
                //TODO:remove last BR
            }
        }
        else {
            submitForm($(this), onLoginSucceeded, onSubmitFailed);
        }
        return valid;
    });
});

function resetPassword() {
    hideMessage('#errorPlaceHolderResetPassword');
    hideMessage('#messagePlaceHolder');
    if ($("#usernameForgotPassword").val() != "") {
        submitUrlJson("/admin/home/ForgotPassword", { email: $("#usernameForgotPassword").val() }, onResetPasswordSucceded, onResetPasswordFailed);
    }
    //    else {
    //        displayAlert('Username is required.', '#errorPlaceHolderResetPassword');
    //    }
}

function onLoginSucceeded(response) {
    if (response.Succeeded) {
        var intendedUrl = getParameterByName("ReturnUrl");
        if (intendedUrl) {
            location = intendedUrl;
        }
        else {
            if (response.RedirectUrl) {
                location = response.RedirectUrl;
            }
        }
    }
    else {
        displayAlert(response.Message);
    }
}

function hideError() {
    $('#errorPlaceHolder').hide();
}

function hideMessage(idMessage) {
    if (!idMessage) {
        idMessage = "#messagePlaceHolder";
    }
    $(idMessage).hide();
}

function onResetPasswordSucceded(response) {
    if (response.Succeeded) {
        displaySuccess(response.Message, '#messagePlaceHolder');
    }
    else {
        displayAlert(response.Message, '#errorPlaceHolderResetPassword');
    }
}

function onResetPasswordFailed(response) {
    displayAlert(response.Message, '#errorPlaceHolderResetPassword');
}