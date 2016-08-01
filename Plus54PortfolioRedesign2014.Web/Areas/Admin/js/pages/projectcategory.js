function createProjectCategory(jqForm, attachmentResult) {
    var formData = jqForm.serialize();
    var oldAttachment = getParameterFromTextByName(formData, "ThumbnailPath");
    if (attachmentResult != null) {
        formData = getTextReplaced(formData, "&ThumbnailPath=" + oldAttachment, "&ThumbnailPath=" + attachmentResult.pathattachment);
    }
    callCreate(jqForm, formData);
}