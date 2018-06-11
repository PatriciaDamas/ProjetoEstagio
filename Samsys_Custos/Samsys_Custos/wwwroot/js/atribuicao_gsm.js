
$(document).ready(function () {
   
            $(document.body).on('change', '#gsm', function () {
              
              
                $.ajax({
                    type: 'POST',
                    url: 'https://localhost:44382/ATRIBUICAO/AtribuicaoGsmJson',
                    dataType: 'json',
                    data: { id: $("#gsm").val() },
                    success: function (data) {
                        console.log("Entrei3")
                        var id = $("#gsm").val(); 
                        $("#colaborador option:selected").prop("selected", false);
            
                       
                        console.log(data.length);
                       
                            for (var i = 0; i < data.length; i++) {
                                if (data[i].id_gsm == id) {
                                    console.log("id_gsm" + data[i].id_colaborador)
                                    $("#colaborador option[value=" + data[i].id_colaborador + "]")
                                        .prop("selected", true);

                                }
                               

                            }
                        
                       

                       

                       
                    },
                    error: function (ex) {
                       
                        alert('ERR');
                    }
                });
                return false;
            })
          
        });
  