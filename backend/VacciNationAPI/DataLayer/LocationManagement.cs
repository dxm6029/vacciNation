using VacciNationAPI.Models;
using System;
using MySql.Data.MySqlClient; 
using System.Collections.Generic;

namespace VacciNationAPI.DataLayer
{
    public class LocationManagement
    {
        VacciNation.Connect connection = new VacciNation.Connect();
        
        public List<Dictionary<string, string>> GetAllLocations(){
            MySqlConnection conn = new MySqlConnection();
            List<Dictionary<string, string>> locations = new List<Dictionary<string, string>>();
            try{ 
                string query = "SELECT l.location_id, l.name, a.street, a.street_line2, a.city, a.state, a.zip "
                                + "FROM location as l "
                                + "LEFT JOIN address as a ON l.address_id = a.address_id; ";
                
                conn = connection.OpenConnection();
                MySqlCommand cmd = new MySqlCommand(query, conn);

                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {

                    Dictionary<string, string> location = new Dictionary<string, string>();
                    location.Add("location_id", "" + (rdr.IsDBNull(0) ? -1 : rdr.GetInt32(0)) );
                    location.Add("name", (rdr.IsDBNull(1) ? "" : rdr.GetString(1)) );
                    location.Add("street", (rdr.IsDBNull(2) ? "" : rdr.GetString(2)) );
                    location.Add("street_line2", (rdr.IsDBNull(3) ? "" : rdr.GetString(3)) );
                    location.Add("city", (rdr.IsDBNull(4) ? "" : rdr.GetString(4)) );
                    location.Add("state", (rdr.IsDBNull(5) ? "" : rdr.GetString(5)) );
                    location.Add("zip", (rdr.IsDBNull(6) ? "" : rdr.GetString(6)) );

                    locations.Add(location);
                }
                rdr.Close();

            }catch (Exception e){Console.WriteLine(e.Message); Console.WriteLine(e.StackTrace);}
            finally{
                connection.CloseConnection(conn);
            }
            
            return locations;
        }

        public Dictionary<string, string> GetLocationById(int location_id)
        {
            MySqlConnection conn = new MySqlConnection();
            Dictionary<string, string> location = new Dictionary<string, string>();
            try{ 
                string query = "SELECT l.location_id, l.name, a.street, a.street_line2, a.city, a.state, a.zip "
                               + "FROM location as l "
                               + "LEFT JOIN address as a ON l.address_id = a.address_id "
                                + "WHERE l.location_id = @location_id; ";
                
                conn = connection.OpenConnection();
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@location_id", location_id);

                MySqlDataReader rdr = cmd.ExecuteReader();

                rdr.Read();
                location.Add("location_id", "" + (rdr.IsDBNull(0) ? -1 : rdr.GetInt32(0)) );
                location.Add("name", (rdr.IsDBNull(1) ? "" : rdr.GetString(1)) );
                location.Add("street", (rdr.IsDBNull(2) ? "" : rdr.GetString(2)) );
                location.Add("street_line2", (rdr.IsDBNull(3) ? "" : rdr.GetString(3)) );
                location.Add("city", (rdr.IsDBNull(4) ? "" : rdr.GetString(4)) );
                location.Add("state", (rdr.IsDBNull(5) ? "" : rdr.GetString(5)) );
                location.Add("zip", (rdr.IsDBNull(6) ? "" : rdr.GetString(6)) );

                rdr.Close();

            }catch (Exception e){Console.WriteLine(e.Message); Console.WriteLine(e.StackTrace);}
            finally{
                connection.CloseConnection(conn);
            }
            
            return location;
        }

