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


        public int updateTimeslotCitizenDose(Timeslot timeslot, int vaccineType){
            int result = -1;
            MySqlConnection conn = connection.OpenConnection();

            try{

                // // check to make sure a dose of that type is in the dose table - need to add to make sure it is available 
                // string query = "SELECT dose_id FROM dose WHERE vaccine_id=@vaccineType";
                // MySqlCommand cmd = new MySqlCommand(query, conn);
                // cmd.Parameters.AddWithValue("@vaccineType", vaccineType);

                // MySqlDataReader rdr = cmd.ExecuteReader();
                // int availableDoseID = -1;

                // // loop through dose of appropriate type
                // while (rdr.Read())
                // {   
                //     // check to make sure dose is not already taken
                //     string availCheck = "SELECT dose_id FROM timeslot WHERE dose_id=@doseId AND status = @stat";
                //     MySqlCommand comm = new MySqlCommand(availCheck, conn);
                //     comm.Parameters.AddWithValue("@doseId", rdr.GetInt32(0));
                //     comm.Parameters.AddWithValue("@stat", 1);

                //     MySqlDataReader rdr2 = cmd.ExecuteReader();
                //     if(rdr2.Read()){
                //         availableDoseID = rdr.GetInt32(0);
                //     }
                // }
                // rdr.Close();

                // // there are no doses of that type available at the moment
                // if(availableDoseID == -1){
                //     return -2;
                // }

                // update the timeslot to inlude that citizen and dose
                string updateSlot = "BEGIN; SELECT @local := dose.dose_id FROM dose LEFT JOIN timeslot on dose.dose_id = timeslot.dose_id WHERE vaccine_id=@vaccine_id AND timeslot_id IS NULL LIMIT 1  FOR UPDATE; Update timeslot set dose_id = IF(ISNULL(@local), dose_id, @local), citizen_id=@citizen_id, status_id=@statusNum WHERE timeslot_id = @timeslot_id; COMMIT;";
                MySqlCommand cd = new MySqlCommand(updateSlot, conn);
                cd.Parameters.AddWithValue("@vaccine_id", vaccineType);
                cd.Parameters.AddWithValue("@citizen_id", timeslot.citizen_id);
                cd.Parameters.AddWithValue("@statusNum", 2);
                cd.Parameters.AddWithValue("@timeslot_id", timeslot.timeslot_id);

                int rows = cd.ExecuteNonQuery();

                if(rows <= 0){
                    return -2;
                }

            } catch (Exception e){ Console.WriteLine(e.Message); Console.WriteLine(e.StackTrace);} // probably should log something here eventually
            finally{
               connection.CloseConnection(conn);
            }
            return result;

        }

    } 

}//namespace