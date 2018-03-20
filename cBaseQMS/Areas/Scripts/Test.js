$(document).ready(function () {
    $('.testmasters-menubar').css('display', 'block');
    $('.test-menubar li, .testmasters-menubar li').removeClass('active-page');
    $('.TestMaster, .TestBlock').addClass('active-page');
    disableControls(true);
    var ddlTestMasters = $("#ddlTestMaster");
    GetAllTestMasters(ddlTestMasters, testMasterid,false);

    var ddlCopyTo = $("#ddlCopyTo");
    GetAllTestMasters(ddlCopyTo, testMasterid,true);

    loadGrid();
    fetchGridData(testMasterid,"all",true);
    $('#chkActive').on("change",function () {
        if (this.checked) {
            fetchGridData(testMasterid, "false",  true);
        }
        else
            fetchGridData(testMasterid, "all",  true);

    });
});

function disableControls(t) {
    $("#btnRename").disabled(t);
    $("#btnCopyTest").disabled(t);
    //$("#btnCancel").disabled(t);
    $("#btnRemoveFromMaster").disabled(t);
    //$("#btnlocationAttr").disabled(t);
    // $("#btnPartAndLocation").disabled(t);   
    $("#btnMove").disabled(t);
    $("#btnDelete").disabled(t);
    //$("#btnPartAttribute").disabled(t);
    $("#btnConfiguration").disabled(t);
    $("#btnCreateTemplate").disabled(t);
    $("#btnInactivate").disabled(t);
  
}
function GetAllTestMasters(control, val, ForCopyTo) {
    
    var URL = "Tests/GetAllTestMasters?testMasterId=" + val + "&ForCopyTo=" + ForCopyTo ;
    $.ajax({

        type: "GET",
        url: URL,
        data: '{}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            $.each(response, function () {
                console.log(response);
                control.append($("<option " + (this['Selected'] === true ? "selected== selected=" : "") + " ></option>").val(this['Value']).html(this['Text']));
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
function loadGrid() {
    $("#tblTestLists").jqGrid({
        colNames: ['', 'Sequence','', 'Test Name', 'Description', 'Active'],
        colModel: [
            { key: false, lable: '', name: 'act', sortable: false, hidden: false, editable: false, align: "center", width: 20 },
            { key: true, name: 'Sequence', label: 'Sequence',  editable: false },
            { key: true, name: 'TestID', label: 'TestID', sorttype: 'integer', editable: false, hidden: true },
            { name: 'TestName', label: 'Test Name', align: "left", editable: false },
            { name: 'Description', label: 'Description', align: "left", editable: false, hidden: false},
            { name: 'Active', label: 'ACTIVE', align: "left", editable: false, hidden: false }
         

        ],
        ajaxGridOptions: { cache: false },
        loadonce: true,
        viewrecords: true, // show the current page, data rang and total records on the toolbar
        datatype: 'local',
        autowidth: true,  // set 'true' here
        shrinkToFit: true,
        rowNum: 100000,
        gridComplete: function () {
            var ids = jQuery("#tblTestLists").getDataIDs();
            for (var i = 0; i < ids.length; i++) {
                var cl = ids[i];
                var appParamId = jQuery('#tblTestLists').getCell(cl, 'TestID');
                var appTestName = jQuery('#tblTestLists').getCell(cl, 'TestName');
                var appDescription = jQuery('#tblTestLists').getCell(cl, 'Description');
                var be = "<input value=" + appParamId + "   onclick=CheckedTest(" + appParamId + ",'" + escape(appTestName) + "','" + escape(appDescription) + "',this) type='radio' name='rbTest'/>";
                console.log(be);
                jQuery("#tblTestLists").jqGrid('setRowData', ids[i], { act: be });

            }
        }
        //
    });
}
function CheckedTest(testid, testName, testDescription,checkedRadio) {
    $("#testInputName").val(testName);
    disableControls(false);
    $("#btnNewTest").disabled(true);   
}

function fetchGridData(t,v,redirect) {
    $("#tblTestLists").GridUnload();
    loadGrid();
    var URL = "Tests/GetAllTestByTestMasterID?testMasterid=" + t + "&active=" + v + "&redirect=" + redirect;
    var gridArrayData = [];
    $.ajax({
        url: URL,
        success: function (result) {
            console.log(result);
            for (var i = 0; i < result.rows.length; i++) {
                var item = result.rows[i];
                console.log(item);
                gridArrayData.push({
                    TestID: item.TestID,
                    TestName: item.TestName,
                    Descriptions: item.Descriptions,
                    Active: item.IsActive,
                    Sequence: item.Sequence

                });
            }

            // set the new data
            $("#tblTestLists").jqGrid('setGridParam', { data: gridArrayData });
            // refresh the grid
            $("#tblTestLists").trigger('reloadGrid');
        }
    });
}
function getTestMasterGridMappingid() {
    var id;
    //getting data from input column
    //var inputs = $('#tblTestMasterLists').getGridParam('data');

    //getting data from value column
    if ($('table#tblTestLists input[type="radio"]').length > 0) {
        var grid = $('table#tblTestLists input[type="radio"]:checked');
        //console.log(grid.length);
        for (var i = 0; i < grid.length; i++) {
            id = grid[i].value;

        }
    }
    return id;

}

function CreateCopyTest(val) {
    var testMasterId;
    if (val === 'Copy Test') {
        testMasterId = $("#ddlCopyTo").val();
      
    }
    else {
        testMasterId=$("#ddlTestMaster").val();
      
    }
    CreateTest(testMasterId);
}
function CreateTest(val) {
    
    var test = {};
    test.TestMasterID =val ;
    test.TestName = $("#testInputName").val();
    test.Operation = "add";
    $.ajax({

        type: "POST",
        url: "Tests/CreateTest",
        data: JSON.stringify({ testModel: test }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (Data) {
            if (!Data.Success) {
                console.log(Data);

                $("#error-list ul").html("");
                $.each(Data.error, function (index, item) {
                    console.log(item.ErrorMessage);
                    $("#error-list ul").append('<li>' + item.ErrorMessage + '</li>');
                });

                $(".alert-danger").show().delay(3000).fadeOut(200);
            }
            else {
                console.log(Data);
                fetchGridData(Data.testMasterId,"all",true);
                $("#testInputName").val("");
                
                $("#alertSucess").text(Data.Message);
                $(".alert-success").show().delay(3000).fadeOut(200);
            }

        },
        failure: function (response) {

        },
        error: function (response) {

        }
    });

}
function Move(direction) {
    var $table = $("#tblTestLists");

    var ids = jQuery("#tblTestLists").jqGrid('getDataIDs');
    var rowData = jQuery("#tblTestLists").jqGrid('getRowData');
    var nextKeyID = 0;
    var prevKeyID = 0;
    var i = getTestMasterGridMappingid();
    if ($('table#tblTestLists input[type="radio"]:checked').closest('tr').prev('tr').length > 0) {
        prevKeyID = $('table#tblTestLists input[type="radio"]:checked').closest('tr').prev('tr').find('td:first').find('input').val();
    }
    else
        prevKeyID == 0;
    if ($('table#tblTestLists input[type="radio"]:checked').closest('tr').next('tr').length > 0) {
        nextKeyID = $('table#tblTestLists input[type="radio"]:checked').closest('tr').next('tr').find('td:first').find('input').val();
    }
    else
        nextKeyID = 0;
    $("#error-list ul").html('');
    if (direction === 'DW' && nextKeyID <= 0) {
        $("#error-list ul").append('<li>Not possible for current item</li>');
        $(".alert-danger").show().delay(3000).fadeOut(200);
        return false;
    }

    if (direction === 'UP' && (prevKeyID <= 0 || jQuery.type(prevKeyID) ==='undefined')) {
        $("#error-list ul").append('<li>Not possible for current item</li>');
        $(".alert-danger").show().delay(3000).fadeOut(200);
        return false;
    }
    if (jQuery.type(prevKeyID) === 'undefined')
    {
        prevKeyID = 0;
    }
    var currentKeyID = $('table#tblTestLists input[type="radio"]:checked').closest('tr').find('td:first').find('input').val()
    ShiftUpDown(currentKeyID, nextKeyID, prevKeyID, direction, $("#ddlTestMaster").val());
    
}

function ShiftUpDown(currentKeyID, nextKeyID, prevKeyID, opCode, testMasterId) {
    $.ajax({
        url: "Tests/ShiftSequence/",
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify({ testMasterId:testMasterId,currentKeyID: currentKeyID, nextKeyID: nextKeyID, prevKeyID: prevKeyID, opCode: opCode }),
        type: "POST",
        success: function (Data) {
            if (!Data.Success) {
                console.log(Data);
                $("#error-list ul").html("");
                $.each(Data.error, function (index, item) {
                    console.log(item.ErrorMessage);
                    $("#error-list ul").append('<li>' + item.ErrorMessage + '</li>');
                });

                $(".alert-danger").show().delay(3000).fadeOut(200);
            }
            else {
                //console.log(Data);
                fetchGridData(Data.TestMasterId, "all",true);
                $("#testInputName").val("");
                $("#alertSucess").text(Data.Message);
                $(".alert-success").show().delay(3000).fadeOut(200);
            }

        },
        error: function () {

            return false;
        }
    });
}

function btnCancel() {

    window.location.href = "Tests/GoToTestMasterPage";
}

function btnRemoveFromMaster() {
    var tests = {};
    var testId = getTestMasterGridMappingid();
    tests.TestID = testId;
    tests.TestName = $("#testInputName").val();

    tests.Operation = "Delete";
    tests.TestMasterID = $("#ddlTestMaster").val();
    tests.IsActive = 0;
    var URL = "Tests/RemoveFromTestMaster";
    $.ajax({

        type: "POST",
        url: URL,
        data: JSON.stringify({ testsViewModel: tests}),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (Data) {
            if (!Data.Success) {           
                $("#error-list ul").html("");
                $.each(Data.error, function (index, item) {
                    console.log(item.ErrorMessage);
                    $("#error-list ul").append('<li>' + item.ErrorMessage + '</li>');
                });

                $(".alert-danger").show().delay(3000).fadeOut(200);
            }
            else {
                //console.log(Data);
                fetchGridData(Data.TestMasterId, "all",true);
                $("#testInputName").val("");
                $("#alertSucess").text(Data.Message);
                $(".alert-success").show().delay(3000).fadeOut(200);
            }

        },
        failure: function (response) {
            alert(response.data);
        },
        error: function (response) {
            alert(response.data);
        }
    });

}
function btnActivateTests(val)
{
    UpdateTestMaster(val);
}
function RenameTestMaster(val) {
    console.log(val);
    UpdateTestMaster(val);
}
function UpdateTestMaster(val) {

    var tests= {};
    tests.TestID = getTestMasterGridMappingid();

    if (val === "Delete Test" ) {

        tests.Operation = "Delete"
    }
    else if (val === "Rename") {
        tests.Operation = "Update"
    }
    else if (val === "Inactivate Test") {

        tests.Operation = "activate"
        tests.IsActive = $('table#tblTestLists input[type="radio"]:checked').closest('tr').find('td:last').text();
    }

    tests.TestName = $("#testInputName").val();
   
    var URL = "Tests/UpdateTest";

    $.ajax({
        type: "POST",
        url: URL,
        data: JSON.stringify({ testsViewModel: tests }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (Data) {
            console.log(Data);
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
             
                fetchGridData(Data.testMasterId, "all",true);
            }

        }
    });


}

function MoveTest(val) {
    var tests = {};
    TestIDFrom = getTestMasterGridMappingid();

    if (val === "Move test") {

        tests.Operation = "Move";
    }
    
    tests.TestMasterId = $("#ddlCopyTo").val();
    tests.TestName = $("#testInputName").val();
    var TestMasterIdFrom = testMasterId = $("#ddlTestMaster").val();
    var URL = "Tests/MoveTest";

    $.ajax({
        type: "POST",
        url: URL,
        data: JSON.stringify({ testsViewModel: tests, TestMasterIdFrom: TestIDFrom }),
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

                fetchGridData(Data.testMasterid, "all", true);
            }

        }
    });

}