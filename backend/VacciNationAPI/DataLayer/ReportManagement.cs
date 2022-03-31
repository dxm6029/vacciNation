using VacciNationAPI.Models;
using System;
using MySql.Data.MySqlClient; 
using System.Collections.Generic;
using System.Security.Cryptography;


namespace VacciNationAPI.DataLayer
{

    public class ReportManagement {

        VacciNation.Connect connection = new VacciNation.Connect();
        User user = new User();
        
        // ~~~~~~~~~~~~~~~~~ location insights ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        public LocationReport locationInsightsResponseBuilder(string date, int location_id){

            LocationReport locationReport = null;
            try{ 

                var today = DateTime.Today;
                var month = today.Date.Month.ToString().Length == 1 ? "0" + today.Date.Month.ToString() : today.Date.Month.ToString();
                var day = today.Date.Day.ToString().Length == 1 ? "0" + today.Date.Day.ToString() : today.Date.Day.ToString();
                var year = today.Date.Year.ToString();
                var todayStr = year + "-" + month + "-" + day;
                if(date == null || date == ""){
                    date = todayStr;
                } 

                LocationManagement lm = new LocationManagement();
                Dictionary<string, string> l =  lm.GetLocationById(location_id);
                int totalAdministeredByDay = totalVaccinesAdministeredByLocation(date, location_id, todayStr);

                Dictionary<int, string> categoryIDs = getAllCategoryIds();
                Dictionary<string, int> total_per_sequence = new Dictionary<string, int>();
                foreach(int category in categoryIDs.Keys){
                    int num = totalVaccinesAdministeredByCategory(category, date, location_id, todayStr);
                    total_per_sequence.Add(categoryIDs[category], num);
                }

                List<string> suppliers = getAllSuppliers();
                Dictionary<string, int> total_per_supplier = new Dictionary<string, int>();
                foreach(string supplier in suppliers){
                    int num = totalVaccinesAdministeredBySupplier(supplier, date, location_id, todayStr);
                    total_per_supplier.Add(supplier, num);
                }

                int numReactions = getNumAdverseReactionsLocation(location_id, date, todayStr);

                // total adverse reactions
                locationReport = new LocationReport(location_id, l["name"], totalAdministeredByDay, total_per_supplier, total_per_sequence, date, numReactions);

            }catch (Exception e){ Console.WriteLine(e.Message); Console.WriteLine(e.StackTrace); }
            finally{
            }
            return locationReport;
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

        public Dictionary<int, string> getAllCategoryIds(){
            MySqlConnection conn = new MySqlConnection();
            Dictionary<int,string> categoryIDs = new Dictionary<int, string>();
            try{ 
                conn = connection.OpenConnection();
              
                string query = "SELECT DISTINCT category_id, name FROM vaccine_category";
                MySqlCommand cmd = new MySqlCommand(query, conn);

                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {   
                    // should not hit 0, but for consistency
                    categoryIDs.Add(rdr.IsDBNull(0) ?  -1 : rdr.GetInt32(0), rdr.IsDBNull(1) ?  "" : rdr.GetString(1));
                }
                rdr.Close();

            }catch (Exception e){ Console.WriteLine(e.Message); Console.WriteLine(e.StackTrace); }
            finally{
                connection.CloseConnection(conn);
            }

            return categoryIDs;
        }

