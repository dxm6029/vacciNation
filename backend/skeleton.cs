using System;
using System.Data.SqlClient;
namespace VacciNation
{
    public class Skeleton
    {
        public Connect connection = new Connect();
        
        // should be able to use this connection throughout the file with minor adjustments for delete, update, and insert
        public void SqlQueryTemplate(){
            SqlConnection conn = connection.OpenConnection();
            string sqlString = "SELECT name, dob FROM citizen";
            SqlCommand cmd = new SqlCommand(sqlString, conn);
            //command.Parameters.AddWithValue("@tPatSName", "Your-Parm-Value");
            SqlDataReader reader = cmd.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    Console.WriteLine(String.Format("{0}, {1}", reader["name"], reader["dob"]));
                }
            }
            finally
            {
                // Always call Close when done reading.
                reader.Close();
            }

            connection.CloseConnection(conn);
        }


        public void SqlQueryTemplateTwo(){
            string sqlString = "SELECT name, dob FROM citizen";
            using(SqlConnection conn = connection.OpenConnection()){
                SqlCommand cmd = new SqlCommand(sqlString, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                //command.Parameters.AddWithValue("@tPatSName", "Your-Parm-Value");
                try
                {
                    while (reader.Read())
                    {
                        Console.WriteLine(String.Format("{0}, {1}", reader["name"], reader["dob"]));
                    }
                }
                finally
                {
                    // Always call Close when done reading.
                    reader.Close();
                }
            }
        }
    }
}
