// Call the dataTables jQuery plugin
$(document).ready(function() {
    var tables = $('#dataTable').DataTable({
        "bInfo": false, //Dont display info e.g. "Showing 1 to 4 of 4 entries"
        "paging": false,//Dont want paging                
        "bPaginate": false,//Dont want paging  
    });
   
});
