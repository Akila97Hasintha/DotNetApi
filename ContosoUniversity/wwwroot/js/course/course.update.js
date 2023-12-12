
$(function () {
    $(document).on('click', '#btnEdit', function (e) {
        e.preventDefault();
        var id = $('#CourseID').val();
        var url = 'https://localhost:44359/Courses/Edit/' + id;
        var formData = $('#formEdit').serialize();

        $.ajax({
            type: 'POST',
            url: url,
            data: formData,
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
