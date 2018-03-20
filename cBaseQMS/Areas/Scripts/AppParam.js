$(function () {
    $("#tblAppParamGrid").jqGrid({
        url: "/ApplicationSetting/AppParameterData/?status=true",
        datatype: 'json',
        mtype: 'post',
        colNames: ['', 'AppParamID', 'Param Name', 'Param Value'],
        colModel: [
            { key: false, name: 'act', index: 'act', width: 10, sortable: false, hidden: false, editable: false },
            { key: true, name: 'AppParamID', index: 'AppParamID', width: 10, editable: false, hidden: true },
            { name: 'ParamName', index: 'ParamName', width: 55, align: "left", editable: false, editoptions: { maxlength: 30 } },
            { name: 'ParamValue', index: 'ParamValue', width: 85, align: "left" },

        ],
        pager: jQuery('#parampcinput'),
        rowNum: 15,
        loadonce: true,
        rowList: [15, 30, 45, 60, 75],
        height: '100%',
        width: '350',
        viewrecords: true,
        sortname: 'CreatedOn',
        sortorder: 'desc',
        caption: '',
        ignoreCase: true,
        emptyrecords: 'No user records are available to display',
        jsonReader: {
            root: "rows",
            page: "page",
            total: "total",
            records: "records",
            repeatitems: false,
            Id: "0"
        },
        gridComplete: function () {
            var ids = jQuery("#paramInput").jqGrid('getDataIDs');
            for (var i = 0; i < ids.length; i++) {
                var cl = ids[i];
                var appParamId = jQuery('#paramInput').getCell(cl, 'AppParamID');
                be = "<img onclick=\"SelectParameter('" + appParamId + "');\"  src='/Images/eip_edit.png' style='height:23px; cursor:pointer;'>";
                jQuery("#paramInput").jqGrid('setRowData', ids[i], { act: be });
            }
        },
        loadComplete: function () {
            $("#paramInput tr.jqgrow:odd").css("background", "#DBEEFC");
            $("#paramInput tr.jqgrow:even").css("background", "#fff");
        },
        autowidth: true,
        multiselect: false,
        altrows: true
    }).navGrid('#parampcinput', {
        edit: false, add: false, del: false, search: false
    });
});

$("#btnAppParamAdd").click(function () {
    var appParamModel = {
        ParamName: $("#appName").val(),
        ParamValue: $("#appValue").val()
    }

    $.ajax({

        url: "/ApplicationSetting/AddAppParam",
        contentType: "application/json",
        data: JSON.stringify({ appParamModel: appParamModel }),
        type: "POST",

        success: function (response) {
            if (response == "Success") {
                window.location.href = "/cBaseQMS/ApplicationSetting/index";
            }
        },
        failure: function (response) { },
        error: function (response) { }

    });
});