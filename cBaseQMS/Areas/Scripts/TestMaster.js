$(document).ready(function () {
    $('.test-menubar li').removeClass('active-page');
    $('.TestMaster').addClass('active-page');
    $('#copyTestMaster').click(function () {
        $(".alert-div").show();
    });
    fillTests();
    loadGrid();
    loadPartAndLocationGrid();
    fetchGridData(0,false);

   GetAllPartMaster();
   GetAllLocationMaster();
    disableControls(true);
    $("#ddlLocationMaster").disabled(true);
    //$("#ddlPartMaster").disabled(true);
    $("#btnremovePartAndLocation").disabled(true);
    $("#btnPartAndLocation").disabled(true);
    $("#btnlocationAttr").disabled(true);
    $("#btnPartAttribute").disabled(true);
    $("#btnCreateTestMaster").disabled(true);
    $("#ddlTest").change(function () {
        fetchGridData($(this).val(),false);
    });

    $("#ddlLocationMaster").change(function () {
        if ($(this).val() > 0)
            $("#btnlocationAttr").disabled(false);
        else
            $("#btnlocationAttr").disabled(true);
    });

    $("#ddlPartMaster").change(function () {
        if ($(this).val() > 0)
            $("#btnPartAttribute").disabled(false);
        else
            $("#btnPartAttribute").disabled(true);
    });

    $("#ddlPartMaster").change(function () {
        checkExistence($("#ddlLocationMaster").val(), $(this).val());
    });

    $("#ddlLocationMaster").change(function () {
        checkExistence($(this).val(), $("#ddlPartMaster").val());
    });

    $('#testMasterName').on('keyup change', function () {
        var val = getTestMasterGridMappingid();      
        if (($(this).val() && val) !== "") {
      
            $("#btnCreateTestMaster").disabled(true);
        }
        else {
            console.log('val1');
            $("#btnCreateTestMaster").disabled(false);
        }
    });

});

