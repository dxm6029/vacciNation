using VacciNationAPI.Models;
using System;
using MySql.Data.MySqlClient; 
using System.Collections.Generic;
using System.Security.Cryptography;


namespace VacciNationAPI.DataLayer
{

    public class AppointmentManagement {

        VacciNation.Connect connection = new VacciNation.Connect();
        
        public bool insertAppointment(Timeslot timeslot){
             bool result = false;
            MySqlConnection conn = connection.OpenConnection();

            try{
                string query = "INSERT INTO timeslot (location_id, date, status_id) VALUES(@location, @dateTime, @status)";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@location", timeslot.location_id);
                cmd.Parameters.AddWithValue("@dateTime", timeslot.date);
                cmd.Parameters.AddWithValue("@status", timeslot.status);
                
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

    } 

}//namespace