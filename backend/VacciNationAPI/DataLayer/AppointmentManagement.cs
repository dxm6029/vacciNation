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
            MySqlTransaction myTrans =  conn.BeginTransaction();
            try{
                string updateSlot = "SELECT dose.dose_id FROM dose LEFT JOIN timeslot on dose.dose_id = timeslot.dose_id WHERE vaccine_id=@vaccine_id AND timeslot_id IS NULL LIMIT 1 FOR UPDATE;";
                MySqlCommand cd = new MySqlCommand(updateSlot, conn);
               
                cd.Transaction = myTrans;


                cd.Parameters.AddWithValue("@vaccine_id", vaccineType);

                MySqlDataReader rdr = cd.ExecuteReader();
                int dose_id = -1;
                while(rdr.Read()){
                    dose_id = rdr.GetInt32(0);
                }
                rdr.Close();
                if(dose_id == -1){
                    return -2;
                }

                string updateTable="Update timeslot set dose_id = @dose_id, citizen_id=@citizen_id, status_id=@statusNum WHERE timeslot_id = @timeslot_id;";
                cd = new MySqlCommand(updateTable, conn);
                cd.Parameters.AddWithValue("@dose_id", dose_id);
                cd.Parameters.AddWithValue("@citizen_id", timeslot.citizen_id);
                cd.Parameters.AddWithValue("@statusNum", 2);
                cd.Parameters.AddWithValue("@timeslot_id", timeslot.timeslot_id);

                int rows = cd.ExecuteNonQuery();

                myTrans.Commit();

                if(rows <= 0){
                    return -2;
                }
                else{
                    return rows;
                }

            } catch (Exception e){ 
                Console.WriteLine(e.Message); 
                Console.WriteLine(e.StackTrace);} // probably should log something here eventually
                try{myTrans.Rollback();} catch(Exception ex){}
            finally{
               connection.CloseConnection(conn);
            }
            return result;

        }

        public bool updateTimeslotStaff(Timeslot timeslot){
            bool result = false;
            MySqlConnection conn = connection.OpenConnection();
            try{
               
                string query="Update timeslot set staff_id=@staff_id WHERE timeslot_id = @timeslot_id;";
                MySqlCommand cd = new MySqlCommand(query, conn);
                cd.Parameters.AddWithValue("@staff_id", timeslot.staff_id);
                cd.Parameters.AddWithValue("@timeslot_id", timeslot.timeslot_id);

                int rows = cd.ExecuteNonQuery();

                if(rows > 0){
                    result = true;
                }

            } catch (Exception e){ 
                Console.WriteLine(e.Message); 
                Console.WriteLine(e.StackTrace);} // probably should log something here eventually
            finally{
               connection.CloseConnection(conn);
            }

            return result;
        }

        public bool removeTimeslot(int timeslot_id){
             bool status = false;
            MySqlConnection conn = new MySqlConnection();
            try{ 
                conn = connection.OpenConnection();

                string query = "DELETE FROM timeslot WHERE timeslot_id = @timeslot";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@timeslot", timeslot_id);

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