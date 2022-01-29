namespace VacciNationAPI.Models
{

    public class Staff {

        public int id;
        public string email;
        public string username;
        public string lastName;
        public string firstName;

        public Staff(int id, string email, string username, string lastName, string firstName){
            this.id = id;
            this.email = email;
            this.username = username;
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

        public void GetUsername(string username){
            this.username = username;
        }

        public void GetLastName(string lastName){
            this.lastName = lastName;
        }

        public void GetFirstName(string firstName){
            this.firstName = firstName;
        }

    } 

}//namespace