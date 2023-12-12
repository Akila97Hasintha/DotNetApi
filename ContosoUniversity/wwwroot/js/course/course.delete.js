$(function () {
    $(document).on('click', '#btnDelete', function (e) {
        e.preventDefault();
        var id = $('#CourseID').val();
        var url = 'https://localhost:44359/Courses/Delete/' + id;
        var formData = $('#formDelete').serialize();

        $.ajax({
            type: 'POST',
            url: url,

            success: function (response) {
                console.log(response);
                if (response.success) {
                    window.location.href = 'https://localhost:44359/Courses/Index';
                }
            },
            error: function (error) {
                // Handle the error, e.g., display an error message.
                console.error('Error:', error);
            }
        });
    });
});
