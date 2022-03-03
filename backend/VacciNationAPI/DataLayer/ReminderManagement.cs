using VacciNationAPI.Models;
using System;
using MySql.Data.MySqlClient; 
using System.Collections.Generic;
using System.Security.Cryptography;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;


namespace VacciNationAPI.DataLayer
{

    public class ReminderManagement {

        VacciNation.Connect connection = new VacciNation.Connect();


        public static async Task Execute(string email, string date, string location, string category, string disease, string supplier)
        {
            var apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");

            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("vaccineschedulingassistant@gmail.com", "Vaccination Scheduling Support");
            var subject = "Reminder: Vaccine Appointment Upcoming";
            var to = new EmailAddress(email, "Citizen");
            var htmlContent = "Hello, <br /> We are just reaching out to remind you that your appointment is on <strong> " + date + " at " + location +  "</strong>. You will be recieving the " + category + " of the " + supplier + " " + disease + " vaccine. Please be sure to bring an ID and your insurance. To ensure you recieve your vaccine on time, please try to arrive 10-15 minutes early to your vaccination site.<br /> <br /> <strong>**Do not reply to this email**</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, "", htmlContent);
            Console.WriteLine("email created");

            var response = await client.SendEmailAsync(msg);
            Console.WriteLine(response.StatusCode);

        }
        public bool  sendNotifications(DateTime date){
            MySqlConnection conn = new MySqlConnection();
            try{ 

                conn = connection.OpenConnection();
 
                var month = date.Date.Month.ToString().Length == 1 ? "0" + date.Date.Month.ToString() : date.Date.Month.ToString();
                var day = date.Date.Day.ToString().Length == 1 ? "0" + date.Date.Day.ToString() : date.Date.Day.ToString();
                var year = date.Date.Year.ToString();
                var dateOnly = year + "-" + month + "-" + day;
                var start = dateOnly + " 00:00:00";
                var end = dateOnly + " 23:59:59";
                Console.WriteLine(dateOnly);

                string query = "SELECT email, location.name, vaccine_category.name, vaccine_disease.name, dose.supplier, date FROM citizen JOIN timeslot USING(citizen_id) JOIN location USING(location_id) JOIN dose USING(dose_id) JOIN vaccine USING(vaccine_id) JOIN vaccine_category ON vaccine_category.category_id=vaccine.category JOIN vaccine_disease ON vaccine_disease.disease_id=vaccine.disease WHERE date > @start AND date < @end";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@start", start);
                cmd.Parameters.AddWithValue("@end", end);

                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {   
                    string email = rdr.IsDBNull(0) ?  "" : rdr.GetString(0);
                    string location = rdr.IsDBNull(1) ?  "" : rdr.GetString(1);
                    string category = rdr.IsDBNull(2) ?  "" : rdr.GetString(2);
                    string disease = rdr.IsDBNull(3) ?  "" : rdr.GetString(3);
                    string supplier = rdr.IsDBNull(4) ?  "" : rdr.GetString(4);
                    string apptDate = rdr.IsDBNull(5) ?  "" : rdr.GetString(5);

                    Execute(email, apptDate, location, category, disease, supplier).Wait();
                }
                rdr.Close();

            }catch (Exception e){ Console.WriteLine(e.Message); Console.WriteLine(e.StackTrace); return false; }
            finally{
                connection.CloseConnection(conn);
            }

            return true;
        }


    } 

}//namespace