
$(document).ready(function () {

    var table = $('#dataTable').DataTable();
    var validate = false;
    var cat = null;

    $('.btnvalidate').click(function () {
        console.log("CLICK!");
        validate = true;
        var data2 = table.row($(this).closest("td")).data();
        var flag = false;
        var tr = $(this).closest("tr");
        if (tr.find(":checkbox").is(":checked")) {
            console.log("checked");
            tr.find("#state").text("False")
            flag = false;
        }
        else {
            console.log("unchecked");
            tr.find("#state").text("True")
            flag = true;
        }

        if (tr.find("#myselect").val() != "vazio") {
            console.log("Cheio");
            cat = tr.find("select").val();
            $.ajax({
                type: 'GET',
                url: 'https://localhost:44382/custo/EditValidacao?id=' + data2[0] + '&interno=' + flag + '&validar=' + validate + '&cat=' + cat,
                success: function (data) {
                    location.reload();
                },
                error: function (err) {
                    alert('Falha ao validar ' + err);
                }
            });
        }
        else {
            console.log("Vazio");

            $.ajax({
                type: 'GET',
                url: 'https://localhost:44382/custo/EditValidacao?id=' + data2[0] + '&interno=' + flag + '&validar=' + validate,
                success: function (data) {
                    location.reload();
                },
                error: function (err) {
                    alert('Falha ao validar ' + err);
                }
            });
        }
  
        console.log("cat=" + cat + " flag=" + flag + " valido=" + validate);
        
    });

 
        

});
