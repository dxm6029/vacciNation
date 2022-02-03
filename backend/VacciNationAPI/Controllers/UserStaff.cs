using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System;
using VacciNationAPI.Models;
using VacciNationAPI.DataLayer;

namespace VacciNationAPI.Controllers{

    [ApiController]
    [Route("[controller]")]
    public class UserStaff: Controller {
        User us = new User();

        [HttpPost]
        public IActionResult AddStaffMember([FromBody] Staff staffInfo) {
            // assumes that password is hashed on the frontend
            bool result = us.insertStaffMember(staffInfo.email, staffInfo.username, staffInfo.password, staffInfo.last_name, staffInfo.first_name);

            if(result){
                return Accepted();
                //return new ObjectResult("Your message") {StatusCode = 403};
            } else {
                return BadRequest();
            }
        }

        [HttpGet]
        public IActionResult GetUserStaffWithoutID(){
            try{
                string username = "";
                string email = "";
                
                if (!String.IsNullOrEmpty(HttpContext.Request.Query["username"])){
                    username = HttpContext.Request.Query["username"];
                }
                if (!String.IsNullOrEmpty(HttpContext.Request.Query["email"])){
                    email = HttpContext.Request.Query["email"];
                }
                
                Staff staff = us.getUserWithoutID(email, username);

                if(staff == null){
                    return NotFound();
                }
                
                return new ObjectResult(staff);
            }
            catch(Exception e){
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetUserStaffWithID(int id){
            try{
                
                Staff staff = us.getUserWithID(id);

                if(staff == null){
                    return NotFound();
                }
                
                return new ObjectResult(staff);
            }
            catch(Exception e){
                return BadRequest();
            }
        }

        [HttpPut]
        public IActionResult PutUserStaff([FromBody] Staff staffInfo){
            try{

                bool result = us.putUserWithID(staffInfo);

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

        [HttpDelete]
        public IActionResult DeleteUserStaff(){
            try{
                string username = "";
                string email = "";
                
                if (!String.IsNullOrEmpty(HttpContext.Request.Query["username"])){
                    username = HttpContext.Request.Query["username"];
                }
                if (!String.IsNullOrEmpty(HttpContext.Request.Query["email"])){
                    email = HttpContext.Request.Query["email"];
                }
                
                bool result = us.deleteUser(email, username);

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


    } // userstaff controller class

}//namespace