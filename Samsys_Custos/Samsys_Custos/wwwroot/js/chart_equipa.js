﻿$(document).ready(function () {
    function getchart() {
        let saveData = $.ajax({
            type: 'get',
            url: 'https://localhost:44382/CUSTO/CustoEquipaJson',
            dataType: "json",
            data: { ano: $("#select_ano").val() },
            success: function (data) {
                if (!jQuery.isEmptyObject(data)) {
                var dataprovider = new Array();
                var meses = ["Janeiro", "Fevereiro", "Março", "Abril", "Maio", "Junho", "Julho", "Agosto", "Setembro", "Outubro", "Novembro", "Dezembro"];
                var equipas = ["SAGE", "PHC", "Comunicação", "Comercial", "Desenvolvimento", "Gerência", "Direção", "Sistemas", "Planeamento e Suporte", "Dep. Técnico Interno", "Logística & Compras", "Consultores Externos", "Financeiro e Administrativo"]

                //Definir um objeto para guardar os dados que queremos ter no dataprovider
           
                console.log(data);
                //Definir os doze meses de um determinado ano no gráfico
                for (var i = 0; i < meses.length; i++) {
                    //console.log(data[i].mes)
                   // console.log(meses[i])
                    var tempdata = {};
                    Object.assign(tempdata, {
                        "Mês": meses[i]
                    });
                    for (var j = 0; j < data.length; j++) {
                        if (meses[i].toLowerCase() === data[j].mes.toLowerCase()) {
                            var equipa = data[j].equipas;
                            var total = data[j].total
                            console.log(data[j].equipas);
                            //Adicionar o resto dos campos ao objeto tempdata
                            Object.assign(tempdata, {
                                [equipa]: total,
                                "url": "https://localhost:44382/CUSTO/CustoEquipaDetalhe?ano=" + data[j].ano + "&mes=" + data[j].mes + "&equipa=" + data[j].equipas
                             
                            });
                          
                        }
                    }

                    if (Object.keys(tempdata).length >=2) {
                        dataprovider.push(tempdata);
                    }
                }
                
               

                var grafico = {
                    "type": "serial",
                    "theme": "light",
                    "legend": {
                        "horizontalGap": 10,
                        "maxColumns": 1,
                        "position": "right",
                        "useGraphSettings": true,
                        "markerSize": 10
                    },
                    "dataProvider": [],
                    "valueAxes": [{
                        "stackType": "regular",
                        "axisAlpha": 0.3,
                        "gridAlpha": 0
                    }],
                    "graphs": [],
                    "categoryField": "Mês",
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

             
                //Obter o tamanho do primeiro objeto
                var key = Object.keys(dataprovider[0]);
            
                for (var k = key.length - 1; k >= 1; k--) {

                   console.log(i)
                        grafico.graphs.push({
                            "balloonText": "<b>[[title]]</b><br><span style='font-size:14px'>[[category]]: <b>[[value]]</b></span>",
                            "fillAlphas": 0.8,
                            "labelText": "[[value]]€" + " - " + key[k],
                            "lineAlpha": 0.3,
                            "title": key[k],
                            "urlField": "url",
                            "type": "column",
                            "color": "#000000",
                            "valueField": key[k]
                        })
                    
                    console.log(key[k])
                }
                grafico.dataProvider = dataprovider;
                var chart = AmCharts.makeChart("chartdiv", grafico);
                    console.log(grafico)


                } else {
                    alert("Não existem custos associados ao ano selecionado");
                }
            }, error: function (err) {
                console.log(err);
            }
        });
    }
    
    getchart();
    $(document.body).on('change', '#select_ano', function () {
        getchart();

    })
});
