
$(function () {

    loadStudentList();

});
function loadStudentList() {

    var jwtToken = localStorage.getItem("jwt");
    console.log('JWT Token:', jwtToken);
    

    var url = 'https://localhost:44359/Students/getStudentList';

   

    var headers = { Authorization: `Bearer ${jwtToken}` };
    console.log('Headers:', headers);


    $.ajax({
        type: 'GET',
        url: url,
        dataType: 'html',
        headers: headers,
        success: function (data) {

            $('#studentIndexPartial').html(data);

        },
        error: function (error) {
            $('#errorModalBody').text('An error occurred while retrieving the students');
            $('#errorModal').modal('show');
            console.error('Error:', error);
        }
    });
}    