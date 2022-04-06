using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System;
using VacciNationAPI.Models;
using VacciNationAPI.DataLayer;

namespace VacciNationAPI.Controllers{

    [ApiController]
    [Route("[controller]")]
    public class UserCitizen: Controller {

         User us = new User();

        // add a citizen user
        [HttpPost]
        public IActionResult AddCitizen([FromBody] Citizen citizenInfo) {
            // assumes that password is hashed on the frontend
            bool result = us.insertCitizen(citizenInfo.email, citizenInfo.last_name, citizenInfo.first_name, citizenInfo.date_of_birth, citizenInfo.phone_number);

            if(result){
                return Accepted();
                //return new ObjectResult("Your message") {StatusCode = 403};
            } else {
                return BadRequest();
            }
        }

        // update a citizen user
        [HttpPut]
        public IActionResult PutUserCitizen([FromBody] Citizen citizen){
            try{

                bool result = us.putCitizenWithID(citizen);

                if(result){
                    return Accepted();
                } else {
                    return BadRequest();
                }
            }
            catch(Exception e){
                Console.WriteLine(e.Message); Console.WriteLine(e.StackTrace);
                return BadRequest();
            }
        }

        // delete a citizen user
        [HttpDelete]
        public IActionResult DeleteUserCitizen(){
            try{
                string firstName = "";
                string lastName = "";
                string email = "";
                
                if (!String.IsNullOrEmpty(HttpContext.Request.Query["first_name"])){
                    firstName = HttpContext.Request.Query["first_name"];
                }
                if (!String.IsNullOrEmpty(HttpContext.Request.Query["last_name"])){
                    lastName = HttpContext.Request.Query["last_name"];
                }
                if (!String.IsNullOrEmpty(HttpContext.Request.Query["email"])){
                    email = HttpContext.Request.Query["email"];
                }
                
                bool result = us.deleteCitizen(email, firstName, lastName);

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

        // get a user citizen without id
        [HttpGet]
        public IActionResult GetUserStaffWithoutID(){
            try{
                string first_name = "";
                string last_name = "";
                string email = "";
                
                if (!String.IsNullOrEmpty(HttpContext.Request.Query["first_name"])){
                    first_name = HttpContext.Request.Query["first_name"];
                }
                if (!String.IsNullOrEmpty(HttpContext.Request.Query["last_name"])){
                    last_name = HttpContext.Request.Query["last_name"];
                }
                if (!String.IsNullOrEmpty(HttpContext.Request.Query["email"])){
                    email = HttpContext.Request.Query["email"];
                }
                
                Citizen citizen = us.getCitizenWithoutID(email, first_name, last_name);

                if(citizen == null){
                    return NotFound();
                }
                
                return new ObjectResult(citizen);
            }
            catch(Exception e){
                return BadRequest();
            }
        }

        // get a user citizen by id
        [HttpGet("{id}")]
        public IActionResult GetUserCitizenWithID(int id){
            try{
                
                Citizen citizen = us.getCitizenWithID(id);

                if(citizen == null){
                    return NotFound();
                }
                
                return new ObjectResult(citizen);
            }
            catch(Exception e){
                return BadRequest();
            }
        }

        // get all citizens for display
        [HttpGet("all")]
        public IActionResult GetAllCitizens(){
            try{                
                List<Citizen> citizens = us.getAllCitizens();
                return new ObjectResult(citizens);
            }
            catch(Exception e){
                return BadRequest();
            }
        }

        // adds an address to a citizen
        // assumes can have multiple addresses
        [HttpPost("address/{id}")]
        public IActionResult AddAddress([FromBody] Address address, int id){
            try{     
                Citizen citizen = us.getCitizenWithID(id);   
                if(citizen == null){
                    return NotFound(new {ErrorMessage = "Citizen does not exist"});
                }

                // insert address & update citizen object with address id   
                bool result = us.insertAddressForCitizen(address, id);   
                if(result){
                    return Accepted();
                }

                return BadRequest(new { ErrorMessage = "Unable to add address" });
            }
            catch(Exception e){
                return BadRequest();
            }
        }

        // removes an address from a citizen
        [HttpDelete("address/{id}")]
        public IActionResult removeAddress(int id){
            // id is the citizen ID
            try{     
                Citizen citizen = us.getCitizenWithID(id);   
                if(citizen == null){
                    return NotFound(new {ErrorMessage = "Citizen does not exist"});
                }

                // delete address & update citizen object with null address id   
                bool result = us.removeAddressForCitizen(citizen.address_id, citizen.citizen_id);   
                if(result){
                    return Accepted();
                }

                return BadRequest(new { ErrorMessage = "Unable to remove address" });
            }
            catch(Exception e){
                return BadRequest();
            }
        }

        // assumes can have multiple insurances
        // adds an insurance to a citizen
        [HttpPost("insurance/{id}")]
        public IActionResult AddInsurance([FromBody] Insurance insurance, int id){
            try{     
                Citizen citizen = us.getCitizenWithID(id);   
                if(citizen == null){
                    return NotFound(new {ErrorMessage = "Citizen does not exist"});
                }

                // insert address & update citizen object with address id   
                bool result = us.insertInsuranceForCitizen(insurance, id);   
                if(result){
                    return Accepted();
                }

                return BadRequest(new { ErrorMessage = "Unable to add insurance" });
            }
            catch(Exception e){
                return BadRequest();
            }
        }

        // deletes an insurance from a citizen
        [HttpDelete("insurance/{id}")]
        public IActionResult RemoveInsurance(int id){
            // id is the citizen ID
            try{     
                Citizen citizen = us.getCitizenWithID(id);   
                if(citizen == null){
                    return NotFound(new {ErrorMessage = "Citizen does not exist"});
                }

                // delete address & update citizen object with null address id   
                bool result = us.removeInsuranceForCitizen(citizen.insurance_id, citizen.citizen_id);   
                if(result){
                    return Accepted();
                }

                return BadRequest(new { ErrorMessage = "Unable to remove insurance" });
            }
            catch(Exception e){
                return BadRequest();
            }
        }


    }
}