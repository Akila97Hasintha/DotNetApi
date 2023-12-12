
$(function () {
    $(document).on('click', '#btnEdit', function (e) {
        e.preventDefault();
        var jwtToken = localStorage.getItem("jwt");
        console.log('JWT Token:', jwtToken);
        var headers = { Authorization: `Bearer ${jwtToken}` };
        console.log('Headers:', headers);
        var id = $('#ID').val();
        var url = 'https://localhost:44359/Students/Edit/' + id;
        var formData = $('#formEdit').serialize();

        $.ajax({
            type: 'POST',
            url: url,
            data: formData,
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
