
$(document).ready(function () {
    var table=$('#mytable').DataTable();
    $('#mytable tbody').on('click', 'tr', function () {
        var data = table.row(this).data();
        window.location = 'https://localhost:44363/custo/colaborador?id=' + data[2];
    });
});

