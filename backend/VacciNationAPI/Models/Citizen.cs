namespace VacciNationAPI.Models
{

    public class Citizen {

        public int citizen_id {get; set;}
        public string email {get; set;}
        public string last_name {get; set;}
        public string first_name {get; set;}
        public int insurance_id {get; set;}
        public string date_of_birth {get; set;}
        public string phone_number {get; set;}
        public int address_id {get; set;}
        public string id_type { get; set; }
        public string id_number { get; set; }

        public Citizen(int citizen_id, string email, string last_name, string first_name, int insurance_id, string date_of_birth, string phone_number, int address_id, string id_type, string id_number){
            this.citizen_id = citizen_id;
            this.email = email;
            this.last_name = last_name;
            this.first_name = first_name;
            this.insurance_id = insurance_id;
            this.date_of_birth = date_of_birth;
            this.phone_number = phone_number;
            this.address_id = address_id;
            this.id_type = id_type;
            this.id_number = id_number;
        }

        public override string ToString()
        {
            return $"citizen_id: {citizen_id}, email: {email}, last_name: {last_name}, first_name: {first_name}, insurance_id: {insurance_id}, date_of_birth: {date_of_birth}, phone_number: {phone_number}, address_id: {address_id}, id_type: {id_type}, id_number: {id_number}";
        }
    } 

}//namespace