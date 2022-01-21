 
using System;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace VacciNation
{
    public class Connect
    {
        public SqlConnection OpenConnection(){
            string connetionString = "Data Source=ServerName;Initial Catalog=DatabaseName;User ID=UserName;Password=Password";

            try{
                SqlConnection conn = new SqlConnection(connetionString);
                conn.Open();
                Console.WriteLine("The connection is open");
                return conn;
            } catch(Exception e){
                Console.WriteLine("The connection did not open");
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

        public SqlConnection GetConnection(){
            return conn;
        }

    }
}
