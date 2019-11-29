    // Initialize and add the map
    function initMap() {
            // The location of Uluru
            var uluru = {lat: -12.9715854, lng: -38.5132827 };
    // The map, centered at Uluru
    var map = new google.maps.Map(
                document.getElementById('map'), {zoom: 19, center: uluru });

            map.addListener('click', function (e) {
        placeMarkerAndPanTo(e.latLng, map);
    });

}

var marker;

        function deleteMarkers() {
        clearMarkers();
    markers = [];
}


        function placeMarkerAndPanTo(latLng, map) {
            if (marker) {
        marker.setPosition(latLng);
    } else {
        marker = new google.maps.Marker({
            position: latLng,
            map: map
        });
    }
    map.panTo(latLng);
            var loc = document.getElementById("Location");
    loc.value = latLng;
}