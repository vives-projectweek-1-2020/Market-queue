const express = require('express');
const app = express();
const port = 3000;

app.get('/:databasequery/:columns', function (req, res) {
  var decodedDBquery = decodeURI(req.params["databasequery"]);
  var decodedColumns = decodeURI(req.params["columns"]);
  var columns = GetColumns(decodedColumns);
  var textToSend = "";
  var query = "" + GetQuery(decodedDBquery);
  console.log("Request: '" + query + "' with columns '" + columns + "'");
  RunQueryWithResult(query,columns,function(results){
    connection = mysql.createConnection({
      host: "localhost",
      user: "root",
      password: ""
    });
    (results).forEach(result => {
      textToSend += result + `<br>`
    });
  res.send(textToSend);
  });
})
app.get('/:databasequery', function (req, res) {
  var decodedDBquery = decodeURI(req.params["databasequery"]);
  var textToSend = "";
  var query = "" + GetQuery(decodedDBquery);
  console.log("Request: '" + query + "'");
  RunQuery(query,function(results){
    connection = mysql.createConnection({
      host: "localhost",
      user: "root",
      password: ""
    });
    (results).forEach(result => {
      textToSend += result + `<br>`
    });
  res.send(textToSend);
  });
  
  
})

app.listen(port, () => console.log(`Example app listening at http://localhost:${port}`));


var mysql = require('mysql');
var connection = mysql.createConnection({
  host: "localhost",
  user: "root",
  password: ""
});

function GetColumns(input)
{
  return input.split(',');
}

function GetQuery(url){
  if(url.startsWith('database '))
  {
    var query = url.substr(9);
    return query;
  }
}

function RunQuery(query,callback)
{
  var returningResult = [];
  connection.connect(function(err) {
    if (err) throw err;
    connection.query(query, function (err, rows, fields) {
      if (err) throw err;
      });
      return callback(returningResult);
    });
}
function RunQueryWithResult(query,columns,callback)
{
  var returningResult = [];
  connection.connect(function(err) {
    if (err) throw err;
    connection.query(query, function (err, rows, fields) {
      if (err) throw err;
      rows.forEach(row => {
        //returningResult.push(row);
        var rowData = "";
        
          columns.forEach(column => {
            rowData += row[column] + `\t,\t`;
          });
          returningResult.push(rowData.substr(0,rowData.length-2));
        
      });
      return callback(returningResult);
    });
  });
}