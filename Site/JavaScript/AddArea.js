
let x = document.getElementById("text");
let latitude;
let Longitude;
let jsonData;
let streetNamePlaceInput;
let villageNameInput;
document.getElementById("streetNamePlace").placeholder = "Street name here";
document.getElementById("villageName").placeholder = "Village/city name here";
function geolocationOrAdress(){
  streetNamePlaceInput = document.getElementById("streetNamePlace").value;
  villageNameInput = document.getElementById("villageName").value;
  if (document.getElementById("isAtPlace").checked){
    getLocation();
  }
  else{
    getCoordinates();
  }
}
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
  x.innerHTML = "Latitude: " + position.coords.latitude + "<br>Longitude: " + position.coords.longitude;
  getPlace().then(data=> console.log(data));
}


async function getPlace(){
    let Response = await fetch('https://api.opencagedata.com/geocode/v1/json?q=' + latitude + '+' + longitude + '&key=6e978319e06444d481d5ac3f328be3ef');
    let data = await Response.json()
    jsonData = data;
    x.innerHTML = jsonData.results[0].formatted;
    return data;
}

async function getCoordinates(){
  let Response = await fetch('https://api.opencagedata.com/geocode/v1/json?q=' + streetNamePlaceInput +  "%20" + villageNameInput + '&key=6e978319e06444d481d5ac3f328be3ef');
  let data = await Response.json()
  jsonData = data;
  x.innerHTML = "Latitude: " + jsonData.results[0].geometry.lat + "<br>Longitude: " + jsonData.results[0].geometry.lng;
  console.log(jsonData.results[0].geometry.lat + "+" + jsonData.results[0].geometry.lng);
}
