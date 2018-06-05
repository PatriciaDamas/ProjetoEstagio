$(document).ready(function () {
    function getchart() {
        let saveData = $.ajax({
            type: 'get',
            url: 'https://localhost:44363/CUSTO/GeralJson',
            dataType: "json",
            data: { ano: $("#select_ano").val() },
            success: function (data) {
                var dataprovider = new Array();
                var meses = ["Janeiro", "Fevereiro", "Março", "Abril", "Maio", "Junho", "Julho", "Agosto", "Setembro", "Outubro", "Novembro", "Dezembro"];
                var keys;
                var ano = $("#select_ano").val();

                
                for (var j = 0; j < meses.length; j++) {

                    var tempdata = {};
                    Object.assign(tempdata, {
                        "Mês": meses[j]
                    });

                    for (var i = 0; i < data.length; i++) {
                        if (data[i].mes.toLowerCase() === meses[j].toLowerCase()) {
                            var Categoria = data[i].nomeCompleto;
                            var Total = data[i].total;
                            Object.assign(tempdata, {
                                [Categoria]: Total
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


                keys = Object.keys(dataprovider[0]);

                for (var k = 1; k < keys.length; k++) {
                    settings.graphs.push({
                        "balloonText": "<b>[[title]]</b><br><span style='font-size:14px'>[[category]]: <b>[[value]]</b></span>",
                        "fillAlphas": 1,
                        "id": "AmGraph-" + k,
                        "title": keys[k],
                        "labelText":"[[value]]€" + " - " + keys[k], 
                        "type": "column",
                        "valueField": keys[k]
                    });
                }
                settings.dataProvider = dataprovider;
                var chart = AmCharts.makeChart("chartdiv", settings);

                console.log(settings);

               
               
            }
        });
    }

    getchart();
    $(document.body).on('change', '#select_ano', function () {
        getchart();

    })

});
