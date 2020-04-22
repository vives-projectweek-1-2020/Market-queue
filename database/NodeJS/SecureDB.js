const express = require('express');
const app = express();
const port = 3000;

const mysql = require('mysql');
let connection = mysql.createConnection({
    host: "localhost",
    user: "root",
    password: ""
  });

app.listen(port, () => console.log(`Example app listening at http://localhost:${port}`));

connection.connect((error)=>{
  if (error) { console.log("Can't connect to DB\nerrorOR: " + JSON.stringify(error));}
  else { console.log("Connected to DB"); }
})

//GET AREA
app.get('/get/area', function(req, res) {
  if(JSON.stringify(req.query) == "{}")
  {
    let query = 'SELECT * FROM market_queue.area;';
    connection.query(query, (error, rows)=>{
        if(!error){ res.end(JSON.stringify(rows)); }
        else { res.end("ERROR: " + JSON.stringify(error)); }
      });
  }
  else if (req.query.id != undefined && Object.keys(req.query).length == 1)
  {
    let query = 'SELECT * FROM market_queue.area where id =' + req.query.id + ";";
    connection.query(query, (error, rows)=>{
      if(!error){ res.end(JSON.stringify(rows)); }
      else { res.end("ERROR: " + JSON.stringify(error)); }
    });
  }
  else if (req.query.latitude != undefined && Object.keys(req.query).length == 1)
  {
    let query = 'SELECT * FROM market_queue.area where latitude =' + req.query.latitude + ";";
    connection.query(query, (error, rows)=>{
      if(!error){ res.end(JSON.stringify(rows)); }
      else { res.end("ERROR: " + JSON.stringify(error)); }
    });
  }
  else if (req.query.longitude != undefined && Object.keys(req.query).length == 1)
  {
    let query = 'SELECT * FROM market_queue.area where longitude =' + req.query.longitude + ";";
    connection.query(query, (error, rows)=>{
      if(!error){ res.end(JSON.stringify(rows)); }
      else { res.end("ERROR: " + JSON.stringify(error)); }
    });
  }
  else if (req.query.latitude != undefined && req.query.longitude != undefined && Object.keys(req.query).length == 2)
  {
    res.end("=> GET NEAREST");
  }
  else
  {
    res.end("ERROR: wrong input!");
  }
});
//ADD AREA
app.get('/add/area', function(req, res) {
  if(JSON.stringify(req.query) == "{}")
  {
    res.end("ERROR: no data given!");
  }
  else if(req.query.latitude != undefined && req.query.longitude != undefined && Object.keys(req.query).length == 2)
  {
    let query = 'INSERT INTO market_queue.area (latitude,longitude) VALUES (' + req.query.latitude + ',' + req.query.longitude + ');';
    connection.query(query, (error, rows)=>{
        if(!error){ res.end("SUCCESS: " + JSON.stringify(rows)); }
        else { res.end("ERROR: " + JSON.stringify(error)); }
      });
  }
  else
  {
    console.log(req.query);
    res.end("ERROR: wrong input!");
  }
});

//VISITOR
app.get('/get/visitor', function(req, res) {
  if(JSON.stringify(req.query) == "{}")
  {
    let query = 'SELECT * FROM market_queue.visitor where DATE_ADD(check_in_time,INTERVAL duration MINUTE)>NOW();';
    connection.query(query, (error, rows)=>{
      if(!error){ res.end(JSON.stringify(rows)); }
      else { res.end("ERROR: " + JSON.stringify(error)); }
    });
  }
  else if (req.query.all == "true" && Object.keys(req.query).length == 1)
  {
    let query = 'SELECT * FROM market_queue.visitor;';
    connection.query(query, (error, rows)=>{
      if(!error){ res.end(JSON.stringify(rows)); }
      else { res.end("ERROR: " + JSON.stringify(error)); }
    });
  }
  else if (req.query.id != undefined && Object.keys(req.query).length == 1)
  {
    let query = 'SELECT * FROM market_queue.visitor where id =' + req.query.id + " AND DATE_ADD(check_in_time,INTERVAL duration MINUTE)>NOW();";
    connection.query(query, (error, rows)=>{
      if(!error){ res.end(JSON.stringify(rows)); }
      else { res.end("ERROR: " + JSON.stringify(error)); }
    });
  }
  else if (req.query.id != undefined && req.query.all == true && Object.keys(req.query).length == 2)
  {
    let query = 'SELECT * FROM market_queue.visitor where id =' + req.query.id + ";";
    connection.query(query, (error, rows)=>{
      if(!error){ res.end(JSON.stringify(rows)); }
      else { res.end("ERROR: " + JSON.stringify(error)); }
    });
  }
  else if (req.query.area_id != undefined && Object.keys(req.query).length == 1)
  {
    let query = 'SELECT * FROM market_queue.visitor where area_id =' + req.query.area_id + " AND DATE_ADD(check_in_time,INTERVAL duration MINUTE)>NOW();";
    connection.query(query, (error, rows)=>{
      if(!error){ res.end(JSON.stringify(rows)); }
      else { res.end("ERROR: " + JSON.stringify(error)); }
    });
  }
  else if (req.query.area_id != undefined && req.query.all == true && Object.keys(req.query).length == 2)
  {
    let query = 'SELECT * FROM market_queue.visitor where area_id =' + req.query.area_id + ";";
    connection.query(query, (error, rows)=>{
      if(!error){ res.end(JSON.stringify(rows)); }
      else { res.end("ERROR: " + JSON.stringify(error)); }
    });
  }
  else
  {
    console.log(req.query);
    res.end("ERROR: wrong input!");
  }
  
});
app.get('/add/visitor/:columns/:values', function(req, res) {
  let query = 'INSERT INTO market_queue.visitor (' + req.params.columns + ') VALUES (' + req.params.values + ');';
  console.log(query);
  connection.query(query, (error)=>{
      if(!error){ res.end("SUCCESS: " + JSON.stringify(rows)); }
      else { res.end("ERROR: " + JSON.stringify(error)); }
    });
});
/*
else if(req.query.area_id != undefined && req.query.duration != undefined && req.query.offset != undefined && Object.keys(req.query).length == 3)
  {
    let query = 'INSERT INTO market_queue.area (area_id,check_in_time,duration) VALUES (' 
              + req.query.area_id + ',ADDTIME(current_time(), ' + req.query.offset + '),' + req.query.duration + ');';
    connection.query(query, (error, rows)=>{
        if(!error){ res.end("SUCCESS: " + JSON.stringify(rows)); }
        else { res.end("ERROR: " + JSON.stringify(error)); }
      });
  }
*/