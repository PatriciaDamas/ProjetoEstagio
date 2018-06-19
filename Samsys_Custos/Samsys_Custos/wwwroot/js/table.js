
$(document).ready(function () {
    var table = $('#dataTable').DataTable();
    $('#dataTable tbody').on('click', 'tr', function () {
        var data = table.row(this).data();
        window.location = 'https://localhost:44382/custo/colaborador?id=' + data[0];
    });
});

