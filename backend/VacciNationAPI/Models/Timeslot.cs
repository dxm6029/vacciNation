namespace VacciNationAPI.Models
{

    public class Timeslot {

        public int timeslot_id {get; set;}
        public int staff_id {get; set;}
        public int citizen_id {get; set;}
        public int location_id {get; set;}
        public int dose_id {get; set;}
        public string date {get; set;}
        public int status {get; set;}

        public string reactions {get; set;}

        public Timeslot(int timeslot_id, int staff_id, int citizen_id, int location_id, int dose_id, string date, int status){
            this.timeslot_id = timeslot_id;
            this.staff_id = staff_id;
            this.citizen_id = citizen_id;
            this.location_id = location_id;
            this.dose_id = dose_id;
            this.date = date;
            this.status = status;
        }

    } 

}//namespace