        public List<Dictionary<string, string>> GetLocationsByZip(string zip){
            MySqlConnection conn = new MySqlConnection();
            List<Dictionary<string, string>> locations = new List<Dictionary<string, string>>();
            try{ 
                string query = "SELECT l.location_id, l.name, a.street, a.street_line2, a.city, a.state, a.zip "
                               + "FROM location as l "
                               + "LEFT JOIN address as a ON l.address_id = a.address_id " 
                               + "WHERE a.zip=@zip; ";
                
                conn = connection.OpenConnection();
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@zip", zip);

                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {

                    Dictionary<string, string> location = new Dictionary<string, string>();
                    location.Add("location_id", "" + (rdr.IsDBNull(0) ? -1 : rdr.GetInt32(0)) );
                    location.Add("name", (rdr.IsDBNull(1) ? "" : rdr.GetString(1)) );
                    location.Add("street", (rdr.IsDBNull(2) ? "" : rdr.GetString(2)) );
                    location.Add("street_line2", (rdr.IsDBNull(3) ? "" : rdr.GetString(3)) );
                    location.Add("city", (rdr.IsDBNull(4) ? "" : rdr.GetString(4)) );
                    location.Add("state", (rdr.IsDBNull(5) ? "" : rdr.GetString(5)) );
                    location.Add("zip", (rdr.IsDBNull(6) ? "" : rdr.GetString(6)) );

                    locations.Add(location);
                }
                rdr.Close();

            }catch (Exception e){Console.WriteLine(e.Message); Console.WriteLine(e.StackTrace);}
            finally{
                connection.CloseConnection(conn);
            }
            
            return locations;
        }
        
        public List<Dictionary<string, string>> GetLocationsByCity(string city){
            MySqlConnection conn = new MySqlConnection();
            List<Dictionary<string, string>> locations = new List<Dictionary<string, string>>();
            try{ 
                string query = "SELECT l.location_id, l.name, a.street, a.street_line2, a.city, a.state, a.zip "
                               + "FROM location as l "
                               + "LEFT JOIN address as a ON l.address_id = a.address_id " 
                               + "WHERE a.city=@city; ";
                
                conn = connection.OpenConnection();
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@city", city);

                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {

                    Dictionary<string, string> location = new Dictionary<string, string>();
                    location.Add("location_id", "" + (rdr.IsDBNull(0) ? -1 : rdr.GetInt32(0)) );
                    location.Add("name", (rdr.IsDBNull(1) ? "" : rdr.GetString(1)) );
                    location.Add("street", (rdr.IsDBNull(2) ? "" : rdr.GetString(2)) );
                    location.Add("street_line2", (rdr.IsDBNull(3) ? "" : rdr.GetString(3)) );
                    location.Add("city", (rdr.IsDBNull(4) ? "" : rdr.GetString(4)) );
                    location.Add("state", (rdr.IsDBNull(5) ? "" : rdr.GetString(5)) );
                    location.Add("zip", (rdr.IsDBNull(6) ? "" : rdr.GetString(6)) );

                    locations.Add(location);
                }
                rdr.Close();

            }catch (Exception e){Console.WriteLine(e.Message); Console.WriteLine(e.StackTrace);}
            finally{
                connection.CloseConnection(conn);
            }
            
            return locations;
        }
        
