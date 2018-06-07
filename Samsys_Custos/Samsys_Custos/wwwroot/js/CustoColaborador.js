$(document).ready(function () {


    $("#filtrar_custo_colaborador").click(function () {
        console.log("entrei")
        console.log($("#fitro_Nome").val())
        console.log($("#filtro_Ano").val())
        $.ajax({
            type: 'POST',
            url: 'https://localhost:44382/custo/colaborador',
            data: {
                nome: $("#filtro_Nome").val(),
                id: $("#filtro_Ano").val()
            },
            success: function (data) {
                console.log(data)
            },
            error: function (ex) {
                console.log(ex);
                alert('Failed to retrieve sub-category.' + ex);
            }
        });
    });









})