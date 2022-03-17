using System.Collections.Generic;

namespace VacciNationAPI.Models
{
    public class PageContent
    {
        public int content_id;
        public string label;
        public string text_en;
        public string text_es;

        public PageContent(int content_id, string label, string text_en, string text_es)
        {
            this.content_id = content_id;
            this.label = label;
            this.text_en = text_en;
            this.text_es = text_es;
        }
    }
}