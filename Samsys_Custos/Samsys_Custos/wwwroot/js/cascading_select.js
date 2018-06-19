
$(document).ready(function () {
    console.log("Entrei")
            $(document.body).on('change', '.tipo_categoria', function () {

                $(".rubrica").empty();
                $.ajax({
                    type: 'POST',
                    url: 'https://localhost:44382/CUSTO/getrubrica',
                    dataType: 'json',
                    data: { id: $(".tipo_categoria").val() },


                    success: function (data) {
                        var myarray = JSON.parse(data)
                        $.each(myarray, function (i, subcategory) {

                            $(".rubrica").append('<option value="' + subcategory.Value + '">' +
                                subcategory.Text + '</option>');
                            console.log(subcategory.Value)

                        });
                    },
                    error: function (ex) {
                        console.log(ex);
                        alert('Failed to retrieve sub-category.' + ex);
                    }
                });
                return false;
            })
           
            
        });
  