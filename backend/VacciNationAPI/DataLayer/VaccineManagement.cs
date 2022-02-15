using VacciNationAPI.Models;
using System;
using MySql.Data.MySqlClient; 
using System.Collections.Generic;

namespace VacciNationAPI.DataLayer
{
    public class VaccineManagement {
        
        VacciNation.Connect connection = new VacciNation.Connect();
        
        public List<string> GetAllVaccines(){
            MySqlConnection conn = new MySqlConnection();
            List<string> vaccines = new List<string>();
            try{ 
                conn = connection.OpenConnection();

                string query = "SELECT v.vaccine_id, c.name as category, d.name as disease, v.description FROM vaccine as v LEFT JOIN vaccine_category as c ON v.category = c.category_id LEFT JOIN vaccine_disease as d ON v.disease = d.disease_id; ";
                
                MySqlCommand cmd = new MySqlCommand(query, conn);

                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {   
                    vaccines.Add("{ vaccine_id: " + (rdr.IsDBNull(0) ? -1 : rdr.GetInt32(0)) + ", category: " + ( rdr.IsDBNull(1) ? -1 : rdr.GetString(1)) + ", disease: " + (rdr.IsDBNull(2) ?  "" : rdr.GetString(2))+  ", description: " +  (rdr.IsDBNull(3) ?  "" : rdr.GetString(3)) +"}");
                }
                rdr.Close();

            }catch (Exception e){Console.WriteLine(e.Message); Console.WriteLine(e.StackTrace);}
            finally{
                connection.CloseConnection(conn);
            }
            
            return vaccines;
        }
        
    }
}