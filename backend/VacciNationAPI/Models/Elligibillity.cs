namespace VacciNationAPI.Models
{
    public class Eligibility
    {
        public int eligibility_id { get; set; }
        public int vaccine_id {get; set; }
        public int dependency {get; set;}

        public Eligibility(int eligibility_id, int vaccine_id, int dependency)
        {
            this.eligibility_id = eligibility_id;
            this.vaccine_id = vaccine_id;
            this.dependency = dependency;
        }
    }
}