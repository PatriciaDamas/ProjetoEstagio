
$(document).ready(function () {
    var table = $('#dataTable').DataTable();


    $('#dataTable tbody').on('click', 'tr', function () {
        console.log("entrei!");
        var data = table.row(this).data();
        console.log(data)
        var flag = false;
        var cat = null;

        if ($(this).find(":checkbox").is(':checked')) {
            $(this).find("p").text("False")
            flag = false;
        }
        else {
            $(this).find("p").text("True")

            flag = true;
        }
        console.log($(this).find("select").val());

        if ($(this).find("select").val() != "vazio") {
            cat = $(this).find("select").val();
            console.log(cat);
        }
        $.ajax({
             type: 'GET',
            url: 'https://localhost:44382/custo/EditValidacao?id=' + data[0] + '&flag=' + flag + '&cat='+cat,
             success: function (data) {
                 console.log(data);
             },
             error: function (err) {
                 console.log(err);
                 alert('Falha ao validar ' + err);
             }
         });
    });
        

});
