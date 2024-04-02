"use strict";
/*import { Loader } from "@googlemaps/js-api-loader"
const loader = new Loader({
    apiKey: "YOUR_API_KEY",
    version: "weekly",
    ...additionalOptions,
});

loader.load().then(async () => {
    const { Map } = await google.maps.importLibrary("maps");

    map = new Map(document.getElementById("map"), {
        center: { lat: -34.397, lng: 150.644 },
        zoom: 8,
    });
});*/

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

//contact popup
$('#contactDialog').popup();
$('#contactDialog2').popup();


