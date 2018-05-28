
        $(document).ready(function () {
            $(document.body).on('change', '#tipo_categoria', function () {

                $("#rubrica").empty();
                $.ajax({
                    type: 'POST',
                    url: 'https://localhost:44339/CUSTO/getrubrica',
                    dataType: 'json',
                    data: { id: $("#tipo_categoria").val() },


                    success: function (data) {
                        var myarray = JSON.parse(data)
                        $.each(myarray, function (i, subcategory) {

                            $("#rubrica").append('<option value="' + subcategory.Value + '">' +
                                subcategory.Text + '</option>');

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
  