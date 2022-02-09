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

        [HttpPut("Signup")] // citizen booking appointment (no permissions)
        public IActionResult PutCitizenDoseTimeslot([FromBody] Timeslot timeslot){
             try{
                int vaccineType = -1;
                if (!String.IsNullOrEmpty(HttpContext.Request.Query["vaccine_type"])){
                    vaccineType = Int32.Parse(HttpContext.Request.Query["vaccine_type"]);
                }

                // should include citizen id, timeslot id, and vaccine type
                int result = am.updateTimeslotCitizenDose(timeslot, vaccineType);

                if(result == -1){
                    return BadRequest();
                } else if(result == -2){
                    return NotFound(new { ErrorMessage = "No available vaccines with specified type" });
                }else {
                    return BadRequest();
                }
            }
            catch(Exception e){
                return BadRequest();
            }


        }
    }
}