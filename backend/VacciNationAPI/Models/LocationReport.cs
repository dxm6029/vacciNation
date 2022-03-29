using System.Collections.Generic;

namespace VacciNationAPI.Models
{

    public class LocationReport {

        public int location_id {get; set;}
        public string location_name {get; set;}
        public int total_patients_processed {get; set;}
        public Dictionary<string, int> total_per_manufacturer {get; set;}
        public Dictionary<string, int> total_per_sequence {get; set;}
        public string date {get; set;}
        public int total_adverse_reactions{get; set;}

        public LocationReport(int location_id, string location_name, int total_patients_processed, Dictionary<string, int> total_per_manufacturer, Dictionary<string, int> total_per_sequence, string date, int total_adverse_reactions){
            this.location_id = location_id;
            this.location_name = location_name;
            this.total_patients_processed = total_patients_processed;
            this.total_per_manufacturer = total_per_manufacturer;
            this.total_per_sequence = total_per_sequence;
            this.date = date;
            this.total_adverse_reactions = total_adverse_reactions;
        }

    } 

}//namespace