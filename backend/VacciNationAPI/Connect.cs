using System;
using MySql.Data.MySqlClient; 
using System.IO;
namespace VacciNation
{
    public class Connect
    {

        public static void Load(string filePath)
        {
            if (!File.Exists(filePath))
                return;

            foreach (var line in File.ReadAllLines(filePath))
            {
                var parts = line.Split(
                    '=',
                    StringSplitOptions.RemoveEmptyEntries);

                if (parts.Length != 2)
                    continue;

                Environment.SetEnvironmentVariable(parts[0], parts[1]);
            }
        }

        public MySqlConnection OpenConnection(){

            Load("../../server.env");

            string server = Environment.GetEnvironmentVariable("DB_IP");
            string database = Environment.GetEnvironmentVariable("DB_NAME");
            string uid = Environment.GetEnvironmentVariable("DB_USER");
            string password = Environment.GetEnvironmentVariable("DB_PWD");
            int port = Int32.Parse(Environment.GetEnvironmentVariable("DB_PORT"));
            string connetionString = "SERVER=" + server + ";" + "Port = " + port + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

            try{
                MySqlConnection conn = new MySqlConnection(connetionString);
                conn.Open();
                Console.WriteLine("The connection is open");
                return conn;
            } catch(Exception e){
                Console.WriteLine("The connection did not open");
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
                return null;
            }
        }



        public Boolean CloseConnection(MySqlConnection conn){
            try{
                conn.Close();
                Console.WriteLine("The connection is closed");
                return true;
            } catch(Exception e){
                Console.WriteLine("The connection did not close");
                Console.WriteLine(e.StackTrace);
                return false;
            }
        }

    }
}
