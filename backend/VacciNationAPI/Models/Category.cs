namespace VacciNationAPI.Models
{
    public class Category
    {
        public int category_id { get; set; }
        public string name { get; set; }

        public Category(int category_id, string name)
        {
            this.category_id = category_id;
            this.name = name;
        }
    }
}