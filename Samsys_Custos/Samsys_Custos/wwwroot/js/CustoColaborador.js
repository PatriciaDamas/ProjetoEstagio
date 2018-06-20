$(document).ready(function () {

    console.log($("#select_colaborador").val());
    $("#table").hide();
    $("#btnFiltro").click(function () {
        var ano = $("#select_ano_custos").val();
        var colaborador = $("#select_colaborador").val();
        //window.location = 'https://localhost:44382/custo/colaborador?id=' + colaborador + '&ano=' + ano;
        $.ajax({
            type: 'GET',
            url: 'https://localhost:44382/custo/CustoColaboradorJson',
            data: {
                ano: $("#select_ano_custos").val(),
                id: $("#select_colaborador").val()
            },
            success: function (data) {

                if ($("#select_colaborador").val() != 'null' && $("#select_ano_custos").val() != 'null') {


                    $("#table").show();
                    $("#tableColaborador").empty();
                    var table = '';
                    console.log(data.length);

                    for (var i = 0; i < data.length; i++) {
                        console.log(data[i])
                        table += '<tr>';
                        //table += '<td>' + data[i].colaborador + '</td>';
                        table += '<td>' + data[i].categoria + '</td>';
                        table += '<td>' + data[i].ano + '</td>';
                        table += '<td>' + data[i].janeiro + '</td>';
                        table += '<td>' + data[i].fevereiro + '</td>';
                        table += '<td>' + data[i].março + '</td>';
                        table += '<td>' + data[i].abril + '</td>';
                        table += '<td>' + data[i].maio + '</td>';
                        table += '<td>' + data[i].junho + '</td>';
                        table += '<td>' + data[i].julho + '</td>';
                        table += '<td>' + data[i].agosto + '</td>';
                        table += '<td>' + data[i].setembro + '</td>';
                        table += '<td>' + data[i].outubro + '</td>';
                        table += '<td>' + data[i].novembro + '</td>';
                        table += '<td>' + data[i].dezembro + '</td>';
                        table += '<tr>';
                    }

                    $("#tableColaborador").append(table)

                } else {
                    alert("Por favor, preencha as caixas de filtro corretamente")
                }
            },
            error: function (ex) {
                console.log(ex);
                alert('Failed to retrieve sub-category.' + ex);
            }
        });
    });









})