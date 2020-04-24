const express = require('express');
const app = express();
const port = 3000;
const cors = require('cors')
const https = require('https');
const fs = require('fs');

app.use(cors({
 origin(origin, callback) {
 return callback(null, true)
 },
 optionsSuccessStatus: 200,
 credentials: true,
}))


const mysql = require('mysql');
let connection = mysql.createConnection({
    host: "localhost",
    user: "root",
    password: ""
  });

var key = fs.readFileSync('./selfsigned.key');
var cert = fs.readFileSync('./selfsigned.crt');
var options = {
  key: key,
  cert: cert
};
https.createServer(options, app).listen(port, () => console.log(`Example app listening at http://localhost:${port}`));

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
  else if (req.query.latitude != undefined && req.query.longitude != undefined && req.query.return == "area_id" && Object.keys(req.query).length == 3)
  {
    let query = 'SELECT id FROM market_queue.area WHERE '
        + 'latitude>('+req.query.latitude+'-0.01) AND latitude<('+req.query.latitude+'+0.01) '
        + 'AND longitude>('+req.query.longitude+'-0.01) AND longitude<('+req.query.longitude+'+0.01);'

        connection.query(query, (error, rows)=>{
          if(!error){ res.end(JSON.stringify(rows)); }
          else { res.end("ERROR: " + JSON.stringify(error)); }
        });
  }
  else if (req.query.latitude != undefined && req.query.longitude != undefined && req.query.return == "visitor" && Object.keys(req.query).length == 3)
  {
    let query = 'SELECT COUNT(*) AS total FROM market_queue.visitor '
                + 'JOIN market_queue.area ON market_queue.area.id = market_queue.visitor.area_id '
                + 'WHERE latitude>('+req.query.latitude+'-0.01) AND latitude<('+req.query.latitude+'+0.01) '
                + 'AND longitude>('+req.query.longitude+'-0.01) AND longitude<('+req.query.longitude+'+0.01) '
                + 'AND DATE_ADD(check_in_time,INTERVAL duration MINUTE)>NOW() '
                + 'AND check_in_time<NOW();';
    connection.query(query, (error, rows)=>{
      if(!error){ res.end(JSON.stringify(rows)); }
      else { res.end("ERROR: " + JSON.stringify(error)); }
    });
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
    res.end("ERROR: wrong input!");
  }
});

//GET VISITOR
app.get('/get/visitor', function(req, res) {
  if(JSON.stringify(req.query) == "{}")
  {
    let query = 'SELECT * FROM market_queue.visitor where DATE_ADD(check_in_time,INTERVAL duration MINUTE)>NOW() AND check_in_time<NOW();';
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
    let query = 'SELECT * FROM market_queue.visitor where id =' + req.query.id + " AND DATE_ADD(check_in_time,INTERVAL duration MINUTE)>NOW() AND check_in_time<NOW();";
    connection.query(query, (error, rows)=>{
      if(!error){ res.end(JSON.stringify(rows)); }
      else { res.end("ERROR: " + JSON.stringify(error)); }
    });
  }
  else if (req.query.id != undefined && req.query.all == "true" && Object.keys(req.query).length == 2)
  {
    let query = 'SELECT * FROM market_queue.visitor where id =' + req.query.id + ";";
    connection.query(query, (error, rows)=>{
      if(!error){ res.end(JSON.stringify(rows)); }
      else { res.end("ERROR: " + JSON.stringify(error)); }
    });
  }
  else if (req.query.area_id != undefined && Object.keys(req.query).length == 1)
  {
    let query = 'SELECT * FROM market_queue.visitor where area_id =' + req.query.area_id + " AND DATE_ADD(check_in_time,INTERVAL duration MINUTE)>NOW() AND check_in_time<NOW();";
    connection.query(query, (error, rows)=>{
      if(!error){ res.end(JSON.stringify(rows)); }
      else { res.end("ERROR: " + JSON.stringify(error)); }
    });
  }
  else if (req.query.area_id != undefined && req.query.all == "true" && Object.keys(req.query).length == 2)
  {
    let query = 'SELECT * FROM market_queue.visitor where area_id =' + req.query.area_id + ";";
    connection.query(query, (error, rows)=>{
      if(!error){ res.end(JSON.stringify(rows)); }
      else { res.end("ERROR: " + JSON.stringify(error)); }
    });
  }
  else if (req.query.area_id != undefined && req.query.return == "count" && Object.keys(req.query).length == 2)
  {
    let query = 'SELECT COUNT(*) AS total FROM market_queue.visitor where area_id =' + req.query.area_id + " AND DATE_ADD(check_in_time,INTERVAL duration MINUTE)>NOW() AND check_in_time<NOW();";
    connection.query(query, (error, rows)=>{
      if(!error){ res.end(JSON.stringify(rows)); }
      else { res.end("ERROR: " + JSON.stringify(error)); }
    });
  }
  else if (req.query.area_id != undefined && req.query.return == "count" && req.query.all == "true" && Object.keys(req.query).length == 3)
  {
    let query = 'SELECT COUNT(*) AS total FROM market_queue.visitor where area_id =' + req.query.area_id + ";";
    connection.query(query, (error, rows)=>{
      if(!error){ res.end(JSON.stringify(rows)); }
      else { res.end("ERROR: " + JSON.stringify(error)); }
    });
  }
  else
  {
    res.end("ERROR: wrong input!");
  }
  
});
//ADD VISITOR
app.get('/add/visitor', function(req, res) {
  if(JSON.stringify(req.query) == "{}")
  {
    res.end("ERROR: no data given!");
  }
  else if(req.query.area_id != undefined && req.query.duration != undefined && Object.keys(req.query).length == 2)
  {
    let query = 'INSERT INTO market_queue.visitor (area_id,duration) VALUES (' + req.query.area_id + ',' + req.query.duration + ');';
    connection.query(query, (error,rows)=>{
      if(!error){ res.end("SUCCESS: " + JSON.stringify(rows)); }
      else { res.end("ERROR: " + JSON.stringify(error)); }
    });
  }
  else if(req.query.area_id != undefined && req.query.duration != undefined && req.query.offset != undefined && Object.keys(req.query).length == 3)
  {
    let query = 'INSERT INTO market_queue.visitor (area_id,check_in_time, duration) VALUES (' 
              + req.query.area_id + ',DATE_ADD(NOW(),INTERVAL ' + req.query.offset + ' MINUTE),' + req.query.duration + ');';
    connection.query(query, (error, rows)=>{
        if(!error){ res.end("SUCCESS: " + JSON.stringify(rows)); }
        else { res.end("ERROR: " + JSON.stringify(error)); }
      });
  }
  else
  {
    res.end("ERROR: wrong input!");
  }
});
