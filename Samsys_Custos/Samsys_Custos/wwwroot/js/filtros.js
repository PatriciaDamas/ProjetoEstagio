$(document).ready(function () {
    $("#btnFiltroGeral").click(function () {
        var ano = $("#select_ano_custos").val();
        window.location = 'https://localhost:44382/CUSTO/Geral?ano=' + ano;
     });
});