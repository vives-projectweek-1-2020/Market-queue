var inputPlace=" ";
var inputTime=0;
var inputArriveTime =0;


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



document.getElementById('EnterButton').onclick = () => {
    
    GetPlace();
    GetArriveTime();
    GetVisitTime();
    if(
        window.confirm("Please confirm:\n" +"-Place: " +inputPlace + "\n-Arrive time: "+inputArriveTime+" mins"+ "\n-Visit time: "+inputTime +" mins")
    ){
    ToConsole();
    }
    
}