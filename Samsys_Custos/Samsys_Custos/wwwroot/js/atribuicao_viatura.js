
$(document).ready(function () {
   
            $(document.body).on('change', '#viatura', function () {
              
              
                $.ajax({
                    type: 'POST',
                    url: 'https://localhost:44382/ATRIBUICAO/AtribuicaoViaturaJson',
                    dataType: 'json',
                    data: { id: $("#viatura").val() },
                    success: function (data) {
                        console.log("Entrei3")
                        var id = $("#viatura").val(); 
                        $("#colaborador option:selected").prop("selected", false);
            
                       
                        console.log(data.length);
                        for (var i = 0; i < data.length; i++) {
                            if (data[i].id_viatura == id) {
                                console.log("id_colaborador" + data[i].id_colaborador)
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
  