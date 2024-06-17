using System.Text.Json.Serialization;

namespace MedicXR_API.Services.Athena.Models.Chart
{
	public class EventList
	{
		[JsonPropertyName("events")]
		public IEnumerable<Event> Events { get; set; }
	}

	public class Event
	{
		[JsonPropertyName("note")]
		public string Note { get; set; }
		[JsonPropertyName("source")]
		public string Source { get; set; }
		[JsonPropertyName("status")]
		public string Status { get; set; }
		[JsonPropertyName("enddate")]
		public string EndDate { get; set; }
		[JsonPropertyName("createdby")]
		public string CreatedBy { get; set; }
		[JsonPropertyName("eventtype")]
		public string EventType { get; set; }
		[JsonPropertyName("onsetdate")]
		public string OnSetDate { get; set; }
		[JsonPropertyName("startdate")]
		public string StartDate { get; set; }
		[JsonPropertyName("laterality")]
		public string Laterality { get; set; }
		[JsonPropertyName("createddate")]
		public string CreatedDate { get; set; }
		[JsonPropertyName("encounterdate")]
		public string EncounterDate { get; set; }
		public IEnumerable<DiagnosisList> Diagnoses { get; set; }
	}
}
