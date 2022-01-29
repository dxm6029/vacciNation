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
        public IActionResult AddStaffMember([FromBody] string email, string username, string password, string lastName, string firstName) {
            // assumes that password is hashed on the frontend
            bool result = us.insertStaffMember(email, username, password, lastName, firstName);

            if(result){
                return Accepted();
                //return new ObjectResult("Your message") {StatusCode = 403};
            } else {
                return BadRequest();
            }
        }

        /*

public IActionResult Create([FromBody] Product product) {
            if(product == null || !ModelState.IsValid){
                return BadRequest();
            }

            // here we will return 201 with a LocationHeader
            // specifying the URI of the newly created object
            // in our case won't exist, we are not actually inserting
            // anything
            return CreatedAtRoute("GetProduct", new {id = product.Id}, product);

        }
        */

        [HttpGet]
        public IActionResult GetUserStaffWithoutID(){
            try{

                // call DB function here that returns staff info
                Staff staff = new Staff(1, "", "", "", "");


                if(staff == null){
                    return NotFound();
                }
                
                return new ObjectResult(staff);
            }
            catch(Exception e){
                return BadRequest();;
            }
        }

    } // userstaff controller class

}//namespace