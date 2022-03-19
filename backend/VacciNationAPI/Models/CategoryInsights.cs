namespace VacciNationAPI.Models
{

    public class CategoryInsights {

        public int category{get; set;}
        public int total_by_type{get;set;}
        public string supplier {get; set;}
        
        public CategoryInsights(int category, int total_by_type, string supplier){
            this.category = category;
            this.total_by_type = total_by_type;
            this.supplier = supplier;
        }

    } 

}//namespace