$(document).ready(function () {
    var $si = $('select[name=Station]');
    var $fi = $('select[id=slFeeder]');
    //jsReport.LoadFeeder($si);
});

var jsReport = {
    //Property
    AjaxUrl: '/Report/Ajax?ts=' + new Date().getTime().toString(),
    Station: 0,
    Feeder: 0,
    ArraySpecSelected: '',
    //Method

    LoadFeeder: function (obj) {
        var station = $(obj).val();
        this.Station = station;
        $.post(jsReport.AjaxUrl, { fn: 'loadfeeder', id: station }, function (data) {
            $('#slFeeder').html(data.result);
        });
    },

    LoadSpec: function (obj) {
        var station = this.Station;
        var feeder = $(obj).val();
        this.Feeder = feeder;
        $.post(jsReport.AjaxUrl, { fn: 'loadspec', si: station, fi: feeder }, function (data) {
            $('#slSpec').html(data.result);
        });
    },

    AddSpectSelection: function (obj) {
        var spi = $(obj).val();
        if (this.ArraySpecSelected.indexOf(spi + ';') != -1)
            return;
        this.ArraySpecSelected += spi + ';';
        var dom = '<li class="select2-selection__choice" data-id=' + spi + ' title="{0}">' +
            '<span class="select2-selection__choice__remove" onclick=\'jsReport.RemoveSpec(this)\' role="presentation">×</span>{0}</li>';
        $.post(this.AjaxUrl, { fn: 'loadname', si: this.Station, fi: this.Feeder, spi: spi }, function (data) {
            $('#divSelection').append(String.format(dom, data.result));
        })
    },

    RemoveSpec: function (obj) {
        var spi = $(obj).parent().attr('data-id');
        //alert(this.ArraySpecSelected);
        $(obj).parent().remove();
        this.ArraySpecSelected = this.ArraySpecSelected.replace(spi + ';', '');
        //alert(this.ArraySpecSelected);
    },
    EnableEdit: function () {
        $('a.editable').editable({
            success: function (response, newValue) {
                if (response.status == 'error') return response.message; //msg will be shown in editable form
            }
        });
    },
    SaveReportConfig: function () {
        if (jsReport.ArraySpecSelected == '') {
            alert('Vui lòng chọn các thông số báo cáo');
            return;
        }
        var nameConfig = $('#txtNameConfig').val();
        if (nameConfig == '') {
            alert('Vui lòng nhập tên cấu hình thông số báo cáo');
            return;
        }
        $.post(jsReport.AjaxUrl, {
            fn: 'saveconfig',
            na: nameConfig,
            sp: jsReport.ArraySpecSelected,
        }, function (data) {
            $('#divMessage').html(data.result);
        });
    },

    CreateReport: function () {
        var dataSpec = this.ArraySpecSelected;
        var time = $('input[name=reporttime]').val();
        if (dataSpec == '') {
            alert('Vui lòng chọn các thông số báo cáo');
            return;
        }

        if (time == '') {
            alert('Vui lòng chọn thời gian báo cáo');
            return;
        }

        var isParallel = $('input[name=pr]:checked').val();

        $('.loading').show();
        $.post(jsReport.AjaxUrl, {
            fn: 'createreport',
            dt: dataSpec,
            pr: isParallel,
            tm: time,
        }, function (data) {              
            $('#tbReport').html(data.result);
            $('.time').html(data.time);
            $('.loading').hide();
        });
    },

    CreatePeakReport: function () {
        var time = $('input[name=reporttime]').val();  
        if (time == '') {
            alert('Vui lòng chọn thời gian báo cáo');
            return;
        }
        var isParallel = $('input[name=pr]:checked').val();

        $('.loading').show();
        $.post(jsReport.AjaxUrl, {
            fn: 'createpeakreport',
            pr: isParallel,
            tm: time,
        }, function (data) {
            $('#tbReport').html(data.result);
            $('.time').html(data.time);
            $('.loading').hide();
        });
    },
    CreatePeakPReport: function () {
        var time = $('input[name=reporttime]').val();
        if (time == '') {
            alert('Vui lòng chọn thời gian báo cáo');
            return;
        }
        var isParallel = $('input[name=pr]:checked').val();

        $('.loading').show();
        $.post(jsReport.AjaxUrl, {
            fn: 'createpeakpreport',
            pr: isParallel,
            tm: time,
        }, function (data) {
            $('#tbReport').html(data.result);
            $('.time').html(data.time);
            $('.loading').hide();
        });
    },

    LoadConfig: function (obj) {
        $.post(jsReport.AjaxUrl, {
            fn: 'loadconfig',
            id: $(obj).val(),
        }, function (data) {
            $('#divSelection').html(data.result);
            jsReport.ArraySpecSelected = data.dataSpec;
        });

    },
}
String.format = function () {
    // The string containing the format items (e.g. "{0}")
    // will and always has to be the first argument.
    var theString = arguments[0];

    // start with the second argument (i = 1)
    for (var i = 1; i < arguments.length; i++) {
        // "gm" = RegEx options for Global search (more than one instance)
        // and for Multiline search
        var regEx = new RegExp("\\{" + (i - 1) + "\\}", "gm");
        theString = theString.replace(regEx, arguments[i]);
    }

    return theString;
}