$(document).ready(function () {
    $("#btnFiltroGeral").click(function () {
        var ano = $("#select_ano_custos").val();
        window.location = 'https://localhost:44382/CUSTO/Geral?ano=' + ano;
    });
    $("#btnFiltroSalario").click(function () {
        var ano = $("#select_ano_custos").val();
        window.location = 'https://localhost:44382/CUSTO/Salario?ano=' + ano;
    });
    $("#btnFiltroEconomato").click(function () {
        var ano = $("#select_ano_custos").val();
        window.location = 'https://localhost:44382/CUSTO/Economato?ano=' + ano;
    });
    $("#btnFiltroPremio").click(function () {
        var ano = $("#select_ano_custos").val();
        window.location = 'https://localhost:44382/CUSTO/Premio?ano=' + ano;
    });
    $("#btnFiltroGsm").click(function () {
        var ano = $("#select_ano_custos").val();
        window.location = 'https://localhost:44382/CUSTO/Gsm?ano=' + ano;
    });
    $("#btnFiltroViatura").click(function () {
        var ano = $("#select_ano_custos").val();
        window.location = 'https://localhost:44382/CUSTO/Viatura?ano=' + ano;
    });
});