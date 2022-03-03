using System.Collections.Generic;

namespace VacciNationAPI.Models
{
    public class MultEligibility
    {
        public Eligibility eligibility;
        public List<EligibilityText> QnA = new List<EligibilityText>();

        public MultEligibility(Eligibility eligibility, List<EligibilityText> Qna)
        {
            this.eligibility = eligibility;
            this.QnA = Qna;
        }
    }
}