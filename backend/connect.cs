using System;
using System.Data.SqlClient;
namespace VacciNation
{
    public class Connect
    {
        public SqlConnection OpenConnection(){

            // should update connection string to make more secure
            string connetionString = "Server= \"db.VacciNation.com\"; Database= \"vaccination\"; Uid= \"student\"; Pwd=\"student\"; SslMode=Required";

            try{
                SqlConnection conn = new SqlConnection(connetionString);
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

        public Boolean CloseConnection(SqlConnection conn){
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
