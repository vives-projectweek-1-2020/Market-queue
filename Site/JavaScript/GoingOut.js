function GetPlace(){
    var inputPlace = document.getElementById("visitPlace").value
            
     
    console.log("visit place= " + inputPlace)
}

function GetArriveTime(){
    var inputArriveTime = document.getElementById("arriveTime").value
            
     
    console.log("arrive time= " + inputArriveTime +"mins")

}

function GetVisitTime(){
      
    var inputTime = document.getElementById("visitTime").value
            
     
    console.log("visit time= " + inputTime +"mins")
    
    
}





document.getElementById('EnterButton').onclick = () => {
    GetPlace();
    GetArriveTime();
    GetVisitTime();
    
}