        // CAN FILTER BY DATE
        public int totalVaccinesAdministered(string date, int staff_id, string today){
            int total = 0;
            string start = date;
            string end = today;
            MySqlConnection conn = new MySqlConnection();
            try{ 
                conn = connection.OpenConnection();
              
                string query = "SELECT COUNT(timeslot_id) FROM timeslot WHERE status_id=3";
                // CHECK IF DATE / STATUS NULL AND APPENT APPROPRIATELY
                if(date != null){
                    query += " AND date > @start AND timeslot.date < @end";
                    start += " 00:00:00";
                    end += " 23:59:59";
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

        public int totalVaccinesAdministeredByLocation(string date, int location_id, string today){
            int total = 0;
            string start = date;
            string end = today;
            MySqlConnection conn = new MySqlConnection();
            try{ 
                conn = connection.OpenConnection();
              
                //string query = "SELECT COUNT(timeslot_id) FROM timeslot WHERE status_id=3";
                string query = "SELECT COUNT(timeslot_id) FROM timeslot WHERE status_id=3";

                // CHECK IF DATE / STATUS NULL AND APPENT APPROPRIATELY
                if(date != null){
                    query += " AND timeslot.date > @start AND timeslot.date < @end";
                    start += " 00:00:00";
                    end += " 23:59:59";
                }
                if(location_id != -1){
                    query += " AND location_id = @location_id";
                }
                MySqlCommand cmd = new MySqlCommand(query, conn);

                if(date != null){
                    cmd.Parameters.AddWithValue("@start", start);
                    cmd.Parameters.AddWithValue("@end", end);
                }
                if(location_id != -1){
                    cmd.Parameters.AddWithValue("@location_id", location_id);
                }

                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {   
                    // should not hit 0, but for consistency
                    total = rdr.IsDBNull(0) ? 0: rdr.GetInt32(0);
                    //total++;
                    Console.WriteLine(rdr.IsDBNull(0) ? 0: rdr.GetInt32(0));
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

        // checks ON a specific day
         public int totalVaccinesAdministeredByCategory(int category, string date, int location_id, string today){
            int total = 0;
            string start = "";
            string end = "";
            MySqlConnection conn = new MySqlConnection();
            try{ 
                conn = connection.OpenConnection();
              
                string query = "SELECT COUNT(timeslot_id) FROM timeslot JOIN dose USING(dose_id) JOIN vaccine USING(vaccine_id) WHERE status_id=3 AND category=@category";
                // CHECK IF DATE / STATUS NULL AND APPENT APPROPRIATELY
                if(date != null){
                    query += " AND date > @start AND timeslot.date < @end";
                    start = date + " 00:00:00";
                    end = today + " 23:59:59";
                }
                if(location_id != -1){
                    query += " AND timeslot.location_id = @location_id";
                }
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@category", category);

                if(date != null){
                    cmd.Parameters.AddWithValue("@start", start);
                    cmd.Parameters.AddWithValue("@end", end);
                }
                if(location_id != -1){
                    cmd.Parameters.AddWithValue("@location_id", location_id);
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

        public int totalVaccinesAdministeredBySupplier(string supplier, string date, int location_id, string today){
            int total = 0;
            string start = "";
            string end = "";
            MySqlConnection conn = new MySqlConnection();
            try{ 
                conn = connection.OpenConnection();
              
                string query = "SELECT COUNT(timeslot_id) FROM timeslot JOIN dose USING(dose_id) JOIN vaccine USING(vaccine_id) WHERE status_id=3 AND supplier=@supplier";
                // CHECK IF DATE / STATUS NULL AND APPENT APPROPRIATELY
                if(date != null){
                    query += " AND date > @start AND timeslot.date < @end";
                    start = date + " 00:00:00";
                    end = today + " 23:59:59";
                }
                if(location_id != -1){
                    query += " AND timeslot.location_id = @location_id";
                }
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@supplier", supplier);

                if(date != null){
                    cmd.Parameters.AddWithValue("@start", start);
                    cmd.Parameters.AddWithValue("@end", end);
                }
                if(location_id != -1){
                    cmd.Parameters.AddWithValue("@location_id", location_id);
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
        public StaffReport staffInsightsResponseBuilder(string date, int staff_id){
            // if date not provided, defaults to 'today'
            StaffReport staffReport = null;
            try{ 
                var today = DateTime.Today;
                var month = today.Date.Month.ToString().Length == 1 ? "0" + today.Date.Month.ToString() : today.Date.Month.ToString();
                var day = today.Date.Day.ToString().Length == 1 ? "0" + today.Date.Day.ToString() : today.Date.Day.ToString();
                var year = today.Date.Year.ToString();
                var todayStr = year + "-" + month + "-" + day;
                if(date == null || date == ""){
                    date = todayStr;
                } 
                
                // employee name, location, total patients processed, count of adverse reactions for time period
                // date is declared above if not sent in
                // all date things -> date to present day

                Staff staff = user.getUserWithID(staff_id); // gets us employee info

                int totalAdministeredByDay = -1;
                // if a date is given, then get info from date -> present day, otherwise do total of all time
                if(date != todayStr){
                    totalAdministeredByDay = totalVaccinesAdministered(date, staff_id, todayStr);
                } else {
                    totalAdministeredByDay = totalVaccinesAdministered(null, staff_id, null);
                }

                List<string> locations = getLocationsForStaff(staff_id);

                int totalReactions = -1;
                if(date != todayStr){
                    totalReactions = getNumAdverseReactionsStaff(staff_id, date, todayStr);
                } else{
                    totalReactions = getNumAdverseReactionsStaff(staff_id, null, null);
                }


                staffReport = new StaffReport(date, staff_id, staff.first_name, staff.last_name, totalAdministeredByDay, locations, totalReactions);

            }catch (Exception e){ Console.WriteLine(e.Message); Console.WriteLine(e.StackTrace); }
            finally{
            }

            return staffReport;
        } 

        public List<string> getLocationsForStaff(int staff_id){
            List<string> locations = new List<string>();
            MySqlConnection conn = new MySqlConnection();
            try{ 
                conn = connection.OpenConnection();
              
                string query = "SELECT name FROM location JOIN staff_location USING(location_id) JOIN staff USING(staff_id) WHERE staff_id=@staff_id";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@staff_id", staff_id);

                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {   
                    locations.Add(rdr.IsDBNull(0) ? "": rdr.GetString(0));
                }
                rdr.Close();

            }catch (Exception e){ Console.WriteLine(e.Message); Console.WriteLine(e.StackTrace); }
            finally{
                connection.CloseConnection(conn);
            }

            return locations;
        }

         public int getNumAdverseReactionsStaff(int staff_id, string date, string today){
            int total = 0;
            MySqlConnection conn = new MySqlConnection();
            try{ 
                conn = connection.OpenConnection();
                string start = date;
                string end = today;
              
                string query = "SELECT reactions FROM timeslot WHERE staff_id=@staff_id";
                if(date != null){
                    query += " AND date > @start AND timeslot.date < @end";
                    start += " 00:00:00";
                    end += " 23:59:59";
                }
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@staff_id", staff_id);
                if(date != null){
                    cmd.Parameters.AddWithValue("@start", start);
                    cmd.Parameters.AddWithValue("@end", end);
                }

                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {   
                    if(!rdr.IsDBNull(0) && rdr.GetString(0) != ""){
                        total++;
                    }
                }
                rdr.Close();

            }catch (Exception e){ Console.WriteLine(e.Message); Console.WriteLine(e.StackTrace); }
            finally{
                connection.CloseConnection(conn);
            }

            return total;
        }

        public int getNumAdverseReactionsLocation(int location_id, string date, string today){
            int total = 0;
            MySqlConnection conn = new MySqlConnection();
            try{ 
                conn = connection.OpenConnection();
                string start = date;
                string end = today;
              
                string query = "SELECT reactions FROM timeslot WHERE location_id=@location_id";
                if(date != null){
                    query += " AND timeslot.date > @start AND timeslot.date < @end";
                    start += " 00:00:00";
                    end += " 23:59:59";
                }
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@location_id", location_id);
                if(date != null){
                    cmd.Parameters.AddWithValue("@start", start);
                    cmd.Parameters.AddWithValue("@end", end);
                }

                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {   
                    if(!rdr.IsDBNull(0) && rdr.GetString(0) != ""){
                        total++;
                    }
                }
                rdr.Close();

            }catch (Exception e){ Console.WriteLine(e.Message); Console.WriteLine(e.StackTrace); }
            finally{
                connection.CloseConnection(conn);
            }

            return total;
        }

        public List<ReactionReport> getAllReactions(){
            MySqlConnection conn = new MySqlConnection();
            List<ReactionReport> appointments = new List<ReactionReport>();
            try{ 
                conn = connection.OpenConnection();

                string query = "SELECT timeslot_id, timeslot.date, citizen.first_name, citizen.last_name, dose.supplier, vaccine_category.name, staff.staff_id, batch, timeslot.reactions FROM timeslot JOIN staff USING(staff_id) JOIN dose USING(dose_id) JOIN timeslot_status USING(status_id) JOIN vaccine USING(vaccine_id) JOIN location ON timeslot.location_id=location.location_id LEFT JOIN citizen USING(citizen_id) JOIN vaccine_category ON vaccine_category.category_id=vaccine.category";

                MySqlCommand cmd = new MySqlCommand(query, conn);

                User u = new User();

                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {   
                    if(!rdr.IsDBNull(8) && rdr.GetString(8) != ""){
                        int staff_id = rdr.IsDBNull(6) ?  -1 : rdr.GetInt32(6);
                        Staff staff = u.getUserWithID(staff_id);
                        var fname = staff.first_name;  
                        var lname = staff.last_name;
                        ReactionReport r = new ReactionReport(rdr.IsDBNull(0) ? -1 : rdr.GetInt32(0), rdr.IsDBNull(1) ? "" : rdr.GetString(1),rdr.IsDBNull(2) ? "" : rdr.GetString(2), rdr.IsDBNull(3) ? "" : rdr.GetString(3), rdr.IsDBNull(4) ? "" : rdr.GetString(4), rdr.IsDBNull(5) ? "" : rdr.GetString(5), staff.first_name, staff.last_name, rdr.IsDBNull(7) ? "" : rdr.GetString(7), rdr.IsDBNull(8) ? "" : rdr.GetString(8));
                        appointments.Add(r);
                    }
                }
                rdr.Close();

            }catch (Exception e){Console.WriteLine(e.Message); Console.WriteLine(e.StackTrace);}
            finally{
                connection.CloseConnection(conn);
            }
            
            return appointments;
        }

         public List<BatchReport> getBatchReport(string batch, string date, int location){
            MySqlConnection conn = new MySqlConnection();
            List<BatchReport> appointments = new List<BatchReport>();
            try{ 
                conn = connection.OpenConnection();
                string start = date;
                string end = date;

                // where status = complete (3) and batch num = batch
                // optional where date = date and location = location

                string query = "SELECT timeslot_id, citizen.first_name, citizen.last_name, dose.supplier, vaccine_category.name, timeslot.date, location.name FROM timeslot JOIN dose USING(dose_id) JOIN vaccine USING(vaccine_id) JOIN location ON timeslot.location_id=location.location_id LEFT JOIN citizen USING(citizen_id) JOIN vaccine_category ON vaccine_category.category_id=vaccine.category WHERE batch=@batch AND timeslot.status_id=3";
                if(date != null && date != ""){
                    query += " AND date > @start AND timeslot.date < @end";
                    start += " 00:00:00";
                    end += " 23:59:59";
                }
                if(location > 0){
                    query += " AND location.location_id = @location";
                }
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@batch", batch);
                
                //bind params
                if(date != null && date != ""){
                    cmd.Parameters.AddWithValue("@start", start);
                    cmd.Parameters.AddWithValue("@end", end);

                }
                if(location > 0){
                    cmd.Parameters.AddWithValue("@location", location);

                }

                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {   
                    appointments.Add(new BatchReport(rdr.IsDBNull(0) ? -1 : rdr.GetInt32(0), rdr.IsDBNull(1) ?  "" : rdr.GetString(1), rdr.IsDBNull(2) ?  "" : rdr.GetString(2), rdr.IsDBNull(3) ?  "" : rdr.GetString(3), rdr.IsDBNull(4) ?  "" : rdr.GetString(4), rdr.IsDBNull(5) ?  "" : rdr.GetString(5), rdr.IsDBNull(6) ?  "" : rdr.GetString(6)));
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