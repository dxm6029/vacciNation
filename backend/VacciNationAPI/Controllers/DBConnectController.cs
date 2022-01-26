using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System;

namespace VacciNationAPI.Controllers{

    [ApiController]
    [Route("[controller]")]
    public class DBConnectController: Controller {

        [HttpGet]
        public string OpenAndCloseDBConnect(){
            VacciNation.Connect connection = new VacciNation.Connect();
            try{
                var conn = connection.OpenConnection();
                var status = connection.CloseConnection(conn);
                if(status){
                    return "SUCCESS: Opened and Closed DB connection without issues";
                }
                else{
                   return "ERROR: Unable to close DB connection";
                }
            }
            catch(Exception e){
                return "ERROR: " + e.StackTrace;
            }
        }

    } // DB Connect Controller

}//namespace