
var settings;
var self;
(function ($) {
    $.fn.createGrid = function (options) {
        var defaults = {
            Url: '/Home/GetGridData',
            DataType: 'POST',
            Columns: [],
            Mode: '',
            FixClause: '',
            SortColumn: '0',
            SortOrder: 'asc',
            ExportIcon: true,
            ColumnSelection: true,
            UserRole: '',
        };
        settings = $.extend({}, defaults, options);
        if (settings.UserRole == "1" && settings.Mode=="RAWS") {
            settings.Columns=jQuery.grep(settings.Columns, function (value) {
                return value.title != "Change State";
            });
        }
        self = this;
        $(this).DataTable({

            "processing": true,
            "oLanguage": {
                "sProcessing": ""
            },
            "serverSide": true,
            "ajax": {
                "type": settings.DataType,
                "url": settings.Url,
                "data": { 'mode': settings.Mode, 'FixClause': settings.FixClause },
                'dataType': 'json'
            },
            "columns": settings.Columns,
            "pagingType": "simple_numbers",
            "order": [[settings.SortColumn, '' + settings.SortOrder + '']],
            dom: 'l<"toolbar">frtip',
            initComplete: function () {
                var table = $('#' + $(this).attr("id") + '').DataTable();
                var s = '';

                if (settings.ExportIcon) {
                    s += "<img src=\"/assets/images/Icons/excel.png\" onclick =\"Export(1,'" + $(this).attr("id") + "')\"  data-tooltip=\"true\" title=\"Export to Excel\" class=\"export-icon excel\" />" +
                        "<img src =\"/assets/images/Icons/pdf.png\" onclick =\"Export(2,'" + $(this).attr("id") + "')\" data-tooltip=\"true\" title=\"Export to Pdf\" class=\"export-icon pdf\" />" +
                        "<img src =\"/assets/images/Icons/csv.png\" onclick=\"Export(3,'" + $(this).attr("id") + "')\" data-tooltip=\"true\" title=\"Export to Csv\" class=\"export-icon csv\" />" +
                        "<img src=\"/assets/images/Icons/Word.png\" onclick=\"Export(4,'" + $(this).attr("id") + "')\"  data-tooltip=\"true\" title=\"Export to Word\" class=\"export-icon word\" />" +
                        "<iframe id=\"exportFram\" name=\"exportFram\" width=\"0\" height=\"0\"  style=\"visibility: hidden;\"></iframe>";
                }
                if (settings.ColumnSelection) {
                    s += '<div class="dropdown" style="display: inline-block;" > <button id="dLabel1" type="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="true" class="btn btn-sm xs-mr-5 p-0" data-uib-tooltip="Show/Hide Columns"><img width="22" src=\"/assets/images/Icons/columns.png\"></img></button><ul style="max-height:400%;overflow-y:auto;z-index:999;position: absolute;" class="dropdown-menu" aria-labelledby="dLabel1">';
                    for (var i = 0; i < table.columns().count(); i++) {
                        var c = table.column(i).visible() == false ? "" : "checked";
                        if (!($(table.column(i).header()).text().match("Id"))) {
                            s += "<li><label style=\"padding: 5px;width:100%;\"><input class='mr-2' type=\"checkbox\" " + c + " onchange=\"ShowHideColumn(this,'" + $(this).attr("id") + "');\" coldata='" + $(table.column(i).header()).text() + "' />" + $(table.column(i).header()).text() + "</label></li>";
                        }
                    }
                    s += "</ul></div>";
                }
                $("#" + $(this).attr("id") + "").parent().find("div.toolbar").html(s);
                $('[data-tooltip="true"]').tooltip({
                    container: 'body',
                    placement: 'bottom'
                });
            }
        });
        $('div.dataTables_filter input').addClass('form-control input-sm input-small input-inline');
        $('div.dataTables_length select').addClass('form-control input-sm input-xsmall input-inline');
    }
}(jQuery));

function HideLoading() {
    $(".dataTables_processing").hide();
}

function ShowHideColumn(obj, tbl) {
    var table = $('#' + tbl.trim() + '').DataTable();
    for (var i = 0; i < table.columns().count(); i++) {
        if ($(table.column(i).header()).text() == $(obj).attr("coldata")) {
            //table.column(i).visible = obj.checked;
            table.column(i).visible(obj.checked)
        }
    }
}



