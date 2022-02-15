using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System;
using VacciNationAPI.Models;
using VacciNationAPI.DataLayer;

namespace VacciNationAPI.Controllers
{
    [ApiController]
    [Route("vaccine")]
    public class VaccineController: Controller {
        
        User us = new User();
        VaccineManagement vm = new VaccineManagement();
        
        [HttpGet("all")]
        public IActionResult GetAllVaccines(){

            try{                
                List<string> vaccines = vm.GetAllVaccines();
                return new ObjectResult(vaccines);
            }
            catch(Exception e){
                return BadRequest();
            }
        }
        
    }
}