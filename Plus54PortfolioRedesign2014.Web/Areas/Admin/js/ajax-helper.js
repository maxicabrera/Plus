//testing anthill.
function submitForm(jqForm, onSuccess, onFail) {
    //    var jqButton = $("#" + idButton);
    //    var jqForm = $("#" + idForm).closest('form');
    var formData = jqForm.serialize();
    submitFormSerialized(jqForm, formData, onSuccess, onFail);
}

function submitFormSerialized(jqForm, jqFormSerialized, onSuccess, onFail) {
    //    var jqButton = $("#" + idButton);
    //    var jqForm = $("#" + idForm).closest('form');

    var $jqBtn = $(jqForm).find('.clickedButton'),
        btnType = $jqBtn.text() ? true : false,
        btnText = $jqBtn.text() || $jqBtn.val();

    $.ajax({
        type: $(jqForm).attr('method'),
        url: $(jqForm).attr('action'),
        data: jqFormSerialized,
        beforeSend: function (data) {
            $jqBtn.attr("disabled", "disabled");
            if (!btnType) {
                $jqBtn.val(tempText);
            }
            else {
                $jqBtn.text(tempText);
            }
        },
        success: function (responseData) {
            if (restoreTextInTab1 != undefined)
                restoreTextInTab1();
            if (restoreTextInTab2 != undefined)
                restoreTextInTab2();
            if (responseData.Succeeded == false) {
                if (onFail) {
                    onFail(responseData.Message);
                }
                else {
                    alert("error: " + xhr.responseText);
                }
            }
            else if (responseData.Succeeded == true) {
                displaySuccess(responseData.Message);
                if (onSuccess) {
                    onSuccess(responseData);
                }
            }
            else {
                if (onSuccess) {
                    onSuccess(responseData);
                }
            }
        },
        complete: function (data) {
            $jqBtn.removeAttr("disabled");
            if (!btnType) {
                $jqBtn.val(btnText);
            }
            else {
                $jqBtn.text(btnText);
            }
        },
        error: function (xhr, status, error) {
            if (onFail) {
                onFail(xhr.responseText);
            }
            else {
                alert("error: " + xhr.responseText);
            }
        }
    });
}

function submitUrlPost(jqForm, jqFormSerialized, url, onSuccess, onFail) {
    var $jqBtn = $(jqForm).find('.clickedButton'),
        btnType = $jqBtn.text() ? true : false,
        btnText = $jqBtn.text() || $jqBtn.val();

    $.ajax({
        type: "POST",
        url: url,
        data: jqFormSerialized,
        beforeSend: function (data) {
            $jqBtn.attr("disabled", "disabled");
            if (!btnType) {
                $jqBtn.val(tempText);
            }
            else {
                $jqBtn.text(tempText);
            }
        },
        success: function (responseData) {
            if (responseData.Succeeded == false) {
                if (onFail) {
                    onFail(responseData.Message);
                }
                else {
                    alert("error: " + xhr.responseText);
                }
            }
            else if (responseData.Succeeded == true) {
                if (onSuccess) {
                    onSuccess(responseData);
                }
            }
            else {
                if (onSuccess) {
                    onSuccess(responseData);
                }
            }
        },
        complete: function (data) {
            $jqBtn.removeAttr("disabled");
            if (!btnType) {
                $jqBtn.val(btnText);
            }
            else {
                $jqBtn.text(btnText);
            }
        },
        error: function (xhr, status, error) {
            if (onFail) {
                onFail(xhr.responseText);
            }
            else {
                alert("error: " + xhr.responseText);
            }
        }
    });
}

function submitUrl(url, data, onSuccess, onFail) {
    $.ajaxSetup({ cache: false });
    $.ajax({
        type: "GET",
        url: url,
        data: data,
        success: function (responseData) {
            if (onSuccess) {
                onSuccess(responseData);
            }
        },
        error: function (xhr, status, error) {
            if (onFail) {
                onFail(xhr.responseText);
            }
            else {
                alert("error: " + xhr.responseText);
            }
        }
    });
}

function submitUrlJson(url, data, onSuccess, onFail) {
    $.ajax({
        type: "POST",
        url: url,
        data: data,
        dataType: 'json',
        success: function (responseData) {
            if (onSuccess) {
                onSuccess(responseData);
            }
        },
        error: function (xhr, status, error) {
            if (onFail) {
                onFail(xhr.responseText);
            }
            else {
                alert("error: " + xhr.responseText);
            }
        }
    });
}


function CallSubmit(objThis, jqFormSerialized, onSuccess, onFail) {
    var valid = true;
    var errorMessage = "";
    //MAGIC LINE TO GET VALIDATION ERROR LIST
    jQuery.validator.unobtrusive.parse("#" + objThis.attr("id"));
    var val = $(objThis).validate();
    valid = val.valid();

    if (valid) {

		onFail = onFail ? onFail : onSubmitFailed;
		if (jqFormSerialized && jqFormSerialized.length > 0) {
            submitFormSerialized(objThis, jqFormSerialized, onSuccess, onFail);
        }
        else {
            submitForm(objThis, onSuccess, onFail);
        }
    }
    else {
        restoreTextInTab1();
        var errorMessage = "";

        if (val.errorList.length > 0) {
            var errors = val.errorList;
            $.each(errors, function (index, item) {
                errorMessage += item.message + "</BR>";
            });
            //TODO:remove last BR
        }
        else {
            errorMessage = "Invalid fields";
        }

        if (onFail)
            onFail(errorMessage);
        else
            onSubmitFailed(errorMessage);
    }

    return valid;
}


function onSubmitFailed(responseText) {
    if (displayAlert) {
        displayAlert(responseText);
    }
    else {
        alert(responseText);
    }

}