var _entityPath = "";
var _entityName = "";
var _editPage = "";
var _createPage = "";
var _sendPage = "";
var _previewPage = "";
var _listPage = "";
var _indexPage = "";

var _listTabName = "";
var _editTabName = "";

var _deleteId = "";

var _deleteId = "";
var _deleteItems = "";
var _exportItems = "";
var _deleteAllItems = "";
var _moveItems = "";
var _currentFilters = "";
function initTabs(entityPath, entityName) {
    initPages(entityPath, entityName);
    //loadCreate();
}

function initPages(entityPath, entityName, list, addEdit, previewText, sendText) {
    _entityPath = entityPath;
    _entityName = entityName;
    _editPage = "/admin/" + _entityPath + "/Edit/";
    _createPage = "/admin/" + _entityPath + "/Create/";
    _listPage = "/admin/" + _entityPath + "/List/";
    _indexPage = "/admin/" + _entityPath + "/Index/";
    _entityName = entityName != undefined ? entityName : _entityName;
    _listTabName = list;
    _editTabName = addEdit;
}

function isCreatePage() {
    if ((location.pathname + "/").toLowerCase().match("^" + _createPage.toLowerCase())) {
        return true;
    }
    return false;
}

function isEditPage() {
    if (location.pathname.toLowerCase().match("^" + _editPage.toLowerCase())) {
        return true;
    }
    return false;
}

function addLoaderInTab(idTab) {
    alert('function addLoaderInTab(idTab)');
    //    $('#myTab a[href="#' + idTab + '"]').html('<img src="/img/ajax-loader-tab.gif">');
}

function restoreTextInTab1() {
    //alert('function restoreTextInTab1()');
    //$('#myTab a[href="#tab1"]').html(_listTabName);
}

function restoreTextInTab2() {
    //alert('function restoreTextInTab2()');
    //$('#myTab a[href="#tab2"]').html(_editTabName);
}

function successTab1(data) {
    try
    {
        Reload();
    }
    catch(e)
    {
        $('#tab1').html(data);
    }
    //showTab('tab1');
    //restoreTextInTab1();
}

function successTab1WithMessage(data, dataMessage) {
    $('#addedit').modal('hide');
    successTab1(data);

    if (dataMessage) {
        if (dataMessage.Succeeded)
            displaySuccess(dataMessage.Message);
        else
            displayAlert(dataMessage.Message)
    }
}

function successTab2(data) {
    if (data.match("<html>") === null) {
        $('#tab2').html(data);
    }
    else {
        location = "/Home/Login";
    }

    //restoreTextInTab2();
    //restoreTextInTab1();
}

//function showTab(idTab) {
//    $('#myTab a[href="#' + idTab + '"]').tab('show');
//}

function displayDataTab2(data) {
    if (data.match("<html>") === null) {
            $('#tab2').html(data);
    }
    else {
        location = "/Home/Login";
    }
}


function cancelPage() {
    //loadList(null);
    //loadCreate();
    //showTab('tab1');
}

//LIST
function loadList(response) {
    //addLoaderInTab('tab1');
    submitUrl(_listPage, '{}', successTab1);

    if (response && response.Succeeded === false) {
        $("#btnDeleteNotPossible").click();
        var text = $("#deleteNotPossible .error-text").text();
        $("#deleteNotPossible .error-text").text(text + response.Message);
    }

}

function loadListWithMessage(dataMessage) {

    //addLoaderInTab('tab1');
    submitUrl(_listPage, { }, function (data) { successTab1WithMessage(data, dataMessage) });
}

//CREATE
function loadCreate() {
    //addLoaderInTab('tab2');
    submitUrl(_createPage, '{}', successTab2);
}

function reloadCreate() {
    //addLoaderInTab('tab2');
    submitUrl(_createPage, '{}', onReloadCreateSuccess);
}

function onReloadCreateSuccess(data) {
    if (data.match("<html>") === null) {
        $('#tab2').html(data);
    }
    else {
        location = "/Home/Login";
    }
    //restoreTextInTab2();
    //showTab('tab2');
}


function callCreateWithCallBack(jqForm, jqFormSerialized, onCallBackSuccess) {
    //addLoaderInTab('tab1');
    CallSubmit(jqForm, jqFormSerialized, onCallBackSuccess, null);
}


function callCreate(jqForm, jqFormSerialized) {
    //addLoaderInTab('tab1');
    CallSubmit(jqForm, jqFormSerialized, onCallCreateSuccess2, fail);
}

function fail(data) {
    displayAlert(data, '#errorPlaceHolderPopup');
}

function onCallCreateSuccess2(data) {
    if (typeof data === 'object') {
        var json;
        try {
            json = jQuery.parseJSON(data);
            if (json === null && typeof data == 'object') {
                json = data;
            }
            //var json = data;
            if (json.Succeeded == false) {
                displayAlert(json.Message);
            }
            else {
                if (json.RedirectUrl !== "" && json.RedirectUrl !== undefined) {
                    //window.location = json.RedirectUrl;
                    location = json.RedirectUrl;
                }

                if (isCreatePage()) {
                    location = _indexPage;
                }
                else {
                    if (json.Message !== "" && json.Message !== undefined) {
                        //if the content of the tab needs to be reloaded
                        if (json.DontReloadTab == false) {
                            loadCreate();
                            //restoreTextInTab2();
                        }
                        loadListWithMessage(json);
                    }
                    else {
                        $('#tab2').html(json);
                    }
                    //fix for nested requests.
                    if (location == "") {
                        loadList();
                        //restoreTextInTab2();
                    }
                }
            }
        } catch (e) {
            $('#tab2').html(data);
            loadList();
            //restoreTextInTab2();
        }
    }
    else {
        location = "/Home/Login";
    }
}

