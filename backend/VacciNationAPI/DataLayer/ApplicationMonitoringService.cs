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
        
        public List<Dictionary<string, string>> GetFilteredResponseTimes(Dictionary<string, string> filters){
            MySqlConnection conn = connection.OpenConnection();
            List<Dictionary<string, string>> responseTimes = new List<Dictionary<string, string>>();
            try{ 
                string query = "SELECT application, endpoint, response_time, response_code, timestamp FROM monitor WHERE ";
                if (filters.ContainsKey("application"))
                {
                    query += "application = @application AND ";
                }

                if (filters.ContainsKey("endpoint"))
                {
                    query += "endpoint LIKE '%" + filters["endpoint"] + "%' AND ";
                }

                if (filters.ContainsKey("minmillis"))
                {
                    query += "response_time >= @minmillis AND ";
                }
                
                if (filters.ContainsKey("maxmillis"))
                {
                    query += "response_time <= @maxmillis AND ";
                }

                if (filters.ContainsKey("code"))
                {
                    //exclude
                    if (filters["code"][0] == '-')
                    {
                        query += "response_code != @code AND ";
                        filters["code"] = filters["code"].Substring(1);
                    }
                    else
                    {
                        query += "response_code = @code AND ";
                    }
                }

                if (filters.ContainsKey("startdate"))
                {
                    query += "timestamp > @startdate AND ";
                }

                if (filters.ContainsKey("enddate"))
                {
                    query += "timestamp < @enddate AND ";
                }

                //remove trailing AND 
                if (query.Substring(query.Length - 4).Equals("AND "))
                {
                    query = query.Substring(0, query.Length - 4);
                }
                
                Console.WriteLine("Query: " + query);
                
                MySqlCommand cmd = new MySqlCommand(query, conn);
                
                if (filters.ContainsKey("application"))
                {
                    cmd.Parameters.AddWithValue("@application", filters["application"]);
                }

                if (filters.ContainsKey("minmillis"))
                {
                    cmd.Parameters.AddWithValue("@minmillis", filters["minmillis"]);
                }
                
                if (filters.ContainsKey("maxmillis"))
                {
                    cmd.Parameters.AddWithValue("@maxmillis", filters["maxmillis"]);
                }

                if (filters.ContainsKey("code"))
                {
                    cmd.Parameters.AddWithValue("@code", filters["code"]);
                }

                if (filters.ContainsKey("startdate"))
                {
                    cmd.Parameters.AddWithValue("@startdate", filters["startdate"]);
                }

                if (filters.ContainsKey("enddate"))
                {
                    cmd.Parameters.AddWithValue("@enddate", filters["enddate"] + " 23:59:59");
                }
                
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