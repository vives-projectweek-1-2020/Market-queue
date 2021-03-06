﻿using System;
using System.Diagnostics;
using System.Collections;
using System.IO;
using System.Threading;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Serialization;
using DatabaseLibrary;

namespace InsertValues
{
    class Program
    {
        private static Database database;
        private static string[] queries =
        {
            "DROP DATABASE IF EXISTS market_queue;",
            "CREATE DATABASE market_queue;",
            "USE market_queue;",
            "CREATE TABLE visitor(id int(5) NOT NULL AUTO_INCREMENT,area_id int(5) NOT NULL,check_in_time DATETIME NOT NULL DEFAULT current_time(),duration SMALLINT UNSIGNED NOT NULL,PRIMARY KEY(id));",
            "CREATE TABLE area(id int(5) NOT NULL AUTO_INCREMENT,latitude DECIMAL(15,13) NOT NULL,longitude DECIMAL(15,13) NOT NULL,PRIMARY KEY(id));"
        };
        static void Main(string[] args)
        {
            database = new Database();
            foreach(string query in queries)
            {
                ExecuteNonQuery(query);
            }
            string content = File.ReadAllText("./speelruimte.json");
            JArray data = JsonConvert.DeserializeObject<JArray>(content);
            foreach (JToken child in data.Children())
            {
                string longitude = (child["json_geometry"]["coordinates"][0]).ToString().Replace(',','.');
                string latitude = (child["json_geometry"]["coordinates"][1]).ToString().Replace(',', '.');
                ExecuteNonQuery($"INSERT INTO area (latitude,longitude) VALUES ({latitude},{longitude});");
            }
            Console.WriteLine("\n\nDatabase is set!");

            /*
            database.ExecuteNonQuery("insert into visitor (area_id,duration) values (5,2);");
            var q = database.ExecuteQueryWithColumnNames("SELECT * FROM visitor;", new string[] { "id","area_id","check_in_time","duration"});
            q.ForEach(x => Console.WriteLine($"{x[0]} -- {x[1]} -- {x[2]} -- {x[3]}"));
            */
        }
        static void ExecuteNonQuery(string query)
        {
            bool result = database.ExecuteNonQuery(query);
            if (!result)
            {
                Console.WriteLine("Something went wrong when trying: " + query);
            }
            else
            {
                Console.WriteLine(query);
            }
        }
    }
}
