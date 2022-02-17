using VacciNationAPI.Models;
using System;
using MySql.Data.MySqlClient; 
using System.Collections.Generic;
using System.Security.Cryptography;


namespace VacciNationAPI.DataLayer
{

    public class ReportManagement {

        VacciNation.Connect connection = new VacciNation.Connect();
        
        // ~~~~~~~~~~~~~~~~~ vaccine insights ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        public string vaccineInsightsResponseBuilder(string date){
            Console.WriteLine("i made it to the builder");
            string builder = "{";
            try{ 
                builder += " date: " + date + ",";
                int totalAdministered = totalVaccinesAdministered(null, -1);
                builder += " total_administered: " + totalAdministered + ",";
                int totalAdministeredByDay = totalVaccinesAdministered(date, -1);
                //DOMINIQUE - make this better
                builder += " total_administered_on_date: " + totalAdministeredByDay + ",";
                int totalUpcoming = totalVaccinesUpcoming();
                builder += " total_upcoming: " + totalUpcoming + ",";
                // get all categories and suppliers and loop through
                List<int> categoryIDs = getAllCategoryIds();
                List<string> suppliers = getAllSuppliers();
                builder += " total_administered_by_type: [";
                foreach(string supplier in suppliers){
                    builder += "{ " + supplier + ": ";
                    foreach(int category in categoryIDs){
                        int totalByType = totalVaccinesAdministeredByType(supplier, category, null, -1);
                        builder +=  "{ " + category + ": " + totalByType + "},";
                    }
                    builder += "},";
                }
                builder += "],";

                builder += " total_administered_by_type_date: [";
                foreach(string supplier in suppliers){
                    builder += "{ " + supplier + ": ";
                    foreach(int category in categoryIDs){
                        int totalByType = totalVaccinesAdministeredByType(supplier, category, date, -1);
                        builder +=  "{ " + category + ": " + totalByType + "},";
                        // total_administered_pfizer_1
                    }
                    builder += "},";
                }
                builder += "],";
                int totalMissed = totalVaccinesMissed(null);
                builder += " total_missed: " + totalMissed + ",";
                int totalMissedOnDate = totalVaccinesMissed(date);
                builder += " total_missed_on_date: " + totalMissedOnDate;

            }catch (Exception e){ Console.WriteLine(e.Message); Console.WriteLine(e.StackTrace); }
            finally{
            }
            builder += "}";
            Console.WriteLine(builder);
            return builder;
        }

        public List<string>  getAllSuppliers(){
            MySqlConnection conn = new MySqlConnection();
            List<string> suppliers = new List<string>();
            try{ 
                conn = connection.OpenConnection();
              
                string query = "SELECT DISTINCT supplier FROM dose";
                MySqlCommand cmd = new MySqlCommand(query, conn);

                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {   
                    // should not hit 0, but for consistency
                    suppliers.Add(rdr.IsDBNull(0) ?  "" : rdr.GetString(0));
                }
                rdr.Close();

            }catch (Exception e){ Console.WriteLine(e.Message); Console.WriteLine(e.StackTrace); }
            finally{
                connection.CloseConnection(conn);
            }

            return suppliers;
        }

        public List<int> getAllCategoryIds(){
            MySqlConnection conn = new MySqlConnection();
            List<int> categoryIDs = new List<int>();
            try{ 
                conn = connection.OpenConnection();
              
                string query = "SELECT DISTINCT category FROM vaccine";
                MySqlCommand cmd = new MySqlCommand(query, conn);

                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {   
                    // should not hit 0, but for consistency
                    categoryIDs.Add(rdr.IsDBNull(0) ?  -1 : rdr.GetInt32(0));
                }
                rdr.Close();

            }catch (Exception e){ Console.WriteLine(e.Message); Console.WriteLine(e.StackTrace); }
            finally{
                connection.CloseConnection(conn);
            }

            return categoryIDs;
        }

