namespace VacciNationAPI.Models
{

    public class StaffPrint {

        public int staff_id {get; set;}
        public string email {get; set;}
        public string username {get; set;}
        public string last_name {get; set;}
        public string first_name {get; set;}
        public string role {get; set;}


        public StaffPrint(int staff_id, string email, string username, string last_name, string first_name, string role){
            this.staff_id = staff_id;
            this.email = email;
            this.username = username;
            this.last_name = last_name;
            this.first_name = first_name;
            this.role = role;
        }

    } 

}//namespace