using System.Collections.Generic;

namespace VacciNationAPI.Models
{

    public class ReportStaffInsights {

        public string date {get; set;}
        public int staff_id{get; set;}
        public int total_administered {get; set;}
        public int total_administered_on_date {get; set;}
        public List<CategoryInsights> total_administered_by_type{get; set;}
        public List<CategoryInsights> total_administered_by_type_date{get; set;}


        public ReportStaffInsights(string date, int staff_id, int total_administered, int total_administered_on_date, List<CategoryInsights> total_administered_by_type, List<CategoryInsights> total_administered_by_type_date){
            this.date = date;
            this.staff_id = staff_id;
            this.total_administered = total_administered;
            this.total_administered_on_date = total_administered_on_date;
            this.total_administered_by_type = total_administered_by_type;
            this.total_administered_by_type_date = total_administered_by_type_date;
        }

    } 

}//namespace