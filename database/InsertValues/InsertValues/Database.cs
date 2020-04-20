using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;

namespace InsertValues
{
    // Based on https://www.codeproject.com/Articles/43438/Connect-C-to-MySQL
    class Database
    {
        private MySqlConnection connection;
        private string server = "localhost";
        private string uid = "root";
        private string password = "";

        //Constructor
        public Database()
        {
            Initialize();
        }

        //Initialize values
        private void Initialize()
        {
            string connectionString;
            connectionString = "SERVER=" + server + ";"+ "UID=" + uid + ";" + "PASSWORD=" + password + ";";
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

                //return list to be displayed
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
