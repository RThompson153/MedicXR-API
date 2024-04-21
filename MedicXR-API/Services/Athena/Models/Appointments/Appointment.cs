using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace MedicXR_API.Services.Athena.Models.Appointments
{
    public class AppointmentList
    {
        [JsonPropertyName("appointments")]
        public IEnumerable<Appointment> Appointments { get; set; }
    }
    public class Appointment
    {
        [JsonPropertyName("patientid")]
        public string PatientId { get; set; }
        [JsonPropertyName("appointmentid")]
        public string Id { get; set; }
        [JsonPropertyName("appointmenttypeid")]
        public string TypeId { get; set; }
        [JsonPropertyName("date")]
        public string Date { get; set; }
        [JsonPropertyName("starttime")]
        public string StartTime { get; set; }
    }
}
