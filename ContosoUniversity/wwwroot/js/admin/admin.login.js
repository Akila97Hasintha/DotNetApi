$(function () {
    $(document).on('click', '#btnLogIn', function (e) {
        e.preventDefault();

        var url = 'https://localhost:44359/Admins/LogIn';
        var formData = $('#frmLogIn').serialize();

        $.ajax({
            type: 'POST',
            url: url,
            data: formData,
            success: function (response) {
                console.log(response);

                if (response.success) {
                    console.log("Token:", response.token);
                    localStorage.setItem('jwt', response.token);
                    $('#successModalBody').text('Login Successfully');
                    $('#successModal').modal('show');
                    setTimeout(function () {
                        window.location.href = 'https://localhost:44359/Students/Index';
                    }, 2000);

                } else {
                    $('#errorModalBody').text('Invalide UserName or Password');
                    $('#errorModal').modal('show');
                }
            },
            error: function (error) {
                // Handle the error, e.g., display an error message.
                $('#errorModalBody').text('error in login');
                $('#errorModal').modal('show');
                console.error('Error:', error);
            }
        });
    });
});
