$(function () {
    $(document).on('click', '#btnDelete', function (e) {
        e.preventDefault();

        var jwtToken = localStorage.getItem("jwt");
        console.log('JWT Token:', jwtToken);
        var headers = { Authorization: `Bearer ${jwtToken}` };
        console.log('Headers:', headers);
        var id = $('#ID').val();
        var url = 'https://localhost:44359/Students/Delete/'+id;
        var formData = $('#formDelete').serialize();

        $.ajax({
            type: 'POST',
            url: url,
            headers: headers,
           
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
