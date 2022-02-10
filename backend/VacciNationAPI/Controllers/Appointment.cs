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
                } else if(result > 0){
                    return Accepted();
                }else {
                    return BadRequest();
                }
            }
            catch(Exception e){
                return BadRequest();
            }
        }

        [HttpPut("AssignStaff")] // staff assigned to appointment (permissions)
        public IActionResult PutCitiznDoseTimeslot([FromBody] Timeslot timeslot,  [FromHeader] string authorization){
             try{
                string token = authorization;
                int uid = us.checkToken(token);
                if (uid == -1){
                    return Unauthorized();
                }

                // should include citizen id, timeslot id, and vaccine type
                bool result = am.updateTimeslotStaff(timeslot);

                if(result){
                    return Accepted();
                }else {
                    return BadRequest();
                }
            }
            catch(Exception e){
                return BadRequest();
            }
        }

        [HttpPut("UpdateStatus")] // staff assigned to appointment (permissions)
        public IActionResult PutTimeslotStatus([FromBody] Timeslot timeslot,  [FromHeader] string authorization){
             try{
                string token = authorization;
                int uid = us.checkToken(token);
                if (uid == -1){
                    return Unauthorized();
                }

                // should include citizen id, timeslot id, and vaccine type
                bool result = am.updateTimeslotStatus(timeslot);

                if(result){
                    return Accepted();
                }else {
                    return BadRequest();
                }
            }
            catch(Exception e){
                return BadRequest();
            }
        }

        // admin only?  
        [HttpDelete]
        public IActionResult DeleteTimeslot([FromHeader] string authorization){
            try{

                string token = authorization;
                int uid = us.checkToken(token);
                if (uid == -1){
                    return Unauthorized();
                }

                // admin check here
                bool isAdmin = us.isSuperAdmin(uid);
                if(!isAdmin){
                    return StatusCode(403);
                }

                int appointment_id = -1;
                
                if (!String.IsNullOrEmpty(HttpContext.Request.Query["appointment_id"])){
                    appointment_id = Int32.Parse(HttpContext.Request.Query["appointment_id"]);
                }

                if(appointment_id == -1){
                    return BadRequest();
                }
     
                bool result = am.removeTimeslot(appointment_id);

                if(result){
                    return Accepted();
                } else {
                    return BadRequest();
                }
            }
            catch(Exception e){
                return BadRequest();
            }
        }
    }
}