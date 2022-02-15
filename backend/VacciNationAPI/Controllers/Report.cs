using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System;
using VacciNationAPI.Models;
using VacciNationAPI.DataLayer;

namespace VacciNationAPI.Controllers{

    [ApiController]
    [Route("[controller]")]
    public class Report: Controller {

         User us = new User();
         AppointmentManagement am = new AppointmentManagement();
        ReportManagement rm = new ReportManagement();


        // currently set so any staff member can view insights
        [HttpGet("staff")]
        public IActionResult GetStaffInsights([FromHeader] string authorization, [FromBody]int staff_id) {
            string token = authorization;
            int uid = us.checkToken(token);
            if (uid == -1){
                return Unauthorized();
            }

            // get date from query params
            string date = "";
            if (!String.IsNullOrEmpty(HttpContext.Request.Query["date"])){
                date = HttpContext.Request.Query["date"];
            }

            string result = rm.staffInsightsResponseBuilder(date, staff_id);
            if(result != null){
                return new ObjectResult(result);
            } else {
                return BadRequest();
            }
        }

        // currently set so any staff member can view insights
        [HttpGet("vaccine")]
        public IActionResult GetVaccineInsights([FromHeader] string authorization) {
            string token = authorization;
            int uid = us.checkToken(token);
            if (uid == -1){
                return Unauthorized();
            }

            // get date from query params
            string date = "";
            if (!String.IsNullOrEmpty(HttpContext.Request.Query["date"])){
                date = HttpContext.Request.Query["date"];
            }

            string result = rm.vaccineInsightsResponseBuilder(date);
            if(result != null){
                return new ObjectResult(result);
            } else {
                return BadRequest();
            }
        }

        // currently set so any staff member can view insights
        [HttpGet("reactions")]
        public IActionResult GetReactionInsights([FromHeader] string authorization) {
            string token = authorization;
            int uid = us.checkToken(token);
            if (uid == -1){
                return Unauthorized();
            }

            List<string> result = rm.getAllReactions();
            if(result != null){
                return new ObjectResult(result);
            } else {
                return BadRequest();
            }
        }
    }
}