
$(document).ready(function () {
    var table = $('#dataTable').DataTable();
    var contador = 1;
    $('#dataTable tbody').on('click', 'tr', function () {

        var data = table.row(this).data();
        console.log(data[0])
        if ($(this).find(":checkbox").is(':checked')) {
            $.ajax({
                type: 'GET',
                url: 'https://localhost:44382/custo/EditValidacao?id='+data[0],
                success: function (data) {
                    console.log(data);
                },
                error: function (ex) {
                    console.log(ex);
                    alert('Falha ao validar ' + ex);
                }
            });
        }
        

    })


});




/* $.ajax({
            type: 'GET',
            url: 'https://localhost:44382/custo/EditValidacao?id=' + data[0],
        })*/


       // window.location = 'https://localhost:44382/custo/EditValidacao?id=' + data[0];