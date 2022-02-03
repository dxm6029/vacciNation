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

        [HttpPost]
        public IActionResult AddCitizen([FromBody] Citizen citizenInfo) {
            // assumes that password is hashed on the frontend
            bool result = us.insertCitizen(citizenInfo.email, citizenInfo.last_name, citizenInfo.first_name);

            if(result){
                return Accepted();
                //return new ObjectResult("Your message") {StatusCode = 403};
            } else {
                return BadRequest();
            }
        }

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

        [HttpDelete]
        public IActionResult DeleteUserStaff(){
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
    }
}