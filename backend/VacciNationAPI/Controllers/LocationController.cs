using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System;
using VacciNationAPI.Models;
using VacciNationAPI.DataLayer;

namespace VacciNationAPI.Controllers
{
    [ApiController]
    [Route("location")]
    public class LocationController: Controller
    {
        
        User us = new User();
        private LocationManagement locationManager = new LocationManagement();
        private ApplicationMonitoringService monitor = new ApplicationMonitoringService();
        
        [HttpGet]
        public IActionResult GetLocations()
        {

            long startTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            string responseCode = "200";

            try
            {
                //get specific location if set
                if (!String.IsNullOrEmpty(HttpContext.Request.Query["location_id"]))
                {
                    int locationId = Int32.Parse(HttpContext.Request.Query["location_id"]);

                    Dictionary<string, string> location = locationManager.GetLocationById(locationId);
                    return new ObjectResult(location);
                }

                //get locations by zip
                if (!String.IsNullOrEmpty(HttpContext.Request.Query["zip"]))
                {
                    string zip = HttpContext.Request.Query["zip"];

                    List<Dictionary<string, string>> locations = locationManager.GetLocationsByZip(zip);
                    return new ObjectResult(locations);
                }

                //get locations by city
                if (!String.IsNullOrEmpty(HttpContext.Request.Query["city"]))
                {
                    string city = HttpContext.Request.Query["city"];

                    List<Dictionary<string, string>> locations = locationManager.GetLocationsByCity(city);
                    return new ObjectResult(locations);
                }

                //get all locations
                else
                {
                    List<Dictionary<string, string>> locations = locationManager.GetAllLocations();
                    return new ObjectResult(locations);
                }

            }
            catch (Exception e)
            {
                responseCode = "400";
                return BadRequest();
            }
            finally
            {
                monitor.RecordResponseTime("Backend", "GET /location", startTime, responseCode);
            }

        }
        
        [HttpPost]
        public IActionResult CreateLocation([FromBody] Location location,  [FromHeader] string authorization) {
            string token = authorization;
            int uid = us.checkToken(token);
            if (uid == -1){
                return Unauthorized();
            }

            bool isAdmin = us.isSuperAdmin(uid);
            if(!isAdmin){
                return StatusCode(403);
            }

            bool result = location.address_id > 0 ? locationManager.InsertLocation(location) : locationManager.InsertLocationWithAddress(location);

            if(result){
                return Accepted();
            }

            return BadRequest();
        }
        
        [HttpPut]
        public IActionResult UpdateLocation([FromBody] Location location,  [FromHeader] string authorization) {
            string token = authorization;
            int uid = us.checkToken(token);
            if (uid == -1){
                return Unauthorized();
            }

            bool isAdmin = us.isSuperAdmin(uid);
            if(!isAdmin){
                return StatusCode(403);
            }

            bool result = locationManager.UpdateLocation(location);

            if(result){
                return Accepted();
            }

            return BadRequest();
        }
        
        [HttpGet("staff")]
        public IActionResult GetStaff(){

            try
            {
                //get staff at location specified
                if (!String.IsNullOrEmpty(HttpContext.Request.Query["location_id"]))
                {
                    int locationId = Int32.Parse(HttpContext.Request.Query["location_id"]);

                    List<Dictionary<string, string>> staff = locationManager.GetStaffByLocation(locationId);
                    return new ObjectResult(staff);
                }

                return BadRequest();

            }
            catch (Exception e)
            {
                return BadRequest();
            }

        }
        
        [HttpPost("staff")]
        public IActionResult CreateStaffLocation([FromBody] StaffLocation sl,  [FromHeader] string authorization) {
            string token = authorization;
            int uid = us.checkToken(token);
            if (uid == -1){
                return Unauthorized();
            }

            bool isAdmin = us.isSuperAdmin(uid);
            if(!isAdmin){
                return StatusCode(403);
            }

            bool result = locationManager.InsertStaffLocation(sl);

            if(result){
                return Accepted();
            }

            return BadRequest();
        }
        
        [HttpDelete("staff")]
        public IActionResult DeleteStaffLocation([FromBody] StaffLocation sl,  [FromHeader] string authorization) {
            string token = authorization;
            int uid = us.checkToken(token);
            if (uid == -1){
                return Unauthorized();
            }

            bool isAdmin = us.isSuperAdmin(uid);
            if(!isAdmin){
                return StatusCode(403);
            }

            bool result = locationManager.DeleteStaffLocation(sl);

            if(result){
                return Accepted();
            }

            return BadRequest();
        }

    }
}