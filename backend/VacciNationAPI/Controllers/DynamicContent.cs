using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System;
using VacciNationAPI.Models;
using VacciNationAPI.DataLayer;
using  Newtonsoft.Json;


namespace VacciNationAPI.Controllers{

    [ApiController]
    [Route("[controller]")]
    public class DynamicContent: Controller {

         User us = new User();

        DynamicContentManagement dcm = new DynamicContentManagement();
        [HttpPost]
        public IActionResult AddContent([FromHeader] string authorization, [FromBody] PageContent pageContent) {
            string token = authorization;
            int uid = us.checkToken(token);
            if (uid == -1){
                return Unauthorized();
            }

            bool added = dcm.AddPageContent(pageContent);

            if(added){
                return Accepted();
            } else {
                return BadRequest();
            }
        }

        [HttpDelete("{content_id}")]
         public IActionResult deleteContent([FromHeader] string authorization, int content_id) {

            bool removed = dcm.removePageContent(content_id);

            if(removed){
                return Accepted();
            } else {
                return BadRequest();
            }
        }

        [HttpPut("id")]
         public IActionResult UpdateContentById([FromHeader] string authorization, [FromBody] PageContent pageContent) {
            string token = authorization;
            int uid = us.checkToken(token);
            if (uid == -1){
                return Unauthorized();
            }

           bool added = dcm.putPageContentId(pageContent);

            if(added){
                return Accepted();
            } else {
                return BadRequest();
            }
        }

        [HttpPut("label")]
         public IActionResult UpdateContentByLabel([FromHeader] string authorization, [FromBody] PageContent pageContent) {
            string token = authorization;
            int uid = us.checkToken(token);
            if (uid == -1){
                return Unauthorized();
            }

           bool added = dcm.putPageContentLabel(pageContent);

            if(added){
                return Accepted();
            } else {
                return BadRequest();
            }
        }

        [HttpGet("{content_id}")]
         public IActionResult GetContentById(int content_id) {

            PageContent content = dcm.getPageContentWithID(content_id);

            if(content != null){
                return new ObjectResult(JsonConvert.SerializeObject( content ));
            } else {
                return BadRequest();
            }
        }

        [HttpGet]
         public IActionResult GetContentByLabel() {

            string label = "";
                
            if (!String.IsNullOrEmpty(HttpContext.Request.Query["label"])){
                label = HttpContext.Request.Query["label"];
            }

            PageContent content = dcm.getPageContentWithLabel(label);

            if(content != null){
                return new ObjectResult(JsonConvert.SerializeObject( content ));
            } else {
                return BadRequest();
            }
        }

        [HttpGet]
        public IActionResult GetAllContent() {

            List<PageContent> content = dcm.getAllPageContent();

            if(content != null){
                return new ObjectResult(JsonConvert.SerializeObject( content ));
            } else {
                return BadRequest();
            }
        }

         // old code
        EligibilityManagement em = new EligibilityManagement();

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

        [HttpGet]
         public IActionResult getEligibility() {

            Dictionary<int, MultEligibility> added = em.getEligibilityInfo();

            if(added != null){
                return new ObjectResult(JsonConvert.SerializeObject( added ));
            } else {
                return BadRequest();
            }
        }

        [HttpDelete("Category/{eligibility_id}")]
         public IActionResult deleteEligibilityCategory([FromHeader] string authorization, int eligibility_id) {

            bool removed = em.removeEligibilityCategory(eligibility_id);

            if(removed){
                return Accepted();
            } else {
                return BadRequest();
            }
        }

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