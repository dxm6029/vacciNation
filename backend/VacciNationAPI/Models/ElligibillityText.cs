namespace VacciNationAPI.Models
{
    public class EligibilityText
    {
        public int text_id { get; set; }
        public string language { get; set; }
        public int eligibility_id { get; set; }
        public string type { get; set; }
        public string text { get; set; }

        public EligibilityText(int text_id, string language, int eligibility_id, string type, string text)
        {
            this.text_id = text_id;
            this.language = language;
            this.eligibility_id = eligibility_id;
            this.type = type;
            this.text = text;
        }
    }
}