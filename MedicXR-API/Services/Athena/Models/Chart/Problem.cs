using System.Text.Json.Serialization;

namespace MedicXR_API.Services.Athena.Models.Chart
{
    public class ProblemList
    {
        [JsonPropertyName("problems")]
        public IEnumerable<Problem> Problems { get; set; }
    }

    public class Problem
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string CodeSet { get; set; }
        public int ProblemId { get; set; }
        public string LastModifiedBy { get; set; }
        public string DeactivatedDate { get; set; }
        public string DeactivatedUser { get; set; }
        public string BestMatchIcd10Code { get; set; }
        public string LastModifiedDateTime { get; set; }
        public string MostRecentDiagnosisNote { get; set; }
        public IEnumerable<EventList> Events { get; set; }
    }
}
