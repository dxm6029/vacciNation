using VacciNationAPI.Models;
using System;
using MySql.Data.MySqlClient; 

namespace VacciNationAPI.DataLayer
{

    public class User {

        VacciNation.Connect connection = new VacciNation.Connect();

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

         public bool insertCitizen(string email, string lastName, string firstName){
            bool result = false;
            MySqlConnection conn = connection.OpenConnection();

            try{
                string query = "INSERT INTO citizen (email, last_name, first_name) VALUES(@email, @lastName, @firstName)";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@email", email);
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

                // TODO: may need to add stuff in here for in here for insurance later on!!

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

    } 

}//namespace