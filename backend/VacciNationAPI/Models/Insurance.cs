namespace VacciNationAPI.Models
{

    public class Insurance {

        public int insurance_id {get; set;}
        public string name {get; set;}
        public string carrier {get; set;}
        public string group_number {get; set;}
        public string member_id {get; set;}

        public Insurance(int insurance_id, string name, string carrier, string group_number, string member_id){
            this.insurance_id = insurance_id;
            this.name = name;
            this.carrier = carrier;
            this.group_number = group_number;
            this.member_id = member_id;
        }

    } 

}//namespace