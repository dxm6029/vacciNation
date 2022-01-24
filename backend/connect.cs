using System;
using MySql.Data.MySqlClient; 
namespace VacciNation
{
    public class Connect
    {
        public MySqlConnection OpenConnection(){

            // should update connection string to make more secure
            // string connetionString = "Server=db.VacciNation.com, 3306;" +
            // "Database=vaccination;" +
            // "User id=student;" +
            // "Password=student;";

            string server = "db.VacciNation.com:3306";
            string database = "vaccination";
            string uid = "student";
            string password = "student";
            string connetionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

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
