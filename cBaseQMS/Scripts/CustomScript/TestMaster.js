
function saveData(data) {
    
    $.ajax({
        url: "/TestMaster/saveTestMaster",
        data: data,  
        contentType: 'application/json; charset=utf-8',
        type: "POST",
        success: function (data) {
            if (data == "success") {

            }
            if (data == "Already Exists") {

            }
            if (data == "error") {

            }
        },
        error: function () {

            return false;
        }
    });
}