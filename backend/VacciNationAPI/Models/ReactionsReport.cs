namespace VacciNationAPI.Models
{

    public class ReactionReport {

        public int timeslot_id{get; set;}
        public string date {get; set;}
        public string citizen_first_name {get; set;}
        public string citizen_last_name {get; set;}
        public string supplier {get; set;}
        public string vaccine_category {get; set;}
        public string staff_first_name {get; set;}
        public string staff_last_name {get; set;}
        public string batch {get; set;}
        public string reaction {get; set;}

        public ReactionReport(int timeslot_id, string date, string citizen_first_name, string citizen_last_name, string supplier, string vaccine_category, string staff_first_name, string staff_last_name, string batch, string reaction){
            this.timeslot_id = timeslot_id;
            this.date = date;
            this.citizen_first_name = citizen_first_name;
            this.citizen_last_name = citizen_last_name;
            this.supplier = supplier;
            this.vaccine_category = vaccine_category;
            this.staff_first_name = staff_first_name;
            this.staff_last_name = staff_last_name;
            this.batch = batch;
            this.reaction = reaction;
        }

    } 

}//namespace