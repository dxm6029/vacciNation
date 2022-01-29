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

        public Staff getUserWithoutID(){
            MySqlConnection conn = new MySqlConnection();
            try{ 
                conn = connection.OpenConnection();

                string query = "SELECT staff_id, email, username, last_name, first_name FROM staff WHERE email=? AND username=?";


            }catch (Exception e){
                return null;
            }
            finally{
                connection.CloseConnection(conn);
            }
            
            return null;
        }

    } 

}//namespace