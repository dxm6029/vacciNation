namespace VacciNationAPI.Models
{

    public class Staff {

        public int id;
        public string email;
        public string username;
        public string password;
        public string lastName;
        public string firstName;

        public Staff(int id, string email, string username, string password, string lastName, string firstName){
            this.id = id;
            this.email = email;
            this.username = username;
            this.password = password;
            this.lastName = lastName;
            this.firstName = firstName;
        }

        public int GetID(){
            return this.id;
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
            return this.lastName;
        }

        public string GetFirstName(){
            return this.firstName;
        }

        public void SetID(int id){
            this.id = id;
        }

        public void SetEmail(string email){
            this.email = email;
        }

        public void SetUsername(string username){
            this.username = username;
        }

        public void SetLastName(string lastName){
            this.lastName = lastName;
        }

        public void SetFirstName(string firstName){
            this.firstName = firstName;
        }

    } 

}//namespace