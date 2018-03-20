$(document).ready(function () {
    fillTestMaster();

});

function fillTestMaster() {
    var ddlTests = $("#ddlTestMaster");
    ddlTests.empty().append('<option selected="selected" value="0" disabled = "disabled">Loading.....</option>');
    $.ajax({
        type: "GET",
        url: "TestMaster/GetAllTest",
        data: '{}',
        // rowNum: -1,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            ddlTests.empty().append('<option selected="selected" value="0">All Tests*</option>').append('<option  value="">*All Unassigned Tests</option>');
            $.each(response, function () {
                ddlTests.append($("<option></option>").val(this['Value']).html(this['Text']));
            });
        },
        failure: function (response) {
            //alert(response.data);
        },
        error: function (response) {
            //alert(response.data);
        }
    });
}