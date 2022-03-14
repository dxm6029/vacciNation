using VacciNationAPI.Models;
using System;
using MySql.Data.MySqlClient; 
using System.Collections.Generic;
using Ubiety.Dns.Core.Common;

namespace VacciNationAPI.DataLayer
{
    public class ApplicationMonitoringService
    {
        
        VacciNation.Connect connection = new VacciNation.Connect();

        public void RecordResponseTime(string application, string endpoint, long startTime, string responseCode)
        {
            MySqlConnection conn = connection.OpenConnection();
            long responseTimeMillis = DateTimeOffset.Now.ToUnixTimeMilliseconds() - startTime;
            
            try{ 
                string query = "INSERT INTO monitor (application, endpoint, response_time, response_code, timestamp) VALUES (@application, @endpoint, @response_time, @response_code, CURRENT_TIMESTAMP())";
                
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@application", application);
                cmd.Parameters.AddWithValue("@endpoint", endpoint);
                cmd.Parameters.AddWithValue("@response_time", responseTimeMillis);
                cmd.Parameters.AddWithValue("@response_code", responseCode);
                int numAffected = cmd.ExecuteNonQuery();
                               
                //location insert failed
                if (numAffected < 1)
                {
                    Console.WriteLine("Error logging response time");
                }
                
            }
            catch (Exception e) {
                Console.WriteLine(e.Message); 
                Console.WriteLine(e.StackTrace);
            }
            finally{
                connection.CloseConnection(conn);
            }
        }
        
    }
}