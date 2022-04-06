using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System;
using VacciNationAPI.Models;
using VacciNationAPI.DataLayer;
using  Newtonsoft.Json;


namespace VacciNationAPI.Controllers{

    [ApiController]
    [Route("[controller]")]
    public class EligibilityController: Controller {

         User us = new User();
        EligibilityManagement em = new EligibilityManagement();

        // set eligibility category information
        [HttpPost("Category")]
        public IActionResult AddEligibilityCategory([FromHeader] string authorization, [FromBody] Eligibility eligibility) {
            string token = authorization;
            int uid = us.checkToken(token);
            if (uid == -1){
                return Unauthorized();
            }

            bool added = em.AddEligibilityCategory(eligibility);

            if(added){
                return Accepted();
            } else {
                return BadRequest();
            }
        }

        // set eligibility information 
        [HttpPost]
        public IActionResult AddEligibilityReq([FromHeader] string authorization, [FromBody] EligibilityText eligibility) {
            string token = authorization;
            int uid = us.checkToken(token);
            if (uid == -1){
                return Unauthorized();
            }

            bool added = em.AddEligibilityQnA(eligibility, eligibility.eligibility_id);

            if(added){
                return Accepted();
            } else {
                return BadRequest();
            }
        }

        // update eligibility category information
        [HttpPut("Category")]
         public IActionResult UpdateEligibilityCategory([FromHeader] string authorization, [FromBody] Eligibility eligibility) {
            string token = authorization;
            int uid = us.checkToken(token);
            if (uid == -1){
                return Unauthorized();
            }

           bool added = em.putEligibilityCategory(eligibility);

            if(added){
                return Accepted();
            } else {
                return BadRequest();
            }
        }

        // update eligibility information
        [HttpPut]
        public IActionResult UpdateEligibilityReq([FromHeader] string authorization, [FromBody] EligibilityText eligibility) {
            string token = authorization;
            int uid = us.checkToken(token);
            if (uid == -1){
                return Unauthorized();
            }

            bool added = em.putEligibilityText(eligibility);

            if(added){
                return Accepted();
            } else {
                return BadRequest();
            }
        }

        // get all eligibility information
        [HttpGet]
         public IActionResult getEligibility() {

            Dictionary<int, MultEligibility> added = em.getEligibilityInfo();

            if(added != null){
                return new ObjectResult(JsonConvert.SerializeObject( added ));
            } else {
                return BadRequest();
            }
        }

        // delete eligibility category
        [HttpDelete("Category/{eligibility_id}")]
         public IActionResult deleteEligibilityCategory([FromHeader] string authorization, int eligibility_id) {

            bool removed = em.removeEligibilityCategory(eligibility_id);

            if(removed){
                return Accepted();
            } else {
                return BadRequest();
            }
        }

        // delete eligibility text
        [HttpDelete("{text_id}")]
         public IActionResult deleteEligibility([FromHeader] string authorization, int text_id) {

            bool removed = em.removeEligibility(text_id);

            if(removed){
                return Accepted();
            } else {
                return BadRequest();
            }
        }
    }
}