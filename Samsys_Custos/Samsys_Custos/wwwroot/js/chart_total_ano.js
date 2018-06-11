﻿$(document).ready(function () {
    function getchart() {
        let saveData = $.ajax({
            type: 'get',
            url: 'https://localhost:44382/CUSTO/CustoTotalAnoJson',
            dataType: "json",
            data: { ano: $("#select_ano").val() },
            success: function (data) {
                var ano = $("#select_ano").val() 
                console.log(ano)
                var dataprovider = new Array();
                for (var i = 0; i < data.length; i++) {
                    console.log(data[i].nomeCompleto)
                    var tempdata = {};
                    Object.assign(tempdata, {
                        "Categoria": data[i].nomeCompleto,
                        "value": data[i].valor,
                        "url": "https://localhost:44382/custo/custos_filtro?categoria=" + data[i].nomeCompleto + "&ano=" + data[i].ano

                    });
                    dataprovider.push(tempdata);
                }
                var grafico = {
                    "type": "pie",
                    "theme": "light",
                    "dataProvider": [],
                    "valueField": "value",
                    "titleField": "Categoria",
                    "urlField": "url",
                    "outlineAlpha": 0.4,
                    "depth3D": 15,
                    "balloonText": "[[title]]<br><span style='font-size:14px'><b>[[value]]</b> ([[percents]]%)</span>",
                    "angle": 30,
                    "export": {
                        "enabled": true
                    }
                    /*"listeners": [{
                        "event": "clickSlice",
                        "method": function (event) {
                            var chart = event.chart;
                            console.log(event.dataItem.dataContext.Categoria);

                        }
                    }]*/
                }
                grafico.dataProvider = dataprovider;
                var chart = AmCharts.makeChart("chartdiv2", grafico);

            



            }
        });
    }
    
    getchart();
    $(document.body).on('change', '#select_ano', function () {
        getchart();

    })

   
});
