namespace VacciNationAPI.Models
{

    public class BatchReport {

        public int timeslot_id {get; set;}
        public string citizen_first_name {get; set;}
        public string citizen_last_name {get; set;}
        public string supplier {get; set;}
        public string vaccine_category {get; set;}
        public string date {get; set;}
        public string location_name {get; set;}

        public BatchReport(int timeslot_id, string citizen_first_name, string citizen_last_name, string supplier, string vaccine_category, string date, string location_name ){
            this.timeslot_id = timeslot_id;
            this.citizen_first_name = citizen_first_name;
            this.citizen_last_name = citizen_last_name;
            this.supplier = supplier;
            this.vaccine_category = vaccine_category;
            this.date = date;
            this.location_name = location_name;
        }

    } 

}//namespace