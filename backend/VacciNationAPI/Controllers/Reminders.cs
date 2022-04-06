using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System;
using VacciNationAPI.Models;
using VacciNationAPI.DataLayer;

namespace VacciNationAPI.Controllers{

    [ApiController]
    [Route("[controller]")]
    public class Reminders: Controller {

         User us = new User();
        ReminderManagement rm = new ReminderManagement();

        // send email reminders with appointment information to all citizens with an appointment
        [HttpGet("SendReminders")]
        public IActionResult GetReactionInsights([FromHeader] string authorization) {
            string token = authorization;
            int uid = us.checkToken(token);
            if (uid == -1){
                return Unauthorized();
            }

            var today = DateTime.Today;
            var tomorrow = today.AddDays(1);

            

            Console.WriteLine("attempting to enter send method");
            bool sentSuccessfully = rm.sendNotifications(tomorrow);


            if(sentSuccessfully){
                return Accepted();
            } else {
                return BadRequest();
            }
        }
    }
}