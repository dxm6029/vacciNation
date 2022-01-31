namespace VacciNationAPI.Models
{

    public class Staff {

        private int staff_id;
        private string email;
        private string username;
        private string password;
        private string last_name;
        private string first_name;

        public Staff(int staff_id, string email, string username, string password, string last_name, string first_name){
            this.staff_id = staff_id;
            this.email = email;
            this.username = username;
            this.password = password;
            this.last_name = last_name;
            this.first_name = first_name;
        }

        public int GetID(){
            return this.staff_id;
        }

        public string GetEmail(){
            return this.email;
        }

        public string GetUsername(){
            return this.username;
        }

        public string GetPassword(){
            return this.password;
        }

        public string GetLastName(){
            return this.last_name;
        }

        public string GetFirstName(){
            return this.first_name;
        }

        public void SetID(int id){
            this.staff_id = id;
        }

        public void SetEmail(string email){
            this.email = email;
        }

        public void SetUsername(string username){
            this.username = username;
        }

        public void SetLastName(string lastName){
            this.last_name = lastName;
        }

        public void SetFirstName(string firstName){
            this.first_name = firstName;
        }

    } 

}//namespace