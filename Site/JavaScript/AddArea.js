
let x = document.getElementById("text");
let latitude;
let Longitude;
let jsonData;

function getLocation() {
  if (navigator.geolocation) {
    navigator.geolocation.getCurrentPosition(showPosition);
  } else { 
    x.innerHTML = "Geolocation is not supported by this browser.";
  }
}

function showPosition(position) {
latitude = position.coords.latitude;
longitude = position.coords.longitude;
 // x.innerHTML = "Latitude: " + position.coords.latitude + 
  //"<br>Longitude: " + position.coords.longitude;
getPlace().then(data=> console.log(data));
}


async function getPlace(){
    let Response = await fetch('https://api.opencagedata.com/geocode/v1/json?q=' + latitude + '+' + longitude + '&key=6e978319e06444d481d5ac3f328be3ef');
    let data = await Response.json()
    jsonData = data;
    x.innerHTML = jsonData.results[0].formatted;
    return data;
}

