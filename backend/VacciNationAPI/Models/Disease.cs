namespace VacciNationAPI.Models
{
    public class Disease
    {
        public int disease_id { get; set; }
        public string name { get; set; }

        public Disease(int disease_id, string name)
        {
            this.disease_id = disease_id;
            this.name = name;
        }
    }
}