namespace VacciNationAPI.Models
{

    public class Staff {

        public int staff_id {get; set;}
        public string email {get; set;}
        public string username {get; set;}
        public string password {get; set;}
        public string last_name {get; set;}
        public string first_name {get; set;}

        public Staff(int staff_id, string email, string username, string password, string last_name, string first_name){
            this.staff_id = staff_id;
            this.email = email;
            this.username = username;
            this.password = password;
            this.last_name = last_name;
            this.first_name = first_name;
        }

    } 

}//namespace