"use strict";

//These signalr methods are in place for future funtionalities
var connection = new signalR.HubConnectionBuilder().withUrl("/DataHub").build();
connection.on("ReceiveMessage", function (user, message) {

});
connection.start().then(function () {

    var connectionId = connection.connectionId;
    connection.invoke("SendLocations", connectionId);
}).catch(function (err) {
    return console.error(err.toString());
});
connection.on("LocationData", function (siteLocationsIn) {

});