        public bool InsertLocationWithAddress(Location location){
            bool result = false;
            MySqlConnection conn = connection.OpenConnection();
            MySqlTransaction myTrans =  conn.BeginTransaction();

            try
            {
                //insert address
                string query = "INSERT INTO address (street, street_line2, city, state, zip) VALUES(@street, @street_line2, @city, @state, @zip)";

                MySqlCommand insertAddress = new MySqlCommand(query, conn);
                insertAddress.Parameters.AddWithValue("@street", location.street);
                insertAddress.Parameters.AddWithValue("@street_line2", location.street_line2);
                insertAddress.Parameters.AddWithValue("@city", location.city);
                insertAddress.Parameters.AddWithValue("@state", location.state);
                insertAddress.Parameters.AddWithValue("@zip", location.zip);

                int numAffected = insertAddress.ExecuteNonQuery();

                
                //address insert failed
                if (numAffected < 1)
                {
                    myTrans.Rollback();
                    return false;
                }

                query = "SELECT address_id FROM address WHERE street=@street AND city=@city AND state=@state and zip=@zip";
                MySqlCommand selectAddressId = new MySqlCommand(query, conn);
                selectAddressId.Parameters.AddWithValue("@street", location.street);
                selectAddressId.Parameters.AddWithValue("@city", location.city);
                selectAddressId.Parameters.AddWithValue("@state", location.state);
                selectAddressId.Parameters.AddWithValue("@zip", location.zip);
                
                MySqlDataReader rdr = selectAddressId.ExecuteReader();
                
                rdr.Read();
                int addressId = rdr.GetInt32(0);
                rdr.Close();
                
                Console.WriteLine("Address was inserted with id " + addressId);

                //insert location
                query = "INSERT INTO location (name, address_id) VALUES(@name, @address_id);";
                MySqlCommand insertLocation = new MySqlCommand(query, conn);
                insertLocation.Parameters.AddWithValue("@name", location.name);
                insertLocation.Parameters.AddWithValue("@address_id", addressId);
                numAffected = insertLocation.ExecuteNonQuery();
                
                Console.WriteLine("Num affected after insert location: " + numAffected);
                               
                //location insert failed
                if (numAffected < 1)
                {
                    myTrans.Rollback();
                    return false;
                }

                //success
                myTrans.Commit();
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message); 
                Console.WriteLine(e.StackTrace);
                try{ myTrans.Rollback(); } catch(Exception ex){}
                return false;
            } // probably should log something here eventually
            finally{
                connection.CloseConnection(conn);
            }
            
            return true;

        }
        
        public bool InsertLocation(Location location){
            bool result = false;
            MySqlConnection conn = connection.OpenConnection();

            try
            {
                
                //insert location
                string query = "INSERT INTO location (name, address_id) VALUES(@name, @address_id);";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@name", location.name);
                cmd.Parameters.AddWithValue("@address_id", location.address_id);
                int numAffected = cmd.ExecuteNonQuery();
                               
                //location insert failed
                if (numAffected < 1)
                {
                    return false;
                }
                
            }
            catch (Exception e) {
                Console.WriteLine(e.Message); 
                Console.WriteLine(e.StackTrace);
                return false;
            } // probably should log something here eventually
            finally{
                connection.CloseConnection(conn);
            }
            
            return true;

        }
        
        public bool UpdateLocation(Location location)
        {
            bool result = false;
            MySqlConnection conn = connection.OpenConnection();
            string query;
            int rows;
            
            try{
                
                //if name is set, update name
                if (!string.IsNullOrEmpty(location.name))
                {
                    query = "UPDATE location SET name=@name WHERE location_id=@location_id;";
                    MySqlCommand locationUpdate = new MySqlCommand(query, conn);
                    locationUpdate.Parameters.AddWithValue("@name", location.name);
                    locationUpdate.Parameters.AddWithValue("location_id", location.location_id);
                    
                    rows = locationUpdate.ExecuteNonQuery();

                    if(rows > 0){
                        result = true;
                    }
                }
                
                //update address if set
                
                //build update string with whatever is set
                query="Update address set  ";

                if (!String.IsNullOrEmpty(location.street)) {
                    query += "street=@street, ";
                }

                if (!String.IsNullOrEmpty(location.street_line2)) {
                    query += "street_line2=@street_line2, ";
                }
                
                if (!String.IsNullOrEmpty(location.city)) {
                    query += "city=@city, ";
                }
                
                if (!String.IsNullOrEmpty(location.state)) {
                    query += "state=@state, ";
                }
                
                if (!String.IsNullOrEmpty(location.zip)) {
                    query += "zip=@zip, ";
                }

                // remove trailing ,
                if (query[query.Length - 2] == ',')
                {
                    query = query.Remove(query.Length - 2);
                }
                else
                {
                    //none of the values are set, so nothing should happen
                    return result;
                }

                query += " WHERE address_id = (SELECT address_id FROM location WHERE location_id=@location_id);";

                MySqlCommand cd = new MySqlCommand(query, conn);
                
                if (!String.IsNullOrEmpty(location.street))
                {
                    cd.Parameters.AddWithValue("@street", location.street);
                }

                if (!String.IsNullOrEmpty(location.street_line2)) {
                    cd.Parameters.AddWithValue("@street", location.street);
                }
                
                if (!String.IsNullOrEmpty(location.city)) {
                    cd.Parameters.AddWithValue("@street", location.street);
                }
                
                if (!String.IsNullOrEmpty(location.state)) {
                    cd.Parameters.AddWithValue("@street", location.street);
                }
                
                if (!String.IsNullOrEmpty(location.zip)) {
                    cd.Parameters.AddWithValue("@street", location.street);
                }
                
                cd.Parameters.AddWithValue("@location_id", location.location_id);

                rows = cd.ExecuteNonQuery();

                if(rows > 0) {
                    result = true;
                }
                
            } catch (Exception e){ 
                Console.WriteLine(e.Message); 
                Console.WriteLine(e.StackTrace);
                return false;
            } // probably should log something here eventually
            finally{
                connection.CloseConnection(conn);
            }

            return result;
        }