        // CAN FILTER BY DATE
        public int totalVaccinesAdministered(string date, int staff_id){
            int total = 0;
            string start = "";
            string end = "";
            MySqlConnection conn = new MySqlConnection();
            try{ 
                conn = connection.OpenConnection();
              
                string query = "SELECT COUNT(timeslot_id) FROM timeslot WHERE status_id=3";
                // CHECK IF DATE / STATUS NULL AND APPENT APPROPRIATELY
                if(date != null){
                    query += " AND date > @start AND timeslot.date < @end";
                    start = date + " 00:00:00";
                    end = date + " 23:59:59";
                }
                if(staff_id != -1){
                    query += " AND staff_id = @staff_id";
                }
                MySqlCommand cmd = new MySqlCommand(query, conn);

                if(date != null){
                    cmd.Parameters.AddWithValue("@start", start);
                    cmd.Parameters.AddWithValue("@end", end);
                }
                if(staff_id != -1){
                    cmd.Parameters.AddWithValue("@staff_id", staff_id);
                }

                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {   
                    // should not hit 0, but for consistency
                    total = rdr.IsDBNull(0) ? 0: rdr.GetInt32(0);
                }
                rdr.Close();

            }catch (Exception e){ Console.WriteLine(e.Message); Console.WriteLine(e.StackTrace); }
            finally{
                connection.CloseConnection(conn);
            }

            return total;
        }

        // look for reserved (not missed, open, or completed)
        public int totalVaccinesUpcoming(){
            int total = 0;
            MySqlConnection conn = new MySqlConnection();
            try{ 
                conn = connection.OpenConnection();
              
                string query = "SELECT COUNT(timeslot_id) FROM timeslot WHERE status_id=2";
                MySqlCommand cmd = new MySqlCommand(query, conn);

                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {   
                    // should not hit 0, but for consistency
                    total = rdr.IsDBNull(0) ? 0: rdr.GetInt32(0);
                }
                rdr.Close();

            }catch (Exception e){ Console.WriteLine(e.Message); Console.WriteLine(e.StackTrace); }
            finally{
                connection.CloseConnection(conn);
            }

            return total;
        }

        // type = supplier and category
        // can also add date to filter by both type and date
        public int totalVaccinesAdministeredByType(string supplier, int category, string date, int staff_id){
            int total = 0;
            string start = "";
            string end = "";
            MySqlConnection conn = new MySqlConnection();
            try{ 
                conn = connection.OpenConnection();
              
                string query = "SELECT COUNT(timeslot_id) FROM timeslot JOIN dose USING(dose_id) JOIN vaccine USING(vaccine_id) WHERE status_id=3 AND supplier=@supplier AND category=@category";
                // CHECK IF DATE / STATUS NULL AND APPENT APPROPRIATELY
                if(date != null){
                    query += " AND date > @start AND timeslot.date < @end";
                    start = date + " 00:00:00";
                    end = date + " 23:59:59";
                }
                if(staff_id != -1){
                    query += " AND staff_id = @staff_id";
                }
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@supplier", supplier);
                cmd.Parameters.AddWithValue("@category", category);

                if(date != null){
                    cmd.Parameters.AddWithValue("@start", start);
                    cmd.Parameters.AddWithValue("@end", end);
                }
                if(staff_id != -1){
                    cmd.Parameters.AddWithValue("@staff_id", staff_id);
                }

                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {   
                    // should not hit 0, but for consistency
                    total = rdr.IsDBNull(0) ? 0: rdr.GetInt32(0);
                }
                rdr.Close();

            }catch (Exception e){ Console.WriteLine(e.Message); Console.WriteLine(e.StackTrace); }
            finally{
                connection.CloseConnection(conn);
            }

            return total;
        }

        public int totalVaccinesMissed(string date){
           int total = 0;
            string start = "";
            string end = "";
            MySqlConnection conn = new MySqlConnection();
            try{ 
                conn = connection.OpenConnection();
              
                string query = "SELECT COUNT(timeslot_id) FROM timeslot WHERE status_id=4";
                  if(date != null){
                    query += " AND date > @start AND timeslot.date < @end";
                    start = date + " 00:00:00";
                    end = date + " 23:59:59";
                }
                MySqlCommand cmd = new MySqlCommand(query, conn);
                 if(date != null){
                    cmd.Parameters.AddWithValue("@start", start);
                    cmd.Parameters.AddWithValue("@end", end);
                }

                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {   
                    // should not hit 0, but for consistency
                    total = rdr.IsDBNull(0) ? 0: rdr.GetInt32(0);
                }
                rdr.Close();

            }catch (Exception e){ Console.WriteLine(e.Message); Console.WriteLine(e.StackTrace); }
            finally{
                connection.CloseConnection(conn);
            }

            return total;
        }

