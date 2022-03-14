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
                
                List<Dictionary<string, string>> responseTimes = monitor.GetAllResponseTimes();
                return new ObjectResult(responseTimes);
                
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
    }
}