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


        public static async Task Execute()
        {
            var apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("dmolee@gmail.com", "VacciNation");
            var subject = "Sending with SendGrid is Fun";
            var to = new EmailAddress("dxm6029@rit.edu", "Dominique Molee");
            var plainTextContent = "and easy to do anywhere, even with C#";
            var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            Console.WriteLine("email created");

            var response = await client.SendEmailAsync(msg);
            Console.WriteLine("trying to send");

        }
        public bool  sendNotifications(DateTime date){
            MySqlConnection conn = new MySqlConnection();
            List<string> suppliers = new List<string>();
            try{ 

            Console.WriteLine("execute");

                Execute().Wait();
            Console.WriteLine("execute finished");



                //conn = connection.OpenConnection();
              
                // var dateOnly = date.Date;
                // var start = dateOnly.ToString() + " 00:00:00";
                // var end = dateOnly.ToString() + " 23:59:59";

                // string query = "SELECT email FROM citizen JOIN timeslot USING(citizen_id) WHERE date > @start AND date < @end";
                // MySqlCommand cmd = new MySqlCommand(query, conn);
                // cmd.Parameters.AddWithValue("@start", start);
                

                // MySqlDataReader rdr = cmd.ExecuteReader();

                // while (rdr.Read())
                // {   
                //     // should not hit 0, but for consistency
                //     suppliers.Add(rdr.IsDBNull(0) ?  "" : rdr.GetString(0));
                // }
                // rdr.Close();

            }catch (Exception e){ Console.WriteLine(e.Message); Console.WriteLine(e.StackTrace); }
            finally{
                //connection.CloseConnection(conn);
            }

            return true;
        }


    } 

}//namespace