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


        // get all reporting insights for a specific staff member 
        [HttpGet("staff/{staff_id}")]
        public IActionResult GetStaffInsights([FromHeader] string authorization, int staff_id) {
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

            StaffReport result = rm.staffInsightsResponseBuilder(date, staff_id);
            if(result != null){
                return new ObjectResult(result);
            } else {
                return BadRequest();
            }
        }

        // get all reporting insights for a specific location
        [HttpGet("location/{location_id}")]
        public IActionResult GetVaccineInsights([FromHeader] string authorization, int location_id) {
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

            LocationReport result = rm.locationInsightsResponseBuilder(date, location_id);
            if(result != null){
                return new ObjectResult(result);
            } else {
                return BadRequest();
            }
        }

        // get all reporting insights for reactions
        [HttpGet("reactions")]
        public IActionResult GetReactionInsights([FromHeader] string authorization) {
            string token = authorization;
            int uid = us.checkToken(token);
            if (uid == -1){
                return Unauthorized();
            }

            List<ReactionReport> result = rm.getAllReactions();
            if(result != null){
                return new ObjectResult(result);
            } else {
                return BadRequest();
            }
        }

        // get all reporting insights for a batch
        [HttpGet("batch/{batch}")]
        public IActionResult GetBatchInsights([FromHeader] string authorization, string batch) {
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

            int location_id = -1;
            if (!String.IsNullOrEmpty(HttpContext.Request.Query["location_id"])){
                location_id = Int32.Parse(HttpContext.Request.Query["location_id"]);
            }

            List<BatchReport> result = rm.getBatchReport(batch, date, location_id);
            if(result != null){
                return new ObjectResult(result);
            } else {
                return BadRequest();
            }
        }
    }
}