$(document).ready(function () {
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
        var ddlCompItem = $("#ddlComponentItems");
        $.ajax({
            type: 'GET',
            url: '/TestExpression/LoadComponentItems',
            data: { type: $(this).val(), testMasterId: $("#TestMaster").val() },
            contentType: "application/json; charset=utf-8",
            success: function (result)
            {
                if (result.success)
                {
                    var items = result.results;
                    ddlCompItem.empty().append('<option selected="selected" value="0">Please select</option>');
                    $.each(items, function () {
                        ddlCompItem.append($("<option></option>").val(this['value']).html(this['text']));
                    });
                }
                else
                    ddlCompItem.empty().append('<option selected="selected" value="0">Please select</option>');
            }
        });
    });
});