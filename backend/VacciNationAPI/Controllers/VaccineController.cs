using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System;
using VacciNationAPI.Models;
using VacciNationAPI.DataLayer;

namespace VacciNationAPI.Controllers
{
    [ApiController]
    [Route("vaccine")]
    public class VaccineController: Controller {
        
        User us = new User();
        VaccineManagement vm = new VaccineManagement();
        
        //get full list of vaccines
        [HttpGet("all")]
        public IActionResult GetAllVaccines(){

            try{                
                List<string> vaccines = vm.GetAllVaccines();
                return new ObjectResult(vaccines);
            }
            catch(Exception e){
                return BadRequest();
            }
        }
        
        // Creating a new vaccine
        [HttpPost]
        public IActionResult CreateVaccine([FromBody] Vaccine vaccine,  [FromHeader] string authorization) {
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
            bool result = vm.InsertVaccine(vaccine);

            if(result){
                return Accepted();
            }

            return BadRequest();
        }

        // Updating an existing vaccine
        [HttpPut] 
        public IActionResult UpdateVaccine([FromBody] Vaccine vaccine,  [FromHeader] string authorization){
            try{
                string token = authorization;
                int uid = us.checkToken(token);
                if (uid == -1){
                    return Unauthorized();
                }

                //walled behind admin
                bool isAdmin = us.isSuperAdmin(uid);
                if(!isAdmin){
                    return StatusCode(403);
                }

                //if no attributes are set, nothing should happen
                if (vaccine.category_id == 0 && vaccine.disease_id == 0 && string.IsNullOrEmpty(vaccine.description))
                {
                    return BadRequest();
                }
                
                // should include category, disease, or description
                bool result = vm.updateVaccine(vaccine);

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

        // Get vaccines by either category or disease
        [HttpGet]
        public IActionResult GetAllVaccinesOfType(){

            int vaccineCategory = -1;
            if (!String.IsNullOrEmpty(HttpContext.Request.Query["category_id"])){
                vaccineCategory = Int32.Parse(HttpContext.Request.Query["category_id"]);
            }

            int vaccineDisease = -1;
            if (!String.IsNullOrEmpty(HttpContext.Request.Query["disease_id"])){
                vaccineDisease = Int32.Parse(HttpContext.Request.Query["disease_id"]);
            }

            if(vaccineCategory == -1 && vaccineDisease == -1){
                return BadRequest();
            }

            try{                
                List<string> vaccines = vm.getAllVaccinesByType(vaccineCategory, vaccineDisease);
                return new ObjectResult(vaccines);
            }
            catch(Exception e){
                return BadRequest();
            }
        }
    }
}