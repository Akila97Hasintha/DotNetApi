
$(function () {

    loadStudentList();

});
function loadStudentList() {
    var jwtToken = localStorage.getItem("jwt");
    console.log('JWT Token:', jwtToken);
    var headers = { Authorization: `Bearer ${jwtToken}` };
    console.log('Headers:', headers);
    var url = 'https://localhost:44359/Courses/getCourseList';

    $.ajax({
        type: 'GET',
        url: url,
        dataType: 'html',
        headers: headers,
        success: function (data) {

            $('#courseIndexPartial').html(data);

        },
        error: function (error) {
            console.error('Error:', error);
        }
    });
}    