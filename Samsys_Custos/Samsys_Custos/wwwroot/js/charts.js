$(document).ready(function () {
    function getchart() {
        let saveData = $.ajax({
            type: 'get',
            url: 'https://localhost:44382/CUSTO/GeralJson',
            dataType: "json",
            data: { ano: $("#select_ano").val() },
            success: function (data) {
                console.log(data)
                if (!jQuery.isEmptyObject(data)) {

                    var dataprovider = new Array();
                    var meses = ["Janeiro", "Fevereiro", "Março", "Abril", "Maio", "Junho", "Julho", "Agosto", "Setembro", "Outubro", "Novembro", "Dezembro"];
                    var categorias = ["Viaturas", "Pessoal", "Infraestrutura", "Serviços Externos","Comunicações","Merchandising"];

                    for (var j = 0; j < meses.length; j++) {

                        var tempdata = {};
                        Object.assign(tempdata, {
                            "Mês": meses[j]
                        });

                        for (var i = 0; i < data.length; i++) {
                            if (data[i].mes.toLowerCase() === meses[j].toLowerCase()) {
                                var Categoria = data[i].nomeCompleto;
                                var Total = data[i].total;
                                var url = "https://localhost:44382/custo/custos_filtro?categoria=" + data[i].nomeCompleto + "&ano=" + data[i].ano + "&mes=" + data[i].mes
                                var urlName = "url" + Categoria;
                                console.log(data[i].mes)
                                console.log(data[i].ano)
                                Object.assign(tempdata, {
                                    [Categoria]: Total,
                                    [urlName]: url
                                });
                            }
                        }
                        if (Object.keys(tempdata).length > 1) {
                            dataprovider.push(tempdata);
                        }

                    }


                    var settings = {
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
                            "axisAlpha": 1,
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


                   var keys = Object.keys(dataprovider[0]);

                    for (var k = 0; k < categorias.length; k++) {
                        console.log(keys[k])
                        var myUrl = "url" + categorias[k];
                        settings.graphs.push({
                            "balloonText": "<b>[[title]]</b><br><span style='font-size:14px'>[[category]]: <b>[[value]]</b></span>",
                            "fillAlphas": 1,
                            "urlField": myUrl,
                            "id": "AmGraph-" + k,
                            "title": categorias[k],
                            "labelText": "[[value]]€" + " - " + categorias[k],
                            "type": "column",
                            "color": "#000000",
                            "valueField": categorias[k]
                        });
                    }
                    settings.dataProvider = dataprovider;

                    var chart = AmCharts.makeChart("chartdiv", settings);

                    console.log(settings);


                } else {
                    alert("Não existem custos associados ao ano selecionado");
                }
            },
            error: function(err){
                console.log(err);
            }
        });
    }

    getchart();
    $(document.body).on('change', '#select_ano', function () {
        getchart();

    })

});
