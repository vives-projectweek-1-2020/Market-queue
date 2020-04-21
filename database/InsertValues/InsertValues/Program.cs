using System;
using System.Diagnostics;
using System.Collections;
using System.IO;
using System.Threading;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Serialization;

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
            "CREATE TABLE area(id int(5) NOT NULL AUTO_INCREMENT,latitude DECIMAL(15,13) NOT NULL,longitude DECIMAL(15,13) NOT NULL,visitors SMALLINT UNSIGNED DEFAULT 0,PRIMARY KEY(id));"
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
            Console.WriteLine("Database is set!");
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
