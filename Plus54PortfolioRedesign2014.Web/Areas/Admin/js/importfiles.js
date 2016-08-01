var currentProgressURL;
function initIFSubmit(jqForm, attachmentResult, progressURL) {
    currentProgressURL = progressURL
    var formData = jqForm.serialize();
    if (attachmentResult != null) {
        formData = getTextReplaced(formData, "&Link=", "&Link=" + attachmentResult.pathattachment);
    }
    callCreateWithCallBack(jqForm, formData, onUploadFile, null)
}

function setupProgress(taskId) {

    $(function () {
        $("#progressbar").progressbar({
            value: 0
        });
    });

//    $('#mainDiv').hide();
    $('#divProgress').show();
    $('#divProgressFinished').hide();

    // Periodically update monitors
    var intervalId = setInterval(function () {
        $.post(currentProgressURL, { idTask: taskId }, function (info) {
            if (info.Completed) {
                updateProgress(info);
                $("#progressbar").progressbar("option", "value", 100);
                clearInterval(intervalId);
                $(".pCurrentItem").html(info.CurrentItem - info.ErrorItems);
                $(".pTotalItems").html(info.TotalItems);

                //TODO: modal could be closed and the list updated async with ajax
                $('#divProgress').hide();
                displaySuccess($('#divProgressFinished').text() + " Successfully.", "#messagePlaceHolderImport");
                $('#doneButton').show();
                //$('#mainDiv').show();
            } else {
                updateProgress(info);
            }
            return false;
        });
    }, 1000);
}

function updateProgress(info) {

    $(".pCurrentItem").html(info.CurrentItem);
    $(".pTotalItems").html(info.TotalItems);
    //To display log
    //$("#pProgress").html(info.InfoText);

    $("#progressbar").progressbar("option", "value", info.Progress);
}

function onUploadFile(data) {
    var json;
    try {
        setupProgress(data);
    }
    catch (e) {

    }
}

function closeImport() {
    $('#import').modal('hide');
    location.reload(true);
}