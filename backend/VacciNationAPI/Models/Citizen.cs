namespace VacciNationAPI.Models
{

    public class Citizen {

        public int citizen_id {get; set;}
        public string email {get; set;}
        public string last_name {get; set;}
        public string first_name {get; set;}
        public int insurance_id {get; set;}

        public Citizen(int citizen_id, string email, string last_name, string first_name, int insurance_id){
            this.citizen_id = citizen_id;
            this.email = email;
            this.last_name = last_name;
            this.first_name = first_name;
            this.insurance_id = insurance_id;
        }

    } 

}//namespace