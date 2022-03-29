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
         private ApplicationMonitoringService monitor = new ApplicationMonitoringService();

        [HttpPost]
        public IActionResult CreateTimeslot([FromBody] Timeslot timeslot,  [FromHeader] string authorization) {
            
            long startTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            string responseCode = "202";

            try
            {
                string token = authorization;
                int uid = us.checkToken(token);
                if (uid == -1)
                {
                    responseCode = "401";
                    return Unauthorized();
                }

                bool isAdmin = us.isSuperAdmin(uid);
                if (!isAdmin)
                {
                    responseCode = "403";
                    return StatusCode(403);
                }

                // sql call to add timeslot
                bool result = am.insertAppointment(timeslot);

                if (result)
                {
                    return Accepted();
                    //return new ObjectResult("Your message") {StatusCode = 403};
                }
                else
                {
                    responseCode = "400";
                    return BadRequest();
                }
            }
            finally
            {
                monitor.RecordResponseTime("Backend", "POST /Appointment", startTime, responseCode);
            }
        }

        [HttpPut("Signup")] // citizen booking appointment (no permissions)
        public IActionResult PutCitizenDoseTimeslot([FromBody] Timeslot timeslot){
            
            long startTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            string responseCode = "202";
            
            try{
                int vaccineType = -1;
                if (!String.IsNullOrEmpty(HttpContext.Request.Query["vaccine_type"])){
                    vaccineType = Int32.Parse(HttpContext.Request.Query["vaccine_type"]);
                }

                // should include citizen id, timeslot id, and vaccine type
                int result = am.updateTimeslotCitizenDose(timeslot, vaccineType);

                if(result == -1){
                    responseCode = "400";
                    return BadRequest();
                } else if(result == -2){
                    responseCode = "404";
                    return NotFound(new { ErrorMessage = "No available vaccines with specified type" });
                } else if(result > 0){
                    return Accepted();
                }else {
                    responseCode = "400";
                    return BadRequest();
                }
            }
            catch(Exception e){
                responseCode = "400";
                return BadRequest();
            }
            finally
            {
                monitor.RecordResponseTime("Backend", "PUT /Appointment/Signup", startTime, responseCode);
            } 
        }

        [HttpPut("AssignStaff")] // staff assigned to appointment (permissions)
        public IActionResult PutCitiznDoseTimeslot([FromBody] Timeslot timeslot,  [FromHeader] string authorization){
            
            long startTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            string responseCode = "202";
            
            try{
                string token = authorization;
                int uid = us.checkToken(token);
                if (uid == -1){
                    responseCode = "401";
                    return Unauthorized();
                }

                // should include citizen id, timeslot id, and vaccine type
                bool result = am.updateTimeslotStaff(timeslot, uid);

                if(result){
                    return Accepted();
                }else {
                    responseCode = "400";
                    return BadRequest();
                }
            }
            catch(Exception e){
                responseCode = "400";
                return BadRequest();
            }
            finally
            {
                monitor.RecordResponseTime("Backend", "PUT /Appointment/AssignStaff", startTime, responseCode);
            } 
        }

        [HttpPut("UpdateStatus")] // staff assigned to appointment (permissions)
        public IActionResult PutTimeslotStatus([FromBody] Timeslot timeslot,  [FromHeader] string authorization){
            
            long startTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            string responseCode = "202";
            
            try{
                string token = authorization;
                int uid = us.checkToken(token);
                if (uid == -1){
                    responseCode = "401";
                    return Unauthorized();
                }

                // should include citizen id, timeslot id, and vaccine type
                bool result = am.updateTimeslotStatus(timeslot, uid);

                if(result){
                    return Accepted();
                }else {
                    responseCode = "400";
                    return BadRequest();
                }
            }
            catch(Exception e){
                responseCode = "400";
                return BadRequest();
            }
            finally
            {
                monitor.RecordResponseTime("Backend", "PUT /Appointment/UpdateStatus", startTime, responseCode);
            }
        }

        [HttpPut("AddReactions")] // add reaction to timeslot, only authorized users
        public IActionResult PutTimeslotReactions([FromBody] Timeslot timeslot,  [FromHeader] string authorization){
             
            long startTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            string responseCode = "202";
            
            try{
                string token = authorization;
                int uid = us.checkToken(token);
                if (uid == -1){
                    responseCode = "401";
                    return Unauthorized();
                }

                // should include citizen id, timeslot id, and vaccine type
                bool result = am.updateTimeslotReactions(timeslot, uid);

                if(result){
                    return Accepted();
                }else {
                    responseCode = "400";
                    return BadRequest();
                }
            }
            catch(Exception e){
                responseCode = "400";
                return BadRequest();
            }
            finally
            {
                monitor.RecordResponseTime("Backend", "PUT /Appointment/AddReactions", startTime, responseCode);
            }
        }

        // admin only?  
        [HttpDelete]
        public IActionResult DeleteTimeslot([FromHeader] string authorization){
            
            long startTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            string responseCode = "202";
            
            try{

                string token = authorization;
                int uid = us.checkToken(token);
                if (uid == -1){
                    responseCode = "401";
                    return Unauthorized();
                }

                // admin check here
                bool isAdmin = us.isSuperAdmin(uid);
                if(!isAdmin){
                    responseCode = "403";
                    return StatusCode(403);
                }

                int appointment_id = -1;
                
                if (!String.IsNullOrEmpty(HttpContext.Request.Query["appointment_id"])){
                    appointment_id = Int32.Parse(HttpContext.Request.Query["appointment_id"]);
                }

                if(appointment_id == -1){
                    responseCode = "400";
                    return BadRequest();
                }
     
                bool result = am.removeTimeslot(appointment_id, uid);

                if(result){
                    return Accepted();
                } else {
                    responseCode = "400";
                    return BadRequest();
                }
            }
            catch(Exception e){
                responseCode = "400";
                return BadRequest();
            }
            finally
            {
                monitor.RecordResponseTime("Backend", "DELETE /Appointment", startTime, responseCode);
            }
        }

        [HttpGet("all")]
        public IActionResult GetAllAppointments([FromHeader] string authorization){

            long startTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            string responseCode = "200";
            
            try{  
                
                string token = authorization;
                int uid = us.checkToken(token);
                if (uid == -1){
                    responseCode = "401";
                    return Unauthorized();
                }

                List<Dictionary<string, string>> appointments = am.getAllAppointments(false);
                return new ObjectResult(appointments);
            }
            catch(Exception e){
                responseCode = "400";
                return BadRequest();
            }
            finally
            {
                monitor.RecordResponseTime("Backend", "GET /Appointment/all", startTime, responseCode);
            }
        }

        [HttpGet("all/open")]
        public IActionResult GetAllOpenAppointments(){

            long startTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            string responseCode = "200";
            
            try{                
                List<Dictionary<string, string>> appointments = am.getAllAppointments(true);
                return new ObjectResult(appointments);
            }
            catch(Exception e){
                responseCode = "400";
                return BadRequest();
            }
            finally
            {
                monitor.RecordResponseTime("Backend", "GET /Appointment/all/open", startTime, responseCode);
            }
        }

        [HttpGet]
        public IActionResult GetAllAppointmentsOfType([FromHeader] string authorization){
            
            long startTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            string responseCode = "200";
            
            try {
                
                string token = authorization;
                int uid = us.checkToken(token);
                if (uid == -1){
                    responseCode = "401";
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
                    responseCode = "400";
                    return BadRequest();
                }

                List<string> appointments = am.getAllAppointmentsByType(false, vaccineSupplier, vaccineCategory);
                return new ObjectResult(appointments);
            }
            catch(Exception e){
                responseCode = "400";
                return BadRequest();
            }
            finally
            {
                monitor.RecordResponseTime("Backend", "GET /Appointment", startTime, responseCode);
            }
        }

        [HttpGet("all/open/{vaccine_supplier}")]
        public IActionResult GetAllOpenAppointmentsOfType(string vaccine_supplier){
            
            long startTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            string responseCode = "200";
            
            try{  
                int vaccineCategory = -1;
                if (!String.IsNullOrEmpty(HttpContext.Request.Query["category_id"])){
                    vaccineCategory = Int32.Parse(HttpContext.Request.Query["category_id"]);
                }

                if(vaccineCategory == -1 || vaccine_supplier == ""){
                    responseCode = "400";
                    return BadRequest();
                }

                          
                List<string> appointments = am.getAllAppointmentsByType(true, vaccine_supplier, vaccineCategory);
                return new ObjectResult(appointments);
            }
            catch(Exception e){
                responseCode = "400";
                return BadRequest();
            }
            finally
            {
                monitor.RecordResponseTime("Backend", "GET /Appointment/all/" + vaccine_supplier, startTime, responseCode);
            }
        }

        [HttpGet("citizen/{id}")]
        public IActionResult GetAllAppointmentsCitizen([FromHeader] string authorization, int id){
            
            long startTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            string responseCode = "200";
            
            try{   
                string token = authorization;
                int uid = us.checkToken(token);
                if (uid == -1){
                    responseCode  = "401";
                    return Unauthorized();
                }

                Citizen citizen = us.getCitizenWithID(id);
                if(citizen == null){
                    responseCode = "404";
                    return NotFound();
                }

                List<string> appointments = am.getAllAppointmentsForCitizen(id);
                return new ObjectResult(appointments);
            }
            catch(Exception e){
                responseCode = "400";
                return BadRequest();
            }
            finally
            {
                monitor.RecordResponseTime("Backend", "GET /Appointment/citizen/" + id, startTime, responseCode);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetAppointmentsByID([FromHeader] string authorization, int id){
            
            long startTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            string responseCode = "200";
            
            try{    
                string token = authorization;
                int uid = us.checkToken(token);
                if (uid == -1){
                    responseCode = "401";
                    return Unauthorized();
                }

                Citizen citizen = us.getCitizenWithID(id);
                if(citizen == null){
                    responseCode = "404";
                    return NotFound();
                }

                string appointment = am.getAppointmentsWithID(id);
                return new ObjectResult(appointment);
            }
            catch(Exception e){
                responseCode = "400";
                return BadRequest();
            }
            finally
            {
                monitor.RecordResponseTime("Backend", "GET /Appointment/" + id, startTime, responseCode);
            }
        }

         [HttpGet("all/location")]
        public IActionResult GetAllAppointmentsLocation([FromHeader] string authorization){
            
            long startTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            string responseCode = "200";
            
            try{  
                string token = authorization;
                int uid = us.checkToken(token);
                if (uid == -1){
                    responseCode = "401";
                    return Unauthorized();
                }

                int location_id = -1;

                if (!String.IsNullOrEmpty(HttpContext.Request.Query["location_id"])){
                    location_id = Int32.Parse(HttpContext.Request.Query["location_id"]);
                }

                if(location_id == -1){
                    responseCode = "400";
                    return BadRequest(); 
                }

                List<string> appointment = am.getAllAppointmentsForLocation(false, location_id);
                return new ObjectResult(appointment);
            }
            catch(Exception e){
                responseCode = "400";
                return BadRequest();
            }
            finally
            {
                monitor.RecordResponseTime("Backend", "GET /Appointment/all/location", startTime, responseCode);
            }
        }

         [HttpGet("all/location/open")]
        public IActionResult GetAllOpenAppointmentsLocation(){
            
            long startTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            string responseCode = "200";
            int location_id = -1;

            try{ 
                if (!String.IsNullOrEmpty(HttpContext.Request.Query["location_id"])){
                    location_id = Int32.Parse(HttpContext.Request.Query["location_id"]);
                }

                if(location_id == -1){
                    responseCode = "400";
                    return BadRequest();
                }

                List<string> appointment = am.getAllAppointmentsForLocation(true, location_id);
                return new ObjectResult(appointment);
            }
            catch(Exception e){
                responseCode = "400";
                return BadRequest();
            }
            finally
            {
                monitor.RecordResponseTime("Backend", "GET /Appointment/all/location/open", startTime, responseCode);
            }
        }

         [HttpGet("all/location/{supplier}")]
        public IActionResult GetAllAppointmentsLocationAndType([FromHeader] string authorization, string supplier){
            
            long startTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            string responseCode = "200";
            
            try{   
                string token = authorization;
                int uid = us.checkToken(token);
                if (uid == -1){
                    responseCode = "401";
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
                    responseCode = "400";
                    return BadRequest();
                }

                List<string> appointment = am.getAllAppointmentsForLocationAndType(false, location_id, supplier, category_id);
                return new ObjectResult(appointment);
            }
            catch(Exception e){
                responseCode = "400";
                return BadRequest();
            }
            finally
            {
                monitor.RecordResponseTime("Backend", "GET /Appointment/all/location/" + supplier, startTime, responseCode);
            }
        }


         [HttpGet("all/location/open/{supplier}")]
        public IActionResult GetAllOpenAppointmentsLocationAndType([FromHeader] string authorization, string supplier){
            
            long startTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            string responseCode = "200";
            
            try {
                string token = authorization;
                int uid = us.checkToken(token);
                if (uid == -1){
                    responseCode = "401";
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
                    responseCode = "400";
                    return BadRequest();
                }
            
                List<string> appointment = am.getAllAppointmentsForLocationAndType(true, location_id, supplier, category_id);
                return new ObjectResult(appointment);
            }
            catch(Exception e){
                responseCode = "400";
                return BadRequest();
            }
            finally
            {
                monitor.RecordResponseTime("Backend", "GET /Appointment/all/location/open/" + supplier, startTime, responseCode);
            }
        }

         [HttpGet("date")]
        public IActionResult GetAllAppointmentsDate([FromHeader] string authorization){
            
            long startTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            string responseCode = "200";
            
            try{   
                string token = authorization;
                int uid = us.checkToken(token);
                if (uid == -1){
                    responseCode = "401";
                    return Unauthorized();
                }
                
                string date = "";

                if (!String.IsNullOrEmpty(HttpContext.Request.Query["date"])){
                    date = HttpContext.Request.Query["date"];
                }

                if(date == ""){
                    responseCode = "400";
                    return BadRequest();
                }

                List<string> appointment = am.getAllAppointmentsForDate(date);
                return new ObjectResult(appointment);
            }
            catch(Exception e){
                responseCode = "400";
                return BadRequest();
            }
            finally
            {
                monitor.RecordResponseTime("Backend", "GET /Appointment/date", startTime, responseCode);
            }
        }

         [HttpGet("date/{location_id}")]
        public IActionResult GetAllAppointmentsDate([FromHeader] string authorization, int location_id){
            
            long startTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            string responseCode = "200";

            try{  
                string token = authorization;
                int uid = us.checkToken(token);
                if (uid == -1){
                    responseCode = "401";
                    return Unauthorized();
                }
                
                string date = "";

                if (!String.IsNullOrEmpty(HttpContext.Request.Query["date"])){
                    date = HttpContext.Request.Query["date"];
                }

                if(date == ""){
                    responseCode = "400";
                    return BadRequest();
                }

                List<string> appointment = am.getAllAppointmentsForDateAndLocation(date, location_id);
                return new ObjectResult(appointment);
            }
            catch(Exception e){
                responseCode = "400";
                return BadRequest();
            }
            finally
            {
                monitor.RecordResponseTime("Backend", "GET /Appointment/date/" + location_id, startTime, responseCode);
            }
        }

    }
}