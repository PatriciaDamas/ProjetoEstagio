﻿    var currentdate = new Date();
var datetime = "Last Sync: " + currentdate.getDate() + "/"
    + (currentdate.getMonth() + 1) + "/"
    + currentdate.getFullYear() + " @ "
    + currentdate.getHours() + ":"
    + currentdate.getMinutes();
    console.log(datetime);
    document.getElementById("currentdate").value = datetime;
