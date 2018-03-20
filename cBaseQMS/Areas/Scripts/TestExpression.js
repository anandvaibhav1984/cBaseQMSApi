$(document).ready(function () {
    //ddl test master change to populate tests
    $("#TestMaster").change(function () {
        var ddlTest = $("#ddlTests");
        $.ajax({
            type: 'GET',
            url: '/TestExpression/LoadTestsByTestMasterId',
            data: { testMasterId: $(this).val() },
            contentType: "application/json; charset=utf-8",
            success: function (result) {
                if (result.success) {
                    var items = result.results;
                    if (items.length > 0)
                    {
                        ddlTest.empty().append('<option selected="selected" value="0">Please select</option>');
                        $.each(items, function () {
                            ddlTest.append($("<option></option>").val(this['value']).html(this['text']));
                        });
                    }
                }
                else
                    ddlTest.empty().append('<option selected="selected" value="0">Please select</option>');
            }
        });
    });

    //load component items by selection of component ddl 
    $("#ddlComponent").change(function () {
        var data;
        var ddlCompItem = $("#ddlComponentItems");
        if ($(this).val() == 'TST')
            data = { type: $(this).val(), testMasterId: $("#TestMaster").val(), testId: 0 };
        else if ($(this).val() == 'EQN')
            data = { type: $(this).val(), testMasterId: $("#TestMaster").val(), testId: $("#ddlTests").val() };
        else if ($(this).val() == 'INP')
            data = { type: $(this).val(), testMasterId: $("#TestMaster").val(), testId: $("#ddlTests").val() };
        else if ($(this).val() == 'LOA')
            data = { type: $(this).val(), testMasterId: $("#TestMaster").val(), testId: 0 };
        else if ($(this).val() == 'PRA')
            data = { type: $(this).val(), testMasterId: $("#TestMaster").val(), testId: $("#ddlTests").val() };
        else if ($(this).val() == 'CAL')
            data = { type: $(this).val(), testMasterId: $("#TestMaster").val(), testId: $("#ddlTests").val() };

        //populate items from ajax call
        var promise = AjaxCall('GET', '/TestExpression/LoadComponentItems', data);

        //getting result
        promise.success(function (result) {
            if (result.success) {
                var items = result.results;
                if (items != null) {
                    ddlCompItem.empty().append('<option selected="selected" value="0">Please select</option>');
                    $.each(items, function () {
                        ddlCompItem.append($("<option></option>").val(this['value']).html(this['text']));
                    });
                }
            }
            else
                ddlCompItem.empty().append('<option selected="selected" value="0">Please select</option>');
        });

        
    });

    //adding items in to expression
    $("#btnAdd").click(function () {
        var url = '';
        var id = '';
        var ddlTypes = $('#ddlComponent');
        var ddlItems = $('#ddlComponentItems');
        var ddlOp = $('#ddlOperator');

        //component items selection
        if (ddlTypes.val() != '' && ddlItems.val() != '0')
        {
            id = $("#" + ddlItems[0].id + " option:selected").text() + '^' + ddlTypes.val() + '^' + ddlItems.val();
            var $ctrl = $('<input/>').attr({ type: 'button', name: 'submit', id: id, 'class': 'btn btn-default btn-inline', 'value': $("#" + ddlItems[0].id + " option:selected").text(), onclick: 'alert("submit");' });
            $('#dvExpression').append($ctrl);
        }

        //operator selection
        if (ddlOp.val() != '0')
        {
            var $ctrl = $('<input/>').attr({ type: 'button', name: 'submit', id: $("#" + ddlOp[0].id + " option:selected").text(), 'class': 'btn btn-primary btn-inline common-btn common-btn-animation submit-btn', 'value': $("#" + ddlOp[0].id + " option:selected").text(), onclick: 'alert("Hello");' });
            $('#dvExpression').append($ctrl);
            $("#" + ddlOp[0].id + "").val('0');
        }

        //make default selection of DDLs
        $("#" + ddlTypes[0].id + "").val('');
        $("#" + ddlItems[0].id + "").val('0');
    });

    //start brace button click
    $('#btnStartBrace').click(function () {
        var $ctrl = $('<input/>').attr({ type: 'button', name: 'submit', id: "(", 'class': 'btn btn-primary btn-inline common-btn common-btn-animation submit-btn', 'value': '(', onclick: 'alert("Hello");' });
        $('#dvExpression').append($ctrl);
    });

    //end brace button click
    $('#btnEndBrace').click(function () {
        var $ctrl = $('<input/>').attr({ type: 'button', name: 'submit', id: ")", 'class': 'btn btn-primary btn-inline common-btn common-btn-animation submit-btn', 'value': ')', onclick: 'alert("Hello");' });
        $('#dvExpression').append($ctrl);
    });

    //clear button click
    $('#btnClear').click(function () {
        $('#dvExpression').html('');
    })

    //save button click
    $('#btnSave').click(function (e) {
        e.preventDefault();
        GetTestDetails();
    });

    //ddl test change to populate expression
    $("#ddlTests").change(function () {
        var data = { testMasterId: $("#TestMaster").val(), testId: $(this).val() };
        var promise = AjaxCall('GET', '/TestExpression/GetExpression', data);
        var id = '';

        //getting result
        promise.success(function (result) {
            var items = result.results;
            if (items != null && items.length > 0) {
                for (var i = 0; i < items.length; i++)
                {
                    var $ctrl = $('<input/>').attr({ type: 'button', name: 'submit', id: items[i].Id, 'class': 'btn btn-default btn-inline', 'value': items[i].Name, onclick: 'alert("submit");' });
                    $('#dvExpression').append($ctrl);
                }
            }
            else
                $('#dvExpression').html('');
        });
    });

});

//custom call with passing params
function AjaxCall(type, url, data)
{
    return $.ajax({
        type: type,
        url: url,
        data: data,
        contentType: "application/json; charset=utf-8"
    });
}
//fetching test details
var GetTestDetails = function () {
    var param = [];
    var item = {};
    var text = '';
    item["TestID"] = $("#ddlTests").val();
    item["TestMasterID"] = $("#TestMaster").val();
    item["Descriptions"] = $("#txtDescription").val();
    
    //getting expression strings
    if ($('#dvExpression').children().length > 0) {
        for (i = 0; i < $('#dvExpression').children().length; i++) {
            text += $('#dvExpression').children()[i].id + "~";
        }
    }
    item["Expression"] = text.slice(0, -1);
    param.push(item);

    $.ajax({
        type: 'POST',
        url: '/TestExpression/SaveTestDetails',
        data: JSON.stringify({ "testExp": item }),
        contentType: "application/json; charset=utf-8",
        success: function (result) {
            if (result.success){
                alert('Expression saved successfully.');
                $("#btnEvaluate").show();
                $("#dvOptions").hide();
                $("#btnSave").hide();
            } 
            else
                alert('Error while saving expression');        }
    });
}