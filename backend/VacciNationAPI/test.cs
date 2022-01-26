using System;
using System.Data.SqlClient;
namespace VacciNation
{
    public class Test
    {

        public void OpenAndCloseDBConnect(){
            VacciNation.Connect connection = new VacciNation.Connect();
            try{
                var conn = connection.OpenConnection();
                var status = connection.CloseConnection(conn);
                if(status){
                    Console.WriteLine("SUCCESS: Opened and Closed DB connection without issues");
                }
                else{
                    Console.WriteLine("ERROR: Unable to close DB connection");
                }
            }
            catch(Exception e){
                Console.WriteLine("ERROR: " + e.StackTrace);
            }
        }

    }
}
