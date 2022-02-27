namespace VacciNationAPI.Models
{
    public class Location
    {
        public int location_id { get; set; }
        public string name { get; set; }
        public int address_id { get; set; }
        public string zip {get; set;}
        public string street {get; set;}
        public string street_line2 {get; set;}
        public string city {get; set;}
        public string state {get; set;}

        public Location(int location_id, string name, int address_id, string zip, string street, string street_line2, string city, string state)
        {
            this.location_id = location_id;
            this.name = name;
            this.address_id = address_id;
            this.zip = zip;
            this.street = street;
            this.street_line2 = street_line2;
            this.city = city;
            this.state = state;
        }
    }
}