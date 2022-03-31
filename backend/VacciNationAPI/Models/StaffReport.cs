using System.Collections.Generic;

namespace VacciNationAPI.Models
{

    public class StaffReport {

        public string date {get; set;}
        public int staff_id {get; set;}
        public string staff_first_name {get; set;}
        public string staff_last_name {get; set;}
        public int total_patients_processed {get; set;}
        public List<string> location {get; set;}
        public int total_adverse_reactions {get; set;}

        public StaffReport(string date, int staff_id, string staff_first_name, string staff_last_name, int total_patients_processed, List<string> location, int total_adverse_reactions){
            this.date = date;
            this.staff_id = staff_id;
            this.staff_first_name = staff_first_name;
            this.staff_last_name = staff_last_name;
            this.total_patients_processed = total_patients_processed;
            this.location = location;
            this.total_adverse_reactions = total_adverse_reactions;
        }

    } 

}//namespace