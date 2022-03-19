using System.Collections.Generic;

namespace VacciNationAPI.Models
{

    public class ReportVaccineInsights {

        public string date {get; set;}
        public int total_administered {get; set;}
        public int total_administered_on_date {get; set;}
        public int total_upcoming{get; set;}
        public List<CategoryInsights> total_administered_by_type{get; set;}
        public List<CategoryInsights> total_administered_by_type_date{get; set;}
        public int total_missed{get; set;}
        public int total_missed_on_date{get; set;}


        public ReportVaccineInsights(string date, int total_administered, int total_administered_on_date, int total_upcoming, List<CategoryInsights> total_administered_by_type, List<CategoryInsights> total_administered_by_type_date, int total_missed, int total_missed_on_date){
            this.date = date;
            this.total_administered = total_administered;
            this.total_administered_on_date = total_administered_on_date;
            this.total_upcoming = total_upcoming;
            this.total_administered_by_type = total_administered_by_type;
            this.total_administered_by_type_date = total_administered_by_type_date;
            this.total_missed = total_missed;
            this.total_missed_on_date = total_missed_on_date;
        }

    } 

}//namespace