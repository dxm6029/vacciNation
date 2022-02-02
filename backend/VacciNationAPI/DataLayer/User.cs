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
                    query += " last_name = @lastname,";
                }

                query = query.TrimEnd(',');

                query += " WHERE staff_id = @id";

                MySqlCommand cmd = new MySqlCommand(query, conn);

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
                    cmd.Parameters.AddWithValue("@lastname", staff.last_name);
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

    } 

}//namespace