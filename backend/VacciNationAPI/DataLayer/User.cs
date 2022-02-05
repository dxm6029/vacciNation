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

            } catch (Exception e){ } // probably should log something here eventually
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

            }catch (Exception e){ }
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

            }catch (Exception e){ }
            finally{
                connection.CloseConnection(conn);
            }
            
            return staff;
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
            try{ 
                conn = connection.OpenConnection();

                string query = "UPDATE citizen SET";

                if(citizen.email != null){
                    query += " email = @email,";
                }

                if(citizen.first_name != null){
                    query += " first_name = @firstName,";
                }

                if(citizen.last_name != null){
                    query += " last_name = @lastName,";
                }

                if(citizen.phone_number != null){
                    query += " phone_number = @phone_number,";
                }

                if(citizen.date_of_birth != null){
                    query += " date_of_birth = @date_of_birth,";
                }

                // TODO: may need to add stuff in here for in here for insurance and address later on!!

                query = query.TrimEnd(',');

                query += " WHERE citizen_id = @id";

                MySqlCommand cmd = new MySqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@id", citizen.citizen_id);

                if(citizen.email != null){
                    cmd.Parameters.AddWithValue("@email", citizen.email);
                }

                if(citizen.first_name != null){
                    cmd.Parameters.AddWithValue("@firstName", citizen.first_name);
                }

                if(citizen.last_name != null){
                    cmd.Parameters.AddWithValue("@lastName", citizen.last_name);
                }

                if(citizen.phone_number != null){
                    cmd.Parameters.AddWithValue("@phone_number", citizen.phone_number);
                }

                if(citizen.date_of_birth != null){
                    cmd.Parameters.AddWithValue("@date_of_birth", citizen.date_of_birth);
                }

                int rows = cmd.ExecuteNonQuery();

                if(rows > 0){
                    status = true;
                }

            }catch (Exception e){ }
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

                string query = "SELECT citizen_id, email, last_name, first_name, insurance_id, date_of_birth, phone_number, address_id FROM citizen WHERE email=@email AND first_name=@firstname AND last_name=@lastname";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@firstname", firstname);
                cmd.Parameters.AddWithValue("@lastname", lastname);

                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {   
                    citizen = new Citizen(rdr.GetInt32(0), rdr.GetString(1), rdr.GetString(2), rdr.GetString(3), rdr.IsDBNull(4) ? -1 : rdr.GetInt32(7) , rdr.GetMySqlDateTime(5).ToString(), rdr.GetString(6) != null ? rdr.GetString(6) : "", rdr.IsDBNull(7) ? -1 : rdr.GetInt32(7) );
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

                string query = "SELECT citizen_id, email, last_name, first_name, insurance_id, date_of_birth, phone_number, address_id FROM citizen WHERE citizen_id=@id";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);

                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {   
                    citizen = new Citizen(rdr.GetInt32(0), rdr.GetString(1), rdr.GetString(2), rdr.GetString(3), rdr.IsDBNull(4) ? -1 : rdr.GetInt32(7) , rdr.GetMySqlDateTime(5).ToString(), rdr.GetString(6) != null ? rdr.GetString(6) : "", rdr.IsDBNull(7) ? -1 : rdr.GetInt32(7) );
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
                    staff.Add("{ staff_id: " + rdr.GetInt32(0).ToString() + ", email: " + rdr.GetString(1) + ", username: " + rdr.GetString(2)+  ", last_name: " +  rdr.GetString(3)+ ", first_name: " +  rdr.GetString(4) + ", role: " +  rdr.GetString(5) + "}");
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
                    citizen.Add(new Citizen(rdr.GetInt32(0), rdr.IsDBNull(1) ?  "" : rdr.GetString(1), rdr.IsDBNull(2) ?  "" : rdr.GetString(2), rdr.IsDBNull(3) ?  "": rdr.GetString(3), rdr.IsDBNull(4) ? -1 : rdr.GetInt32(4) , rdr.GetMySqlDateTime(5).ToString(), rdr.IsDBNull(6) ?  "" : rdr.GetString(6), rdr.IsDBNull(7) ? -1 : rdr.GetInt32(7)));
                }
                rdr.Close();

            }catch (Exception e){ Console.WriteLine(e.Message); Console.WriteLine(e.StackTrace); }
            finally{
                connection.CloseConnection(conn);
            }
            
            return citizen;
        }


    } 

}//namespace