using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System;
using VacciNationAPI.Models;
using VacciNationAPI.DataLayer;

namespace VacciNationAPI.Controllers
{
    [ApiController]
    [Route("monitor")]
    public class MonitorController: Controller
    {
        User us = new User();
        private ApplicationMonitoringService monitor = new ApplicationMonitoringService();
        
        [HttpGet]
        public IActionResult GetLocations([FromHeader] string authorization)
        {

            long startTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            string responseCode = "200";
            
            try
            {
                string token = authorization;
                int uid = us.checkToken(token);
                if (uid == -1)
                {
                    responseCode = "401";
                    return Unauthorized();
                }

                List<Dictionary<string, string>> responseTimes;
                //no filters specified
                if (HttpContext.Request.Query.Count == 0)
                {
                    responseTimes = monitor.GetAllResponseTimes();
                }
                else
                {
                    Dictionary<string, string> filters = new Dictionary<string, string>();
                    if (!String.IsNullOrEmpty(HttpContext.Request.Query["application"]))
                    {
                        filters.Add("application", HttpContext.Request.Query["application"]);
                    }
                    
                    if (!String.IsNullOrEmpty(HttpContext.Request.Query["endpoint"]))
                    {
                        filters.Add("endpoint", HttpContext.Request.Query["endpoint"]);
                    }
                    
                    if (!String.IsNullOrEmpty(HttpContext.Request.Query["minmillis"]))
                    {
                        filters.Add("minmillis", HttpContext.Request.Query["minmillis"]);
                    }
                    
                    if (!String.IsNullOrEmpty(HttpContext.Request.Query["maxmillis"]))
                    {
                        filters.Add("maxmillis", HttpContext.Request.Query["maxmillis"]);
                    }
                    
                    if (!String.IsNullOrEmpty(HttpContext.Request.Query["code"]))
                    {
                        filters.Add("code", HttpContext.Request.Query["code"]);
                    }
                    
                    if (!String.IsNullOrEmpty(HttpContext.Request.Query["startdate"]))
                    {
                        filters.Add("startdate", HttpContext.Request.Query["startdate"]);
                    }
                    
                    if (!String.IsNullOrEmpty(HttpContext.Request.Query["enddate"]))
                    {
                        filters.Add("enddate", HttpContext.Request.Query["enddate"]);
                    }

                    responseTimes = monitor.GetFilteredResponseTimes(filters);
                }

                return new ObjectResult(responseTimes);
                
            }
            catch (Exception e)
            {
                responseCode = "400";
                return BadRequest();
            }
            finally
            {
                monitor.RecordResponseTime("Backend", "GET /monitor", startTime, responseCode);
            }

        }
    }
}