namespace VacciNationAPI.Models
{
    public class StaffLocation
    {
        public int staff_id { get; set; }
        public int location_id { get; set; }

        public StaffLocation(int staff_id, int location_id)
        {
            this.staff_id = staff_id;
            this.location_id = location_id;
        }
    }
}