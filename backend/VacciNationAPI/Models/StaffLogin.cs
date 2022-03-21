namespace VacciNationAPI.Models
{

    public class StaffLogin {

        public int staff_id {get; set;}
        public string email {get; set;}
        public string username {get; set;}
        public string last_name {get; set;}
        public string first_name {get; set;}
        public string token {get; set;}

        public StaffLogin(int staff_id, string email, string username, string last_name, string first_name, string token){
            this.staff_id = staff_id;
            this.email = email;
            this.username = username;
            this.last_name = last_name;
            this.first_name = first_name;
            this.token = token;
        }

    } 

}//namespace