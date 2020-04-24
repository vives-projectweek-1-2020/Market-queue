var inputPlace=" ";
var inputTime=0;
var inputArriveTime =0;
var confirmation=""
var confirmBox = document.getElementById("confirmBlank");
confirmBox.style.display = "none";
var confirmButtn = document.getElementById("confirmButton");

function GetPlace(){
     inputPlace = document.getElementById("visitPlace").value
            
}

function GetArriveTime(){
    inputArriveTime = document.getElementById("arriveTime").value

}

function GetVisitTime(){
      
    inputTime = document.getElementById("visitTime").value
 
}



function ToConsole(){
    console.log("visit place= " + inputPlace)
    console.log("arrive time= " + inputArriveTime +" mins")
    console.log("visit time= " + inputTime +" mins")
}

function ShowConfimation(){

    confirmation="Please confirm:<br><br>" +"-Place: " +inputPlace + "<br>-Arrive time: "+inputArriveTime+" mins"+ "<br>-Visit time: "+inputTime +" mins"
    document.getElementById('confirm').innerHTML = confirmation
    confirmBox.style.display = "block"
}

document.getElementById('EnterButton').onclick = () => {
    
    GetPlace();
    GetArriveTime();
    GetVisitTime();
    if(inputTime>0 && inputArriveTime>=0)
    {
        ShowConfimation();
    }
}
var getter;
document.getElementById('confirmButton').onclick = () => {
    ToConsole();
    getCoordinates(inputPlace,function(geojson){
        var MyLatitude = geojson.results[0].geometry.lat;
        var MyLongitude = geojson.results[0].geometry.lng;

        getJSON('http://91.181.93.103:3040/get/area?longitude=' + MyLongitude + '&latitude=' + MyLatitude + '&return=area_id',function(json){
            var areaID = JSON.parse(json)[0].id;
              getJSON('http://91.181.93.103:3040/add/visitor?area_id=' + areaID + '&duration=' + inputTime + '&offset=' + inputArriveTime,function(json2){
                  console.log(json2);
                  if(json2.startsWith("SUCCESS"))
                  {
                    document.getElementById('confirm').innerHTML = "<br><br>Thank you for your contribution!<br><br>"
                    document.getElementById('cancelButton').innerHTML = "Back"
                    confirmButtn.style.display ="none"
                  }
                  else
                  {
                    document.getElementById('confirm').innerHTML = "<br><br>Something went wrong!<br><br>"
                    document.getElementById('cancelButton').innerHTML = "Back"
                    confirmButtn.style.display ="none"
                  }
              });
        })
    })
   
    
}
document.getElementById('cancelButton').onclick = () => {
    confirmButtn.style.display ="initial"
    confirmBox.style.display = "none"
    document.getElementById('cancelButton').innerHTML = "Cancel"
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

async function getCoordinates(location,callback){
    let Response = await fetch('https://api.opencagedata.com/geocode/v1/json?q='+ location + '&key=6e978319e06444d481d5ac3f328be3ef');
    let data = await Response.json()
    callback(data);
  }