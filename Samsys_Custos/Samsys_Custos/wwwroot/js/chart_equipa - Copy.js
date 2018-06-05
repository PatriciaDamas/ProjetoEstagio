$(document).ready(function () {
    function getchart() {
        let saveData = $.ajax({
            type: 'get',
            url: 'https://localhost:44363/CUSTO/CustoEquipaJson',
            dataType: "json",
            success: function (data) {
               
                var dataprovider = new Array();
                var meses = ["Janeiro", "Fevereiro", "Março", "Abril", "Maio", "Junho", "Julho", "Agosto", "Setembro", "Outubro", "Novembro", "Dezembro"];
                var equipas = ["SAGE", "PHC", "Comunicação", "Comercial", "Desenvolvimento", "Gerência", "Direção", "Sistemas", "Planeamento e Suporte", "Dep. Técnico Interno", "Logística & Compras", "Consultores Externos", "Financeiro e Administrativo"]

                //Definir um objeto para guardar os dados que queremos ter no dataprovider
           

                //Definir os doze meses de um determinado ano no gráfico
                for (var i = 0; i < meses.length; i++) {
                    //console.log(data[i].mes)
                   // console.log(meses[i])
                    var tempdata = {};
                    Object.assign(tempdata, {
                        "Mês": meses[i]
                    });
                    for (var k = 0; k < equipas.length; k++) {
                        Object.assign(tempdata, {
                            "Equipa": equipas[k]
                        });
                        for (var j = 0; j < data.length; j++) {
                            if (meses[i].toLowerCase() == data[j].mes.toLowerCase() && equipas[k].toLowerCase()==data[j].equipas.toLowerCase()) {
                                var total = data[j].total
                                //Adicionar o resto dos campos ao objeto tempdata
                                Object.assign(tempdata, {
                                    Total: total
                                });

                            }
                        }
                        if (Object.keys(tempdata).length > 2) {
                            dataprovider.push(tempdata);
                        }
                        tempdata = {};
                        Object.assign(tempdata, {
                            "Mês": meses[i]
                        });

                    }
                    //inserir tudo que está dentro do tempdata para o dataprovider do gráfico

                }
                console.log(dataprovider.length)
               

                var grafico = {
                    "type": "serial",
                    "theme": "light",
                    "legend": {
                        "horizontalGap": 10,
                        "maxColumns": 10,
                        "position": "right",
                        "useGraphSettings": true,
                        "markerSize": 10
                    },
                    "dataProvider": [],
                    "valueAxes": [{
                        "stackType": "regular",
                        "axisAlpha": 1,
                        "gridAlpha": 0
                    }],
                    "graphs": [],
                    "categoryField": "Equipa",
                    "categoryAxis": {
                        "gridPosition": "start",
                        "axisAlpha": 0,
                        "gridAlpha": 0,
                        "position": "left"
                    },
                    "export": {
                        "enabled": true
                    }

                };


             
                for (var i = 0; i < 6; i++) {
                 
                    grafico.graphs.push({
                        "balloonText": "<b>[[title]]</b><br><span style='font-size:14px'>[[category]]: <b>[[value]]</b></span>",
                        "fillAlphas": 0.8,
                        "labelText": dataprovider[i].Mês,
                        "id": "AmGraph-" + i,
                        "title": dataprovider[i].Equipa,
                        "type": "column",
                        "valueField": "Total"
                    });  
                    
                }
                grafico.dataProvider = dataprovider;

                var chart = AmCharts.makeChart("chartdiv", grafico);
                console.log(grafico)
            }
        });
    }
    
    getchart();
    $(document.body).on('change', '#select_ano', function () {
        getchart();

    })
});
