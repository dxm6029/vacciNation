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

                string query = "SELECT staff_id, email, username, last_name, first_name FROM staff WHERE email=\"@email\" AND username=\"@username\"";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@username", username);

                Console.WriteLine(query);

                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {   
                    Console.WriteLine("i am in here");
                    Console.WriteLine(rdr[0]);
                    staff = new Staff((int)rdr[0], (string)rdr[1], (string)rdr[2], (string)rdr[3], (string)rdr[4], (string)rdr[5]);
                }
                rdr.Close();

            }catch (Exception e){ }
            finally{
                connection.CloseConnection(conn);
            }
            
            return staff;
        }

    } 

}//namespace