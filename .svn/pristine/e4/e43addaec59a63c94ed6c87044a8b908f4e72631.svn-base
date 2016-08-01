var _entityPath = "";
var _entityName = "";
var _editPage = "";
var _createPage = "";
var _listPage = "";
var _indexPage = "";

var _listTabName = "";
var _editTabName = "";

var _deleteId = "";
var _sendTabName = "";
var _previewTabName = "";

var _deleteId = "";
var _deleteItems = "";
var _exportItems = "";
var _deleteAllItems = "";
var _moveItems = "";

function initTabs(entityPath, entityName) {
    initPages(entityPath, entityName);
    loadCreate();
}

function initPages(entityPath, entityName, list, addEdit) {
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
    $('#myTab a[href="#' + idTab + '"]').html('<img src="/img/ajax-loader-tab.gif">');
}

function restoreTextInTab1() {
    $('#myTab a[href="#tab1"]').html(_listTabName);
}

function restoreTextInTab2() {
    $('#myTab a[href="#tab2"]').html(_editTabName);
}

function successTab1(data) {
    showTab('tab1');
    restoreTextInTab1();
    try {
        Reload();
    }
    catch (e) {
        $('#tab1').html(data);
    }
}

function successTab1WithMessage(data, dataMessage) {
    successTab1(data);

    if (dataMessage) {
        if (dataMessage.Succeeded)
            displaySuccess(dataMessage.Message);
        else
            displayAlert(dataMessage.Message)
    }
}

function successTab2(data) {
    $('#tab2').html(data);
    restoreTextInTab2();
    restoreTextInTab1();
}

function showTab(idTab) {
    $('#myTab a[href="#' + idTab + '"]').tab('show');
}

function displayDataTab2(data) {
    $('#tab2').html(data);
    showTab('tab2');
    restoreTextInTab2();
}

function cancelPage() {
    loadCreate();
    showTab('tab1');
}

//LIST
function loadList(response) {
    addLoaderInTab('tab1');
    submitUrl(_listPage, '{}', successTab1);

    if (response && response.Succeeded === false) {
        $("#btnDeleteNotPossible").click();
        var text = $("#deleteNotPossible .error-text").text();
        $("#deleteNotPossible .error-text").text(text + response.Message);
    }

}

function loadListWithMessage(dataMessage) {
    addLoaderInTab('tab1');
    submitUrl(_listPage, '{}', function (data) { successTab1WithMessage(data, dataMessage) });
}

//CREATE
function loadCreate() {
    addLoaderInTab('tab2');
    submitUrl(_createPage, '{}', successTab2);
}

function reloadCreate() {
    addLoaderInTab('tab2');
    submitUrl(_createPage, '{}', onReloadCreateSuccess);
}

function onReloadCreateSuccess(data) {
    $('#tab2').html(data);
    restoreTextInTab2();
    showTab('tab2');
}


function callCreateWithCallBack(jqForm, jqFormSerialized, onCallBackSuccess, onCallBackFail) {
    addLoaderInTab('tab1');
    if (onCallBackSuccess == null) {
        onCallBackSuccess = onCallCreateSuccess;
    }
    CallSubmit(jqForm, jqFormSerialized, onCallBackSuccess, onCallBackFail);
}


function callCreate(jqForm, jqFormSerialized) {
    addLoaderInTab('tab1');
    CallSubmit(jqForm, jqFormSerialized, onCallCreateSuccess, null);
}

function onCallCreateSuccess(data) {
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
        }
    } catch (e) {
        $('#tab2').html(data);
        loadList();
        restoreTextInTab2();
    }
}

//EDIT
function editItem(item) {
    addLoaderInTab('tab2');
    submitUrl(_editPage + item, '{}', function (data) { displayDataTab2(data, true); });
    return false;
}

function callEdit(jqForm, jqFormSerialized) {
    addLoaderInTab('tab2');
    CallSubmit(jqForm, jqFormSerialized, onCallEditSuccess, null);
}

function onCallEditSuccess(data) {
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

            restoreTextInTab2();
        }
    }
}

//DETAILS
function loadDetails(item) {
    submitUrl("/" + _entityPath + "/Details/" + item, '{}', function (data) { displayDataTab2(data, true); });
    return false;
}

//EXPORT
function exportItems(datatableId) {
    var oTable = $('#' + datatableId).dataTable();
    //$("#selectedExportItems").val( '&' + $('input:checkbox', oTable.fnGetNodes()).serialize());
    //$("#exportForm").html($('input:checkbox:checked', $('#' + 'tblTerritories').dataTable().fnGetNodes()).hide())
    document.forms["exportForm"].submit();
}

//DELETE
function deleteItem(item) {
    _deleteId = item;
    return false;
}


function confirmDelete() {
    if (_deleteId != "")
        submitUrl("/" + _entityPath + "/Delete/" + _deleteId, '{}', confirmDeleteCallback);

    _deleteId = "";
    return false;
}

function confirmDeleteCallback(data) {
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
                //TODO Check this method
            else {
                loadList();
            }
        }
    } catch (e) {
        $('#tab2').html(data);
        
        restoreTextInTab2();
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


function bulkDelete(datatableId, entityPath) {
    var oTable = $('#' + datatableId).dataTable();
    _deleteItems = '&' + $('input:checkbox', oTable.fnGetNodes()).serialize();
    _entityPath = entityPath;
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
    var filtersValues = getFiltersValues();
    if (_deleteItems) {
        _deleteItems += "&filtros=" + filtersValues;
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

function getFiltersValues() {
    var filtersValues = "";
    $(".filter select").each(function () {
        if ($(this).val() != "")
            filtersValues += $(this).find('option').first().text() + '=' + $(this).find('option:selected').first().text() + '|';
    });
    $("select.custom-filter").each(function () {
        if ($(this).val() != "")
            filtersValues += $(this).find('option').first().text() + '=' + $(this).find('option:selected').first().text() + '|';
    });
    $(".dataTables_filter label").each(function () {
        if ($(this).find("input").val() != "")
            filtersValues += $(this).attr('class') + '=' + $(this).find('input').val() + '|';
    });
    if (filtersValues != "") {
        filtersValues = filtersValues.substring(0, filtersValues.length - 1);
    }
    return filtersValues;
}

function duplicateItem(datatableId, entityPath) {
    var filtersValues = getFiltersValues();
    var oTable = $('#' + datatableId).dataTable();
    var _duplicateItem = $('input:checkbox', oTable.fnGetNodes()).serialize();
    window.location = "/" + entityPath + "/Duplicate/?" + _duplicateItem + "&filters=" + filtersValues;
}

function confirmBulkMove() {
    if (_moveItems) {
        _moveItems += "&salesForceToMove=" + $("#salesForceToMove").val();
        submitUrl("/" + _entityPath + "/BulkMove/", _moveItems, loadList);
    }
    _moveItems = null;
    return false;
}


function bulkExport(datatableId) {
    document.forms["exportForm"].submit();
    return false;
}

function hideUnusedTabs(status) {
    if (status) {
        var tabs = $("#myTab li a");
        $(tabs[2]).hide();
        $(tabs[1]).hide();
    }
}

function isInArray(array, object) {
    for (var i = 0; i < array.length; i++) {
        if (array[i].id === object.id)
            return true;
    }
    return false;
}

function loadMiscPage(relativeUrl, idTab) {
    submitUrl(relativeUrl, '{}', function (data) { displayDataTab(data, idTab) });
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
        location = "/Admin/Home/Login";
    }
}
