namespace VacciNationAPI.Models
{

    public class Insurance {

        public int insurance_id {get; set;}
        public string last_name {get; set;}
        public string first_name {get; set;}
        public string carrier {get; set;}
        public int group_number {get; set;}
        public string member_id {get; set;}

        public Insurance(int insurance_id, string last_name, string first_name, string carrier, int group_number, string member_id){
            this.insurance_id = insurance_id;
            this.last_name = last_name;
            this.first_name = first_name;
            this.carrier = carrier;
            this.group_number = group_number;
            this.member_id = member_id;
        }

    } 

}//namespace