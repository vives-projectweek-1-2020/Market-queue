using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;

namespace DatabaseLibrary
{
    // Based on https://www.codeproject.com/Articles/43438/Connect-C-to-MySQL
    public class Database
    {
        private MySqlConnection connection;
        private string server = "192.168.1.41"; //91.181.93.103
        private string port = "3306";   //3341 or 3340
        private string uid = "projectweek";
        private string password = "GuessWhat";

        //Constructor
        public Database()
        {
            Initialize();
        }

        //Initialize values
        private void Initialize()
        {
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "PORT=" + port + ";"+ "UID=" + uid + ";" + "PASSWORD=" + password + ";";
            connection = new MySqlConnection(connectionString);
        }
        //open connection to database
        private bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException)
            {
                return false;
            }
        }

        //Close connection
        private bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException)
            {
                return false;
            }
        }
        
        public bool ExecuteNonQuery(string query)
        {
            //Open connection
            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();

                //close Connection
                this.CloseConnection();

                return true;
            }
            else
            {
                return false;
            }
        }
        public List<List<string>> ExecuteQueryWithColumnNames(string query,string[] columnNames)
        {
            List<List<string>> result = new List<List<string>>(); 
            //Open connection
            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();

                while(dataReader.Read())
                {
                    List<string> row = new List<string>();
                    foreach (string columnName in columnNames)
                    {
                        row.Add(dataReader[columnName].ToString());
                    }
                    result.Add(row);
                }

                dataReader.Close();

                //close Connection
                this.CloseConnection();
            }
            return result;
        }

    }
}