function checkExistence(locationValue, partValue) {

    if (locationValue === "" || partValue === "") {
        return false;
    }
    var testMasterMapping = {};
    testMasterMapping.TestMasterID = getTestMasterGridMappingid();
    testMasterMapping.LocationMasterID = locationValue,
        testMasterMapping.PartMasterID = partValue
    var URL = "TestMaster/checkExistence";

    $.ajax({
        type: "POST",
        url: URL,
        data: JSON.stringify({ testMasterMappingViewModel: testMasterMapping }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (Data) {
            console.log(Data);
            if (!Data.Success) {
                $("#error-list ul").html("");
                $.each(Data.error, function (index, item) {
                    console.log(item);
                    $("#error-list ul").append('<li>' + item + '</li>');
                });

                $(".alert-danger").show().delay(3000).fadeOut(500);
                $("#btnPartAndLocation").disabled(true);
            }
            else {
                console.log(Data);
                $("#btnPartAndLocation").disabled(false);
            }

        }
    });
}
function disableControls(t) {
    $("#tblLocationNPart").disabled(t);
    $("#ddlLocationMaster").disabled(t);
    $("#ddlPartMaster").disabled(t);
    // $("#btnCreateTestMaster").disabled(t);
    //$("#btnlocationAttr").disabled(t);
    // $("#btnPartAndLocation").disabled(t);   
    $("#btnRename").disabled(t);
    $("#btnSave").disabled(t);
    //$("#btnPartAttribute").disabled(t);
    $("#copyTestMaster").disabled(t);
    $("#cancelTestMaster").disabled(t);
    $("#btnCopy").disabled(t);
    $("#btnDelete").disabled(t);
    $("#idManage").disabled(t);


}
function loadPartAndLocationGrid() {
    $("#tblLocationNPart").jqGrid({
        colNames: ['', 'TestMasterMapID', 'Locotion Name', 'Part Number'],
        colModel: [
            { key: false, lable: '', name: 'act', sortable: false, hidden: false, editable: false, align: "center", width: 20 },
            { key: true, name: 'TestMasterMapID', label: '', editable: false, hidden: true },
            { name: 'LocationName', label: 'Locotion Name', align: "left", editable: false },
            { name: 'PartNumber', label: 'Part Number ', align: "left", editable: false },

        ],
        ajaxGridOptions: { cache: false },
        loadonce: true,
        viewrecords: true, // show the current page, data rang and total records on the toolbar
        autowidth: true,  // set 'true' here
        shrinkToFit: true,
        datatype: 'local',
        rowNum: 100000,
        gridComplete: function () {

            var ids = jQuery("#tblLocationNPart").getDataIDs();
            for (var i = 0; i < ids.length; i++) {
                //console.log(URL);
                //console.log(x);
                var cl = ids[i];
                var appParamId = jQuery('#tblLocationNPart').getCell(cl, 'TestMasterMapID');

                be = "<input value=" + appParamId + "  onclick=enableRemove() val='" + appParamId + "' type='radio' name='rbLocationAndPart'/>";

                jQuery("#tblLocationNPart").jqGrid('setRowData', ids[i], { act: be });
            }
        }

    });
}

function enableRemove() {
    $("#btnremovePartAndLocation").disabled(false);
}
function loadGrid() {
    $("#tblTestMasterLists").jqGrid({
        colNames: ['', 'TestMasterID', 'TEST MASTER', '', ''],
        colModel: [
            { key: false, lable: '', name: 'act', sortable: false, hidden: false, editable: false, align: "center", width: 20 },
            { key: true, name: 'TestMasterID', label: 'TestMasterID', sorttype: 'integer', editable: false, hidden: true },
            { name: 'TestMasterName', label: 'Test Master Name', align: "left", editable: false },
            { name: 'Description', label: 'Description', align: "left", editable: false, hidden: true },
            { name: 'check', label: 'check', align: "left", editable: false, hidden: true },

        ],
        ajaxGridOptions: { cache: false },
        loadonce: true,
        viewrecords: true, // show the current page, data rang and total records on the toolbar
        datatype: 'local',
        autowidth: true,  // set 'true' here
        shrinkToFit: true,
        rowNum: 100000,
        gridComplete: function () {
            var ids = jQuery("#tblTestMasterLists").getDataIDs();
            for (var i = 0; i < ids.length; i++) {
                var cl = ids[i];
                var appParamId = jQuery('#tblTestMasterLists').getCell(cl, 'TestMasterID');
                var appTestMasterName = jQuery('#tblTestMasterLists').getCell(cl, 'TestMasterName');
                var appDescription = jQuery('#tblTestMasterLists').getCell(cl, 'Description');
                var check = jQuery('#tblTestMasterLists').getCell(cl, 'check');
                console.log(check);
                if (check === "" || check === "false") {
                    check = false;
                }
                else
                    check = true;

                var be = "<input value=" + appParamId + "  " + (check === true ? "checked=true" : "") + " onclick=CheckedTestMaster(" + appParamId + ",'" + escape(appTestMasterName) + "','" + escape(appDescription) + "',this) type='radio' name='rbTestMaster'/>";
                jQuery("#tblTestMasterLists").jqGrid('setRowData', ids[i], { act: be });

            }
        }
        //
    });
}
function CheckedTestMaster(checkedRadio, appTestMasterName, appDescription, elem) {
    if (elem === "Delete") {
        $("#testMasterName").val("");
        $("#testMasterDesc").val("");
    }
    else {
        $('#tblTestMasterLists tr.ui-active-state').removeClass('ui-active-state');
        $(elem).closest('tr').addClass("ui-active-state");
        $(this).data("checkedRadioValue", checkedRadio);
        $("#ddlPartMaster").prop("selectedIndex", 0);
        $("#ddlLocationMaster").prop("selectedIndex", 0);
        $("#testMasterName").val(unescape(appTestMasterName));
        $("#testMasterDesc").val(unescape(appDescription));
        $("#btnCreateTestMaster").disabled(true);
        $("#btnSave").disabled(false);
        fetchLocationAndPartGridData(checkedRadio,true);
    }


}
function DeleteTestMaster(val) {
    console.log(val);
    UpdateTestMaster(val);
    //    UpdateTestMaster()
}
function RenameTestMaster(val) {
    console.log(val);
    UpdateTestMaster(val);
}
function UpdateTestMaster(val) {

    var testMaster = {};
    testMaster.TestMasterID = getTestMasterGridMappingid();

    if (val === "Delete") {

        testMaster.operation = "Delete"
    }
    else if (val === "Save") {
        testMaster.operation = "Update"
    }

    testMaster.TestMasterName = $("#testMasterName").val();
    testMaster.Description = $("#testMasterDesc").val();

    var URL = "TestMaster/UpdateTestMaster";

    $.ajax({
        type: "POST",
        url: URL,
        data: JSON.stringify({ testMasterViewModel: testMaster }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (Data) {
            //console.log(Data);
            if (!Data.Success) {
                $("#error-list ul").html("");
                $.each(Data.error, function (index, item) {
                   // console.log(item.ErrorMessage);
                    $("#error-list ul").append('<li>' + item.ErrorMessage + '</li>');
                });

                $(".alert-danger").show().delay(3000).fadeOut(500);
                $("#btnPartAndLocation").disabled(true);
            }
            else {

                $("#alertSucess").text(Data.Message);
                $(".alert-success").show().delay(1000).fadeOut(200);
                CheckedTestMaster(Data.testMasterId, Data.testMasterName, Data.testMasterDescription, val);
                fetchGridDataAfterUpdate(Data.testMasterId, val,true);
            }

        }
    });


}

function fillTests() {
    var ddlTests = $("#ddlTest");
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


function GetAllPartMaster() {
    var ddlPartMaster = $("#ddlPartMaster");
    ddlPartMaster.empty().append('<option selected="selected" value="0" disabled = "disabled">Loading.....</option>');
    $.ajax({
        type: "GET",
        url: "TestMaster/GetAllPartMaster",
        data: '{}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            ddlPartMaster.empty().append('<option selected="selected" value="">Select Part</option>');
            $.each(response, function () {
                ddlPartMaster.append($("<option></option>").val(this['Value']).html(this['Text']));
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

function GetAllLocationMaster() {
    var ddlLocationMaster = $("#ddlLocationMaster");
    ddlLocationMaster.empty().append('<option selected="selected" value="" disabled = "disabled">Loading.....</option>');
    $.ajax({
        type: "GET",
        url: "TestMaster/GetAllLocationMaster",
        data: '{}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            ddlLocationMaster.empty().append('<option selected="selected" value="">Select Location</option>');
            $.each(response, function () {
                ddlLocationMaster.append($("<option></option>").val(this['Value']).html(this['Text']));
            });
        },
        failure: function (response) {
            alert(response.data);
        },
        error: function (response) {
            alert(response.data);
        }
    });
}


function CreateTestMaster() {
    var testMaster = {};
    testMaster.TestMasterName = $("#testMasterName").val(),
        testMaster.Description = $("#testMasterDesc").val()
    testMaster.operation = "Insert"
    $(".input-error").removeClass('input-error');
    $.ajax({

        type: "Post",
        url: "/TestMaster/CreateTestMaster",
        data: JSON.stringify({ testMasterModel: testMaster }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (Data) {
            if (!Data.Success) {
               // console.log(Data);

                $("#error-list ul").html("");
                $.each(Data.error, function (index, item) {
                    console.log(item.ErrorMessage);
                    $("#error-list ul").append('<li>' + item.ErrorMessage + '</li>');
                });

                $(".alert-danger").show().delay(3000).fadeOut(200);
            }
            else {
                //console.log("false1");
                fetchGridData(0,true);
                $("#testMasterName").val("");
                $("#testMasterDesc").val("");
                $("#alertSucess").text(Data.Message);
                $(".alert-success").show().delay(3000).fadeOut(200);
            }

        },
        failure: function (response) {
            //console.log('AB')
            //console.log(response);
        },
        error: function (response) {
            //console.log('AV')
            // console.log(response);
        }
    });

}

function fillTestMasterByTestIdtest(value) {

    var URL = "TestMaster/GetAllTestMastersByTest?testid=" + value;
    var test = {};
    test.TestID = value
    $.ajax({
        type: "POST",
        url: URL,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            //console.log(response);
            // console.log("a");
        }
    });
};


function fetchGridData(t,redirect) {
    $("#tblTestMasterLists").GridUnload();
    loadGrid();
    var URL = "TestMaster/GetAllTestMastersByTest?testid=" + t + "&redirect=" + redirect;
    var gridArrayData = [];
    $.ajax({
        url: URL,
        success: function (result) {
            console.log(result);
            for (var i = 0; i < result.rows.length; i++) {
                var item = result.rows[i];
                gridArrayData.push({
                    TestMasterID: item.TestMasterID,
                    TestMasterName: item.TestMasterName,
                    Description: item.Description,


                });
            }

            // set the new data
            $("#tblTestMasterLists").jqGrid('setGridParam', { data: gridArrayData });
            // refresh the grid
            $("#tblTestMasterLists").trigger('reloadGrid');
        }
    });
}

function fetchGridDataAfterUpdate(t, o, redirect) {
    $("#tblTestMasterLists").GridUnload();
    loadGrid();
    var tv = 0;
    var URL = "TestMaster/GetAllTestMastersByTest?testid=" + tv + "&redirect=" + redirect;
    var gridArrayData = [];
    $.ajax({
        url: URL,
        success: function (result) {
            console.log(result);
            for (var i = 0; i < result.rows.length; i++) {
                var item = result.rows[i];
                gridArrayData.push({
                    TestMasterID: item.TestMasterID,
                    TestMasterName: item.TestMasterName,
                    Description: item.Description,
                    check: (o === "Save") ? (item.TestMasterID === t ? true : false) : false

                });

            }

            // set the new data
            $("#tblTestMasterLists").jqGrid('setGridParam', { data: gridArrayData });
            // refresh the grid
            $("#tblTestMasterLists").trigger('reloadGrid');
        }
    });
}


function fetchLocationAndPartGridData(t,redirect) {
    $("#tblLocationNPart").GridUnload();
    loadPartAndLocationGrid();
    disableControls(false);
    var URL = "TestMaster/GetAllLocationAndPartMasterMapping?testMasterid=" + t+"&redirect="+redirect;
    var gridArrayData = [];
    $.ajax({
        url: URL,
        success: function (result) {
            // console.log(result);
            for (var i = 0; i < result.rows.length; i++) {
                var item = result.rows[i];
                gridArrayData.push({
                    TestMasterMapID: item.TestMasterMapID,
                    LocationName: item.LocationName,
                    PartNumber: item.PartNumber
                });
            }
            // console.log(gridArrayData);
            // set the new data
            $("#tblLocationNPart").jqGrid('setGridParam', { data: gridArrayData });
            // refresh the grid
            $("#tblLocationNPart").trigger('reloadGrid');
        }
    });
}

function SavePartAndLocation() {
    var errorId = $("#msg_id");

    var msg = "Please select";
    var TestMasterMapping = {};
    TestMasterMapping.TestMasterID = getTestMasterGridMappingid();
    TestMasterMapping.PartMasterID = $('#ddlPartMaster').val();
    TestMasterMapping.LocationMasterID = $('#ddlLocationMaster').val()
    TestMasterMapping.operation = "Insert";
    $(this).data("TestMasterID", TestMasterMapping.TestMasterID);
    $.ajax({

        type: "Post",
        url: "TestMaster/CreateTestMasterMapping",
        data: JSON.stringify({ TestMasterMappingViewModel: TestMasterMapping }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (Data) {
            // console.log(Data);
            if (Data.Success) {
                fetchLocationAndPartGridData(Data.testMasterId,true);
                $("#alertSucess").text(Data.msg);
                $(".alert-success").show().delay(1000).fadeOut(200);
                $("#btnPartAndLocation").disabled(true);
            }
            else {
                //fluent validation returning array of validation exception

                $("#error-list ul").html("");
                $.each(Data.msg, function (index, item) {
                    //console.log(item);
                    $("#error-list ul").append('<li>' + item + '</li>');
                });

                $(".alert-danger").show().delay(1000).fadeOut(200);

            };
        },
        failure: function (response) {

        },
        error: function (response) {

        }
    });

}

function RemovePartAndLocation() {
    var TestMasterMapping = {};
    TestMasterMapping.testMasterId = getTestMasterGridMappingid();
    TestMasterMapping.TestMasterMapID = getPartAndLocatioMappingid();
    TestMasterMapping.IsActive = false;
    TestMasterMapping.operation = "Remove";
    //if (jQuery.type(rowKey) === 'undefined') {
    //    $("#error-list ul").html("");
    //    $("#error-list ul").append('<li>please select location to remove</li>');
    //    $(".alert-danger").show().delay(1000).fadeOut(200);
    //    return false;
    //}

    var URL = "TestMaster/RemovePartAndLocation"
    $.ajax({
        type: "Post",
        url: URL,
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ testMasterMappingViewModel: TestMasterMapping }),
        success: function (Data) {
            if (Data.Success) {
                console.log(Data);
                fetchLocationAndPartGridData(Data.testMasterId,true);
                $("#alertSucess").text(Data.msg);
                $(".alert-success").show().delay(1000).fadeOut(200);
                $("#btnremovePartAndLocation").disabled(true);
            };
        },
        failure: function (response) {
            console.log("Data");
        },
        error: function (response) {
            console.log("Data1");
        }
    });

}
function getTestMasterGridMappingid() {
    var id;
    //getting data from input column
    //var inputs = $('#tblTestMasterLists').getGridParam('data');

    //getting data from value column
    if ($('table#tblTestMasterLists input[type="radio"]').length > 0) {
        var grid = $('table#tblTestMasterLists input[type="radio"]:checked');
        //console.log(grid.length);
        for (var i = 0; i < grid.length; i++) {
            id = grid[i].value;

        }
    }
    return id;

}
function getPartAndLocatioMappingid() {
    var id;
    //getting data from input column
    //  var inputs = $('#tblLocationNPart').getGridParam('data');

    //getting data from value column
    if ($('table#tblLocationNPart input[type="radio"]').length > 0) {
        var grid = $('table#tblLocationNPart input[type="radio"]:checked');
        //console.log(grid.length);
        for (var i = 0; i < grid.length; i++) {
            id = grid[i].value;

        }
    }
    return id;

}
function rebindCheckBoxValeAfterUpdate(val) {

    //getting data from input column
    var inputs = $('#tblTestMasterLists').getGridParam('data');

    //getting data from value column
    if ($('table#tblTestMasterLists input[type="radio"]').length > 0) {
        var grid = $('table#tblTestMasterLists input[type="radio"]');
        //console.log(grid.length);
        for (var i = 0; i < grid.length; i++) {
            if (val === (inputs[i].TestMasterMapID)) {
                $(this).prop('checked', 'checked');
            }

        }
    }


}
function ManageTestMaster()
{
    var testMasterid = getTestMasterGridMappingid()
    //window.location.href = "/cBaseQMS/Tests/Index?testMasterid=" + testMasterid
    window.location.href = "TestMaster/ManageTestMaster?testMasterid=" + testMasterid
    

}