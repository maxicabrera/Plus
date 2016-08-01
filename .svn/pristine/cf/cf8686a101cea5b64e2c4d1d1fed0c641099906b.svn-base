var arraySelectedGames = new Array();
var gameTable;
var currentUrl;

function loadSelectGameControl(idTable, action, columns, currentUrl, oTable) {
    currentUrl = action;

    if (!oTable) {
        oTable = $('#' + idTable).dataTable({
            'bProcessing': true,
            'bServerSide': true,
            'bJQueryUI': true,
            "bAutoWidth": false,
            'iDisplayLength': 10,
            'iDisplayStart': 0,
            'sAjaxSource': currentUrl,
            "aLengthMenu": [[10, 50, 100], [10, 50, 100]],
            'sPaginationType': 'full_numbers',
            'aoColumns': columns,
            'oLanguage': {
                'sUrl': '/Home/GetGridTranslations'
            },
            'fnServerData': function (sSource, aoData, fnCallback) {
                $.ajax({
                    "dataType": 'json',
                    "type": "POST",
                    "url": currentUrl,
                    "data": aoData,
                    "success": function (json) {
                        fnCallback(json);
                        toogleGames(idTable);
                    }
                })
            },
            'fnInitComplete': function (oSettings, json) {
                $('a.fg-button').click(function () {
                    $('#selectAllGames').removeAttr('checked');
                });

                $('.dataTables_filter input').unbind('keypress keyup').bind('keypress keyup', function (e) {
                    if ($(this).val().length < 3 && e.keyCode != 13) {
                        if (e.keyCode === 8 && $(this).val().length === 0)
                            oTable.fnFilter($(this).val());
                        return;
                    }
                    oTable.fnFilter($(this).val());
                });

                $('#selectAllGames').click(function () {
                    selectAllGames();
                });

            },
            "fnRowCallback": function (nRow, aData, iDisplayIndex) {
                $('#' + idTable + ' tbody tr').each(function () {
                    var object = { name: $(aData[0]).data('name'), id: $(aData[0]).val() }
                    if (isInArray(arraySelectedGames, object)) {
                        $('td:eq(0)', nRow).html(aData[0].replace('[0]', "checked='checked'"));
                    }
                });
                return nRow;
            },
            "fnDrawCallback": function (oSettings) {
                var id = '#' + idTable + ' tbody tr';
                $(id).each(function () {
                    var iPos = oTable.fnGetPosition(this);
                    if (iPos != null) {
                        var aData = oTable.fnGetData(iPos);
                        var object = { name: $(aData[0]).data('name'), id: $(aData[0]).val() }
                        if (isInArray(arraySelectedGames, object)) {
                            aData[0] = aData[0].replace("[0]", "checked='checked'");
                        }
                    }
                    $(this).click(function () {
                        var iPos = oTable.fnGetPosition(this);
                        var aData = oTable.fnGetData(iPos);
                        var iId = { name: $(aData[0]).data('name'), id: $(aData[0]).val() }
                        is_in_array = isInArray(arraySelectedGames, iId);

                        if (!is_in_array) {
                            arraySelectedGames[arraySelectedGames.length] = iId;
                        }
                        else {
                            arraySelectedGames = $.grep(arraySelectedGames, function (value) {
                                return value.id !== iId.id;
                            });
                        }
                    });
                });
            }
        });
        //.fnSetFilteringDelay(400); //adding delay in keyup before filtering
    }
    else {
        oTable.fnDraw();
    }
}


function addGameItems() {

    $('#selectedGames').html('');

    for (var i = 0; i < arraySelectedGames.length; i++) {
        $('#selectedGames').append("<div class='row-fluid show-grid'><button type='button' class='close' aria-hidden='true'>×</button>"
            + "<p>" + arraySelectedGames[i].name + "</p><input type='hidden' name='idGames' value='" + arraySelectedGames[i].id + "'/></div>");
    }

    setButtonCloseClick();
}

function setButtonCloseClick() {
    $('.close').on('click', function () {
        $(this).parent().remove();
    });
}

function selectAllGames() {

    var selectAll = $('#selectAllGames').is(':checked');
    $('#tblGames').find('[name="selectedItems"]').not(':disabled').each(function () {
        $(this).attr('checked', selectAll);

        var iId = { id: $(this).val(), name: $(this).data('name') };

        var is_in_array = isInArray(arraySelectedGames, iId);

        if (!is_in_array) {
            arraySelectedGames[arraySelectedGames.length] = iId;
        }
        else if (selectAll == false) {
            arraySelectedGames = $.grep(arraySelectedGames, function (value) {
                return value.id !== iId.id;
            });
        }
    });

    toogleGames("tblGames");

    return false;
}


function toogleGames(idDataTable) {
    var $this = $(this);
    var array = new Array();
    $('#' + idDataTable).find('[name="selectedItems"]:checked').each(function (index, control) {
        var object = { name: $(control).data('name'), id: $(control).val() }
        array.push(object);
    });
}