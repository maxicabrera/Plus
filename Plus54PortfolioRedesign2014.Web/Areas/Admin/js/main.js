/* SEARCH */
function displayAlert(message, idPlaceHolder) {
    if (!idPlaceHolder) {
        idPlaceHolder = "#errorPlaceHolder";
    }

    var errorPlaceHolder = $(idPlaceHolder);
    if (errorPlaceHolder) {
        errorPlaceHolder.each(function () {
            $(this).html('<div class="alert alert-error"><a class="close" data-dismiss="alert" href="#">x</a>' + message + '</div>');
            $(this).show();
        });
    }
    else {
        alert('PLEASE ADD ERROR PLACEHOLDER TO AVOID THIS JS ALERT \n error: ' + message);
    }
}

function displaySuccess(message, idPlaceHolder) {
    if (!idPlaceHolder) {
        idPlaceHolder = "#messagePlaceHolder";
    }
    var messagePlaceHolder = $(idPlaceHolder);
    if (messagePlaceHolder) {
        messagePlaceHolder.each(function () {
            $(this).html('<div class="alert alert-success"><a class="close" data-dismiss="alert" href="#">x</a>' + message + '</div>');
            $(this).show();
        });
    }
    else {
        alert('PLEASE ADD MESSAGE PLACEHOLDER TO AVOID THIS JS ALERT \n error: ' + message);
    }
}

/**** LOGOUT ****/
$('#aLogout').click(function (e) {
    e.preventDefault();
    logout();
})

function logout() {
    submitUrl("/home/Logout", {}, onLogoutSucceeded, onSubmitFailed);
}

function onLogoutSucceeded(response) {
    if (response.Succeeded) {
        if (response.RedirectUrl) {
            location = response.RedirectUrl;
        }
    }
    else {
        displayAlert(response.Message);
    }
}
/**** LOGOUT ****/


/**** DASHBOARD ****/

function getPieChart(divId, data, title, dataLabels) {
    var plot = jQuery.jqplot(divId, [data], {
        title: { text: title, show: true },
        seriesDefaults: {
            renderer: jQuery.jqplot.PieRenderer,
            rendererOptions: { showDataLabels: true, dataLabels: dataLabels }
        },
        series: { pointLabels: { escapeHTML: true} },
        legend: { show: true, location: 'e' },
        grid: { drawGridLines: false, gridLineColor: '#000', background: '#F2FCFB', borderWidth: 0, shadow: false }
    });
}

function getBarChart(divId, data, title) {
    var plot1 = $.jqplot(divId, [data], {
        title: title,
        series: [{ renderer: $.jqplot.BarRenderer}],
        axesDefaults: {
            tickRenderer: $.jqplot.CanvasAxisTickRenderer,
            tickOptions: { angle: 0, fontSize: '10pt', showGridline: false }
        },
        axes: { xaxis: { renderer: $.jqplot.CategoryAxisRenderer} },
        grid: { drawGridLines: false, gridLineColor: '#000', background: '#F2FCFB', borderWidth: 0, shadow: false }
    });
}

function getReports(idBrandValue) {
    submitUrlJson("/home/GetReports", { idBrand: idBrandValue }, onGetReportSuccess, onGetReportFail);
}

function onGetReportSuccess(response) {
    var data1 = [["<a href='/alerts' class='chart-label'>Alerts</a>", response.AlertsQty],
                           ["<a href='/newsletters' class='chart-label'>Newsletters</a>", response.NewslettersQty],
                           ["<a href='/spotlights' class='chart-label'>Spotlights</a>", response.SpotlightsQty]];

    var data2 = [["<a href='/alerts' class='chart-label'>Un-opened Emails</a>", response.AlertsEmailsQty], ["<a href='/alerts' class='chart-label'>Open Rate</a>", response.AlertsOpenRate]];

    var data3 = [["<a href='/spotlights' class='chart-label'>Un-opened Emails</a>", response.SpotlightsEmailsQty], ["<a href='/spotlights' class='chart-label'>Open Rate</a>", response.SpotlightsOpenRate]];

    var data4 = [["<a href='/newsletters' class='chart-label'>Un-opened Emails</a>", response.NewslettersEmailsQty], ["<a href='/newsletters' class='chart-label'>Open Rate</a>", response.NewslettersOpenRate]];

    var data5 = [[response.EmailsQty + ' Emails', response.EmailsQty]];

    var data6 = [[response.SubscribertsQty + ' Subscribers', response.SubscribertsQty]];

    if (response.AlertsQty !== 0 || response.NewslettersQty !== 0 || response.SpotlightsQty !== 0) {
        getPieChart("chart1", data1, "Number of Alerts, Newsletters and Spotlights", "value");
    }

    if (response.AlertsEmailsQty !== 0 || response.AlertsOpenRate !== 0 )
        getPieChart("chart2", data2, "Alerts Open Rate", "percent");

    if (response.SpotlightsEmailsQty !== 0 || response.SpotlightsOpenRate !== 0)
        getPieChart("chart3", data3, "Spotlights Open Rate", "percent");

    if (response.NewslettersEmailsQty !== 0 || response.NewslettersOpenRate !== 0)
        getPieChart("chart4", data4, "Newsletter Open Rate", "percent");

    if(response.EmailsQty !== 0)
        getBarChart("chart5", data5, "Emails");

    if (response.SubscribertsQty !== 0)
        getBarChart("chart6", data6, "Subscribers");
}

function onGetReportFail(errorMessage) {
    //alert(errorMessage);
}

/**** CHOSEN ****/
function setChosens() {
    $(".chzn-select").each(function () {
        $(this).chosen({ width: "251px" });
    });
}

/**** CUSTOM FILTERS ****/
function setCustomFilterOnChange() {

    $(".custom-filter").change(function () {
        Reload();
    });

}

$(document).ready(function () {

	setChosens();
	setCustomFilterOnChange();

    $("#spinner").bind("ajaxSend", function () {
        $(this).width($(window).width() + $(window).width());
        $(this).height($(window).height() * 3);
        $(this).show();
    }).bind("ajaxStop", function () {
        $(this).hide();
    }).bind("ajaxError", function () {
        $(this).hide();
    });

    $('.nav li a:eq(0)').addClass('home');
    $('.nav li a:eq(1)').addClass('alerts');
    $('.nav li a:eq(2)').addClass('spotlights');
    $('.nav li a:eq(3)').addClass('newsletters');



    $('.ui-state-default.sortable-none .custom-filter').first().addClass('large');

    var url = document.location.pathname.replace(/\/+$/, "") + '/'
        url = url.toLowerCase();
    
    var urlSection = url.split('/')[1];
    //console.log(urlSection);

    //$(".nav a." + urlSection ).addClass('active');
    $('.nav a[href="/' + urlSection + '/"]').addClass('active');

    if (url == '/') {
        $('.nav a.home').addClass('active');
    } 
     else { }


});