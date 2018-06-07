
$(document).ready(function () {

    //Vai buscar o nome da categoria nivel 1
    var catArray = new Array();
    $('#mytable tr').each(function () {
        catArray.push($(this).find(".categoria").html());
    });

    function doAjax(ArrCount) {
        var e_id = catArray[ArrCount];
        $.ajax({
            type: 'POST',
            url: 'https://localhost:44382/CUSTO/getpai',
            dataType: 'json',
            data: { id: e_id },


            success: function (data) {
                if (ArrCount > 0) {
                    $('#mytable tr').eq(ArrCount).find('td').eq(5).html(data);
                }

                ArrCount++;
                if (ArrCount < catArray.length) {
                    doAjax(ArrCount);
                }
                else {
                    $("#mytable").show();
                }

            },
            error: function (ex) {
                console.log(ex);
                alert('Failed to retrieve sub-category.' + ex);
            }
        });
    }
    doAjax(0);
    });

