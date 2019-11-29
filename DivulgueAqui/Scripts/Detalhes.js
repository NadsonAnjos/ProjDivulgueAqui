function initMap() {
    var uluru = { lat: -12.9715854, lng: -38.5132827 };
    var map = new google.maps.Map(
        document.getElementById('map'), { zoom: 19, center: uluru });
    placeMarkerAndPanTo(map);
}

var marker;

function deleteMarkers() {
    clearMarkers();
    markers = [];
}


function placeMarkerAndPanTo(map) {
    var latLng = document.getElementById("localizacao").textContent;
    console.log(latLng);

    var latLngSplit = latLng.replace('(', '');
    latLngSplit = latLngSplit.replace(')', '');
    var localizacao = latLngSplit.split(', ');
    console.log(parseFloat(localizacao[0], 10));
    console.log(parseFloat(localizacao[1], 10));

    var coordenadas = { lat: parseFloat(localizacao[0], 10), lng: parseFloat(localizacao[1],10) }

    if (marker) {
        marker.setPosition(coordenadas);
    } else {
        marker = new google.maps.Marker({
            position: coordenadas,
            map: map
        });
    }
    map.panTo(coordenadas);
}