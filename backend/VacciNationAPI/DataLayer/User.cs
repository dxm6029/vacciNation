using VacciNationAPI.Models;
using System;
using MySql.Data.MySqlClient; 
using System.Collections.Generic;
using System.Security.Cryptography;


namespace VacciNationAPI.DataLayer
{

    public class User {

        VacciNation.Connect connection = new VacciNation.Connect();


        // check token - token - user id
        public int checkToken(string token){
            int id = -1;
            MySqlConnection conn = connection.OpenConnection();

            try{
                string query = "SELECT staff_id FROM staff WHERE token=@token";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@token", token);

                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {   
                    id = rdr.GetInt32(0);
                }
                rdr.Close();

            } catch (Exception e){ } // probably should log something here eventually
            finally{
               connection.CloseConnection(conn);
            }
            return id;
        }

        // add token - id - boolean
        public string addToken(int id){

            bool result = false;
            string token = "";
            MySqlConnection conn = connection.OpenConnection();

            using(RandomNumberGenerator rng = new RNGCryptoServiceProvider())
            {
                byte[] tokenData = new byte[32];
                rng.GetBytes(tokenData);

                token = Convert.ToBase64String(tokenData);
            }

            try{
                string query = "UPDATE staff SET token=@token WHERE staff_id=@id";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@token", token);

                int numAffected = cmd.ExecuteNonQuery();

                if(numAffected > 0){
                    result = true;
                }

            } catch (Exception e){ } // probably should log something here eventually
            finally{
               connection.CloseConnection(conn);
            }


            return token;

        }

        public bool removeToken(string auth){

            bool result = false;
            MySqlConnection conn = connection.OpenConnection();

            try{
                string query = "UPDATE staff SET token=NULL WHERE token=@auth";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@auth", auth);

                int numAffected = cmd.ExecuteNonQuery();

                if(numAffected > 0){
                    result = true;
                }

            } catch (Exception e){ } // probably should log something here eventually
            finally{
               connection.CloseConnection(conn);
            }


            return result;

        }

        public Staff checkCreds(string username, string password){
            
            MySqlConnection conn = connection.OpenConnection();
            Staff staff = null;
            try{
                string query = "SELECT staff_id, email, username, last_name, first_name FROM staff WHERE password=@password AND username=@username";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);

                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {   
                    staff = new Staff(rdr.GetInt32(0), rdr.GetString(1), rdr.GetString(2), "", rdr.GetString(3), rdr.GetString(4));
                }
                rdr.Close();

            } catch (Exception e){ } // probably should log something here eventually
            finally{
               connection.CloseConnection(conn);
            }
            return staff;
        }

