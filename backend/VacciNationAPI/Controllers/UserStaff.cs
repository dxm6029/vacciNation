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

        // assumes only admin can make staff acounts
        [HttpPost]
        public IActionResult AddStaffMember([FromBody] Staff staffInfo, [FromHeader] string authorization) {
             string token = authorization;
                int uid = us.checkToken(token);
                if (uid == -1){
                    return Unauthorized();
                }

            // where check role to verify that they are admin
        
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
        public IActionResult GetUserStaffWithoutID([FromHeader] string authorization){
            try{
                 string token = authorization;
                int uid = us.checkToken(token);
                if (uid == -1){
                    return Unauthorized();
                }

                // can add role check here depending on who we want to do this!

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
        public IActionResult GetUserStaffWithID(int id, [FromHeader] string authorization){
            try{
                string token = authorization;
                int uid = us.checkToken(token);
                if (uid == -1){
                    return Unauthorized();
                }

                
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

        [HttpGet("self")]
        public IActionResult GetUserStaffWithToken([FromHeader] string authorization){
            string token = authorization;
            try{

                int id = us.checkToken(token);
                if(id == -1){
                    return Unauthorized();
                }
                
                Staff staff = us.getUserWithID(id);

                if(staff != null){
                    return new ObjectResult(staff);
                } else {
                    return NotFound();
                }
            }
            catch(Exception e){
                return BadRequest();
            }
        }

        [HttpGet("all")]
        public IActionResult GetAllStaff([FromHeader] string authorization){

            string token = authorization;
            int uid = us.checkToken(token);
            if (uid == -1){
                return Unauthorized();
            }

            try{                
                List<string> staffMembers = us.getAllStaff();
                return new ObjectResult(staffMembers);
            }
            catch(Exception e){
                return BadRequest();
            }
        }


        [HttpPut]
        public IActionResult PutUserStaff([FromBody] Staff staffInfo, [FromHeader] string authorization){
            try{

                string token = authorization;
                int uid = us.checkToken(token);
                if (uid == -1){
                    return Unauthorized();
                }

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

        [HttpPut("self")]
        public IActionResult PutUserStaffSelf([FromBody] Staff staffInfo, [FromHeader] string authorization){
            try{

                string token = authorization;
                int uid = us.checkToken(token);
                if (uid == -1){
                    return Unauthorized();
                }

                staffInfo.staff_id = uid;

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


        // just for admin
        [HttpDelete]
        public IActionResult DeleteUserStaff([FromHeader] string authorization){
            try{

                string token = authorization;
                int uid = us.checkToken(token);
                if (uid == -1){
                    return Unauthorized();
                }

                // admin check here

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


        // staff deleting themselves
        [HttpDelete("self")]
        public IActionResult DeleteUserStaffByToken([FromHeader] string authorization){
            string token = authorization;
            try{

                int id = us.checkToken(token);
                if(id == -1){
                    return Unauthorized();
                }
                Staff staff = us.getUserWithID(id);
                
                bool result = us.deleteUser(staff.email, staff.username);

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

        [HttpPost("login")]
        public IActionResult Login([FromBody]Staff staff){
            try{
                // check credentials
                Staff authorizedStaff = us.checkCreds(staff.username, staff.password);

                if(authorizedStaff == null){
                    return NotFound();
                }

                // add token
                string tok = us.addToken(authorizedStaff.staff_id);
                
                string body = "{staff_id: " + authorizedStaff.staff_id.ToString() + ", email: " + authorizedStaff.email + ", username: " + authorizedStaff.username+  ", last_name: " +  authorizedStaff.last_name + ", first_name: " +  authorizedStaff.first_name + ", token: " +  tok + "}";

                return new ObjectResult(body);
            }
            catch(Exception e){
                return BadRequest();
            }
        }


    } // userstaff controller class

}//namespace