        // ~~~~~~~~~~~~~~~~~ staff insights ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        public string staffInsightsResponseBuilder(string date, int staff_id){
            string builder = "{";
            try{ 
                builder += " date: " + date + ", staff_id: " + staff_id;
                int totalAdministered = totalVaccinesAdministered(null, staff_id);
                builder += " total_administered: " + totalAdministered + ",";
                int totalAdministeredByDay = totalVaccinesAdministered(date, staff_id);
                builder += " total_administered_on_date: " + totalAdministeredByDay + ",";

                // get all categories and suppliers and loop through
                List<int> categoryIDs = getAllCategoryIds();
                List<string> suppliers = getAllSuppliers();
                builder += " total_administered_by_type: [";
                foreach(string supplier in suppliers){
                    builder += "{ " + supplier + ": ";
                    foreach(int category in categoryIDs){
                        int totalByType = totalVaccinesAdministeredByType(supplier, category, null, staff_id);
                        builder +=  "{ " + category + ": " + totalByType + "},";
                    }
                    builder += "},";
                }
                builder += "],";

                builder += " total_administered_by_type_date: [";
                foreach(string supplier in suppliers){
                    builder += "{ " + supplier + ": ";
                    foreach(int category in categoryIDs){
                        int totalByType = totalVaccinesAdministeredByType(supplier, category, date, staff_id);
                        builder +=  "{ " + category + ": " + totalByType + "},";
                        // total_administered_pfizer_1
                    }
                    builder += "},";
                }
                builder += "],";

            }catch (Exception e){ Console.WriteLine(e.Message); Console.WriteLine(e.StackTrace); }
            finally{
            }
            builder += "}";

            return builder;
        }   

        public List<string> getAllReactions(){
            MySqlConnection conn = new MySqlConnection();
            List<string> appointments = new List<string>();
            try{ 
                conn = connection.OpenConnection();

                string query = "SELECT timeslot_id, timeslot.citizen_id, citizen.first_name, citizen.last_name, timeslot.dose_id, dose.supplier, vaccine_category.category_id, vaccine_category.name, timeslot.date, timeslot.status_id, timeslot_status.description, timeslot.reactions FROM timeslot JOIN staff USING(staff_id) JOIN dose USING(dose_id) JOIN timeslot_status USING(status_id) JOIN vaccine USING(vaccine_id) JOIN location USING(location_id) LEFT JOIN citizen USING(citizen_id) JOIN vaccine_category ON vaccine_category.category_id=vaccine.category";

                MySqlCommand cmd = new MySqlCommand(query, conn);

                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {   
                    appointments.Add("{ appointment_id: " + (rdr.IsDBNull(0) ? -1 : rdr.GetInt32(0)) + ", citizen_id: " +  (rdr.IsDBNull(1) ? -1 : rdr.GetInt32(1)) + ", citizen_first_name: " + (rdr.IsDBNull(2) ?  "" : rdr.GetString(2))+  ", citizen_last_name: " +  (rdr.IsDBNull(3) ?  "" : rdr.GetString(3)) + ", dose_id: " + ( rdr.IsDBNull(4) ? -1 : rdr.GetInt32(4)) + ", supplier: " + (rdr.IsDBNull(5) ?  "" : rdr.GetString(5)) 
                    + ", category_id: " + (rdr.IsDBNull(6) ? -1: rdr.GetInt32(6)) +  ", description: " + (rdr.IsDBNull(7) ?  "" : rdr.GetString(7)) + ", date: " + (rdr.IsDBNull(8) ?  "" : rdr.GetString(8))  + ", status_id: " + ( rdr.IsDBNull(9) ? -1 : rdr.GetInt32(9)) + ", status_desc: " + (rdr.IsDBNull(10) ?  "" : rdr.GetString(10)) + ", reactions: " + (rdr.IsDBNull(11) ?  "" : rdr.GetString(11)) +"}");
                }
                rdr.Close();

            }catch (Exception e){Console.WriteLine(e.Message); Console.WriteLine(e.StackTrace);}
            finally{
                connection.CloseConnection(conn);
            }
            
            return appointments;
        }

    } 

}//namespace