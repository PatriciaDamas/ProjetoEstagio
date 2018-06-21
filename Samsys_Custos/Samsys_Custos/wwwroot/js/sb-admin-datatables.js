// Call the dataTables jQuery plugin
$(document).ready(function() {
  var tables =  $('#dataTable').DataTable();
    tables.buttons.remove("next");
});
