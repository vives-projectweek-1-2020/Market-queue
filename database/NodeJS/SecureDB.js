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

//AREA
app.get('/get/area', function(req, res) {
  if(JSON.stringify(req.query) == "{}")
  {
    let query = 'SELECT * FROM market_queue.area;';
    console.log(query);
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
});
app.get('/get', function(req, res) {
  res.end(JSON.stringify(req.query));
});
app.get('/add/area/:columns/:values', function(req, res) {
  let query = 'INSERT INTO market_queue.area (' + req.params.columns + ') VALUES (' + req.params.values + ');';
  console.log(query);
  connection.query(query, (error, rows)=>{
      if(!error){ res.end("SUCCESS: " + JSON.stringify(rows)); }
      else { res.end("ERROR: " + JSON.stringify(error)); }
    });
});

//VISITOR
app.get('/get/visitor', function(req, res) {
  let query = 'SELECT * FROM market_queue.visitor;';
  console.log(query);
  connection.query(query, (error, rows)=>{
      if(!error){ res.end(JSON.stringify(rows)); }
      else { res.end("ERROR: " + JSON.stringify(error)); }
    });
});
app.get('/add/visitor/:columns/:values', function(req, res) {
  let query = 'INSERT INTO market_queue.visitor (' + req.params.columns + ') VALUES (' + req.params.values + ');';
  console.log(query);
  connection.query(query, (error)=>{
      if(!error){ res.end("SUCCESS: " + JSON.stringify(rows)); }
      else { res.end("ERROR: " + JSON.stringify(error)); }
    });
});
