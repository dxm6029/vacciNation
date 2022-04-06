using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System;

namespace VacciNationAPI.Controllers{

    [ApiController]
    [Route("[controller]")]
    public class DBConnectController: Controller {

        //test the ability to connect to our database
        [HttpGet]
        public string OpenAndCloseDBConnect(){
            VacciNation.Connect connection = new VacciNation.Connect();
            try{
                var conn = connection.OpenConnection();
                var status = connection.CloseConnection(conn);
                if(status){
                    return "200: Opened and Closed DB connection without issues";
                }
                else{
                   return "400: Unable to close DB connection";
                }
            }
            catch(Exception e){
                return "400: " + e.StackTrace;
            }
        }

    } // DB Connect Controller

}//namespace