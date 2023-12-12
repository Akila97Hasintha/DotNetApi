$(document).ready(function () {

    // Call the function when the document is ready
    getStudentDetails();

});
function getStudentDetails() {
    var jwtToken = localStorage.getItem("jwt");
    console.log('JWT Token:', jwtToken);
    var headers = { Authorization: `Bearer ${jwtToken}` };
    console.log('Headers:', headers);
    var id = $('#ID').val();
    console.log("id:", id);
    var url = 'https://localhost:44359/Students/GetStudentDetails/' + id;

    $.ajax({
        type: 'GET',
        url: url,
        dataType: 'html',
        headers: headers,
        success: function (data) {

            $('#studentDetailsPartial').html(data);
            console.log("Student details loaded successfully");
        },
        error: function (error) {
            console.error('Error:', error);
        }
    });
}