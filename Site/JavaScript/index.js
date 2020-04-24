let status = document.getElementById("CurrentStatusAroundYou");
var latitude;
var longitude;

if (navigator.geolocation) {
    navigator.geolocation.getCurrentPosition(function(position){
        latitude = position.coords.latitude;
        longitude = position.coords.longitude;
        getJSON('https://91.181.93.103:3040/get/area?longitude=' + longitude + '&latitude=' + latitude + '&return=visitor',function(json){
            var count = JSON.parse(json)[0].total;
            console.log(count);
            status.innerHTML = count;
        })
    });
}
else
{ 
    status.innerHTML = "Geolocation is not supported by this browser.";
}


var getJSON = function(url, callback) {
    var xmlhttp = new XMLHttpRequest();
    
    xmlhttp.onreadystatechange = function() {
      if (this.readyState == 4 && this.status == 200) {
        callback(this.responseText);
      }
    };
    xmlhttp.open("GET", url, true);
    xmlhttp.send();
};