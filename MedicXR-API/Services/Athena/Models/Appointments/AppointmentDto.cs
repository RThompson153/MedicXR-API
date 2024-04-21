namespace MedicXR_API.Services.Athena.Models.Appointments
{
    public class AppointmentDto
    {
        //[JsonPropertyName(AthenaConstants.AppointmentDate)]
        public string AppointmentDate { get; set; }
        //[JsonPropertyName(AthenaConstants.AppointmentTime)]
        public string AppointmentTime { get; set; }
        //[JsonPropertyName(AthenaConstants.AppointmentTypeId)]
        public string AppointmentTypeId { get; set; }
        //[JsonPropertyName(AthenaConstants.DepartmentId)]
        public string DepartmentId { get; set; }
        //[JsonPropertyName(AthenaConstants.ProviderId)]
        public string ProviderId { get; set; }
        //[JsonPropertyName(AthenaConstants.ReasonId)]
        public string ReasonId { get; set; }
    }
}
