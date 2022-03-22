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

        public bool updateTimeslotStatusWithBatch(Timeslot timeslot, string batch){
            bool result = false;
            MySqlConnection conn = connection.OpenConnection();
            try{
               
                string query="Update timeslot set status_id=3 WHERE timeslot_id = @timeslot_id;";
                MySqlCommand cd = new MySqlCommand(query, conn);
                cd.Parameters.AddWithValue("@timeslot_id", timeslot.timeslot_id);

                int rows = cd.ExecuteNonQuery();

                AppointmentList appointment = getAppointmentsWithID(timeslot.timeslot_id);

                query="Update dose set batch=@batch WHERE dose_id = @dose_id;";
                cd = new MySqlCommand(query, conn);
                cd.Parameters.AddWithValue("@batch", batch);
                cd.Parameters.AddWithValue("@dose_id", appointment.dose_id);

                int rows2 = cd.ExecuteNonQuery();


                if(rows > 0 && rows2 > 0){
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


        public bool updateTimeslotReactions(Timeslot timeslot){
            bool result = false;
            MySqlConnection conn = connection.OpenConnection();
            try{
               
                string query="Update timeslot set reactions=@reactions WHERE timeslot_id = @timeslot_id;";
                MySqlCommand cd = new MySqlCommand(query, conn);
                cd.Parameters.AddWithValue("@reactions", timeslot.reactions);
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
        public List<AppointmentList> getAllAppointmentsByType(bool open, string supplier, int categoryId){
            MySqlConnection conn = new MySqlConnection();
            List<AppointmentList> appointments = new List<AppointmentList>();
            try{ 
                conn = connection.OpenConnection();

                string query = "SELECT timeslot_id, timeslot.staff_id, staff.first_name, staff.last_name, timeslot.citizen_id, citizen.first_name, citizen.last_name, timeslot.location_id, location.name, timeslot.dose_id, dose.supplier, vaccine_category.category_id, vaccine_category.name, timeslot.date, timeslot.status_id, timeslot_status.description FROM timeslot JOIN staff USING(staff_id) JOIN dose USING(dose_id) JOIN timeslot_status USING(status_id) JOIN vaccine USING(vaccine_id) JOIN location ON timeslot.location_id=location.location_id LEFT JOIN citizen USING(citizen_id) JOIN vaccine_category ON vaccine_category.category_id=vaccine.category WHERE dose.supplier=@supplier AND vaccine.category=@category";
                if(open){
                    query += " AND status_id=1";
                }
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@supplier", supplier);
                cmd.Parameters.AddWithValue("@category", categoryId);

                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {   
                    appointments.Add(new AppointmentList(rdr.IsDBNull(0) ? -1 : rdr.GetInt32(0), rdr.IsDBNull(1) ? -1 : rdr.GetInt32(1), rdr.IsDBNull(2) ?  "" : rdr.GetString(2), rdr.IsDBNull(3) ?  "" : rdr.GetString(3), rdr.IsDBNull(4) ? -1 : rdr.GetInt32(4),rdr.IsDBNull(5) ?  "" : rdr.GetString(5),rdr.IsDBNull(6) ?  "" : rdr.GetString(6),rdr.IsDBNull(7) ? -1 : rdr.GetInt32(7), rdr.IsDBNull(8) ?  "" : rdr.GetString(8),rdr.IsDBNull(9) ? -1 : rdr.GetInt32(9), rdr.IsDBNull(10) ?  "" : rdr.GetString(10),rdr.IsDBNull(11) ? -1: rdr.GetInt32(11),rdr.IsDBNull(12) ?  "" : rdr.GetString(12), rdr.IsDBNull(13) ?  "" : rdr.GetString(13), rdr.IsDBNull(14) ? -1 : rdr.GetInt32(14), rdr.IsDBNull(15) ?  "" : rdr.GetString(15)));
                }
                rdr.Close();

            }catch (Exception e){Console.WriteLine(e.Message); Console.WriteLine(e.StackTrace);}
            finally{
                connection.CloseConnection(conn);
            }
            
            return appointments;
        }

        public List<Dictionary<string, string>> getAllAppointments(bool open){
            MySqlConnection conn = new MySqlConnection();
            List<Dictionary<string, string>> appointments = new List<Dictionary<string, string>>();
            try{ 
                conn = connection.OpenConnection();

                string query = "SELECT timeslot_id, timeslot.staff_id, staff.first_name, staff.last_name, timeslot.citizen_id, citizen.first_name, citizen.last_name, timeslot.location_id, location.name, timeslot.dose_id, dose.supplier, vaccine_category.category_id, vaccine_category.name, timeslot.date, timeslot.status_id, timeslot_status.description FROM timeslot JOIN staff USING(staff_id) JOIN dose USING(dose_id) JOIN timeslot_status USING(status_id) JOIN vaccine USING(vaccine_id) JOIN location ON location.location_id=timeslot.location_id LEFT JOIN citizen USING(citizen_id) JOIN vaccine_category ON vaccine_category.category_id=vaccine.category";
                if(open){
                    query += " WHERE status_id=1";
                }
                MySqlCommand cmd = new MySqlCommand(query, conn);

                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Dictionary<string, string> appointment = new Dictionary<string, string>();
                    appointment.Add("appointment_id", "" + (rdr.IsDBNull(0) ? -1 : rdr.GetInt32(0)) );
                    appointment.Add("staff_id", "" + (rdr.IsDBNull(1) ? -1 : rdr.GetInt32(1)));
                    appointment.Add("staff_first_name", (rdr.IsDBNull(2) ?  "" : rdr.GetString(2)));
                    appointment.Add("staff_last_name", (rdr.IsDBNull(3) ?  "" : rdr.GetString(3)));
                    appointment.Add("citizen_id", "" + (rdr.IsDBNull(4) ? -1 : rdr.GetInt32(4)));
                    appointment.Add("citizen_first_name", (rdr.IsDBNull(5) ?  "" : rdr.GetString(5)));
                    appointment.Add("citizen_last_name", (rdr.IsDBNull(6) ?  "" : rdr.GetString(6)));
                    appointment.Add("location_id", "" + (rdr.IsDBNull(7) ? -1 : rdr.GetInt32(7)));
                    appointment.Add("location_name", (rdr.IsDBNull(8) ? "" : rdr.GetString(8)));
                    appointment.Add("dose_id", "" + (rdr.IsDBNull(9) ? -1 : rdr.GetInt32(9)));
                    appointment.Add("supplier", (rdr.IsDBNull(10) ?  "" : rdr.GetString(10)));
                    appointment.Add("category_id", "" + (rdr.IsDBNull(11) ? -1 : rdr.GetInt32(11)));
                    appointment.Add("description", (rdr.IsDBNull(12) ? "" : rdr.GetString(12)));
                    appointment.Add("date", (rdr.IsDBNull(13) ? "" : rdr.GetString(13)));
                    appointment.Add("status_id", "" + ( rdr.IsDBNull(14) ? -1 : rdr.GetInt32(14)));
                    appointment.Add("status_desc", (rdr.IsDBNull(15) ? "" : rdr.GetString(15)));
                    
                    appointments.Add(appointment);
                }
                rdr.Close();

            }catch (Exception e){Console.WriteLine(e.Message); Console.WriteLine(e.StackTrace);}
            finally{
                connection.CloseConnection(conn);
            }
            
            return appointments;
        }

        public List<AppointmentList> getAllAppointmentsForCitizen(int citizen_id){
            MySqlConnection conn = new MySqlConnection();
            List<AppointmentList> appointments = new List<AppointmentList>();
            try{ 
                conn = connection.OpenConnection();

                string query = "SELECT timeslot_id, timeslot.staff_id, staff.first_name, staff.last_name, timeslot.citizen_id, citizen.first_name, citizen.last_name, timeslot.location_id, location.name, timeslot.dose_id, dose.supplier, vaccine_category.category_id, vaccine_category.name, timeslot.date, timeslot.status_id, timeslot_status.description, timeslot.reactions FROM timeslot JOIN staff USING(staff_id) JOIN dose USING(dose_id) JOIN timeslot_status USING(status_id) JOIN vaccine USING(vaccine_id) JOIN location ON timeslot.location_id=location.location_id LEFT JOIN citizen USING(citizen_id) JOIN vaccine_category ON vaccine_category.category_id=vaccine.category WHERE citizen.citizen_id=@citizen_id";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@citizen_id", citizen_id);

                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {   
                    appointments.Add(new AppointmentList(rdr.IsDBNull(0) ? -1 : rdr.GetInt32(0), rdr.IsDBNull(1) ? -1 : rdr.GetInt32(1), rdr.IsDBNull(2) ?  "" : rdr.GetString(2), rdr.IsDBNull(3) ?  "" : rdr.GetString(3), rdr.IsDBNull(4) ? -1 : rdr.GetInt32(4),rdr.IsDBNull(5) ?  "" : rdr.GetString(5),rdr.IsDBNull(6) ?  "" : rdr.GetString(6),rdr.IsDBNull(7) ? -1 : rdr.GetInt32(7), rdr.IsDBNull(8) ?  "" : rdr.GetString(8),rdr.IsDBNull(9) ? -1 : rdr.GetInt32(9), rdr.IsDBNull(10) ?  "" : rdr.GetString(10),rdr.IsDBNull(11) ? -1: rdr.GetInt32(11),rdr.IsDBNull(12) ?  "" : rdr.GetString(12), rdr.IsDBNull(13) ?  "" : rdr.GetString(13), rdr.IsDBNull(14) ? -1 : rdr.GetInt32(14), rdr.IsDBNull(15) ?  "" : rdr.GetString(15)));
                }
                rdr.Close();

            }catch (Exception e){Console.WriteLine(e.Message); Console.WriteLine(e.StackTrace);}
            finally{
                connection.CloseConnection(conn);
            }
            
            return appointments;
        }

        public AppointmentList getAppointmentsWithID(int appointment_id){
            MySqlConnection conn = new MySqlConnection();
            AppointmentList appointment = null;
            try{ 
                conn = connection.OpenConnection();

                string query = "SELECT timeslot_id, timeslot.staff_id, staff.first_name, staff.last_name, timeslot.citizen_id, citizen.first_name, citizen.last_name, timeslot.location_id, location.name, timeslot.dose_id, dose.supplier, vaccine_category.category_id, vaccine_category.name, timeslot.date, timeslot.status_id, timeslot_status.description FROM timeslot JOIN staff USING(staff_id) JOIN dose USING(dose_id) JOIN timeslot_status USING(status_id) JOIN vaccine USING(vaccine_id) JOIN location ON timeslot.location_id=location.location_id LEFT JOIN citizen USING(citizen_id) JOIN vaccine_category ON vaccine_category.category_id=vaccine.category WHERE timeslot.timeslot_id=@timeslot_id";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@timeslot_id", appointment_id);

                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {   
                   appointment = new AppointmentList(rdr.IsDBNull(0) ? -1 : rdr.GetInt32(0), rdr.IsDBNull(1) ? -1 : rdr.GetInt32(1), rdr.IsDBNull(2) ?  "" : rdr.GetString(2), rdr.IsDBNull(3) ?  "" : rdr.GetString(3), rdr.IsDBNull(4) ? -1 : rdr.GetInt32(4),rdr.IsDBNull(5) ?  "" : rdr.GetString(5),rdr.IsDBNull(6) ?  "" : rdr.GetString(6),rdr.IsDBNull(7) ? -1 : rdr.GetInt32(7), rdr.IsDBNull(8) ?  "" : rdr.GetString(8),rdr.IsDBNull(9) ? -1 : rdr.GetInt32(9), rdr.IsDBNull(10) ?  "" : rdr.GetString(10),rdr.IsDBNull(11) ? -1: rdr.GetInt32(11),rdr.IsDBNull(12) ?  "" : rdr.GetString(12), rdr.IsDBNull(13) ?  "" : rdr.GetString(13), rdr.IsDBNull(14) ? -1 : rdr.GetInt32(14), rdr.IsDBNull(15) ?  "" : rdr.GetString(15));
                }
                rdr.Close();

            }catch (Exception e){Console.WriteLine(e.Message); Console.WriteLine(e.StackTrace);}
            finally{
                connection.CloseConnection(conn);
            }
            
            return appointment;
        }

         public List<AppointmentList> getAllAppointmentsForLocation(bool open, int location_id){
             MySqlConnection conn = new MySqlConnection();
            List<AppointmentList> appointments = new List<AppointmentList>();
            try{ 
                conn = connection.OpenConnection();

                string query = "SELECT timeslot_id, timeslot.staff_id, staff.first_name, staff.last_name, timeslot.citizen_id, citizen.first_name, citizen.last_name, timeslot.location_id, location.name, timeslot.dose_id, dose.supplier, vaccine_category.category_id, vaccine_category.name, timeslot.date, timeslot.status_id, timeslot_status.description FROM timeslot JOIN staff USING(staff_id) JOIN dose USING(dose_id) JOIN timeslot_status USING(status_id) JOIN vaccine USING(vaccine_id) JOIN location ON timeslot.location_id=location.location_id LEFT JOIN citizen USING(citizen_id) JOIN vaccine_category ON vaccine_category.category_id=vaccine.category WHERE timeslot.location_id=@location_id";
                if(open){
                    query += " AND status_id=1";
                }
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@location_id", location_id);

                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {   
                    appointments.Add(new AppointmentList(rdr.IsDBNull(0) ? -1 : rdr.GetInt32(0), rdr.IsDBNull(1) ? -1 : rdr.GetInt32(1), rdr.IsDBNull(2) ?  "" : rdr.GetString(2), rdr.IsDBNull(3) ?  "" : rdr.GetString(3), rdr.IsDBNull(4) ? -1 : rdr.GetInt32(4),rdr.IsDBNull(5) ?  "" : rdr.GetString(5),rdr.IsDBNull(6) ?  "" : rdr.GetString(6),rdr.IsDBNull(7) ? -1 : rdr.GetInt32(7), rdr.IsDBNull(8) ?  "" : rdr.GetString(8),rdr.IsDBNull(9) ? -1 : rdr.GetInt32(9), rdr.IsDBNull(10) ?  "" : rdr.GetString(10),rdr.IsDBNull(11) ? -1: rdr.GetInt32(11),rdr.IsDBNull(12) ?  "" : rdr.GetString(12), rdr.IsDBNull(13) ?  "" : rdr.GetString(13), rdr.IsDBNull(14) ? -1 : rdr.GetInt32(14), rdr.IsDBNull(15) ?  "" : rdr.GetString(15)));
                }
                rdr.Close();

            }catch (Exception e){Console.WriteLine(e.Message); Console.WriteLine(e.StackTrace);}
            finally{
                connection.CloseConnection(conn);
            }
            
            return appointments;
        }

        public List<AppointmentList> getAllAppointmentsForLocationAndType(bool open, int location_id, string supplier, int categoryId){
             MySqlConnection conn = new MySqlConnection();
            List<AppointmentList> appointments = new List<AppointmentList>();
            try{ 
                conn = connection.OpenConnection();

                string query = "SELECT timeslot_id, timeslot.staff_id, staff.first_name, staff.last_name, timeslot.citizen_id, citizen.first_name, citizen.last_name, timeslot.location_id, location.name, timeslot.dose_id, dose.supplier, vaccine_category.category_id, vaccine_category.name, timeslot.date, timeslot.status_id, timeslot_status.description FROM timeslot JOIN staff USING(staff_id) JOIN dose USING(dose_id) JOIN timeslot_status USING(status_id) JOIN vaccine USING(vaccine_id) JOIN location ON timeslot.location_id=location.location_id LEFT JOIN citizen USING(citizen_id) JOIN vaccine_category ON vaccine_category.category_id=vaccine.category WHERE timeslot.location_id=@location_id AND dose.supplier=@supplier AND vaccine.category=@category";
                if(open){
                    query += " AND status_id=1";
                }
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@location_id", location_id);
                cmd.Parameters.AddWithValue("@supplier", supplier);
                cmd.Parameters.AddWithValue("@category", categoryId);

                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {   
                    appointments.Add(new AppointmentList(rdr.IsDBNull(0) ? -1 : rdr.GetInt32(0), rdr.IsDBNull(1) ? -1 : rdr.GetInt32(1), rdr.IsDBNull(2) ?  "" : rdr.GetString(2), rdr.IsDBNull(3) ?  "" : rdr.GetString(3), rdr.IsDBNull(4) ? -1 : rdr.GetInt32(4),rdr.IsDBNull(5) ?  "" : rdr.GetString(5),rdr.IsDBNull(6) ?  "" : rdr.GetString(6),rdr.IsDBNull(7) ? -1 : rdr.GetInt32(7), rdr.IsDBNull(8) ?  "" : rdr.GetString(8),rdr.IsDBNull(9) ? -1 : rdr.GetInt32(9), rdr.IsDBNull(10) ?  "" : rdr.GetString(10),rdr.IsDBNull(11) ? -1: rdr.GetInt32(11),rdr.IsDBNull(12) ?  "" : rdr.GetString(12), rdr.IsDBNull(13) ?  "" : rdr.GetString(13), rdr.IsDBNull(14) ? -1 : rdr.GetInt32(14), rdr.IsDBNull(15) ?  "" : rdr.GetString(15)));
                }
                rdr.Close();

            }catch (Exception e){Console.WriteLine(e.Message); Console.WriteLine(e.StackTrace);}
            finally{
                connection.CloseConnection(conn);
            }
            
            return appointments;
        }

        public List<AppointmentList> getAllAppointmentsForDate(string date){
             MySqlConnection conn = new MySqlConnection();
            List<AppointmentList> appointments = new List<AppointmentList>();
            try{ 

                string start = date + " 00:00:00";
                string end = date + " 23:59:59";
                conn = connection.OpenConnection();

                string query = "SELECT timeslot_id, timeslot.staff_id, staff.first_name, staff.last_name, timeslot.citizen_id, citizen.first_name, citizen.last_name, timeslot.location_id, location.name, timeslot.dose_id, dose.supplier, vaccine_category.category_id, vaccine_category.name, timeslot.date, timeslot.status_id, timeslot_status.description FROM timeslot JOIN staff USING(staff_id) JOIN dose USING(dose_id) JOIN timeslot_status USING(status_id) JOIN vaccine USING(vaccine_id) JOIN location ON timeslot.location_id=location.location_id LEFT JOIN citizen USING(citizen_id) JOIN vaccine_category ON vaccine_category.category_id=vaccine.category WHERE timeslot.date > @start AND timeslot.date < @end";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@start", start);
                cmd.Parameters.AddWithValue("@end", end);

                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {   
                    appointments.Add(new AppointmentList(rdr.IsDBNull(0) ? -1 : rdr.GetInt32(0), rdr.IsDBNull(1) ? -1 : rdr.GetInt32(1), rdr.IsDBNull(2) ?  "" : rdr.GetString(2), rdr.IsDBNull(3) ?  "" : rdr.GetString(3), rdr.IsDBNull(4) ? -1 : rdr.GetInt32(4),rdr.IsDBNull(5) ?  "" : rdr.GetString(5),rdr.IsDBNull(6) ?  "" : rdr.GetString(6),rdr.IsDBNull(7) ? -1 : rdr.GetInt32(7), rdr.IsDBNull(8) ?  "" : rdr.GetString(8),rdr.IsDBNull(9) ? -1 : rdr.GetInt32(9), rdr.IsDBNull(10) ?  "" : rdr.GetString(10),rdr.IsDBNull(11) ? -1: rdr.GetInt32(11),rdr.IsDBNull(12) ?  "" : rdr.GetString(12), rdr.IsDBNull(13) ?  "" : rdr.GetString(13), rdr.IsDBNull(14) ? -1 : rdr.GetInt32(14), rdr.IsDBNull(15) ?  "" : rdr.GetString(15)));
                }
                rdr.Close();

            }catch (Exception e){Console.WriteLine(e.Message); Console.WriteLine(e.StackTrace);}
            finally{
                connection.CloseConnection(conn);
            }
            
            return appointments;
        }

        public List<AppointmentList> getOpenAppointmentsForDate(string date){
             MySqlConnection conn = new MySqlConnection();
            List<AppointmentList> appointments = new List<AppointmentList>();
            try{ 

                string start = date + " 00:00:00";
                string end = date + " 23:59:59";
                conn = connection.OpenConnection();

                string query = "SELECT timeslot_id, timeslot.staff_id, staff.first_name, staff.last_name, timeslot.citizen_id, citizen.first_name, citizen.last_name, timeslot.location_id, location.name, timeslot.dose_id, dose.supplier, vaccine_category.category_id, vaccine_category.name, timeslot.date, timeslot.status_id, timeslot_status.description FROM timeslot JOIN staff USING(staff_id) JOIN dose USING(dose_id) JOIN timeslot_status USING(status_id) JOIN vaccine USING(vaccine_id) JOIN location ON timeslot.location_id=location.location_id LEFT JOIN citizen USING(citizen_id) JOIN vaccine_category ON vaccine_category.category_id=vaccine.category WHERE timeslot.date > @start AND timeslot.date < @end AND timeslot_status.status_id=1";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@start", start);
                cmd.Parameters.AddWithValue("@end", end);

                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {   
                    appointments.Add(new AppointmentList(rdr.IsDBNull(0) ? -1 : rdr.GetInt32(0), rdr.IsDBNull(1) ? -1 : rdr.GetInt32(1), rdr.IsDBNull(2) ?  "" : rdr.GetString(2), rdr.IsDBNull(3) ?  "" : rdr.GetString(3), rdr.IsDBNull(4) ? -1 : rdr.GetInt32(4),rdr.IsDBNull(5) ?  "" : rdr.GetString(5),rdr.IsDBNull(6) ?  "" : rdr.GetString(6),rdr.IsDBNull(7) ? -1 : rdr.GetInt32(7), rdr.IsDBNull(8) ?  "" : rdr.GetString(8),rdr.IsDBNull(9) ? -1 : rdr.GetInt32(9), rdr.IsDBNull(10) ?  "" : rdr.GetString(10),rdr.IsDBNull(11) ? -1: rdr.GetInt32(11),rdr.IsDBNull(12) ?  "" : rdr.GetString(12), rdr.IsDBNull(13) ?  "" : rdr.GetString(13), rdr.IsDBNull(14) ? -1 : rdr.GetInt32(14), rdr.IsDBNull(15) ?  "" : rdr.GetString(15)));
                }
                rdr.Close();

            }catch (Exception e){Console.WriteLine(e.Message); Console.WriteLine(e.StackTrace);}
            finally{
                connection.CloseConnection(conn);
            }
            
            return appointments;
        }

        public List<AppointmentList> getAllAppointmentsForDateAndLocation(string date, int location_id){
             MySqlConnection conn = new MySqlConnection();
            List<AppointmentList> appointments = new List<AppointmentList>();
            try{ 

                string start = date + " 00:00:00";
                string end = date + " 23:59:59";
                conn = connection.OpenConnection();

                string query = "SELECT timeslot_id, timeslot.staff_id, staff.first_name, staff.last_name, timeslot.citizen_id, citizen.first_name, citizen.last_name, timeslot.location_id, location.name, timeslot.dose_id, dose.supplier, vaccine_category.category_id, vaccine_category.name, timeslot.date, timeslot.status_id, timeslot_status.description FROM timeslot JOIN staff USING(staff_id) JOIN dose USING(dose_id) JOIN timeslot_status USING(status_id) JOIN vaccine USING(vaccine_id) JOIN location ON timeslot.location_id=location.location_id LEFT JOIN citizen USING(citizen_id) JOIN vaccine_category ON vaccine_category.category_id=vaccine.category WHERE timeslot.date > @start AND timeslot.date < @end AND timeslot.location_id=@location_id";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@start", start);
                cmd.Parameters.AddWithValue("@end", end);
                cmd.Parameters.AddWithValue("@location_id", location_id);

                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {   
                    appointments.Add(new AppointmentList(rdr.IsDBNull(0) ? -1 : rdr.GetInt32(0), rdr.IsDBNull(1) ? -1 : rdr.GetInt32(1), rdr.IsDBNull(2) ?  "" : rdr.GetString(2), rdr.IsDBNull(3) ?  "" : rdr.GetString(3), rdr.IsDBNull(4) ? -1 : rdr.GetInt32(4),rdr.IsDBNull(5) ?  "" : rdr.GetString(5),rdr.IsDBNull(6) ?  "" : rdr.GetString(6),rdr.IsDBNull(7) ? -1 : rdr.GetInt32(7), rdr.IsDBNull(8) ?  "" : rdr.GetString(8),rdr.IsDBNull(9) ? -1 : rdr.GetInt32(9), rdr.IsDBNull(10) ?  "" : rdr.GetString(10),rdr.IsDBNull(11) ? -1: rdr.GetInt32(11),rdr.IsDBNull(12) ?  "" : rdr.GetString(12), rdr.IsDBNull(13) ?  "" : rdr.GetString(13), rdr.IsDBNull(14) ? -1 : rdr.GetInt32(14), rdr.IsDBNull(15) ?  "" : rdr.GetString(15)));
                }
                rdr.Close();

            }catch (Exception e){Console.WriteLine(e.Message); Console.WriteLine(e.StackTrace);}
            finally{
                connection.CloseConnection(conn);
            }
            
            return appointments;
        }

        public List<AppointmentList> getOpenAppointmentsForDateAndLocation(string date, int location_id){
             MySqlConnection conn = new MySqlConnection();
            List<AppointmentList> appointments = new List<AppointmentList>();
            try{ 

                string start = date + " 00:00:00";
                string end = date + " 23:59:59";
                conn = connection.OpenConnection();

                string query = "SELECT timeslot_id, timeslot.staff_id, staff.first_name, staff.last_name, timeslot.citizen_id, citizen.first_name, citizen.last_name, timeslot.location_id, location.name, timeslot.dose_id, dose.supplier, vaccine_category.category_id, vaccine_category.name, timeslot.date, timeslot.status_id, timeslot_status.description FROM timeslot JOIN staff USING(staff_id) JOIN dose USING(dose_id) JOIN timeslot_status USING(status_id) JOIN vaccine USING(vaccine_id) JOIN location ON timeslot.location_id=location.location_id LEFT JOIN citizen USING(citizen_id) JOIN vaccine_category ON vaccine_category.category_id=vaccine.category WHERE timeslot.date > @start AND timeslot.date < @end AND timeslot.location_id=@location_id AND timeslot_status.status_id=1";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@start", start);
                cmd.Parameters.AddWithValue("@end", end);
                cmd.Parameters.AddWithValue("@location_id", location_id);

                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {   
                    appointments.Add(new AppointmentList(rdr.IsDBNull(0) ? -1 : rdr.GetInt32(0), rdr.IsDBNull(1) ? -1 : rdr.GetInt32(1), rdr.IsDBNull(2) ?  "" : rdr.GetString(2), rdr.IsDBNull(3) ?  "" : rdr.GetString(3), rdr.IsDBNull(4) ? -1 : rdr.GetInt32(4),rdr.IsDBNull(5) ?  "" : rdr.GetString(5),rdr.IsDBNull(6) ?  "" : rdr.GetString(6),rdr.IsDBNull(7) ? -1 : rdr.GetInt32(7), rdr.IsDBNull(8) ?  "" : rdr.GetString(8),rdr.IsDBNull(9) ? -1 : rdr.GetInt32(9), rdr.IsDBNull(10) ?  "" : rdr.GetString(10),rdr.IsDBNull(11) ? -1: rdr.GetInt32(11),rdr.IsDBNull(12) ?  "" : rdr.GetString(12), rdr.IsDBNull(13) ?  "" : rdr.GetString(13), rdr.IsDBNull(14) ? -1 : rdr.GetInt32(14), rdr.IsDBNull(15) ?  "" : rdr.GetString(15)));
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