//EDIT
function editItem(item) {
    //addLoaderInTab('tab2');
    submitUrl(_editPage + item, '{}', function (data) { displayDataTab2(data, true); });
    return false;
}

function callEdit(jqForm, jqFormSerialized) {
    //addLoaderInTab('tab2');
    CallSubmit(jqForm, jqFormSerialized, onCallEditSuccess, fail);
}

function onCallEditSuccess(data) {
    if (typeof data === 'object') {
        if (data.Succeeded == false) {
            displayAlert(data.Message);
        }
        else {
            if (isEditPage()) {
                location = _indexPage;
            }
            else {
                $('#tab2').html(data);

                if (data.Message !== "" && data.Message !== undefined) {
                    loadListWithMessage(data);
                }
                else {
                    loadList();
                }

                //restoreTextInTab2();
            }
        }
    }
    else {
        location = "/Home/Login";
    }
}

//DETAILS
function loadDetails(item) {
    submitUrl("/" + _entityPath + "/Details/" + item, '{}', function (data) { displayDataTab2(data, true); });
    return false;
}

//EXPORT
function bulkExport(datatableId) {
    document.forms["exportForm"].submit();
    return false;
}

//DELETE
function deleteItem(item) {
    _deleteId = item;
    return false;
}


function confirmDelete() {
    if (_deleteId != "") {
        submitUrl("/" + _entityPath + "/Delete/" + _deleteId, '{}', confirmDeleteCallback);
    }
    _deleteId = "";
    return false;
}

function confirmDeleteCallback(data) {
    
    $("#delete").modal('hide');
    if (data.Message !== "" && data.Message !== undefined) {
        loadListWithMessage(data);
    }
    else {
        loadList(data);
    }
}

function cancelDelete() {
    _deleteId = "";
    return false;
}

//Bulk DELETE
function selectAll(datatableId) {
    var selectAll = $('#selectAll').is(':checked');

    $('#' + datatableId).find('[name="selectedItems"]').attr('checked', selectAll);

    return false;
}


function bulkDelete(datatableId) {
    var oTable = $('#' + datatableId).dataTable();
    _deleteItems = '&' + $('input:checkbox', oTable.fnGetNodes()).serialize();
    //    _deleteItems = utils.qsToObject(_deleteItems);

    // get all items to be deleted to display more info on the popup
    var array = new Array();
    $('[name="selectedItems"]:checked', oTable.fnGetNodes()).each(function (index, control) { array.push(control.attributes['data'].value) });
    if (array.length === 0)
        return false;
    $('#bulkDeleteMoreInfo').html(array.join('<br/>'));

    return false;
}

function confirmBulkDelete() {
    if (_deleteItems) {
        submitUrl("/" + _entityPath + "/BulkDelete/", _deleteItems, confirmDeleteCallback);
    }
    _deleteItems = null;
    return false;
}

function selectAllAjax(datatableId) {
    var selectAll = $('#selectAll').is(':checked');
    $('#' + datatableId).find('[name="selectedItems"]').not(':disabled').each(function () {
        $(this).attr('checked', selectAll);
        var iId = $(this).val();
        var is_in_array = $.inArray(iId, selected);
        if (is_in_array == -1) {
            selected[selected.length] = iId;
        }
        else if (selectAll == false) {
            selected = $.grep(selected, function (value) {
                return value != iId;
            });
        }
    });

    toogleMenuButtons(datatableId);

    return false;
}

function bulkDeleteAjax(datatableId) {
    var oTable = $('#' + datatableId).dataTable();
    _deleteItems = '&selectedItems=' + selected.join('&selectedItems=');

    return false;
}


function bulkDeleteAll(datatableId) {
    var oTable = $('#' + datatableId).dataTable();
    _deleteAllItems = '&' + $('input:checkbox', oTable.fnGetNodes()).serialize();
    //    _deleteItems = utils.qsToObject(_deleteItems);

    return false;
}

function confirmBulkDeleteAll() {
    if (_deleteAllItems)
        submitUrl("/" + _entityPath + "/BulkDeleteAll/", _deleteAllItems, loadList);

    _deleteAllItems = null;
    return false;
}

function bulkMove(datatableId) {
    var oTable = $('#' + datatableId).dataTable();
    _moveItems = '&' + $('input:checkbox', oTable.fnGetNodes()).serialize();

    //    _deleteItems = utils.qsToObject(_deleteItems);

    return false;
}

function confirmBulkMove() {
    if (_moveItems) {
        _moveItems += "&salesForceToMove=" + $("#salesForceToMove").val();
        submitUrl("/" + _entityPath + "/BulkMove/", _moveItems, loadList);
    }
    _moveItems = null;
    return false;
}

function displayDataTab(data, idTab) {
    if (data.match("<html>") === null) {
        var tab = $('#' + idTab);
        if (tab.length > 0) {
            tab.html(data);
        }
        else {
            $('#tab2').html(data);
        }
    }
    else {
        location = "/Home/Login";
    }
}

function loadMiscPage(relativeUrl, idTab) {
    submitUrl(relativeUrl, '{}', function (data) { displayDataTab(data, idTab) });
}

function isInArray(array, object) {
    for (var i = 0; i < array.length; i++) {
        if (array[i].id === object.id)
            return true;
    }
    return false;
}