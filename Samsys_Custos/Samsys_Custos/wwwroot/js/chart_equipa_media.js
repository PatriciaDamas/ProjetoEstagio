$(document).ready(function () {
    function getchart() {
        let saveData = $.ajax({
            type: 'get',
            url: 'https://localhost:44382/CUSTO/CustoEquipaMediaJson',
            dataType: "json",
            data: { ano: $("#select_ano_custos").val() },
            success: function (data) {
                if (!jQuery.isEmptyObject(data)) {
                var dataprovider = new Array();
                var meses = ["Janeiro", "Fevereiro", "Março", "Abril", "Maio", "Junho", "Julho", "Agosto", "Setembro", "Outubro", "Novembro", "Dezembro"];
                var equipas = ["SAGE", "PHC", "Comunicação", "Comercial", "Desenvolvimento", "Gerência", "Direção", "Sistemas", "Planeamento e Suporte", "Dep. Técnico Interno", "Logistica e Compras", "Consultores Externos", "Financeiro e Administrativo"]

                //Definir um objeto para guardar os dados que queremos ter no dataprovider
           
                console.log(data);
                //Definir os doze meses de um determinado ano no gráfico
                    for (var i = 0; i < meses.length; i++) {
                        var tempdata = {};
                        Object.assign(tempdata, {
                            "Mês": meses[i]
                        });
                        for (var j = 0; j < data.length; j++) {
                            if (meses[i].toLowerCase() === data[j].mes.toLowerCase()) {

                                var equipa = data[j].equipas;
                                var total = data[j].total;
                                Object.assign(tempdata, {
                                    [equipa]: total

                                });
                            }
                        }

                        if (Object.keys(tempdata).length >= 2) {
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

             
                
            
                for (var k = equipas.length - 1 ; k >= 0; k--) {
                    console.log(k);

                   
                        grafico.graphs.push({
                            "balloonText": "<b>[[title]]</b><br><span style='font-size:14px'>[[category]]: <b>[[value]]</b></span>",
                            "fillAlphas": 0.8,
                            "labelText": "[[value]]€" + " - " + equipas[k],
                            "lineAlpha": 0.3,
                            "title": equipas[k],
                            "type": "column",
                            "color": "#000000",
                            "valueField": equipas[k]
                        })
                    
            
                }
                    grafico.dataProvider = dataprovider;
                    var chart = AmCharts.makeChart("chartEquipaMedia", grafico);
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
    $(document.body).on('change', '#select_ano_custos', function () {
        getchart();

    })
});
