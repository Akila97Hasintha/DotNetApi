$(function () {
    $(document).on('click', '#btnSignUp', function (e) {
        e.preventDefault();

        var url = 'https://localhost:44359/Admins/SignUp';
        var formData = $('#frmAdmin').serialize();

        $.ajax({
            type: 'POST',
            url: url,
            data: formData,
            success: function (response) {
                console.log(response);
                if (response.success) {
                    window.location.href = 'https://localhost:44359/Students/Index';
                }
            },
            error: function (error) {
                // Handle the error, e.g., display an error message.
                console.error('Error:', error);
            }
        });
    });
});
