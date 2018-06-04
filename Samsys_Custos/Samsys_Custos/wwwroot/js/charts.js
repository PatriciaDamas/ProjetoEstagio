$("document").ready(function () {
    let saveData = $.ajax({
        type: 'get',
        url: 'https://localhost:44363/CUSTO/GeralJson',
        dataType: "json",
        success: function (data) {
            var parseddata = JSON.parse(data);
            console.log(data);
        }
    });
})
