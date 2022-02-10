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
                                Console.WriteLine("im here1"); 

            MySqlConnection conn = connection.OpenConnection();
            MySqlTransaction myTrans =  conn.BeginTransaction();
            try{
                string updateSlot = "SELECT dose.dose_id FROM dose LEFT JOIN timeslot on dose.dose_id = timeslot.dose_id WHERE vaccine_id=@vaccine_id AND timeslot_id IS NULL LIMIT 1 FOR UPDATE;";
                MySqlCommand cd = new MySqlCommand(updateSlot, conn);
               
                cd.Transaction = myTrans;
                                    Console.WriteLine("im here2"); 


                cd.Parameters.AddWithValue("@vaccine_id", vaccineType);

                MySqlDataReader rdr = cd.ExecuteReader();
                int dose_id = -1;
                while(rdr.Read()){
                    dose_id = rdr.GetInt32(0);
                                        Console.WriteLine("im here3"); 

                }
                if(dose_id == -1){
                    Console.WriteLine("im here"); 
                    return -2;
                }

                string updateTable="Update timeslot set dose_id = @dose_id, citizen_id=@citizen_id, status_id=@statusNum WHERE timeslot_id = @timeslot_id;";
                cd = new MySqlCommand(updateTable, conn);
                cd.Parameters.AddWithValue("@dose_id", dose_id);
                cd.Parameters.AddWithValue("@citizen_id", timeslot.citizen_id);
                cd.Parameters.AddWithValue("@statusNum", 2);
                cd.Parameters.AddWithValue("@timeslot_id", timeslot.timeslot_id);
                    Console.WriteLine("im here4"); 

                    Console.WriteLine(cd.CommandText);

                int rows = cd.ExecuteNonQuery();
                                                   Console.WriteLine("im here4.5"); 

                myTrans.Commit();
                                    Console.WriteLine("im here5"); 


                if(rows <= 0){
                    return -2;
                }

            } catch (Exception e){ 
                myTrans.Rollback();
                Console.WriteLine(e.Message); 
                Console.WriteLine(e.StackTrace);} // probably should log something here eventually
            finally{
               connection.CloseConnection(conn);
            }
            return result;

        }

    } 

}//namespace