function Export(obj, tbl) {
    var table = $('#' + tbl.trim() + '').DataTable();
    //$(".dataTables_processing").show();
    $("#customloader").show();
    var type = 'excel';
    if (obj === 2)
        type = 'pdf'
    else if (obj === 3)
        type = 'csv';
    else if (obj === 4)
        type = 'word';
    var winName = 'MyWindow';
    var winURL = '/ExportData.aspx';
    var windowoption = 'resizable=yes,height=600,width=800,location=0,menubar=0,scrollbars=1';
    //  var params = { 'param1': '1', 'param2': '2' };
    var params1 = jQuery('#' + tbl + '').DataTable().ajax.params();
    var params = {
        'search[value]': params1.search.value,
        'order[0][column]': params1.order[0].column,
        'order[0][dir]': params1.order[0].dir,
        'start': "-2",
        'mode': params1.mode,
        'FixClause': params1.FixClause,
        'type': type,
        'columns': params1.columns
    };
    var form = document.createElement("form");
    form.setAttribute("method", "post");
    form.setAttribute("action", winURL);
    form.setAttribute("target", winName);
    for (var i in params) {
        if (params.hasOwnProperty(i)) {
            var input;
            if (i === "columns") {
                //var list = params[i];
                //for (i = 0; i < list.length; i++) {
                //    input = document.createElement('input');
                //    input.type = 'hidden';
                //    input.name = 'columns[' + i + '][data]';
                //    input.value = list[i].data;
                //    form.appendChild(input);
                //}
                input = document.createElement('input');
                input.type = 'hidden';
                input.name = 'Columns';
                var cols = "";
                for (c = 0; c < table.columns().count(); c++) {
                    if (table.column(c).visible() != false)
                        cols += cols == "" ? table.column(c).dataSrc() : "," + table.column(c).dataSrc();
                }
                input.value = cols;
                form.appendChild(input);
            }
            else {
                input = document.createElement('input');
                input.type = 'hidden';
                input.name = i;
                input.value = params[i];
                form.appendChild(input);
            }
        }
    }
    document.body.appendChild(form);
    // window.open('', winName, windowoption);
    form.target = "exportFram";//winName;
    form.submit();
    document.body.removeChild(form);
    $("#customloader").hide();

}

function ConvertDateddmmyyyy(data) {
    if (data == null) return '1/1/1950';
    var r = /\/Date\(([0-9]+)\)\//gi
    var matches = data.match(r);
    if (matches == null) return '1/1/1950';
    var result = matches.toString().substring(6, 19);
    var epochMilliseconds = result.replace(
        /^\/Date\(([0-9]+)([+-][0-9]{4})?\)\/$/,
        '$1');
    var b = new Date(parseInt(epochMilliseconds));
    var c = new Date(b.toString());
    var curr_date = c.getDate();
    //const monthNames = ["Jan", "Feb", "Mar", "Apr", "May", "Jun",
    //    "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"
    //];
    var curr_month = c.getMonth() + 1;
    var curr_year = c.getFullYear();
    var curr_h = c.getHours();
    var curr_m = c.getMinutes();
    var curr_s = c.getSeconds();
    var curr_offset = c.getTimezoneOffset() / 60
    var d = curr_date + '/' + curr_month.toString() + '/' + curr_year + ' ' + curr_h + ':' + curr_m + ' ' 
    return d;
}

function ConvertDateMMddyyyy(data) {
    if (data == null) return '1/1/1950';
    var r = /\/Date\(([0-9]+)\)\//gi
    var matches = data.match(r);
    if (matches == null) return '1/1/1950';
    var result = matches.toString().substring(6, 19);
    var epochMilliseconds = result.replace(
        /^\/Date\(([0-9]+)([+-][0-9]{4})?\)\/$/,
        '$1');
    var b = new Date(parseInt(epochMilliseconds));
    var c = new Date(b.toString());
    var curr_date = c.getDate();
    var curr_month = c.getMonth() + 1;
    var curr_year = c.getFullYear();
    var curr_h = c.getHours();
    var curr_m = c.getMinutes();
    var curr_s = c.getSeconds();
    var curr_offset = c.getTimezoneOffset() / 60
    var d = curr_month.toString() + '/' + curr_date + '/' + curr_year;
    return d;
}
