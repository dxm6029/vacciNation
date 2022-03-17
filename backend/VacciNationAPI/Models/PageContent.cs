using System.Collections.Generic;

namespace VacciNationAPI.Models
{
    public class PageContent
    {
        public int content_id {get; set;}
        public string label {get; set;}
        public string text_en {get; set;}
        public string text_es {get; set;}

        public PageContent(int content_id, string label, string text_en, string text_es)
        {
            this.content_id = content_id;
            this.label = label;
            this.text_en = text_en;
            this.text_es = text_es;
        }
    }
}