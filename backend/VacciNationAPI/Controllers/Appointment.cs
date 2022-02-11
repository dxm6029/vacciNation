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

        [HttpGet("all")]
        public IActionResult GetAllAppointments([FromHeader] string authorization){

            string token = authorization;
            int uid = us.checkToken(token);
            if (uid == -1){
                return Unauthorized();
            }

            try{                
                List<string> appointments = am.getAllAppointments(false);
                return new ObjectResult(appointments);
            }
            catch(Exception e){
                return BadRequest();
            }
        }

        [HttpGet("all/open")]
        public IActionResult GetAllOpenAppointments(){

            try{                
                List<string> appointments = am.getAllAppointments(true);
                return new ObjectResult(appointments);
            }
            catch(Exception e){
                return BadRequest();
            }
        }

        [HttpGet]
        public IActionResult GetAllAppointmentsOfType([FromHeader] string authorization){
            string token = authorization;
            int uid = us.checkToken(token);
            if (uid == -1){
                return Unauthorized();
            }

            int vaccineCategory = -1;
            if (!String.IsNullOrEmpty(HttpContext.Request.Query["category_id"])){
                vaccineCategory = Int32.Parse(HttpContext.Request.Query["category_id"]);
            }

            string vaccineSupplier = "";
            if (!String.IsNullOrEmpty(HttpContext.Request.Query["vaccine_supplier"])){
                vaccineSupplier = HttpContext.Request.Query["vaccine_supplier"];
            }

            if(vaccineCategory == -1 || vaccineSupplier == ""){
                return BadRequest();
            }

            try{                
                List<string> appointments = am.getAllAppointmentsByType(false, vaccineSupplier, vaccineCategory);
                return new ObjectResult(appointments);
            }
            catch(Exception e){
                return BadRequest();
            }
        }


        [HttpGet("all/open/{vaccine_supplier}")]
        public IActionResult GetAllOpenAppointmentsOfType(string vaccine_supplier){
            int vaccineCategory = -1;
            if (!String.IsNullOrEmpty(HttpContext.Request.Query["category_id"])){
                vaccineCategory = Int32.Parse(HttpContext.Request.Query["category_id"]);
            }

            if(vaccineCategory == -1 || vaccine_supplier == ""){
                return BadRequest();
            }

            try{                
                List<string> appointments = am.getAllAppointmentsByType(true, vaccine_supplier, vaccineCategory);
                return new ObjectResult(appointments);
            }
            catch(Exception e){
                return BadRequest();
            }
        }

        [HttpGet("citizen/{id}")]
        public IActionResult GetAllAppointmentsCitizen([FromHeader] string authorization, int id){
            string token = authorization;
            int uid = us.checkToken(token);
            if (uid == -1){
                return Unauthorized();
            }

            Citizen citizen = us.getCitizenWithID(id);
            if(citizen == null){
                return NotFound();
            }

            try{                
                List<string> appointments = am.getAllAppointmentsForCitizen(id);
                return new ObjectResult(appointments);
            }
            catch(Exception e){
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetAppointmentsByID([FromHeader] string authorization, int id){
            string token = authorization;
            int uid = us.checkToken(token);
            if (uid == -1){
                return Unauthorized();
            }

            Citizen citizen = us.getCitizenWithID(id);
            if(citizen == null){
                return NotFound();
            }

            try{                
                string appointment = am.getAppointmentsWithID(id);
                return new ObjectResult(appointment);
            }
            catch(Exception e){
                return BadRequest();
            }
        }

         [HttpGet("all/location")]
        public IActionResult GetAllAppointmentsLocation([FromHeader] string authorization){
            string token = authorization;
            int uid = us.checkToken(token);
            if (uid == -1){
                return Unauthorized();
            }

            int location_id = -1;

             if (!String.IsNullOrEmpty(HttpContext.Request.Query["location_id"])){
                location_id = Int32.Parse(HttpContext.Request.Query["location_id"]);
            }

            if(location_id == -1){
                return BadRequest();
            }


            try{                
                List<string> appointment = am.getAllAppointmentsForLocation(false, location_id);
                return new ObjectResult(appointment);
            }
            catch(Exception e){
                return BadRequest();
            }
        }

         [HttpGet("all/location/open")]
        public IActionResult GetAllOpenAppointmentsLocation(){
            int location_id = -1;

             if (!String.IsNullOrEmpty(HttpContext.Request.Query["location_id"])){
                location_id = Int32.Parse(HttpContext.Request.Query["location_id"]);
            }

            if(location_id == -1){
                return BadRequest();
            }


            try{                
                List<string> appointment = am.getAllAppointmentsForLocation(true, location_id);
                return new ObjectResult(appointment);
            }
            catch(Exception e){
                return BadRequest();
            }
        }

         [HttpGet("all/location/{supplier}")]
        public IActionResult GetAllAppointmentsLocationAndType([FromHeader] string authorization, string supplier){
            
            string token = authorization;
            int uid = us.checkToken(token);
            if (uid == -1){
                return Unauthorized();
            }
            
            int location_id = -1;

             if (!String.IsNullOrEmpty(HttpContext.Request.Query["location_id"])){
                location_id = Int32.Parse(HttpContext.Request.Query["location_id"]);
            }

            int category_id = -1;

             if (!String.IsNullOrEmpty(HttpContext.Request.Query["category_id"])){
                category_id = Int32.Parse(HttpContext.Request.Query["category_id"]);
            }

            if(location_id == -1 || supplier == "" || category_id == -1){
                return BadRequest();
            }


            try{                
                List<string> appointment = am.getAllAppointmentsForLocationAndType(false, location_id, supplier, category_id);
                return new ObjectResult(appointment);
            }
            catch(Exception e){
                return BadRequest();
            }
        }


         [HttpGet("all/location/open/{supplier}")]
        public IActionResult GetAllOpenAppointmentsLocationAndType([FromHeader] string authorization, string supplier){
            
            string token = authorization;
            int uid = us.checkToken(token);
            if (uid == -1){
                return Unauthorized();
            }
            
            int location_id = -1;

             if (!String.IsNullOrEmpty(HttpContext.Request.Query["location_id"])){
                location_id = Int32.Parse(HttpContext.Request.Query["location_id"]);
            }

            int category_id = -1;

             if (!String.IsNullOrEmpty(HttpContext.Request.Query["category_id"])){
                category_id = Int32.Parse(HttpContext.Request.Query["category_id"]);
            }

            if(location_id == -1 || supplier == "" || category_id == -1){
                return BadRequest();
            }


            try{                
                List<string> appointment = am.getAllAppointmentsForLocationAndType(true, location_id, supplier, category_id);
                return new ObjectResult(appointment);
            }
            catch(Exception e){
                return BadRequest();
            }
        }

         [HttpGet("date")]
        public IActionResult GetAllAppointmentsDate([FromHeader] string authorization){
            
            string token = authorization;
            int uid = us.checkToken(token);
            if (uid == -1){
                return Unauthorized();
            }
            
            string date = "";

             if (!String.IsNullOrEmpty(HttpContext.Request.Query["date"])){
                date = HttpContext.Request.Query["date"];
            }

            if(date == ""){
                return BadRequest();
            }

            try{                
                List<string> appointment = am.getAllAppointmentsForDate(date);
                return new ObjectResult(appointment);
            }
            catch(Exception e){
                return BadRequest();
            }
        }

    }
}