using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System;
using VacciNationAPI.Models;
using VacciNationAPI.DataLayer;

namespace VacciNationAPI.Controllers{

    [ApiController]
    [Route("[controller]")]
    public class Appointment: Controller {

         User us = new User();
         AppointmentManagement am = new AppointmentManagement();

         [HttpPost]
        public IActionResult CreateTimeslot([FromBody] Timeslot timeslot,  [FromHeader] string authorization) {
            string token = authorization;
            int uid = us.checkToken(token);
            if (uid == -1){
                return Unauthorized();
            }

            bool isAdmin = us.isSuperAdmin(uid);
            if(!isAdmin){
                return StatusCode(403);
            }
            // sql call to add timeslot
            bool result = am.insertAppointment(timeslot);

            if(result){
                return Accepted();
                //return new ObjectResult("Your message") {StatusCode = 403};
            } else {
                return BadRequest();
            }
        }
    }
}