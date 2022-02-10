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

        public bool updateTimeslotStatus(Timeslot timeslot){
            bool result = false;
            MySqlConnection conn = connection.OpenConnection();
            try{
               
                string query="Update timeslot set status_id=@status WHERE timeslot_id = @timeslot_id;";
                MySqlCommand cd = new MySqlCommand(query, conn);
                cd.Parameters.AddWithValue("@status", timeslot.status);
                cd.Parameters.AddWithValue("@timeslot_id", timeslot.timeslot_id);

                int rows = cd.ExecuteNonQuery();

                if(rows > 0){
                    result = true;
                }

            } catch (Exception e){ 
                Console.WriteLine(e.Message); 
                Console.WriteLine(e.StackTrace);
            } 
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

        public List<string> getAllAppointments(bool open){
            MySqlConnection conn = new MySqlConnection();
            List<string> appointments = new List<string>();
            try{ 
                conn = connection.OpenConnection();

                string query = "SELECT timeslot_id, timeslot.staff_id, staff.first_name, staff.last_name, timeslot.citizen_id, citizen.first_name, citizen.last_name, timeslot.location_id, location.name, timeslot.dose_id, dose.supplier, vaccine.description, timeslot.date, timeslot.status_id, timeslot_status.description FROM timeslot JOIN staff USING(staff_id) JOIN dose USING(dose_id) JOIN timeslot_status USING(status_id) JOIN vaccine USING(vaccine_id) JOIN location USING(location_id) JOIN citizen USING(citizen_id)";
                if(open){
                    query += " WHERE status_id=1";
                }
                MySqlCommand cmd = new MySqlCommand(query, conn);

                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {   
                    appointments.Add("{ appointment_id: " + (rdr.IsDBNull(0) ? -1 : rdr.GetInt32(0)) + ", staff_id: " + ( rdr.IsDBNull(1) ? -1 : rdr.GetInt32(1)) + ", staff_first_name: " + (rdr.IsDBNull(2) ?  "" : rdr.GetString(2))+  ", staff_last_name: " +  (rdr.IsDBNull(3) ?  "" : rdr.GetString(3)) + ", citizen_id: " +  (rdr.IsDBNull(4) ? -1 : rdr.GetInt32(4)) + ", citizen_first_name: " + (rdr.IsDBNull(5) ?  "" : rdr.GetString(5))+  ", citizen_last_name: " +  (rdr.IsDBNull(6) ?  "" : rdr.GetString(6)) + ", location_id: " + ( rdr.IsDBNull(7) ? -1 : rdr.GetInt32(7)) + ", location_name: " + (rdr.IsDBNull(8) ?  "" : rdr.GetString(8)) + ", dose_id: " + ( rdr.IsDBNull(9) ? -1 : rdr.GetInt32(9)) + ", supplier: " + (rdr.IsDBNull(10) ?  "" : rdr.GetString(10)) + ", description: " + (rdr.IsDBNull(11) ?  "" : rdr.GetString(11)) + ", date: " + (rdr.IsDBNull(12) ?  "" : rdr.GetString(12))  + ", status_id: " + ( rdr.IsDBNull(13) ? -1 : rdr.GetInt32(13)) + ", status_desc: " + (rdr.IsDBNull(14) ?  "" : rdr.GetString(14)) +"}");
                }
                rdr.Close();

            }catch (Exception e){Console.WriteLine(e.Message); Console.WriteLine(e.StackTrace);}
            finally{
                connection.CloseConnection(conn);
            }
            
            return appointments;
        }

    } 

}//namespace