        public bool insertStaffMember(string email, string username, string password, string lastName, string firstName){
            bool result = false;
            MySqlConnection conn = connection.OpenConnection();

            try{
                string query = "INSERT INTO staff (email, username, password, last_name, first_name) VALUES(@email, @username, @password, @lastName, @firstName)";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);
                cmd.Parameters.AddWithValue("@lastName", lastName);
                cmd.Parameters.AddWithValue("@firstName", firstName);

                int numAffected = cmd.ExecuteNonQuery();

                if(numAffected > 0){
                    result = true;
                }

            } catch (Exception e){ Console.WriteLine(e.Message); Console.WriteLine(e.StackTrace);} // probably should log something here eventually
            finally{
               connection.CloseConnection(conn);
            }
            return result;
        }

        public Staff getUserWithoutID(string email, string username){
            MySqlConnection conn = new MySqlConnection();
            Staff staff = null;
            try{ 
                conn = connection.OpenConnection();

                string query = "SELECT staff_id, email, username, last_name, first_name FROM staff WHERE email=@email AND username=@username";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@username", username);

                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {   
                    staff = new Staff(rdr.GetInt32(0), rdr.GetString(1), rdr.GetString(2), "", rdr.GetString(3), rdr.GetString(4));
                }
                rdr.Close();

            }catch (Exception e){ Console.WriteLine(e.Message); Console.WriteLine(e.StackTrace);}
            finally{
                connection.CloseConnection(conn);
            }
            
            return staff;
        }

        public Staff getUserWithID(int id){
            MySqlConnection conn = new MySqlConnection();
            Staff staff = null;
            try{ 
                conn = connection.OpenConnection();

                string query = "SELECT staff_id, email, username, last_name, first_name FROM staff WHERE staff_id=@id";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);

                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {   
                    staff = new Staff(rdr.GetInt32(0), rdr.GetString(1), rdr.GetString(2), "", rdr.GetString(3), rdr.GetString(4));
                }
                rdr.Close();

            }catch (Exception e){ Console.WriteLine(e.Message); Console.WriteLine(e.StackTrace);}
            finally{
                connection.CloseConnection(conn);
            }
            
            return staff;
        }

        public List<int> getUserRoles(int id){
            MySqlConnection conn = new MySqlConnection();
            List<int> role = new List<int>();
            try{ 
                conn = connection.OpenConnection();

                string query = "SELECT role_id FROM staff JOIN staff_role USING(staff_id) WHERE staff_id=@id";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);

                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {   
                    role.Add(rdr.IsDBNull(0) ? -1: rdr.GetInt32(0));
                }
                rdr.Close();

            }catch (Exception e){ Console.WriteLine(e.Message); Console.WriteLine(e.StackTrace);}
            finally{
                connection.CloseConnection(conn);
            }
            
            return role;
        }

        // do not want to let them reset their usernames for consistency?
        public bool putUserWithID(Staff staff){
            bool status = false;
            MySqlConnection conn = new MySqlConnection();
            try{ 
                conn = connection.OpenConnection();

                string query = "UPDATE staff SET";

                if(staff.password != null){
                    query += " password = @password,";
                }

                if(staff.email != null){
                    query += " email = @email,";
                }

                if(staff.first_name != null){
                    query += " first_name = @firstName,";
                }

                if(staff.last_name != null){
                    query += " last_name = @lastName,";
                }

                query = query.TrimEnd(',');

                query += " WHERE staff_id = @id";

                MySqlCommand cmd = new MySqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@id", staff.staff_id);

                if(staff.password != null){
                    cmd.Parameters.AddWithValue("@password", staff.password);
                }

                if(staff.email != null){
                    cmd.Parameters.AddWithValue("@email", staff.email);
                }

                if(staff.first_name != null){
                    cmd.Parameters.AddWithValue("@firstName", staff.first_name);
                }

                if(staff.last_name != null){
                    cmd.Parameters.AddWithValue("@lastName", staff.last_name);
                }

                int rows = cmd.ExecuteNonQuery();

                if(rows > 0){
                    status = true;
                }

            }catch (Exception e){}
            finally{
                connection.CloseConnection(conn);
            }
            
            return status;
        }

        public bool deleteUser(string email, string username){
            bool status = false;
            MySqlConnection conn = new MySqlConnection();
            try{ 
                conn = connection.OpenConnection();

                string query = "DELETE FROM staff WHERE email = @email AND username = @username";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@username", username);

                int rows = cmd.ExecuteNonQuery();
                
                if(rows > 0){
                    status = true;
                }
            } catch (Exception e){}
            finally {
                connection.CloseConnection(conn);
            }

            return status;
        }

        public bool insertCitizen(string email, string lastName, string firstName, string dob, string phoneNum){
            bool result = false;
            MySqlConnection conn = connection.OpenConnection();

            try{
                string query = "INSERT INTO citizen (email, last_name, first_name, date_of_birth, phone_number) VALUES(@email, @lastName, @firstName, @dob, @phone)";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@lastName", lastName);
                cmd.Parameters.AddWithValue("@firstName", firstName);
                cmd.Parameters.AddWithValue("@dob", dob);
                cmd.Parameters.AddWithValue("@phone", phoneNum);

                int numAffected = cmd.ExecuteNonQuery();

                if(numAffected > 0){
                    result = true;
                }

            } catch (Exception e){ } // probably should log something here eventually
            finally{
               connection.CloseConnection(conn);
            }
            return result;
        }

        public bool putCitizenWithID(Citizen citizen){
            bool status = false;
            MySqlConnection conn = new MySqlConnection();
            try
            {
                conn = connection.OpenConnection();

                string query = "UPDATE citizen SET";

                if (citizen.email != null)
                {
                    query += " email = @email,";
                }

                if (citizen.first_name != null)
                {
                    query += " first_name = @firstName,";
                }

                if (citizen.last_name != null)
                {
                    query += " last_name = @lastName,";
                }

                if (citizen.phone_number != null)
                {
                    query += " phone_number = @phone_number,";
                }

                if (citizen.date_of_birth != null)
                {
                    query += " date_of_birth = @date_of_birth,";
                }

                if (citizen.address_id > 0)
                {
                    query += " address_id = @address_id,";
                }

                if (citizen.insurance_id > 0)
                {
                    query += " insurance_id = @insurance_id,";
                }

                if (citizen.id_type != null)
                {
                    query += " id_type = @id_type,";
                }

                if (citizen.id_number != null)
                {
                    query += "id_number = @id_number,";
                }

                query = query.TrimEnd(',');

                query += " WHERE citizen_id = @id";

                MySqlCommand cmd = new MySqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@id", citizen.citizen_id);

                if (citizen.email != null)
                {
                    cmd.Parameters.AddWithValue("@email", citizen.email);
                }

                if (citizen.first_name != null)
                {
                    cmd.Parameters.AddWithValue("@firstName", citizen.first_name);
                }

                if (citizen.last_name != null)
                {
                    cmd.Parameters.AddWithValue("@lastName", citizen.last_name);
                }

                if (citizen.phone_number != null)
                {
                    cmd.Parameters.AddWithValue("@phone_number", citizen.phone_number);
                }

                if (citizen.date_of_birth != null)
                {
                    cmd.Parameters.AddWithValue("@date_of_birth", citizen.date_of_birth);
                }

                if (citizen.address_id > 0)
                {
                    cmd.Parameters.AddWithValue("@address_id", citizen.address_id);
                }

                if (citizen.insurance_id > 0)
                {
                    cmd.Parameters.AddWithValue("@insurance_id", citizen.insurance_id);
                }

                if (citizen.id_type != null)
                {
                    cmd.Parameters.AddWithValue("@id_type", citizen.id_type);
                }

                if (citizen.id_number != null)
                {
                    cmd.Parameters.AddWithValue("@id_number", citizen.id_number);
                }

                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                {
                    status = true;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Error updating citizen object: " + citizen.ToString());
                Console.WriteLine(e.Message); Console.WriteLine(e.StackTrace);
            }
            finally{
                connection.CloseConnection(conn);
            }
            
            return status;
        }

         public bool deleteCitizen(string email, string firstName, string lastName){
            bool status = false;
            MySqlConnection conn = new MySqlConnection();
            try{ 
                conn = connection.OpenConnection();

                string query = "DELETE FROM citizen WHERE email = @email AND first_name = @firstName AND last_name = @lastName";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@firstName", firstName);
                cmd.Parameters.AddWithValue("@lastName", lastName);

                int rows = cmd.ExecuteNonQuery();
                
                if(rows > 0){
                    status = true;
                }
            } catch (Exception e){}
            finally {
                connection.CloseConnection(conn);
            }

            return status;
        }

        public Citizen getCitizenWithoutID(string email, string firstname, string lastname){
            MySqlConnection conn = new MySqlConnection();
            Citizen citizen = null;
            try{ 
                conn = connection.OpenConnection();

                string query = "SELECT citizen_id, email, last_name, first_name, insurance_id, date_of_birth, phone_number, address_id, id_type, id_number FROM citizen WHERE email=@email AND first_name=@firstname AND last_name=@lastname";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@firstname", firstname);
                cmd.Parameters.AddWithValue("@lastname", lastname);

                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {   
                    citizen = new Citizen(rdr.GetInt32(0), rdr.IsDBNull(1) ?  "" : rdr.GetString(1), rdr.IsDBNull(2) ?  "" : rdr.GetString(2), rdr.IsDBNull(3) ?  "": rdr.GetString(3), rdr.IsDBNull(4) ? -1 : rdr.GetInt32(4) , rdr.GetMySqlDateTime(5).ToString(), rdr.IsDBNull(6) ?  "" : rdr.GetString(6), rdr.IsDBNull(7) ? -1 : rdr.GetInt32(7), rdr.IsDBNull(8) ?  "": rdr.GetString(8),rdr.IsDBNull(9) ?  "": rdr.GetString(9));
                }
                rdr.Close();

            }catch (Exception e){ }
            finally{
                connection.CloseConnection(conn);
            }
            
            return citizen;
        }

         public Citizen getCitizenWithID(int id){
            MySqlConnection conn = new MySqlConnection();
            Citizen citizen = null;
            try{ 
                conn = connection.OpenConnection();

                string query = "SELECT citizen_id, email, last_name, first_name, insurance_id, date_of_birth, phone_number, address_id, id_type, id_number FROM citizen WHERE citizen_id=@id";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);

                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {   
                    citizen = new Citizen(rdr.GetInt32(0), rdr.IsDBNull(1) ?  "" : rdr.GetString(1), rdr.IsDBNull(2) ?  "" : rdr.GetString(2), rdr.IsDBNull(3) ?  "": rdr.GetString(3), rdr.IsDBNull(4) ? -1 : rdr.GetInt32(4) , rdr.GetMySqlDateTime(5).ToString(), rdr.IsDBNull(6) ?  "" : rdr.GetString(6), rdr.IsDBNull(7) ? -1 : rdr.GetInt32(7), rdr.IsDBNull(8) ?  "": rdr.GetString(8),rdr.IsDBNull(9) ?  "": rdr.GetString(9) );
                }
                rdr.Close();

            }catch (Exception e){ Console.WriteLine(e.Message); Console.WriteLine(e.StackTrace); }
            finally{
                connection.CloseConnection(conn);
            }
            
            return citizen;
        }

        public List<string> getAllStaff(){
            MySqlConnection conn = new MySqlConnection();
            List<string> staff = new List<string>();
            try{ 
                conn = connection.OpenConnection();

                string query = "SELECT staff.staff_id, email, username, last_name, first_name, role.name FROM staff JOIN staff_role ON staff.staff_id=staff_role.staff_id JOIN role ON staff_role.role_id=role.role_id";
                MySqlCommand cmd = new MySqlCommand(query, conn);

                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {   
                    staff.Add("{ staff_id: " + rdr.GetInt32(0).ToString() + ", email: " + (rdr.IsDBNull(1) ?  "" : rdr.GetString(1)) + ", username: " + (rdr.IsDBNull(2) ?  "" : rdr.GetString(2))+  ", last_name: " +  (rdr.IsDBNull(3) ?  "" : rdr.GetString(3)) + ", first_name: " +  (rdr.IsDBNull(4) ?  "" : rdr.GetString(4)) + ", role: " +  (rdr.IsDBNull(5) ?  "" : rdr.GetString(5)) + "}");
                }
                rdr.Close();

            }catch (Exception e){Console.WriteLine(e.Message); Console.WriteLine(e.StackTrace);}
            finally{
                connection.CloseConnection(conn);
            }
            
            return staff;
        }

        // this assumes we do not automatically want to get the insurance info of citizens
        public List<Citizen> getAllCitizens(){
             MySqlConnection conn = new MySqlConnection();
            List<Citizen> citizen = new List<Citizen>();
            try{ 
                conn = connection.OpenConnection();

                string query = "SELECT citizen_id, email, last_name, first_name, insurance_id, date_of_birth, phone_number, address_id FROM citizen";
                MySqlCommand cmd = new MySqlCommand(query, conn);

                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {   
                    citizen.Add(new Citizen(rdr.GetInt32(0), rdr.IsDBNull(1) ?  "" : rdr.GetString(1), rdr.IsDBNull(2) ?  "" : rdr.GetString(2), rdr.IsDBNull(3) ?  "": rdr.GetString(3), rdr.IsDBNull(4) ? -1 : rdr.GetInt32(4) , rdr.GetMySqlDateTime(5).ToString(), rdr.IsDBNull(6) ?  "" : rdr.GetString(6), rdr.IsDBNull(7) ? -1 : rdr.GetInt32(7), "", ""));
                }
                rdr.Close();

            }catch (Exception e){ Console.WriteLine(e.Message); Console.WriteLine(e.StackTrace); }
            finally{
                connection.CloseConnection(conn);
            }
            
            return citizen;
        }

        public bool isDuplicateUsername(string username){
            bool isDuplicate = false;
             MySqlConnection conn = new MySqlConnection();
            try{ 
                conn = connection.OpenConnection();

                string query = "SELECT staff_id FROM staff WHERE username=@username";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@username", username);

                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {   
                    isDuplicate = true;
                }
                rdr.Close();

            }catch (Exception e){ Console.WriteLine(e.Message); Console.WriteLine(e.StackTrace); }
            finally{
                connection.CloseConnection(conn);
            }

            return isDuplicate;
        }

        // check user role function - ONLY USE IDs - can adjust later on if needed
        public bool isSuperAdmin (int staff_id){
            bool isAdmin = false;

             MySqlConnection conn = new MySqlConnection();
            try{ 
                conn = connection.OpenConnection();

                string query = "SELECT staff.staff_id FROM staff JOIN staff_role ON staff.staff_id=staff_role.staff_id WHERE staff.staff_id=@id AND role_id=@role";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", staff_id);
                cmd.Parameters.AddWithValue("@role", 1);

                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {   
                    isAdmin = true;
                }
                rdr.Close();

            }catch (Exception e){ Console.WriteLine(e.Message); Console.WriteLine(e.StackTrace); }
            finally{
                connection.CloseConnection(conn);
            }

            return isAdmin;
        }

        public int assignRole(int staff_id, int role_id, int assigner_id){
            int response = -1;
            MySqlConnection conn = new MySqlConnection();
            try{ 
                conn = connection.OpenConnection();

                //check to make sure not assigning the same role twice
                string check = "SELECT staff_id FROM staff_role WHERE staff_id=@staff AND role_id=@role";
                MySqlCommand comm = new MySqlCommand(check, conn);
                comm.Parameters.AddWithValue("@staff", staff_id);
                comm.Parameters.AddWithValue("@role", role_id);

                MySqlDataReader rdr = comm.ExecuteReader();
                 while (rdr.Read())
                {   
                    return -2;
                }
                rdr.Close();

                string query = "INSERT INTO staff_role(staff_id, role_id, granted_by) VALUES (@staff_id, @role_id, @granted_by)";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@staff_id", staff_id);
                cmd.Parameters.AddWithValue("@role_id", role_id);
                cmd.Parameters.AddWithValue("@granted_by", assigner_id);


                response = cmd.ExecuteNonQuery();
            }catch (Exception e){ Console.WriteLine(e.Message); Console.WriteLine(e.StackTrace); }
            finally{
                connection.CloseConnection(conn);
            }
            return response;
        }

        public int deleteRole(int staff_id, int role_id){
            int response = -1;
            MySqlConnection conn = new MySqlConnection();
            try{ 
                conn = connection.OpenConnection();

                //check to make sure not assigning the same role twice
                string check = "SELECT staff_id FROM staff_role WHERE staff_id=@staff AND role_id=@role";
                MySqlCommand comm = new MySqlCommand(check, conn);
                comm.Parameters.AddWithValue("@staff", staff_id);
                comm.Parameters.AddWithValue("@role", role_id);

                bool read = false;
                MySqlDataReader rdr = comm.ExecuteReader();
                 while (rdr.Read())
                {   
                    read = true;
                }
                rdr.Close();
                if(!read){
                    // indicates that cannot remove this staff/role combo because it does not exist
                    return -2;
                }

                string query = "DELETE FROM staff_role WHERE staff_id=@staff_id AND role_id=@role_id";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@staff_id", staff_id);
                cmd.Parameters.AddWithValue("@role_id", role_id);

                response = cmd.ExecuteNonQuery();
            }catch (Exception e){ Console.WriteLine(e.Message); Console.WriteLine(e.StackTrace); }
            finally{
                connection.CloseConnection(conn);
            }
            return response;
        }

         public List<string> getAllStaffWithRole(int role_id){
            MySqlConnection conn = new MySqlConnection();
            List<string> staff = new List<string>();
            try{ 
                conn = connection.OpenConnection();

                string query = "SELECT staff.staff_id, email, username, last_name, first_name, role.name FROM staff JOIN staff_role ON staff.staff_id=staff_role.staff_id JOIN role ON staff_role.role_id=role.role_id WHERE role.role_id=@id";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", role_id);

                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {   
                    staff.Add("{ staff_id: " + rdr.GetInt32(0).ToString() + ", email: " + (rdr.IsDBNull(1) ?  "" : rdr.GetString(1)) + ", username: " + (rdr.IsDBNull(2) ?  "" : rdr.GetString(2))+  ", last_name: " +  (rdr.IsDBNull(3) ?  "" : rdr.GetString(3)) + ", first_name: " +  (rdr.IsDBNull(4) ?  "" : rdr.GetString(4)) + ", role: " +  (rdr.IsDBNull(5) ?  "" : rdr.GetString(5)) + "}");
                }
                rdr.Close();

            }catch (Exception e){Console.WriteLine(e.Message); Console.WriteLine(e.StackTrace);}
            finally{
                connection.CloseConnection(conn);
            }
            
            return staff;
        }

        public bool insertAddressForCitizen(Address address, int citizen_id){
            bool result = false;
            bool res = false;
            MySqlConnection conn = connection.OpenConnection();

            try{
                string query = "INSERT INTO address (zip, street, street_line2, city, state) VALUES(@zip, @street, @street_line2, @city, @state)";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@zip", address.zip);
                cmd.Parameters.AddWithValue("@street", address.street);
                cmd.Parameters.AddWithValue("@street_line2", address.street_line2);
                cmd.Parameters.AddWithValue("@city", address.city);
                cmd.Parameters.AddWithValue("@state", address.state);

                int numAffected = cmd.ExecuteNonQuery();

                if(numAffected > 0){
                    result = true;
                }

                // get id
                int address_id = (int)cmd.LastInsertedId;

                // update citizen
                Citizen citizen = new Citizen(citizen_id, null, null, null, -1, null, null, address_id, null, null);


                res = putCitizenWithID(citizen);

            } catch (Exception e){ Console.WriteLine(e.Message); Console.WriteLine(e.StackTrace);} // probably should log something here eventually
            finally{
               connection.CloseConnection(conn);
            }
            return result && res;
        }

        public bool removeAddressForCitizen(int address_id, int citizen_id){
            bool result = false;
            bool res = false;
            MySqlConnection conn = connection.OpenConnection();

            try{

                // update citizen
                string query = "UPDATE citizen SET address_id=NULL WHERE citizen_id=@citizen_id";
                MySqlCommand comm = new MySqlCommand(query, conn);
                comm.Parameters.AddWithValue("@citizen_id", citizen_id);

                int num = comm.ExecuteNonQuery();

                if(num > 0){
                    res = true;
                }

                query = "DELETE FROM address WHERE address_id=@id";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", address_id);

                int numAffected = cmd.ExecuteNonQuery();

                if(numAffected > 0){
                    result = true;
                }

            } catch (Exception e){ Console.WriteLine(e.Message); Console.WriteLine(e.StackTrace);} // probably should log something here eventually
            finally{
               connection.CloseConnection(conn);
            }
            return result && res;
        }

        public bool insertInsuranceForCitizen(Insurance insurance, int citizen_id){
            bool result = false;
            bool res = false;
            MySqlConnection conn = connection.OpenConnection();

            try{
                string query = "INSERT INTO insurance (name, carrier, group_number, member_id) VALUES(@name, @carrier, @group_number, @member_id)";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@name", insurance.name);
                cmd.Parameters.AddWithValue("@carrier", insurance.carrier);
                cmd.Parameters.AddWithValue("@group_number", insurance.group_number);
                cmd.Parameters.AddWithValue("@member_id", insurance.member_id);

                int numAffected = cmd.ExecuteNonQuery();

                if(numAffected > 0){
                    result = true;
                }

                // get id
                int insurance_id = (int)cmd.LastInsertedId;

                // update citizen
                Citizen citizen = new Citizen(citizen_id, null, null, null, insurance_id, null, null, -1, null, null);


                res = putCitizenWithID(citizen);

            } catch (Exception e){ Console.WriteLine(e.Message); Console.WriteLine(e.StackTrace);} // probably should log something here eventually
            finally{
               connection.CloseConnection(conn);
            }
            return result && res;
        }

        public bool removeInsuranceForCitizen(int insurance_id, int citizen_id){
            bool result = false;
            bool res = false;
            MySqlConnection conn = connection.OpenConnection();

            try{

                // update citizen
                string query = "UPDATE citizen SET insurance_id=NULL WHERE citizen_id=@citizen_id";
                MySqlCommand comm = new MySqlCommand(query, conn);
                comm.Parameters.AddWithValue("@citizen_id", citizen_id);

                int num = comm.ExecuteNonQuery();

                if(num > 0){
                    res = true;
                }

                query = "DELETE FROM insurance WHERE insurance_id=@id";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", insurance_id);

                int numAffected = cmd.ExecuteNonQuery();

                if(numAffected > 0){
                    result = true;
                }

            } catch (Exception e){ Console.WriteLine(e.Message); Console.WriteLine(e.StackTrace);} // probably should log something here eventually
            finally{
               connection.CloseConnection(conn);
            }
            return result && res;
        }

         public bool changePassword(int id, string pass){
            bool res = false;
            MySqlConnection conn = connection.OpenConnection();

            try{

                // change password
                string query = "UPDATE staff SET password=@password WHERE staff_id=@staff_id";
                MySqlCommand comm = new MySqlCommand(query, conn);
                comm.Parameters.AddWithValue("@staff_id", id);
                comm.Parameters.AddWithValue("@password", pass);

                int num = comm.ExecuteNonQuery();

                if(num > 0){
                    res = true;
                }

            } catch (Exception e){ Console.WriteLine(e.Message); Console.WriteLine(e.StackTrace);} // probably should log something here eventually
            finally{
               connection.CloseConnection(conn);
            }
            return res;
        }
    } 

}//namespace