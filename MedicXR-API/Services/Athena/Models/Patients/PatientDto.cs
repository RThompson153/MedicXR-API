namespace MedicXR_API.Services.Athena.Models.Patients
{
    public class PatientDto
    {
        public string DepartmentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DOB { get; set; }
        public string Sex { get; set; }
        public string Email { get; set; }
        public string GuarantorEmail { get; set; }
        public string SSN { get; set; }
        public string HomePhone { get; set; }
        public string MobilePhone { get; set; }
        public string WorkPhone { get; set; }
        public string Zip { get; set; }
    }
}
