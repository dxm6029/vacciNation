using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Text.Json;
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
                List<Dictionary<string, string>> vaccines = vm.GetAllVaccines();
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
                List<Dictionary<string, string>> vaccines = vm.getAllVaccinesByType(vaccineCategory, vaccineDisease);
                return new ObjectResult(vaccines);
            }
            catch(Exception e){
                return BadRequest();
            }
        }
        
        //get 1 or all categories
        [HttpGet("category")]
        public IActionResult GetCategory(){

            try{  
                //get all categories if category_id not set
                if (String.IsNullOrEmpty(HttpContext.Request.Query["category_id"]))
                {
                    List<Dictionary<string, string>> categories = vm.GetAllCategories();
                    return new ObjectResult(categories);
                }
                //get specific category
                else
                {
                    int categoryId = Int32.Parse(HttpContext.Request.Query["category_id"]);
                    Dictionary<string, string> category = vm.GetCategoryById(categoryId);
                    return new ObjectResult(category);
                }
                
            }
            catch(Exception e){
                return BadRequest();
            }
        }
        
        // Creating a new category
        [HttpPost("category")]
        public IActionResult CreateCategory([FromBody] Category category,  [FromHeader] string authorization) {
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
            bool result = vm.InsertCategory(category);

            if(result){
                return Accepted();
            }

            return BadRequest();
        }

        // Updating an existing category
        [HttpPut("category")] 
        public IActionResult UpdateCategory([FromBody] Category category,  [FromHeader] string authorization){
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
                if (String.IsNullOrEmpty(category.name))
                {
                    return BadRequest();
                }
                
                // should include category, disease, or description
                bool result = vm.UpdateCategory(category);

                if(result){
                    return Accepted();
                }
                
                return BadRequest();
            }
            catch(Exception e){
                return BadRequest();
            }
        }

        //get 1 or all diseases
        [HttpGet("disease")]
        public IActionResult GetDisease(){

            try{  
                //get all diseases if disease_id not set
                if (String.IsNullOrEmpty(HttpContext.Request.Query["disease_id"]))
                {
                    List<Dictionary<string, string>> diseases = vm.GetAllDiseases();
                    return new ObjectResult(diseases);
                }
                //get specific disease
                else
                {
                    int diseaseId = Int32.Parse(HttpContext.Request.Query["disease_id"]);
                    Dictionary<string, string> disease = vm.GetDiseaseById(diseaseId);
                    return new ObjectResult(disease);
                }
                
            }
            catch(Exception e){
                return BadRequest();
            }
        }
        
        // Creating a new disease
        [HttpPost("disease")]
        public IActionResult CreateDisease([FromBody] Disease disease,  [FromHeader] string authorization) {
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
            bool result = vm.InsertDisease(disease);

            if(result){
                return Accepted();
            }

            return BadRequest();
        }

        // Updating an existing disease
        [HttpPut("disease")] 
        public IActionResult UpdateDisease([FromBody] Disease disease,  [FromHeader] string authorization){
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
                if (String.IsNullOrEmpty(disease.name))
                {
                    return BadRequest();
                }
                
                // should include category, disease, or description
                bool result = vm.UpdateDisease(disease);

                if(result){
                    return Accepted();
                }
                
                return BadRequest();
            }
            catch(Exception e){
                return BadRequest();
            }
        }

        //get 1 or all doses
        [HttpGet("dose")]
        public IActionResult GetDose([FromHeader] string authorization){

            string token = authorization;
            int uid = us.checkToken(token);
            if (uid == -1){
                return Unauthorized();
            }
            
            try{  
                //get all doses if dose_id not set
                if (String.IsNullOrEmpty(HttpContext.Request.Query["dose_id"]))
                {
                    List<Dictionary<string, string>> doses = vm.GetAllDoses();
                    return new ObjectResult(doses);
                }
                //get specific dose
                else
                {
                    int doseId = Int32.Parse(HttpContext.Request.Query["dose_id"]);
                    Dictionary<string, string> dose = vm.GetDoseById(doseId);
                    return new ObjectResult(dose);
                }
                
            }
            catch(Exception e){
                return BadRequest();
            }
        }
        
        // Creating new doses
        [HttpPost("dose")]
        public IActionResult CreateDose([FromBody] Dose dose,  [FromHeader] string authorization) {
            string token = authorization;
            int uid = us.checkToken(token);
            if (uid == -1){
                return Unauthorized();
            }

            bool isAdmin = us.isSuperAdmin(uid);
            if(!isAdmin){
                return StatusCode(403);
            }
            
            bool result = vm.InsertDoses(dose);

            if(result){
                return Accepted();
            }

            return BadRequest();
        }

        // Updating an existing dose
        [HttpPut("dose")] 
        public IActionResult UpdateDose([FromBody] Dose dose,  [FromHeader] string authorization){
            try{
                string token = authorization;
                int uid = us.checkToken(token);
                if (uid == -1){
                    return Unauthorized();
                }

                bool isAdmin = us.isSuperAdmin(uid);
                if(!isAdmin){
                    return StatusCode(403);
                }
                
                // should include category, disease, or description
                bool result = vm.UpdateDose(dose);

                if(result){
                    return Accepted();
                }
                
                return BadRequest();
            }
            catch(Exception e){
                return BadRequest();
            }
        }

    }
}