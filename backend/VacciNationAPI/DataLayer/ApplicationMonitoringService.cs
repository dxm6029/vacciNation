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
        
        public List<Dictionary<string, string>> GetAllResponseTimes(){
            MySqlConnection conn = connection.OpenConnection();
            List<Dictionary<string, string>> responseTimes = new List<Dictionary<string, string>>();
            try{ 
                string query = "SELECT application, endpoint, response_time, response_code, timestamp FROM monitor;";
                
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {

                    Dictionary<string, string> monitor = new Dictionary<string, string>();
                    monitor.Add("application", (rdr.IsDBNull(0) ? "" : rdr.GetString(0)) );
                    monitor.Add("endpoint", (rdr.IsDBNull(1) ? "" : rdr.GetString(1)) );
                    monitor.Add("response_time", "" + (rdr.IsDBNull(2) ? -1 : rdr.GetInt32(2)) );
                    monitor.Add("response_code", (rdr.IsDBNull(3) ? "" : rdr.GetString(3)) );
                    monitor.Add("timestamp", (rdr.IsDBNull(4) ? "" : rdr.GetString(4)) );

                    responseTimes.Add(monitor);
                }
                rdr.Close();

            }catch (Exception e){Console.WriteLine(e.Message); Console.WriteLine(e.StackTrace);}
            finally{
                connection.CloseConnection(conn);
            }
            
            return responseTimes;
        }
    }
}