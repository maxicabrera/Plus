function createProject(jqForm, attachmentResult) {
    var formData = jqForm.serialize();
    var oldAttachment = getParameterFromTextByName(formData, "ThumbnailPath");
    if (attachmentResult != null) {
        formData = getTextReplaced(formData, "&ThumbnailPath=" + oldAttachment, "&ThumbnailPath=" + attachmentResult.pathattachment);
    }
    callCreate(jqForm, formData);
}

function createProjectSiteLogo(jqForm, attachmentResult) {
    var formData = jqForm.serialize();
    var oldAttachment = getParameterFromTextByName(formData, "SiteLogoPath");
    if (attachmentResult != null) {
        formData = getTextReplaced(formData, "&SiteLogoPath=" + oldAttachment, "&SiteLogoPath=" + attachmentResult.pathattachment);
    }
    callCreate(jqForm, formData);
}

function createProjectSliderImages(jqForm, attachmentResult) {
    var formData = jqForm.serialize();
    var oldAttachment = getParameterFromTextByName(formData, "SliderImages");
    if (attachmentResult != null) {
        formData = getTextReplaced(formData, "&SliderImages=" + oldAttachment, "&SliderImages=" + attachmentResult.pathattachment);
    }
    callCreate(jqForm, formData);
}