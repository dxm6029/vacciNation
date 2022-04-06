using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System;
using VacciNationAPI.Models;
using VacciNationAPI.DataLayer;
using Microsoft.AspNetCore.Cors;

namespace VacciNationAPI.Controllers{

    [ApiController]
    [Route("[controller]")]
    [EnableCors()]
    //[EnableCors(origins: "*", headers: "*", methods: "*")]
    public class UserStaff: Controller {
        User us = new User();

        // assumes only admin can make staff acounts
        // add a staff user
        [HttpPost]
        public IActionResult AddStaffMember([FromBody] Staff staffInfo, [FromHeader] string authorization) {
             string token = authorization;
                int uid = us.checkToken(token);
                if (uid == -1){
                    return Unauthorized();
                }

            // where check role to verify that they are admin
            bool isAdmin = us.isSuperAdmin(uid);
            if(!isAdmin){
                return StatusCode(403);
            }

            bool isDup = us.isDuplicateUsername(staffInfo.username);
            if(isDup){
                return BadRequest(new { ErrorMessage = "Username taken" });
            }
        
            // assumes that password is hashed on the frontend
            bool result = us.insertStaffMember(staffInfo.email, staffInfo.username, staffInfo.password, staffInfo.last_name, staffInfo.first_name);

            if(result){
                return Accepted();
                //return new ObjectResult("Your message") {StatusCode = 403};
            } else {
                return BadRequest();
            }
        }

        //get a staff user by non-id
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

        // get a staff user by their id
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

        // get info for self - staff member
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

        // gets all staff members for display
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

        // get all roles of a staff member
        [HttpGet("role/{id}")]
        public IActionResult GetStaffRole([FromHeader] string authorization, int id){

            string token = authorization;
            int uid = us.checkToken(token);
            if (uid == -1){
                return Unauthorized();
            }

            try{                
                List<int> roles = us.getUserRoles(id);
                return new ObjectResult(roles);
            }
            catch(Exception e){
                return BadRequest();
            }
        }

        //get all roles of self
        [HttpGet("role/self")]
        public IActionResult GetStaffRoleSelf([FromHeader] string authorization){

            string token = authorization;
            int uid = us.checkToken(token);
            if (uid == -1){
                return Unauthorized();
            }

            try{                
                List<int> roles = us.getUserRoles(uid);
                return new ObjectResult(roles);
            }
            catch(Exception e){
                return BadRequest();
            }
        }

        // changing a staff member password
        [HttpPut("password")]
        public IActionResult PutStaffPassword([FromBody] Staff staffInfo, [FromHeader] string authorization){
             try{

                string token = authorization;
                int uid = us.checkToken(token);
                if (uid == -1){
                    return Unauthorized();
                }

                staffInfo.staff_id = uid;

                bool result = us.changePassword(staffInfo.staff_id, staffInfo.password);

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


        // admin update others
        // update a staff user
        [HttpPut]
        public IActionResult PutUserStaff([FromBody] Staff staffInfo, [FromHeader] string authorization){
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

        //update info for self - staff
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
        // delete a staff member
        [HttpDelete]
        public IActionResult DeleteUserStaff([FromHeader] string authorization){
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

        // authenticate a staff member
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
                StaffLogin staffLogin = new StaffLogin(authorizedStaff.staff_id, authorizedStaff.email, authorizedStaff.username, authorizedStaff.last_name, authorizedStaff.first_name,tok);

                return new ObjectResult(staffLogin);
            }
            catch(Exception e){
                return BadRequest();
            }
        }

        // log out of staff member account
        [HttpPost("logout")]
        public IActionResult logout([FromHeader] string authorization){
            try{
                int id = us.checkToken(authorization);
                if(id == -1){
                    return Unauthorized();
                }
               
                // remove token
                bool result = us.removeToken(authorization);
                
                if(result){
                    return Accepted();
                }
                else{
                    return BadRequest();
                }

            }
            catch(Exception e){
                return BadRequest();
            }
        }

        // assigns a role to a staff member
        [HttpPost("role")]
        public IActionResult AssignRole([FromHeader] string authorization){
            string token = authorization;
            try{

                int staff_id = -1;
                int role_id = -1;
                
                if (!String.IsNullOrEmpty(HttpContext.Request.Query["staff_id"])){
                    staff_id = Int32.Parse(HttpContext.Request.Query["staff_id"]);
                }

                if (!String.IsNullOrEmpty(HttpContext.Request.Query["role_id"])){
                    role_id = Int32.Parse(HttpContext.Request.Query["role_id"]);
                }

                int id = us.checkToken(token);
                if(id == -1){
                    return Unauthorized();
                }

                bool isAdmin = us.isSuperAdmin(id);
                if(!isAdmin){
                    return StatusCode(403);
                }
                
                Staff staff = us.getUserWithID(staff_id);

                if(staff != null){
                    int result = us.assignRole(staff.staff_id, role_id, id);

                    if(result > 0){
                        return Accepted();
                    } else if (result == -2){
                        return BadRequest(new { ErrorMessage = "Staff member already has this role" });
                    }

                    return BadRequest();
                }
                else{
                    return BadRequest(new { ErrorMessage = "Staff member does not exist" });
                }
            }
            catch(Exception e){
                return BadRequest();
            }

        }


        // remove a role from a staff member
        [HttpDelete("role")]
        public IActionResult RemoveRole([FromHeader] string authorization){
            string token = authorization;
            try{

                int staff_id = -1;
                int role_id = -1;
                
                if (!String.IsNullOrEmpty(HttpContext.Request.Query["staff_id"])){
                    staff_id = Int32.Parse(HttpContext.Request.Query["staff_id"]);
                }

                if (!String.IsNullOrEmpty(HttpContext.Request.Query["role_id"])){
                    role_id = Int32.Parse(HttpContext.Request.Query["role_id"]);
                }

                int id = us.checkToken(token);
                if(id == -1){
                    return Unauthorized();
                }

                bool isAdmin = us.isSuperAdmin(id);
                if(!isAdmin){
                    return StatusCode(403);
                }
                Staff staff = us.getUserWithID(staff_id);

                if(staff != null){
                    int result = us.deleteRole(staff.staff_id, role_id);

                    if(result > 0){
                        return Accepted();
                    } else if (result == -2){
                        return BadRequest(new { ErrorMessage = "Staff member does not have this role already" });
                    }

                    return BadRequest();
                }
                else{
                    return BadRequest(new { ErrorMessage = "Staff member does not exist" });
                }
            }
            catch(Exception e){
                return BadRequest();
            }

        }

        // get all staff members with a certain role
        [HttpGet("all/{id}")]
        public IActionResult GetAllStaff([FromHeader] string authorization, int id){

            string token = authorization;
            int uid = us.checkToken(token);
            if (uid == -1){
                return Unauthorized();
            }

            try{                
                List<string> staffMembers = us.getAllStaffWithRole(id);
                return new ObjectResult(staffMembers);
            }
            catch(Exception e){
                return BadRequest();
            }
        }


    } // userstaff controller class

}//namespace