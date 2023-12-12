$(function () {

    // Call the function when the document is ready
    getStudentDetails();

});
function getStudentDetails() {

    var id = $('#CourseID').val();
    console.log("id:", id);
    var url = 'https://localhost:44359/Courses/GetCourseDetails/' + id;

    $.ajax({
        type: 'GET',
        url: url,
        dataType: 'html',
        success: function (data) {

            $('#courseDetailsPartial').html(data);
            console.log("Student details loaded successfully");
        },
        error: function (error) {
            console.error('Error:', error);
        }
    });
}