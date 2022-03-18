namespace VacciNationAPI.Models
{
    public class AppointmentList
    {
        public int appointment_id { get; set; }
        public int staff_id { get; set; }
        public string staff_first_name {get; set;}
        public string staff_last_name {get; set;}
        public int citizen_id {get; set;}
        public string citizen_first_name {get; set;}
        public string citizen_last_name {get; set;}
        public int location_id {get; set;}
        public string location_name {get; set;}
        public int dose_id {get; set;}
        public string supplier {get; set;}
        public int category_id{get; set;}
        public string description {get; set;}
        public string date {get; set;}
        public int status_id{get; set;}
        public string status_desc{get; set;}

        public AppointmentList(int appointment_id, int staff_id, string staff_first_name, string staff_last_name, int citizen_id, string citizen_first_name, string citizen_last_name, int location_id, string location_name, int dose_id, string supplier, int category_id, string description, string date, int status_id, string status_desc)
        {
            this.appointment_id = appointment_id;
            this.staff_id = staff_id;
            this.staff_first_name = staff_first_name;
            this.staff_last_name = staff_last_name;
            this.citizen_id = citizen_id;
            this.citizen_first_name = citizen_first_name;
            this.citizen_last_name = citizen_last_name;
            this.location_id = location_id;
            this.location_name = location_name;
            this.dose_id = dose_id;
            this.supplier = supplier;
            this.category_id = category_id;
            this.description = description;
            this.date = date;
            this.status_id = status_id;
            this.status_desc = status_desc;
        }
    }
}