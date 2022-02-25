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


        public static async Task Execute(string email, string date)
        {
            var apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
            Console.WriteLine(apiKey);

            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("vaccineschedulingassistant@gmail.com", "Vaccination Scheduling Support");
            var subject = "Reminder: Vaccine Appointment Upcoming";
            var to = new EmailAddress(email, "Citizen");
            var plainTextContent = "Hello, <br />";
            var htmlContent = "We are just reaching out to remind you that your appointment is at <strong> </strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            Console.WriteLine("email created");

            var response = await client.SendEmailAsync(msg);
            Console.WriteLine("trying to send");
            Console.WriteLine(response.StatusCode);

        }
        public bool  sendNotifications(DateTime date){
            MySqlConnection conn = new MySqlConnection();
            bool res = false;
            try{ 

                
            Console.WriteLine("execute");

               // Execute().Wait();
            Console.WriteLine("execute finished");



                conn = connection.OpenConnection();
              
                var dateOnly = date.Date.ToShortDateString();
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
                    Console.WriteLine(email);
                    Console.WriteLine(location);
                    Console.WriteLine(category);
                    Console.WriteLine(disease);
                    Console.WriteLine(supplier);
                    Console.WriteLine(apptDate);


                    //Execute(email, dateOnly).Wait();
                    //suppliers.Add(rdr.IsDBNull(0) ?  "" : rdr.GetString(0));
                }
                rdr.Close();

            }catch (Exception e){ Console.WriteLine(e.Message); Console.WriteLine(e.StackTrace); }
            finally{
                //connection.CloseConnection(conn);
            }

            return true;
        }


    } 

}//namespace