        public bool InsertStaffLocation(StaffLocation sl)
        {
            MySqlConnection conn = connection.OpenConnection();

            try
            {
                
                string query = "INSERT INTO staff_location (staff_id, location_id) VALUES(@staff_id, @location_id);";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@staff_id", sl.staff_id);
                cmd.Parameters.AddWithValue("@location_id", sl.location_id);
                int numAffected = cmd.ExecuteNonQuery();
                               
                //location insert failed
                if (numAffected < 1)
                {
                    return false;
                }
                
            }
            catch (Exception e) {
                Console.WriteLine(e.Message); 
                Console.WriteLine(e.StackTrace);
                return false;
            } // probably should log something here eventually
            finally{
                connection.CloseConnection(conn);
            }
            
            return true;
        }

        public bool DeleteStaffLocation(StaffLocation sl)
        {
            MySqlConnection conn = connection.OpenConnection();

            try
            {
                
                string query = "DELETE FROM staff_location WHERE staff_id=@staff_id AND location_id=@location_id;";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@staff_id", sl.staff_id);
                cmd.Parameters.AddWithValue("@location_id", sl.location_id);
                int numAffected = cmd.ExecuteNonQuery();
                               
                //location delete failed
                if (numAffected < 1)
                {
                    return false;
                }
                
            }
            catch (Exception e) {
                Console.WriteLine(e.Message); 
                Console.WriteLine(e.StackTrace);
                return false;
            } // probably should log something here eventually
            finally{
                connection.CloseConnection(conn);
            }
            
            return true;
        }

        public List<Dictionary<string, string>> GetStaffByLocation(int location_id)
        {
            MySqlConnection conn = new MySqlConnection();
            List<Dictionary<string, string>> staffList = new List<Dictionary<string, string>>();
            try{ 
                string query = "SELECT s.staff_id, s.email, s.first_name, s.last_name "
                               + "FROM staff as s "
                               + "RIGHT JOIN staff_location as l ON l.staff_id = s.staff_id " 
                               + "WHERE l.location_id=@location_id; ";
                
                conn = connection.OpenConnection();
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@location_id", location_id);

                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {

                    Dictionary<string, string> staff = new Dictionary<string, string>();
                    staff.Add("staff_id", "" + (rdr.IsDBNull(0) ? -1 : rdr.GetInt32(0)) );
                    staff.Add("email", (rdr.IsDBNull(1) ? "" : rdr.GetString(1)) );
                    staff.Add("first_name", (rdr.IsDBNull(2) ? "" : rdr.GetString(2)) );
                    staff.Add("last_name", (rdr.IsDBNull(3) ? "" : rdr.GetString(3)) );

                    staffList.Add(staff);
                }
                rdr.Close();

            }catch (Exception e){Console.WriteLine(e.Message); Console.WriteLine(e.StackTrace);}
            finally{
                connection.CloseConnection(conn);
            }
            
            return staffList;
        }
    }
}