namespace VacciNationAPI.Models
{
    public class Dose
    {
        public int dose_id { get; set; }
        public string supplier { get; set; }
        public int vaccine_id { get; set; }
        public int location_id { get; set; }
        public int quantity { get; set; }
        public string batch { get; set; }

        public Dose(int dose_id, string supplier, int vaccine_id, int location_id, int quantity, string batch)
        {
            this.dose_id = dose_id;
            this.supplier = supplier;
            this.vaccine_id = vaccine_id;
            this.location_id = location_id;
            this.quantity = quantity;
            this.batch = batch;
        }
    }
}