namespace VacciNationAPI.Models
{
    public class Vaccine
    {
        
        public int vaccine_id { get; set; }
        public int category_id { get; set; }
        public int disease_id { get; set; }
        public string description { get; set; }

        public Vaccine(int vaccine_id, int category_id, int disease_id, string description)
        {
            this.vaccine_id = vaccine_id;
            this.category_id = category_id;
            this.disease_id = disease_id;
            this.description = description;
        }
    }
}